Imports System.IO
Imports System.Drawing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Presentation
Imports DevExpress.XtraGrid.Views.BandedGrid

Public Class clsEvalReportMethods

    Public dicEvalRptGridImages As Dictionary(Of String, MemoryStream)
    Public dicEvalRptGridBmp As Dictionary(Of String, Bitmap)
    Public dicEvalRptGridDataTables As Dictionary(Of String, DataTable)
    Public dicEvalRptChartImages As Dictionary(Of String, Bitmap)

    Private slidePosition As Integer = 1
    Private chartsPerSlide As Integer = 0
    Private drawingObjectId As UInteger = 0
    Private reportFileName As String = Nothing

    'OpenXML presentation parts
    Dim slide As Slide = Nothing
    Dim slidePart As SlidePart = Nothing

    'Pilot Introduction Slide Variables
    Public Technology As String = ""
    Public TargetType As String = ""
    Public ChartSetName As String = ""
    Public Area As String = ""
    Public SiteCount As String = ""
    Public SelectedArea As String = ""
    Public StartTime As String = ""
    Public EndTime As String = ""
    Public Resolution As String = ""
    Public Filter As String = ""
    Public PrdCompBeforeTime As String = ""
    Public PrdCompAfterTime As String = ""
    Public TopX As String = ""

#Region "Open XML Old Methods"

    Private Sub InsertTitleSlide(presPart As PresentationPart, templateSlidePart As SlidePart, titleText As String)

        Dim slidePart = CloneSlide(presPart, templateSlidePart)

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree

        Dim slideWidthEmu As Long = 12192000
        Dim slideHeightEmu As Long = 6858000

        Dim boxWidth As Long = 8000000
        Dim boxHeight As Long = 1500000

        Dim offsetX As Long = (slideWidthEmu - boxWidth) \ 2
        Dim offsetY As Long = (slideHeightEmu - boxHeight) \ 2

        Dim shapeId As UInteger = CType(GetNextShapeId(shapeTree), UInteger)

        Dim shape As New Shape(
        New NonVisualShapeProperties(
            New NonVisualDrawingProperties() With {.Id = shapeId, .Name = "Title"},
            New NonVisualShapeDrawingProperties(),
            New ApplicationNonVisualDrawingProperties()),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {.X = offsetX, .Y = offsetY},
                New Drawing.Extents() With {.Cx = boxWidth, .Cy = boxHeight}
            )
        ),
        New TextBody(
            New Drawing.BodyProperties() With {
                .Anchor = Drawing.TextAnchoringTypeValues.Center
            },
            New Drawing.ListStyle(),
            New Drawing.Paragraph(
                New Drawing.ParagraphProperties() With {
                    .Alignment = Drawing.TextAlignmentTypeValues.Center
                },
                New Drawing.Run(
                    New Drawing.RunProperties() With {
                        .FontSize = 7200,
                        .Bold = True
                    },
                    New Drawing.Text(titleText)
                )
            )
        )
    )

        shapeTree.Append(shape)
        slidePart.Slide.Save()

    End Sub

    Public Function ExportGridToImage(ByRef gridControl As GridControl) As MemoryStream
        gridControl.ForceInitialize()

        Dim view As GridView = TryCast(gridControl.MainView, GridView)
        If view IsNot Nothing Then
            view.OptionsView.ColumnAutoWidth = True
            view.OptionsPrint.AutoWidth = True
            view.BestFitColumns()
        End If

        Dim ms As New MemoryStream()

        Using ps As New PrintingSystem()
            Using link As New PrintableComponentLink(ps)

                link.Component = gridControl
                link.Landscape = True
                link.PaperKind = System.Drawing.Printing.PaperKind.A3

                ps.Document.AutoFitToPagesWidth = 1
                link.CreateDocument()

                link.ExportToImage(ms, New ImageExportOptions() With {
                .Format = System.Drawing.Imaging.ImageFormat.Png,
                .ExportMode = ImageExportMode.SingleFilePageByPage,
                .PageRange = "1",
                .Resolution = 300
            })

            End Using
        End Using

        ms.Position = 0
        Return ms

    End Function

    Private Function CloneSlide(presPart As PresentationPart, sourceSlide As SlidePart) As SlidePart

        Dim newSlidePart = presPart.AddNewPart(Of SlidePart)()
        newSlidePart.Slide = CType(sourceSlide.Slide.CloneNode(True), Slide)

        ' Copy layout relationship
        newSlidePart.AddPart(sourceSlide.SlideLayoutPart)

        ' Add SlideId
        Dim slideIdList = presPart.Presentation.SlideIdList

        Dim maxId As UInteger = 256
        If slideIdList.ChildElements.Count > 0 Then
            maxId = slideIdList.ChildElements.
                    OfType(Of SlideId)().
                    Max(Function(s) s.Id)
        End If

        slideIdList.Append(New SlideId With {
            .Id = maxId + 1,
            .RelationshipId = presPart.GetIdOfPart(newSlidePart)
        })

        Return newSlidePart
    End Function

    Private Sub InsertImageIntoSlide(slidePart As SlidePart, imageStream As Stream)

        Dim imgPart = slidePart.AddImagePart(ImagePartType.Png)

        imageStream.Position = 0
        imgPart.FeedData(imageStream)

        Dim slideWidthEmu As Long = 12192000
        Dim slideHeightEmu As Long = 6858000

        Const EMU_PER_INCH As Long = 914400

        Dim imgPixelW As Integer
        Dim imgPixelH As Integer

        Dim dpiX As Single
        Dim dpiY As Single

        imageStream.Position = 0

        Using img As Image = Image.FromStream(imageStream, False, False)

            imgPixelW = img.Width
            imgPixelH = img.Height

            dpiX = img.HorizontalResolution
            dpiY = img.VerticalResolution

        End Using

        Dim imgWEmu As Long = CLng((imgPixelW / dpiX) * EMU_PER_INCH)

        Dim imgHEmu As Long = CLng((imgPixelH / dpiY) * EMU_PER_INCH)

        Dim sideMargin As Long = 20000
        Dim topBottomMargin As Long = 20000

        Dim maxW As Long = slideWidthEmu - (2 * sideMargin)

        Dim maxH As Long = slideHeightEmu - (2 * topBottomMargin)

        Dim scale As Double = System.Math.Min(maxW / imgWEmu, maxH / imgHEmu)

        Dim finalW As Long = CLng(imgWEmu * scale)

        Dim finalH As Long = CLng(imgHEmu * scale)

        Dim offsetX As Long = (slideWidthEmu - finalW) \ 2
        Dim offsetY As Long = (slideHeightEmu - finalH) \ 2

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree

        Dim picId As UInteger = CType(GetNextShapeId(shapeTree), UInteger)

        Dim pic As New Picture(New NonVisualPictureProperties(New NonVisualDrawingProperties() With {
                .Id = picId,
                .Name = "Image"
            }, New NonVisualPictureDrawingProperties(
                New Drawing.PictureLocks() With {
                    .NoChangeAspect = True
                }
            ), New ApplicationNonVisualDrawingProperties()
        ), New BlipFill(New Drawing.Blip() With {
                .Embed = slidePart.GetIdOfPart(imgPart)
            }, New Drawing.Stretch(New Drawing.FillRectangle()
            )
        ),
        New ShapeProperties(New Drawing.Transform2D(New Drawing.Offset() With {
                    .X = offsetX,
                    .Y = offsetY
                }, New Drawing.Extents() With {
                    .Cx = finalW,
                    .Cy = finalH
                }
            ),
            New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            }
        )
    )

        shapeTree.Append(pic)
        slidePart.Slide.Save()
    End Sub

#End Region

#Region "Open XML Methods"

    Public Function CreateEvaluateReportFromTemplate(templatePath As String, reportFileName As String)
        Try
            slidePosition = 1

            ' Embed template file with the presentation
            CreatePresentationWithTemplate(templatePath, reportFileName)

            Using presDoc = PresentationDocument.Open(reportFileName, True)

                chartsPerSlide = 1

                Dim presPart = presDoc.PresentationPart
                Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

                ' ---------------------------------------------------
                ' PILOT INTRODUCTION TITLE SLIDE
                ' ---------------------------------------------------

                InsertTitleSlide(presPart, templateSlidePart, "Pilot Introduction")

                ' ---------------------------------------------------
                ' PILOT INTRODUCTION DETAILS SLIDE
                ' ---------------------------------------------------

                InsertPilotInfoSlide(presPart, templateSlidePart)

                ' ---------------------------------------------------
                ' Chnage KPI Table
                ' ---------------------------------------------------
                InsertChangeKPITableGrid(presDoc)

                ' -------- Slide - Change KPI Table --------
                InsertTitleSlide(presPart, templateSlidePart, "Change KPI Trend")

                For Each kvp In dicEvalRptChartImages
                    If kvp.Key.Contains("_Hist") Then
                        If dicEvalRptGridImages.Keys.Contains(Replace(kvp.Key, "_Hist", "_Trend")) Then
                            Dim kpiGridStream As MemoryStream = dicEvalRptGridImages(Replace(kvp.Key, "_Hist", "_Trend"))
                            Dim kpiGridBmp As Bitmap = dicEvalRptGridBmp(Replace(kvp.Key, "_Hist", "_Trend"))
                            Dim dt1 As DataTable = dicEvalRptGridDataTables(Replace(kvp.Key, "_Hist", "_Trend"))
                            If dicEvalRptGridImages.Keys.Contains(Replace(kvp.Key, "_Hist", "_TopX")) Then
                                Dim kpiTopXGridStream As MemoryStream = dicEvalRptGridImages(Replace(kvp.Key, "_Hist", "_TopX"))
                                Dim kpiTopXGridBmp As Bitmap = dicEvalRptGridBmp(Replace(kvp.Key, "_Hist", "_TopX"))
                                Dim dtTopX As DataTable = dicEvalRptGridDataTables(Replace(kvp.Key, "_Hist", "_TopX"))
                                'CreateHistoGramChartSlide(presDoc, dicEvalRptChartImages(kvp.Key), kpiGridStream, kpiTopXGridStream)
                                'CreateHistoGramChartSlide(presDoc, dicEvalRptChartImages(kvp.Key), dt1, kpiTopXGridStream)
                                CreateHistoGramChartSlide(presDoc, dicEvalRptChartImages(kvp.Key), kpiGridBmp, kpiTopXGridBmp)
                            End If
                        End If
                    End If
                Next

                'NORMAL KPI TREND TITLE SLIDE
                InsertTitleSlide(presPart, templateSlidePart, "Normal KPI Trend")

                'Adding Normal Trend Chart Slides
                CreateNormalTrendChartSlides(presDoc)

            End Using

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Return False
        End Try
    End Function

    Private Sub InsertPilotInfoSlide(ByRef presPart As PresentationPart, ByRef templateSlidePart As SlidePart)

        ' ---------------------------------------------------
        ' CREATE NEW SLIDE FROM TEMPLATE
        ' ---------------------------------------------------

        Dim slidePart As SlidePart =
        presPart.AddNewPart(Of SlidePart)()

        slidePart.Slide =
        CType(templateSlidePart.Slide.CloneNode(True), Slide)

        slidePart.AddPart(templateSlidePart.SlideLayoutPart)

        Dim shapeTree =
        slidePart.Slide.CommonSlideData.ShapeTree

        ' Remove existing shapes
        Dim shapesToRemove =
        shapeTree.Elements().
        Where(Function(s) TypeOf s Is Shape OrElse
                          TypeOf s Is Picture).
        ToList()

        For Each shp In shapesToRemove
            shp.Remove()
        Next

        ' ---------------------------------------------------
        ' CREATE TEXT SHAPE
        ' ---------------------------------------------------

        Dim shapeId As UInt32 = 100UI

        Dim textShape As New Shape()

        textShape.NonVisualShapeProperties =
        New NonVisualShapeProperties(
            New NonVisualDrawingProperties() With {
                .Id = shapeId,
                .Name = "Pilot Info"
            },
            New NonVisualShapeDrawingProperties(),
            New ApplicationNonVisualDrawingProperties()
        )

        textShape.ShapeProperties =
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {
                    .X = 300000,
                    .Y = 300000
                },
                New Drawing.Extents() With {
                    .Cx = 8500000,
                    .Cy = 6000000
                }
            )
        )

        ' ---------------------------------------------------
        ' TEXT BODY
        ' ---------------------------------------------------

        Dim textBody As New TextBody()

        textBody.BodyProperties =
        New Drawing.BodyProperties()

        textBody.ListStyle =
        New Drawing.ListStyle()

        ' ---------------------------------------------------
        ' TITLE
        ' ---------------------------------------------------

        textBody.Append(CreateParagraph("Pilot Name:", 3500, True, False))

        ' ---------------------------------------------------
        ' BULLETS
        ' ---------------------------------------------------

        textBody.Append(CreateBulletParagraph("Technology : ", Me.Technology))

        textBody.Append(CreateBulletParagraph("Target Type : ", Me.TargetType))

        textBody.Append(CreateBulletParagraph("ChartSetName : ", Me.ChartSetName))

        textBody.Append(CreateBulletParagraph("Cell/Site/Layer/Cluster/Nationwide : ", Me.Area))

        textBody.Append(CreateBulletParagraph("Count : ", Me.SiteCount & " (Mention the site count)"))

        textBody.Append(CreateBulletParagraph("Selected Area : ", Me.SelectedArea))

        textBody.Append(CreateBulletParagraph("Period Selection:", ""))

        textBody.Append(CreateSubBulletParagraph("Start Time : ", Me.StartTime))

        textBody.Append(CreateSubBulletParagraph("End Time : ", Me.EndTime))

        textBody.Append(CreateSubBulletParagraph("Resolution : ", Me.Resolution))

        textBody.Append(CreateSubBulletParagraph("Filter : ", Me.Filter))

        textBody.Append(CreateBulletParagraph("Period Comparison :", ""))

        textBody.Append(CreateSubBulletParagraph("Before Time : ", PrdCompBeforeTime))

        textBody.Append(CreateSubBulletParagraph("After Time : ", PrdCompAfterTime))

        textBody.Append(CreateBulletParagraph("TopN : Top ", Me.TopX & " Cells"))

        textShape.TextBody = textBody

        shapeTree.Append(textShape)

        ' ---------------------------------------------------
        ' ADD SLIDE TO PRESENTATION
        ' ---------------------------------------------------

        Dim slideIdList =
        presPart.Presentation.SlideIdList

        Dim maxId As UInt32 = 1UI

        If slideIdList.ChildElements.Count > 0 Then
            maxId = slideIdList.ChildElements.
            OfType(Of SlideId)().
            Max(Function(s) s.Id.Value)
        End If

        Dim slideId As New SlideId() With {
            .Id = maxId + 1UI,
            .RelationshipId = presPart.GetIdOfPart(slidePart)
        }

        slideIdList.Append(slideId)
        slidePart.Slide.Save()
    End Sub

    Private Function CreateParagraph(text As String, fontSize As Integer, bold As Boolean, bullet As Boolean) As Drawing.Paragraph
        Dim runProps As New Drawing.RunProperties() With {
            .FontSize = fontSize
        }

        If bold Then
            runProps.Bold = True
        End If

        Dim para As New Drawing.Paragraph()

        If bullet Then
            para.ParagraphProperties =
            New Drawing.ParagraphProperties(
                New Drawing.CharacterBullet() With {.Char = "•"}
            )
        End If

        para.Append(
        New Drawing.Run(runProps, New Drawing.Text(text)))

        Return para

    End Function

    Private Function CreateBulletParagraph(labelText As String, valueText As String) As Drawing.Paragraph
        Dim para As New Drawing.Paragraph()
        para.ParagraphProperties =
        New Drawing.ParagraphProperties() With {
            .Level = 0
        }

        para.Append(
        New Drawing.Run(
            New Drawing.RunProperties() With {
                .Bold = False,
                .FontSize = 1800
            },
            New Drawing.Text("• " & labelText & " ")
        )
    )

        para.Append(
        New Drawing.Run(
            New Drawing.RunProperties() With {
                .Bold = True,
                .FontSize = 1800
            },
            New Drawing.Text(valueText)
        )
    )
        Return para
    End Function

    Private Function CreateSubBulletParagraph(labelText As String, valueText As String) As Drawing.Paragraph
        Dim para As New Drawing.Paragraph()

        para.ParagraphProperties =
        New Drawing.ParagraphProperties() With {
            .Level = 1
        }

        para.Append(
        New Drawing.Run(
            New Drawing.RunProperties() With {
                .Bold = False,
                .FontSize = 1600
            },
            New Drawing.Text("• " & labelText & " ")
        )
    )

        para.Append(
        New Drawing.Run(
            New Drawing.RunProperties() With {
                .Bold = True,
                .FontSize = 1600
            },
            New Drawing.Text(valueText)
        )
    )
        Return para
    End Function

#Region "Normal KPI Trend"

    Private Sub CreateNormalTrendChartSlides(ByRef presDoc As PresentationDocument)

        Dim presPart = presDoc.PresentationPart

        Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

        Dim slidePart As SlidePart = Nothing
        Dim slide As Slide = Nothing

        Dim chartPosition As Integer = 0

        For Each kvp In dicEvalRptChartImages
            If kvp.Key.Contains("_NormalTrend") Then

                ' Create new slide every 4 charts
                If chartPosition Mod 4 = 0 Then

                    slidePart = CreateNewNormalTrendSlide(presPart, templateSlidePart)
                    slide = slidePart.Slide

                End If

                AddNormalTrendChartToSlide(slide, slidePart, kvp.Value, chartPosition Mod 4)
                chartPosition += 1

            End If

        Next

        Dim validator As New DocumentFormat.OpenXml.Validation.OpenXmlValidator()

        'Dim errors = validator.Validate(presDoc)
        'For Each validationError In errors
        '    Debug.WriteLine("------------------------------------------------")
        '    Debug.WriteLine("Description : " & validationError.Description)

        '    If validationError.Path IsNot Nothing Then
        '        Debug.WriteLine("Path        : " & validationError.Path.XPath)
        '    End If

        '    If validationError.Part IsNot Nothing Then
        '        Debug.WriteLine("Part        : " & validationError.Part.Uri.ToString())
        '    End If
        'Next

        presPart.Presentation.Save()

    End Sub

    Private Function CreateNewNormalTrendSlide(ByRef presPart As PresentationPart, ByRef templateSlidePart As SlidePart) As SlidePart

        Dim newSlidePart As SlidePart = presPart.AddNewPart(Of SlidePart)()
        newSlidePart.Slide = CType(templateSlidePart.Slide.CloneNode(True), Slide)

        ' Preserve layout relationship
        newSlidePart.AddPart(templateSlidePart.SlideLayoutPart)
        Dim shapeTree = newSlidePart.Slide.CommonSlideData.ShapeTree
        Dim shapesToRemove = shapeTree.Elements().Where(Function(s) TypeOf s Is Shape OrElse TypeOf s Is Picture).ToList()

        For Each shp In shapesToRemove
            shp.Remove()
        Next

        ' ---------------------------------------------------
        ' ADD SLIDE TO PRESENTATION
        ' ---------------------------------------------------

        Dim slideIdList = presPart.Presentation.SlideIdList
        Dim maxId As UInt32 = 1UI

        If slideIdList.ChildElements.Count > 0 Then

            maxId =
            slideIdList.ChildElements.
            OfType(Of SlideId)().
            Max(Function(s) s.Id.Value)

        End If

        Dim slideId As New SlideId() With {
        .Id = maxId + 1UI,
        .RelationshipId = presPart.GetIdOfPart(newSlidePart)
    }

        slideIdList.Append(slideId)
        newSlidePart.Slide.Save()
        Return newSlidePart
    End Function

    Private Sub AddNormalTrendChartToSlide(ByRef slide As Slide, ByRef slidePart As SlidePart, bmp As Bitmap, position As Integer)

        Dim slideWidth As Long = 9144000
        Dim slideHeight As Long = 6858000

        Dim gap As Long = 10000

        Dim chartWidth As Long = 6.6 * 914400
        Dim chartHeight As Long = 4 * 914400

        Dim x As Long = 0
        Dim y As Long = 0

        Select Case position

            Case 0 ' TOP LEFT

                x = 0
                y = 0

            Case 1 ' TOP RIGHT

                x = chartWidth + gap
                y = 0

            Case 2 ' BOTTOM LEFT

                x = 0
                y = chartHeight + gap

            Case 3 ' BOTTOM RIGHT

                x = chartWidth + gap
                y = chartHeight + gap

        End Select

        InsertNormalTrendBitmap(slide, slidePart, bmp, x, y, chartWidth, chartHeight)

    End Sub

    Private Sub InsertNormalTrendBitmap(ByRef slide As Slide, ByRef slidePart As SlidePart, bmp As Bitmap, x As Long, y As Long, width As Long, height As Long)

        Dim imagePart = slidePart.AddImagePart(ImagePartType.Png)

        Using ms As New MemoryStream()
            bmp.Save(ms, Imaging.ImageFormat.Png)
            ms.Position = 0
            imagePart.FeedData(ms)
        End Using

        Dim relId = slidePart.GetIdOfPart(imagePart)

        Dim shapeTree = slide.CommonSlideData.ShapeTree

        Dim picId As UInt32 = CType(GetNextShapeId(shapeTree), UInt32)

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {
                .Id = picId,
                .Name = "NormalTrendChart"
            },
            New NonVisualPictureDrawingProperties(
                New Drawing.PictureLocks() With {
                    .NoChangeAspect = True
                }),
            New ApplicationNonVisualDrawingProperties()
        ),
        New BlipFill(
            New Drawing.Blip() With {
                .Embed = relId
            },
            New Drawing.Stretch(
                New Drawing.FillRectangle()
            )
        ),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {
                    .X = x,
                    .Y = y
                },
                New Drawing.Extents() With {
                    .Cx = 6.6 * 914400,
                    .Cy = 4 * 914400
                }
            ),
            New Drawing.PresetGeometry(
                New Drawing.AdjustValueList()
            ) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            }
        )
    )

        shapeTree.Append(pic)

    End Sub

#End Region

    Private Sub InsertChangeKPITableGrid(ByRef presDoc As PresentationDocument)
        Dim presPart = presDoc.PresentationPart
        Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

        ' -------- Slide - Change KPI Table --------
        InsertTitleSlide(presPart, templateSlidePart, "Change KPI Table")

        ' -------- GRID IMAGE --------
        If dicEvalRptGridImages.ContainsKey("ChangeKPITable") Then

            Dim baseStream As MemoryStream = dicEvalRptGridImages("ChangeKPITable")

            Dim safeStream As New MemoryStream(baseStream.ToArray())

            Dim slide = CloneSlide(presPart, templateSlidePart)

            safeStream.Position = 0
            InsertImageIntoSlide(slide, safeStream)

            safeStream.Dispose()
        End If
    End Sub

    Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, ByRef dtSummary As DataTable, ByRef kpiTopXGridStream As MemoryStream)
        Try

            Dim presPart = presDoc.PresentationPart

            Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

            ' ---------------------------------------------------
            ' CREATE CHART SLIDE
            ' ---------------------------------------------------

            Dim newSlidePart As SlidePart = CloneSlide(presPart, templateSlidePart)
            Dim newSlide As Slide = newSlidePart.Slide

            drawingObjectId = 1

            ' ---------------------------------------------------
            ' INSERT HISTOGRAM CHART
            ' ---------------------------------------------------

            CopyChartBitmapToSlide(newSlide, newSlidePart, drawingObjectId, chBmp)

            newSlide.Save()

            ' ---------------------------------------------------
            ' CREATE TABLE SLIDE
            ' ---------------------------------------------------

            CreateHistogramGridSlideDataTables(newSlide, newSlidePart, dtSummary, kpiTopXGridStream)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, kpiGridStream As MemoryStream, kpiTopXGridStream As MemoryStream)
        Try
            Dim presPart = presDoc.PresentationPart
            Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

            ' Clone template slide (IMPORTANT)
            Dim newSlidePart As SlidePart = CloneSlide(presPart, templateSlidePart)

            Dim newSlide As Slide = newSlidePart.Slide

            ' Reset drawing ID
            drawingObjectId = 1

            ' Insert image properly
            CopyChartBitmapToSlide(newSlide, newSlidePart, drawingObjectId, chBmp)

            ' 2. Layout constants for the Grids
            Dim gridY As Long = (4 * 914400) + 100000
            Dim gridWidth As Long = (12 * 914400) / 2
            Dim gridHeight As Long = 3500000    '3.4 * 914400

            'Dim availableHeight As Long = 6858000 - gridY - 100000

            ' 3. Insert Grid 1 (Left)
            InsertImageFromStream(newSlide, newSlidePart, drawingObjectId, kpiGridStream, 30000, gridY, gridWidth, gridHeight)

            ' 4. Insert Grid 2 (Right)
            InsertImageFromStream(newSlide, newSlidePart, drawingObjectId + 1, kpiTopXGridStream, 30000 + gridWidth, gridY, gridWidth, gridHeight)

            newSlide.Save()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateHistogramGridSlide(ByRef presDoc As PresentationDocument, ByRef kpiGridStream As MemoryStream, ByRef kpiTopXGridStream As MemoryStream)
        Dim presPart = presDoc.PresentationPart

        Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

        ' ---------------------------------------------------
        ' CREATE NEW SLIDE
        ' ---------------------------------------------------

        Dim slidePart = CreateNewNormalTrendSlide(presPart, templateSlidePart)

        Dim slide = slidePart.Slide

        Dim drawingObjectId As Integer = 100

        ' ---------------------------------------------------
        ' SLIDE DIMENSIONS
        ' ---------------------------------------------------

        Dim slideWidth As Long = 9144000
        Dim slideHeight As Long = 6858000

        ' ---------------------------------------------------
        ' MARGINS / GAP
        ' ---------------------------------------------------

        Dim margin As Long = 30000
        Dim gap As Long = 30000

        ' ---------------------------------------------------
        ' AVAILABLE WIDTH/HEIGHT
        ' ---------------------------------------------------

        Dim usableWidth As Long = slideWidth - (2 * margin) - gap

        Dim usableHeight As Long = slideHeight - (2 * margin)

        ' ---------------------------------------------------
        ' GRID WIDTHS (40 : 60)
        ' ---------------------------------------------------

        Dim grid1Width As Long = CLng(usableWidth * 0.4)

        Dim grid2Width As Long = usableWidth - grid1Width

        ' ---------------------------------------------------
        ' FULL HEIGHT
        ' ---------------------------------------------------

        Dim gridHeight As Long = usableHeight

        ' ---------------------------------------------------
        ' GRID 1 POSITION
        ' ---------------------------------------------------

        Dim grid1X As Long = margin
        Dim grid1Y As Long = margin

        ' ---------------------------------------------------
        ' GRID 2 POSITION
        ' ---------------------------------------------------

        Dim grid2X As Long = grid1X + grid1Width + gap

        Dim grid2Y As Long = margin

        ' ---------------------------------------------------
        ' INSERT GRID 1
        ' ---------------------------------------------------

        InsertImageFromStream(slide, slidePart, drawingObjectId, kpiGridStream, grid1X, grid1Y, grid1Width, gridHeight)

        ' ---------------------------------------------------
        ' INSERT GRID 2
        ' ---------------------------------------------------

        InsertImageFromStream(slide, slidePart, drawingObjectId + 1, kpiTopXGridStream, grid2X, grid2Y, grid2Width, gridHeight)

        slide.Save()

    End Sub

    Private Sub CreateHistogramGridSlideDataTables(ByRef newSlide As Slide, ByRef newSlidePart As SlidePart, ByRef dt1 As DataTable, ByRef kpiTopXGridStream As MemoryStream)

        Dim drawingObjectId As Integer = 100

        ' ---------------------------------------------------
        ' SLIDE DIMENSIONS
        ' ---------------------------------------------------

        Dim slideWidth As Long = 9144000
        Dim slideHeight As Long = 6858000

        ' ---------------------------------------------------
        ' MARGINS / GAP
        ' ---------------------------------------------------

        Dim margin As Long = 30000
        Dim gap As Long = 30000

        ' ---------------------------------------------------
        ' AVAILABLE WIDTH/HEIGHT
        ' ---------------------------------------------------

        Dim usableWidth As Long = slideWidth - (2 * margin) - gap

        Dim usableHeight As Long = slideHeight - (2 * margin)

        ' ---------------------------------------------------
        ' GRID WIDTHS (40 : 60)
        ' ---------------------------------------------------

        Dim grid1Width As Long = CLng(usableWidth * 0.4)
        Dim grid2Width As Long = usableWidth - grid1Width

        ' ---------------------------------------------------
        ' FULL HEIGHT
        ' ---------------------------------------------------

        Dim gridHeight As Long = usableHeight

        ' ---------------------------------------------------
        ' GRID 1 POSITION
        ' ---------------------------------------------------

        Dim grid1X As Long = margin
        Dim grid1Y As Long = margin

        ' ---------------------------------------------------
        ' GRID 2 POSITION
        ' ---------------------------------------------------

        Dim grid2X As Long = grid1X + grid1Width + gap
        Dim grid2Y As Long = margin

        ' ---------------------------------------------------
        ' INSERT GRID 1
        ' ---------------------------------------------------

        'InsertKPIComparisonTable(newSlide, newSlidePart, dt1)

        ' ---------------------------------------------------
        ' INSERT GRID 2
        ' ---------------------------------------------------

        InsertImageFromStream(newSlide, newSlidePart, drawingObjectId, kpiTopXGridStream, grid2X, grid2Y, grid2Width, gridHeight)

        newSlide.Save()

    End Sub

    Private Sub InsertImageFromStreamVGrid(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef nextId As Integer, imgStream As MemoryStream, x As Long, y As Long, targetWidth As Long, targetHeight As Long)

        nextId += 1

        imgStream.Position = 0
        Dim img As Image = Image.FromStream(imgStream)

        Dim dpiX = img.HorizontalResolution
        Dim dpiY = img.VerticalResolution

        Dim emuPerPixelX = 914400.0 / dpiX
        Dim emuPerPixelY = 914400.0 / dpiY

        Dim imgWidthEmu = img.Width * emuPerPixelX
        Dim imgHeightEmu = img.Height * emuPerPixelY

        img.Dispose()

        Dim ratio As Double = imgHeightEmu / imgWidthEmu

        Dim widthBasedHeight As Long = CLng(targetWidth * ratio)

        Dim finalWidth As Long
        Dim finalHeight As Long

        If widthBasedHeight <= targetHeight Then
            ' fits → use full width
            finalWidth = targetWidth
            finalHeight = widthBasedHeight
        Else
            ' too tall → scale by height
            finalHeight = targetHeight
            finalWidth = CLng(finalHeight / ratio)
        End If

        imgStream.Position = 0

        Dim imagePart = slidePart.AddImagePart(ImagePartType.Png)
        imagePart.FeedData(imgStream)

        Dim relId = slidePart.GetIdOfPart(imagePart)

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {.Id = nextId, .Name = "Grid"},
            New NonVisualPictureDrawingProperties(New Drawing.PictureLocks() With {.NoChangeAspect = True}),
            New ApplicationNonVisualDrawingProperties()
        ),
        New BlipFill(
            New Drawing.Blip() With {.Embed = relId},
            New Drawing.Stretch(New Drawing.FillRectangle())
        ),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {.X = x, .Y = y},
                New Drawing.Extents() With {.Cx = finalWidth, .Cy = finalHeight}
            ),
            New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            }
        )
    )

        slide.CommonSlideData.ShapeTree.AppendChild(pic)

    End Sub

    Private Sub InsertImageFromStream(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef nextId As Integer, imgStream As MemoryStream,
                                 x As Long, y As Long, maxWidth As Long, maxHeight As Long)

        nextId += 1

        imgStream.Position = 0

        Dim img As Image =
        Image.FromStream(imgStream)

        Dim imgWidthPx As Double = img.Width
        Dim imgHeightPx As Double = img.Height

        Dim dpiX As Double = img.HorizontalResolution
        Dim dpiY As Double = img.VerticalResolution

        Dim emuPerPixelX As Double = 914400.0 / dpiX
        Dim emuPerPixelY As Double = 914400.0 / dpiY

        Dim imgWidthEmu As Double =
        imgWidthPx * emuPerPixelX

        Dim imgHeightEmu As Double =
        imgHeightPx * emuPerPixelY

        img.Dispose()

        ' ---------------------------------------------------
        ' PRESERVE ASPECT RATIO
        ' ---------------------------------------------------

        Dim widthRatio As Double = maxWidth / imgWidthEmu

        Dim heightRatio As Double = maxHeight / imgHeightEmu

        Dim scaleRatio As Double = System.Math.Min(widthRatio, heightRatio)

        Dim finalWidth As Long = CLng(imgWidthEmu * scaleRatio)

        Dim finalHeight As Long = CLng(imgHeightEmu * scaleRatio)

        imgStream.Position = 0

        Dim imagePart As ImagePart = slidePart.AddImagePart(ImagePartType.Png)

        imagePart.FeedData(imgStream)

        Dim relId As String = slidePart.GetIdOfPart(imagePart)

        Dim shapeTree = slide.CommonSlideData.ShapeTree

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {
                .Id = nextId,
                .Name = "Grid " & nextId
            },
            New NonVisualPictureDrawingProperties(
                New Drawing.PictureLocks() With {
                    .NoChangeAspect = True
                }),
            New ApplicationNonVisualDrawingProperties()
        ),
        New BlipFill(
            New Drawing.Blip() With {
                .Embed = relId
            },
            New Drawing.Stretch(
                New Drawing.FillRectangle()
            )
        ),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {
                    .X = x,
                    .Y = y
                },
                New Drawing.Extents() With {
                    .Cx = finalWidth,
                    .Cy = finalHeight
                }
            ),
            New Drawing.PresetGeometry(
                New Drawing.AdjustValueList()
            ) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            }
        )
    )

        shapeTree.AppendChild(pic)

    End Sub

    Private Sub CopyChartBitmapToSlide(ByRef slide As Slide, ByRef slidePart As SlidePart, ByVal drawingObjectId As Integer, chBmp As Bitmap)
        Try
            Dim shapeTree = slide.CommonSlideData.ShapeTree

            drawingObjectId += 1

            ' Create ImagePart
            Dim imagePart As ImagePart = slidePart.AddImagePart(ImagePartType.Png)

            Using stream As New MemoryStream()
                chBmp.Save(stream, Imaging.ImageFormat.Png)
                stream.Position = 0
                imagePart.FeedData(stream)
            End Using

            Dim relId As String = slidePart.GetIdOfPart(imagePart)

            ' Create Picture
            Dim picture As New Picture()

            ' Non-visual properties
            picture.NonVisualPictureProperties =
            New NonVisualPictureProperties(
                New NonVisualDrawingProperties() With {
                    .Id = drawingObjectId,
                    .Name = "Chart Image " & drawingObjectId
                },
                New NonVisualPictureDrawingProperties(
                    New Drawing.PictureLocks() With {.NoChangeAspect = True}
                ),
                New ApplicationNonVisualDrawingProperties()
            )

            ' BlipFill (image reference)
            Dim blipFill As New BlipFill(
            New Drawing.Blip() With {.Embed = relId},
            New Drawing.Stretch(New Drawing.FillRectangle())
        )

            picture.BlipFill = blipFill

            ' Position & Size (IMPORTANT)
            picture.ShapeProperties = New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {.X = 30000, .Y = 30000},
                New Drawing.Extents() With {.Cx = 12 * 914400, .Cy = 4 * 914400}
                ),
                New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {.Preset = Drawing.ShapeTypeValues.Rectangle}
            )

            shapeTree.AppendChild(picture)

            slide.Save()

        Catch ex As Exception
            Throw New Exception("Error inserting chart image: " & ex.Message)
        End Try
    End Sub

    Public Sub CreatePresentationWithTemplate(ByVal temFilePath As String, ByVal filepath As String)
        Try
            Dim byteArray As Byte() = File.ReadAllBytes(temFilePath)
            Using stream As New MemoryStream()
                stream.Write(byteArray, 0, CInt(byteArray.Length))
                Using pDoc = PresentationDocument.Open(stream, True)
                    pDoc.ChangeDocumentType(PresentationDocumentType.Presentation)
                End Using
                File.WriteAllBytes(filepath, stream.ToArray())
            End Using
        Catch
        End Try
    End Sub

    Private Function CreateCell(text As String, Optional bold As Boolean = False) As Drawing.TableCell

        Dim runProps As New Drawing.RunProperties() With {
        .FontSize = 800,
        .Bold = bold,
        .Language = "en-US"
    }

        Dim run As New Drawing.Run()

        run.RunProperties = runProps
        run.Text = New Drawing.Text(text)

        Dim para As New Drawing.Paragraph()
        para.Append(run)
        para.Append(New Drawing.EndParagraphRunProperties() With {
            .Language = "en-US"
        })

        Dim textBody As New Drawing.TextBody()
        textBody.Append(New Drawing.BodyProperties())
        textBody.Append(New Drawing.ListStyle())
        textBody.Append(para)

        Dim cell As New Drawing.TableCell()
        cell.Append(textBody)
        cell.Append(New Drawing.TableCellProperties())
        Return cell
    End Function

    Private Function GetNextShapeId(shapeTree As ShapeTree) As UInt32
        Dim maxId As UInt32 = 1UI
        ' Scan ALL presentation shape IDs
        Dim allNvProps = shapeTree.Descendants(Of NonVisualDrawingProperties)()
        For Each nvPr In allNvProps
            If nvPr.Id IsNot Nothing Then
                If nvPr.Id.Value > maxId Then
                    maxId = nvPr.Id.Value
                End If
            End If
        Next
        Return maxId + 1UI
    End Function

#Region "Change KPI Trend"

    Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, kpiGridBmp As Bitmap, kpiTopXGridBmp As Bitmap)
        Try
            Dim presPart = presDoc.PresentationPart
            Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

            Dim newSlidePart As SlidePart = CloneSlide(presPart, templateSlidePart)
            Dim newSlide As Slide = newSlidePart.Slide

            drawingObjectId = 1

            CopyChartBitmapToSlide(newSlide, newSlidePart, drawingObjectId, chBmp)

            Dim slideWidth As Long = 9144000
            Dim slideHeight As Long = 6858000
            Dim bottomMargin As Long = 30000

            ' Start lower on the slide
            Dim gridY As Long = 3720000
            Dim gridWidth As Long = (slideWidth \ 2) - 60000
            Dim leftGridX As Long = 30000
            Dim rightGridX As Long = leftGridX + gridWidth + 30000
            Dim leftGridHeight As Long = slideHeight - gridY - bottomMargin '1800000
            Dim rightGridHeight As Long = slideHeight - gridY - bottomMargin

            ' INSERT LEFT GRID BITMAP
            InsertImageFromBitmap(newSlide, newSlidePart, drawingObjectId, kpiGridBmp, leftGridX, gridY, gridWidth, leftGridHeight)

            ' INSERT RIGHT GRID BITMAP
            InsertImageFromBitmap(newSlide, newSlidePart, drawingObjectId, kpiTopXGridBmp, rightGridX, gridY, gridWidth, rightGridHeight)

            newSlide.Save()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertImageFromBitmap(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef nextId As Integer, bmp As Bitmap, x As Long, y As Long, cx As Long, cy As Long)
        Dim maxId As UInt32 = 1UI

        For Each nv In slide.CommonSlideData.ShapeTree.Descendants(Of NonVisualDrawingProperties)()
            If nv.Id IsNot Nothing Then
                maxId = System.Math.Max(maxId, nv.Id.Value)
            End If
        Next

        maxId += 1UI
        Dim ms As New MemoryStream()
        bmp.Save(ms, Imaging.ImageFormat.Png)
        ms.Position = 0

        Dim imagePart As ImagePart = slidePart.AddImagePart(ImagePartType.Png)

        imagePart.FeedData(ms)

        Dim relId As String = slidePart.GetIdOfPart(imagePart)
        Dim shapeTree = slide.CommonSlideData.ShapeTree

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {
                .Id = maxId,
                .Name = "GridImage_" & maxId
            },
            New NonVisualPictureDrawingProperties(
                New Drawing.PictureLocks() With {
                    .NoChangeAspect = True
                }
            ),
            New ApplicationNonVisualDrawingProperties()
        ),
        New BlipFill(
            New Drawing.Blip() With {
                .Embed = relId
            },
            New Drawing.Stretch(
                New Drawing.FillRectangle()
            )
        ),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {
                    .X = x,
                    .Y = y
                },
                New Drawing.Extents() With {
                    .Cx = cx,
                    .Cy = cy
                }
            ),
            New Drawing.PresetGeometry() With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            }
        )
    )
        shapeTree.AppendChild(pic)
    End Sub

#End Region

#End Region

End Class
