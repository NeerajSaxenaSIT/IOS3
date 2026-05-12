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

    Public Sub CreatePresentationFromTemplate(templatePath As String, outputPath As String)

        File.Copy(templatePath, outputPath, True)

        Using doc As PresentationDocument = PresentationDocument.Open(outputPath, True)

            Dim presPart = doc.PresentationPart
            Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

            ' -------- TITLE 1 --------
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

            ' -------- TITLE 2 --------
            InsertTitleSlide(presPart, templateSlidePart, "Change KPI Trend")

            ' -------- HIST IMAGES --------
            For Each kvp In dicEvalRptGridImages

                If kvp.Key.Contains("_Hist") Then

                    Dim baseStream As MemoryStream = kvp.Value

                    Dim safeStream As New MemoryStream(baseStream.ToArray())

                    Dim slide = CloneSlide(presPart, templateSlidePart)

                    safeStream.Position = 0

                    InsertImageIntoSlide(slide, safeStream, "TopLeft")

                    safeStream.Dispose()
                End If

            Next

            RemoveSlide(presPart, templateSlidePart)

            presPart.Presentation.Save()
        End Using

    End Sub

    Private Function CreateSlideFromLayout(presPart As PresentationPart, layoutPart As SlideLayoutPart) As SlidePart

        Dim slidePart = presPart.AddNewPart(Of SlidePart)()
        slidePart.AddPart(layoutPart)

        slidePart.Slide = New Slide(
        New CommonSlideData(New ShapeTree()),
        New ColorMapOverride(New DocumentFormat.OpenXml.Drawing.MasterColorMapping())
    )

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
        .RelationshipId = presPart.GetIdOfPart(slidePart)
    })

        Return slidePart
    End Function

    Private Sub InsertTitleSlide(presPart As PresentationPart, templateSlidePart As SlidePart, titleText As String)

        Dim slidePart = CloneSlide(presPart, templateSlidePart)

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree

        Dim slideWidthEmu As Long = 12192000
        Dim slideHeightEmu As Long = 6858000

        Dim boxWidth As Long = 8000000
        Dim boxHeight As Long = 1500000

        Dim offsetX As Long = (slideWidthEmu - boxWidth) \ 2
        Dim offsetY As Long = (slideHeightEmu - boxHeight) \ 2

        Dim shapeId As UInteger = CType(shapeTree.ChildElements.Count + 1, UInteger)

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

    Public Function ExportVGridToImage(ByRef vGrid As DevExpress.XtraVerticalGrid.VGridControl) As MemoryStream

        vGrid.ForceInitialize()

        ' Keep readable font
        Dim printFont As New System.Drawing.Font("Tahoma", 7)
        For Each ap As DevExpress.Utils.AppearanceObject In vGrid.Appearance
            ap.Font = printFont
            ap.Options.UseFont = True
        Next

        ' 🔥 IMPORTANT: Let DevExpress size naturally
        vGrid.OptionsView.AutoScaleBands = False
        vGrid.RowHeaderWidth = 150
        vGrid.RecordWidth = 350

        ' ❌ REMOVE THIS (was causing issues)
        ' vGrid.Width = 1200

        vGrid.BestFit()

        Dim ms As New MemoryStream()

        Using ps As New PrintingSystem()
            Using link As New PrintableComponentLink(ps)

                link.Component = vGrid
                link.Landscape = True

                link.Margins = New Printing.Margins(0, 0, 0, 0)

                ' ✅ ONLY FIT WIDTH
                ps.Document.AutoFitToPagesWidth = 1

                ' ❌ DO NOT FIT HEIGHT (causes squashing)
                ' ps.Document.AutoFitToPagesHeight = 1

                ' ✅ Mild scaling
                link.PrintingSystem.Document.ScaleFactor = 0.85F

                link.CreateDocument()

                Dim options As New ImageExportOptions() With {
                .Format = Imaging.ImageFormat.Png,
                .Resolution = 120,
                .ExportMode = ImageExportMode.SingleFile
            }

                link.ExportToImage(ms, options)

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

    Private Sub InsertImageIntoSlide_Old(slidePart As SlidePart, imageStream As Stream, Optional isHalfHeight As Boolean = False)

        Dim imgPart = slidePart.AddImagePart(ImagePartType.Png)

        ' Reset stream before feeding
        imageStream.Position = 0
        imgPart.FeedData(imageStream)

        ' Slide size (16:9)
        Dim slideWidthEmu As Long = 12192000
        Dim slideHeightEmu As Long = 6858000

        Const EMU_PER_INCH As Long = 914400

        ' ---- Read image FROM STREAM (FIX) ----
        Dim imgPixelW As Integer
        Dim imgPixelH As Integer
        Dim dpiX As Single
        Dim dpiY As Single

        imageStream.Position = 0 ' IMPORTANT

        Using img As Image = Image.FromStream(imageStream, False, False)
            imgPixelW = img.Width
            imgPixelH = img.Height
            dpiX = img.HorizontalResolution
            dpiY = img.VerticalResolution
        End Using

        ' Convert to EMU
        Dim imgWEmu As Long = CLng((imgPixelW / dpiX) * EMU_PER_INCH)
        Dim imgHEmu As Long = CLng((imgPixelH / dpiY) * EMU_PER_INCH)

        ' Add small margin (2%)
        Dim marginRatio As Double = 0.02
        Dim maxW As Long = slideWidthEmu * (1 - marginRatio)
        Dim maxH As Long = slideHeightEmu * (1 - marginRatio)

        ' Scale (IMPORTANT)
        Dim scale As Double = System.Math.Min(maxW / imgWEmu, maxH / imgHEmu)

        Dim finalW As Long = CLng(imgWEmu * scale)
        Dim finalH As Long = CLng(imgHEmu * scale)

        ' Center
        Dim offsetX As Long = (slideWidthEmu - finalW) \ 2
        Dim offsetY As Long = (slideHeightEmu - finalH) \ 2

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree
        Dim picId As UInteger = CType(shapeTree.ChildElements.Count + 1, UInteger)

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {.Id = picId, .Name = "Grid Image"},
            New NonVisualPictureDrawingProperties(New Drawing.PictureLocks() With {.NoChangeAspect = True}),
            New ApplicationNonVisualDrawingProperties()),
        New BlipFill(
            New Drawing.Blip() With {.Embed = slidePart.GetIdOfPart(imgPart)},
            New Drawing.Stretch(New Drawing.FillRectangle())),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {.X = offsetX, .Y = offsetY},
                New Drawing.Extents() With {.Cx = finalW, .Cy = finalH}),
            New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            })
    )

        shapeTree.Append(pic)
        slidePart.Slide.Save()

    End Sub

    Private Sub InsertImageIntoSlide(slidePart As SlidePart, imageStream As Stream, Optional layoutType As String = "Full")

        Dim imgPart = slidePart.AddImagePart(ImagePartType.Png)

        ' Reset stream before feeding
        imageStream.Position = 0
        imgPart.FeedData(imageStream)

        ' Slide size (16:9)
        Dim slideWidthEmu As Long = 12192000
        Dim slideHeightEmu As Long = 6858000

        Const EMU_PER_INCH As Long = 914400

        ' ---- Read image FROM STREAM ----
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

        ' Convert to EMU
        Dim imgWEmu As Long = CLng((imgPixelW / dpiX) * EMU_PER_INCH)
        Dim imgHEmu As Long = CLng((imgPixelH / dpiY) * EMU_PER_INCH)

        ' Add small margin (2%)
        Dim marginRatio As Double = 0.02

        Dim maxW As Long
        Dim maxH As Long
        Dim offsetX As Long
        Dim offsetY As Long
        Dim scale As Double
        Dim finalW As Long
        Dim finalH As Long

        Select Case layoutType

            Case "TopHalf"

                maxW = slideWidthEmu * (1 - marginRatio)
                maxH = (slideHeightEmu / 2) * (1 - marginRatio)

                ' Always scale by width (NO compromise)
                scale = maxW / imgWEmu

                Dim finalWTemp As Long = CLng(imgWEmu * scale)
                Dim finalHTemp As Long = CLng(imgHEmu * scale)

                finalW = finalWTemp
                finalH = finalHTemp

                ' Center horizontally
                offsetX = (slideWidthEmu - finalW) \ 2

                ' Stick to top
                offsetY = 0

                ' 🔥 If height exceeds half → crop instead of shrink
                Dim cropBottomRatio As Double = 0

                If finalH > maxH Then
                    cropBottomRatio = (finalH - maxH) / finalH
                End If

            Case "BottomLeft", "BottomRight"

                maxW = (slideWidthEmu / 2) * (1 - marginRatio)
                maxH = (slideHeightEmu / 2) * (1 - marginRatio)

                scale = System.Math.Min(maxW / imgWEmu, maxH / imgHEmu)

                finalW = CLng(imgWEmu * scale)
                finalH = CLng(imgHEmu * scale)

            Case Else ' Full

                maxW = slideWidthEmu * (1 - marginRatio)
                maxH = slideHeightEmu * (1 - marginRatio)

                scale = System.Math.Min(maxW / imgWEmu, maxH / imgHEmu)

                finalW = CLng(imgWEmu * scale)
                finalH = CLng(imgHEmu * scale)

        End Select

        ' ---- POSITION ----
        Select Case layoutType

            Case "TopHalf"
                offsetX = (slideWidthEmu - finalW) \ 2
                offsetY = ((slideHeightEmu / 2) - finalH) \ 2

            Case "BottomLeft"
                offsetX = ((slideWidthEmu / 2) - finalW) \ 2
                offsetY = (slideHeightEmu / 2) + ((slideHeightEmu / 2 - finalH) \ 2)

            Case "BottomRight"
                offsetX = (slideWidthEmu / 2) + ((slideWidthEmu / 2 - finalW) \ 2)
                offsetY = (slideHeightEmu / 2) + ((slideHeightEmu / 2 - finalH) \ 2)

            Case Else
                offsetX = (slideWidthEmu - finalW) \ 2
                offsetY = (slideHeightEmu - finalH) \ 2

        End Select

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree
        Dim picId As UInteger = CType(shapeTree.ChildElements.Count + 1, UInteger)

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {.Id = picId, .Name = "Image"},
            New NonVisualPictureDrawingProperties(New Drawing.PictureLocks() With {.NoChangeAspect = True}),
            New ApplicationNonVisualDrawingProperties()),
        New BlipFill(
            New Drawing.Blip() With {.Embed = slidePart.GetIdOfPart(imgPart)},
            New Drawing.Stretch(New Drawing.FillRectangle())),
        New ShapeProperties(
            New Drawing.Transform2D(
                New Drawing.Offset() With {.X = offsetX, .Y = offsetY},
                New Drawing.Extents() With {.Cx = finalW, .Cy = finalH}),
            New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {
                .Preset = Drawing.ShapeTypeValues.Rectangle
            })
    )

        shapeTree.Append(pic)
        slidePart.Slide.Save()

    End Sub

    Private Sub InsertImageIntoPlaceholder(slidePart As SlidePart, imageStream As Stream, shapeName As String)

        Dim shapeTree = slidePart.Slide.CommonSlideData.ShapeTree

        ' 🔥 STEP 1: Try finding shape in slide (rare)
        Dim targetShape = shapeTree.Elements(Of Shape)().
        FirstOrDefault(Function(s) s.NonVisualShapeProperties.
            NonVisualDrawingProperties.Name.Value = shapeName)

        ' 🔥 STEP 2: If not found → search in Layout
        If targetShape Is Nothing Then

            Dim layoutShapes = slidePart.SlideLayoutPart.
            SlideLayout.CommonSlideData.ShapeTree.
            Elements(Of Shape)()

            Dim layoutShape = layoutShapes.FirstOrDefault(Function(s) s.
            NonVisualShapeProperties.NonVisualDrawingProperties.Name.Value = shapeName)

            If layoutShape Is Nothing Then
                Throw New Exception($"Shape '{shapeName}' not found in slide or layout.")
            End If

            ' Use layout shape position
            Dim transform = layoutShape.ShapeProperties.Transform2D
            Dim offset = transform.Offset
            Dim extents = transform.Extents

            ' Add image part
            Dim imgPart = slidePart.AddImagePart(ImagePartType.Png)

            imageStream.Position = 0
            imgPart.FeedData(imageStream)

            Dim picId As UInteger = CType(shapeTree.ChildElements.Count + 1, UInteger)

            Dim pic As New Picture(
            New NonVisualPictureProperties(
                New NonVisualDrawingProperties() With {.Id = picId, .Name = "Image"},
                New NonVisualPictureDrawingProperties(New Drawing.PictureLocks() With {.NoChangeAspect = True}),
                New ApplicationNonVisualDrawingProperties()),
            New BlipFill(
                New Drawing.Blip() With {.Embed = slidePart.GetIdOfPart(imgPart)},
                New Drawing.Stretch(New Drawing.FillRectangle())),
            New ShapeProperties(
                New Drawing.Transform2D(
                    New Drawing.Offset() With {.X = offset.X, .Y = offset.Y},
                    New Drawing.Extents() With {.Cx = extents.Cx, .Cy = extents.Cy}),
                New Drawing.PresetGeometry(New Drawing.AdjustValueList()) With {
                    .Preset = Drawing.ShapeTypeValues.Rectangle
                })
        )

            shapeTree.Append(pic)

        Else
            ' 🔥 (Optional fallback if shape actually exists in slide)
            targetShape.Remove()
        End If

        slidePart.Slide.Save()

    End Sub

    Private Sub RemoveSlide(presPart As PresentationPart, slidePart As SlidePart)

        Dim slideIdList = presPart.Presentation.SlideIdList

        Dim slideId = slideIdList.ChildElements.OfType(Of SlideId)().FirstOrDefault(Function(s) s.RelationshipId = presPart.GetIdOfPart(slidePart))

        If slideId IsNot Nothing Then
            slideIdList.RemoveChild(slideId)
        End If

        presPart.DeletePart(slidePart)

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
                            Dim dt1 As DataTable = dicEvalRptGridDataTables(Replace(kvp.Key, "_Hist", "_Trend"))
                            If dicEvalRptGridImages.Keys.Contains(Replace(kvp.Key, "_Hist", "_TopX")) Then
                                Dim kpiTopXGridStream As MemoryStream = dicEvalRptGridImages(Replace(kvp.Key, "_Hist", "_TopX"))
                                Dim dtTopX As DataTable = dicEvalRptGridDataTables(Replace(kvp.Key, "_Hist", "_TopX"))
                                'CreateHistoGramChartSlide(presDoc, dicEvalRptChartImages(kvp.Key), kpiGridStream, kpiTopXGridStream)
                                CreateHistoGramChartSlide(presDoc, dicEvalRptChartImages(kvp.Key), dt1, dtTopX)
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

#Region "Normal Trend KPI"

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

        Dim picId As UInt32 = CType(shapeTree.ChildElements.Count + 1, UInt32)

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

    'Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, ByRef kpiGridStream As MemoryStream, ByRef kpiTopXGridStream As MemoryStream)
    '    Try

    '        Dim presPart = presDoc.PresentationPart

    '        Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

    '        ' ---------------------------------------------------
    '        ' CLONE TEMPLATE SLIDE
    '        ' ---------------------------------------------------

    '        Dim newSlidePart As SlidePart = CloneSlide(presPart, templateSlidePart)

    '        Dim newSlide As Slide = newSlidePart.Slide

    '        drawingObjectId = 1

    '        ' ---------------------------------------------------
    '        ' INSERT HISTOGRAM CHART
    '        ' ---------------------------------------------------

    '        CopyChartBitmapToSlide(newSlide, newSlidePart, drawingObjectId, chBmp)

    '        newSlide.Save()

    '        CreateHistogramGridSlide(presDoc, kpiGridStream, kpiTopXGridStream)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, ByRef dtSummary As DataTable, ByRef dtTopX As DataTable)
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

            CreateHistogramGridSlideDataTables(presDoc, dtSummary, dtTopX)

        Catch ex As Exception

        End Try

    End Sub

    'Private Sub CreateHistoGramChartSlide(ByRef presDoc As PresentationDocument, chBmp As Bitmap, kpiGridStream As MemoryStream, kpiTopXGridStream As MemoryStream)
    '    Try
    '        Dim presPart = presDoc.PresentationPart
    '        Dim templateSlidePart As SlidePart = presPart.SlideParts.First()

    '        ' Clone template slide (IMPORTANT)
    '        Dim newSlidePart As SlidePart = CloneSlide(presPart, templateSlidePart)

    '        Dim newSlide As Slide = newSlidePart.Slide

    '        ' Reset drawing ID
    '        drawingObjectId = 1

    '        ' Insert image properly
    '        CopyChartBitmapToSlide(newSlide, newSlidePart, drawingObjectId, chBmp)

    '        ' 2. Layout constants for the Grids
    '        Dim gridY As Long = (4 * 914400) + 100000
    '        Dim gridWidth As Long = (12 * 914400) / 2
    '        Dim gridHeight As Long = 3500000    '3.4 * 914400

    '        'Dim availableHeight As Long = 6858000 - gridY - 100000

    '        ' 3. Insert Grid 1 (Left)
    '        InsertImageFromStream(newSlide, newSlidePart, drawingObjectId, kpiGridStream, 30000, gridY, gridWidth, gridHeight)

    '        ' 4. Insert Grid 2 (Right)
    '        InsertImageFromStream(newSlide, newSlidePart, drawingObjectId + 1, kpiTopXGridStream, 30000 + gridWidth, gridY, gridWidth, gridHeight)

    '        newSlide.Save()
    '    Catch ex As Exception

    '    End Try
    'End Sub

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

    Private Sub CreateHistogramGridSlideDataTables(ByRef presDoc As PresentationDocument, ByRef dt1 As DataTable, ByRef dt2 As DataTable)
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

        InsertKPIComparisonTable(slide, slidePart, dt1)

        ' ---------------------------------------------------
        ' INSERT GRID 2
        ' ---------------------------------------------------

        'InsertImageFromStream(slide, slidePart, drawingObjectId + 1, kpiTopXGridStream, grid2X, grid2Y, grid2Width, gridHeight)

        slide.Save()

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

    'Private Sub InsertImageFromStream(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef nextId As Integer, imgStream As MemoryStream,
    '                             x As Long, y As Long, maxWidth As Long, maxHeight As Long)

    '    nextId += 1

    '    imgStream.Position = 0

    '    Dim img As Image =
    '    Image.FromStream(imgStream)

    '    Dim imgWidthPx As Double = img.Width
    '    Dim imgHeightPx As Double = img.Height

    '    Dim dpiX As Double = img.HorizontalResolution
    '    Dim dpiY As Double = img.VerticalResolution

    '    Dim emuPerPixelX As Double = 914400.0 / dpiX
    '    Dim emuPerPixelY As Double = 914400.0 / dpiY

    '    Dim imgWidthEmu As Double =
    '    imgWidthPx * emuPerPixelX

    '    Dim imgHeightEmu As Double =
    '    imgHeightPx * emuPerPixelY

    '    img.Dispose()

    '    ' ---------------------------------------------------
    '    ' PRESERVE ASPECT RATIO
    '    ' ---------------------------------------------------

    '    Dim widthRatio As Double = maxWidth / imgWidthEmu

    '    Dim heightRatio As Double = maxHeight / imgHeightEmu

    '    Dim scaleRatio As Double = System.Math.Min(widthRatio, heightRatio)

    '    Dim finalWidth As Long = CLng(imgWidthEmu * scaleRatio)

    '    Dim finalHeight As Long = CLng(imgHeightEmu * scaleRatio)

    '    imgStream.Position = 0

    '    Dim imagePart As ImagePart = slidePart.AddImagePart(ImagePartType.Png)

    '    imagePart.FeedData(imgStream)

    '    Dim relId As String = slidePart.GetIdOfPart(imagePart)

    '    Dim shapeTree = slide.CommonSlideData.ShapeTree

    '    Dim pic As New Picture(
    '    New NonVisualPictureProperties(
    '        New NonVisualDrawingProperties() With {
    '            .Id = nextId,
    '            .Name = "Grid " & nextId
    '        },
    '        New NonVisualPictureDrawingProperties(
    '            New Drawing.PictureLocks() With {
    '                .NoChangeAspect = True
    '            }),
    '        New ApplicationNonVisualDrawingProperties()
    '    ),
    '    New BlipFill(
    '        New Drawing.Blip() With {
    '            .Embed = relId
    '        },
    '        New Drawing.Stretch(
    '            New Drawing.FillRectangle()
    '        )
    '    ),
    '    New ShapeProperties(
    '        New Drawing.Transform2D(
    '            New Drawing.Offset() With {
    '                .X = x,
    '                .Y = y
    '            },
    '            New Drawing.Extents() With {
    '                .Cx = finalWidth,
    '                .Cy = finalHeight
    '            }
    '        ),
    '        New Drawing.PresetGeometry(
    '            New Drawing.AdjustValueList()
    '        ) With {
    '            .Preset = Drawing.ShapeTypeValues.Rectangle
    '        }
    '    )
    ')

    '    shapeTree.AppendChild(pic)

    'End Sub

    Private Sub InsertImageFromStream(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef nextId As Integer, imgStream As MemoryStream, x As Long, y As Long, cx As Long, cy As Long)
        nextId += 1
        Dim imagePart As ImagePart = slidePart.AddImagePart(ImagePartType.Png)
        imgStream.Position = 0
        imagePart.FeedData(imgStream)

        Dim relId As String = slidePart.GetIdOfPart(imagePart)
        Dim shapeTree = slide.CommonSlideData.ShapeTree

        Dim pic As New Picture(
        New NonVisualPictureProperties(
            New NonVisualDrawingProperties() With {.Id = nextId, .Name = "Image " & nextId},
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
                New Drawing.Extents() With {.Cx = cx, .Cy = cy}
            ),
            New Drawing.PresetGeometry() With {.Preset = Drawing.ShapeTypeValues.Rectangle}
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

    Public Sub InsertNewSlide(ByVal presentationPart As PresentationPart, ByVal position As Integer, chBmp As Bitmap)

        ' Create slide with proper structure
        slide = New Slide(New CommonSlideData(New ShapeTree()))
        drawingObjectId = 1

        ' Add required shape tree properties
        Dim shapeTree = slide.CommonSlideData.ShapeTree

        shapeTree.Append(New NonVisualGroupShapeProperties(
        New NonVisualDrawingProperties() With {.Id = 1, .Name = ""},
        New NonVisualGroupShapeDrawingProperties(),
        New ApplicationNonVisualDrawingProperties()))

        shapeTree.Append(New GroupShapeProperties())

        ' Create SlidePart
        slidePart = presentationPart.AddNewPart(Of SlidePart)()
        slidePart.Slide = slide

        ' ✅ IMPORTANT: Attach Slide Layout (THIS WAS MISSING)
        Dim slideMasterPart As SlideMasterPart = presentationPart.SlideMasterParts.First()
        Dim slideLayoutPart As SlideLayoutPart = slideMasterPart.SlideLayoutParts.First()

        slidePart.AddPart(slideLayoutPart)

        ' Save slide
        slide.Save()

        ' Insert into SlideIdList
        Dim slideIdList As SlideIdList = presentationPart.Presentation.SlideIdList

        Dim maxSlideId As UInt32 = 1UI
        Dim prevSlideId As SlideId = Nothing

        For Each sldId As SlideId In slideIdList.ChildElements
            If sldId.Id.Value > maxSlideId Then
                maxSlideId = sldId.Id.Value
            End If

            position -= 1
            If position = 0 Then
                prevSlideId = sldId
            End If
        Next

        maxSlideId += 1

        Dim newSlideId As SlideId

        If prevSlideId IsNot Nothing Then
            newSlideId = slideIdList.InsertAfter(New SlideId(), prevSlideId)
        Else
            newSlideId = slideIdList.AppendChild(New SlideId())
        End If

        newSlideId.Id = maxSlideId
        newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart)

        presentationPart.Presentation.Save()

        ' ✅ Now insert image
        CopyChartBitmapToSlide(slide, slidePart, drawingObjectId, chBmp)

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

    Private Sub InsertKPIComparisonTable(ByRef slide As Slide, ByRef slidePart As SlidePart, ByRef dt As DataTable)

        'Remove unwanted columns from the datatable
        dt.Columns.Remove("Before")
        dt.Columns.Remove("After")
        dt.Columns.Remove("KPIName")
        dt.Columns.Remove("UserName")

        Dim shapeTree = slide.CommonSlideData.ShapeTree

        ' ---------------------------------------------------
        ' TABLE POSITION
        ' ---------------------------------------------------

        Dim slideWidth As Long = 9144000
        Dim slideHeight As Long = 6858000

        Dim margin As Long = 30000

        Dim x As Long = margin
        Dim y As Long = 150000

        Dim cx As Long = slideWidth - (2 * margin)

        Dim cy As Long = slideHeight - 300000

        ' ---------------------------------------------------
        ' CREATE GRAPHIC FRAME
        ' ---------------------------------------------------

        Dim graphicFrame As New GraphicFrame()

        graphicFrame.NonVisualGraphicFrameProperties =
            New NonVisualGraphicFrameProperties(
                New Drawing.NonVisualDrawingProperties() With {
                    .Id = 5000UI,
                    .Name = "KPI Table"
                },
                New Drawing.NonVisualGraphicFrameDrawingProperties(),
                New ApplicationNonVisualDrawingProperties()
            )

        graphicFrame.Transform =
            New Transform(
                New DocumentFormat.OpenXml.Drawing.Offset() With {.X = x, .Y = y},
                New DocumentFormat.OpenXml.Drawing.Extents() With {.Cx = cx, .Cy = cy}
            )

        ' ---------------------------------------------------
        ' TABLE
        ' ---------------------------------------------------

        Dim table As New Drawing.Table()

        ' ---------------------------------------------------
        ' TABLE PROPERTIES
        ' ---------------------------------------------------

        table.AppendChild(
            New Drawing.TableProperties() With {
                .FirstRow = True,
                .BandRow = False
            })

        ' ---------------------------------------------------
        ' TABLE GRID
        ' ---------------------------------------------------

        Dim tblGrid As New Drawing.TableGrid()

        Dim totalTableWidth As Long = slideWidth - (2 * margin)

        Dim baseColumnWidth As Long = totalTableWidth \ 12

        Dim consumedWidth As Long = 0

        For i As Integer = 0 To 10

            tblGrid.Append(New Drawing.GridColumn() With {
            .Width = baseColumnWidth
        })
            consumedWidth += baseColumnWidth
        Next

        tblGrid.Append(New Drawing.GridColumn() With {
        .Width = totalTableWidth - consumedWidth
    })

        table.Append(tblGrid)

        ' ===================================================
        ' HEADER ROW 1 (GROUPS)
        ' ===================================================

        Dim row1 As New Drawing.TableRow() With {
            .Height = 300000
        }

        row1.Append(CreateCell("Avg"))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))

        row1.Append(CreateCell("P10"))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))

        row1.Append(CreateCell("P90"))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))
        row1.Append(CreateCell(""))

        table.Append(row1)

        ' ===================================================
        ' HEADER ROW 2
        ' ===================================================

        Dim row2 As New Drawing.TableRow() With {
            .Height = 300000
        }

        Dim headers() As String = {
            "AVG_Before", "AVG_After", "AVG_Delta", "AVG_%Delta",
            "P10_Before", "P10_After", "P10_Delta", "P10_%Delta",
            "P90_Before", "P90_After", "P90_Delta", "P90_%Delta"
        }

        For Each h In headers
            row2.Append(CreateCell(h, True))
        Next

        table.Append(row2)

        ' ===================================================
        ' DATA ROW
        ' ===================================================

        For Each dr As DataRow In dt.Rows

            Dim dataRow As New Drawing.TableRow() With {
                .Height = 300000
            }

            For Each col As DataColumn In dt.Columns
                dataRow.Append(CreateCell(dr(col.ColumnName).ToString()))
            Next

            table.Append(dataRow)

        Next

        ' ---------------------------------------------------
        ' ADD TABLE TO GRAPHIC
        ' ---------------------------------------------------

        graphicFrame.Graphic =
            New DocumentFormat.OpenXml.Drawing.Graphic(
                New DocumentFormat.OpenXml.Drawing.GraphicData(table) With {
                    .Uri = "http://schemas.openxmlformats.org/drawingml/2006/table"
                }
            )

        shapeTree.Append(graphicFrame)

        slide.Save()
    End Sub

    Private Function CreateCell(text As String, Optional bold As Boolean = False) As Drawing.TableCell
        Dim runProps As New Drawing.RunProperties() With {
            .FontSize = 1200,
            .Bold = bold
        }

        Dim para As New Drawing.Paragraph(New Drawing.Run(runProps, New Drawing.Text(text)))

        Dim txtBody As New Drawing.TextBody(New Drawing.BodyProperties(), New Drawing.ListStyle(), para)

        Dim cell As New Drawing.TableCell()

        cell.Append(txtBody)

        cell.Append(New Drawing.TableCellProperties())

        Return cell
    End Function

#End Region

End Class
