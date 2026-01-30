Imports IOS.Library

Public Class clsIOSImageList

    Public Shared Sub SetImages(ByRef imglist As ImageList, ByVal strTechnology As String)
        Try
            imglist.Images.Add("TAG", EmbeddedImage("icon_tag.JPG"))
            imglist.Images.Add("EMPTY", EmbeddedImage("icon_site_empty.bmp"))

            imglist.Images.Add("CATEGORY", EmbeddedImage("icon_Category1.png"))
            imglist.Images.Add("CHART", EmbeddedImage("icon_chart.jpg"))
            imglist.Images.Add("KPI", EmbeddedImage("icon_Element1.png"))

            imglist.Images.Add("MSS", EmbeddedImage("icon_MSS.jpg"))
            imglist.Images.Add("MME", EmbeddedImage("icon_MME.jpg"))
            imglist.Images.Add("MGW", EmbeddedImage("icon_MGW.jpg"))
            imglist.Images.Add("SGW", EmbeddedImage("icon_SGW.jpg"))
            imglist.Images.Add("TX", EmbeddedImage("btn_Layer_TX1.png"))
            imglist.Images.Add("PGW", EmbeddedImage("icon_PGW.jpg"))
            imglist.Images.Add("SGSN", EmbeddedImage("icon_SGSN.jpg"))
            imglist.Images.Add("GGSN", EmbeddedImage("icon_GGSN.jpg"))
            imglist.Images.Add("SAPC", EmbeddedImage("icon_SAPC.jpg"))

            imglist.Images.Add("MANUFACTURER", EmbeddedImage("icon_site_4gband1.bmp"))
            imglist.Images.Add("MODEL", EmbeddedImage("icon_site_4gband1.bmp"))

            imglist.Images.Add("DCS", EmbeddedImage("icon_site_red.bmp"))
            imglist.Images.Add("DCS1", EmbeddedImage("icon_site_violet.bmp"))
            imglist.Images.Add("EGSM", EmbeddedImage("icon_site_magenta.bmp"))
            imglist.Images.Add("GSM2", EmbeddedImage("icon_site_violet.bmp"))
            imglist.Images.Add("BTS", EmbeddedImage("icon_BTS.jpg"))
            imglist.Images.Add("CELL", EmbeddedImage("icon_cell.bmp"))
            imglist.Images.Add("BSC", EmbeddedImage("icon_BSC.jpg"))
            imglist.Images.Add("BCF", EmbeddedImage("icon_BCF.jpg"))
            imglist.Images.Add("MSC", EmbeddedImage("icon_site_empty.bmp"))

            imglist.Images.Add("Static0", EmbeddedImage("icon_tag.JPG"))
            imglist.Images.Add("Static1", EmbeddedImage("icon_tag.JPG"))
            imglist.Images.Add("CM0", EmbeddedImage("icon_tag.JPG"))
            imglist.Images.Add("CM1", EmbeddedImage("icon_tag.JPG"))
            imglist.Images.Add("WCEL", EmbeddedImage("icon_site_blue.bmp"))
            imglist.Images.Add("WBTS", EmbeddedImage("icon_WBTS.jpg"))
            imglist.Images.Add("UNODEB", EmbeddedImage("icon_WBTS.jpg"))
            imglist.Images.Add("RNC", EmbeddedImage("icon_RNC.jpg"))
            imglist.Images.Add("URNC", EmbeddedImage("icon_RNC.jpg"))
            imglist.Images.Add("VCI", EmbeddedImage("icon_site_empty.bmp"))
            imglist.Images.Add("VPI", EmbeddedImage("icon_site_empty.bmp"))
            imglist.Images.Add("ENODEB", EmbeddedImage("icon_LBTS.jpg"))
            imglist.Images.Add("LBTS", EmbeddedImage("icon_LBTS.jpg"))
            imglist.Images.Add("NRBTS", EmbeddedImage("icon_NRBTS.jpg"))
            imglist.Images.Add("GNODEB", EmbeddedImage("icon_NRBTS.jpg"))

            If (strTechnology.ToLower.Contains("2g")) Then
                imglist.Images.Add("SITE", EmbeddedImage("icon_BCF.jpg"))
            ElseIf (strTechnology.ToLower.Contains("3g")) Then
                imglist.Images.Add("BAND1", EmbeddedImage("icon_site_3gband1.bmp"))
                imglist.Images.Add("BAND2", EmbeddedImage("icon_site_3gband2.bmp"))
                imglist.Images.Add("BAND3", EmbeddedImage("icon_site_3gband3.bmp"))
                imglist.Images.Add("BAND4", EmbeddedImage("icon_site_3gband4.bmp"))
                imglist.Images.Add("BAND5", EmbeddedImage("icon_site_3gband5.bmp"))
                imglist.Images.Add("AP_E16", EmbeddedImage("btn_Layer_Nano3G.png"))
                imglist.Images.Add("AP_E8", EmbeddedImage("btn_Layer_Nano3G.png"))
                imglist.Images.Add("AP_S8", EmbeddedImage("btn_Layer_Nano3G.png"))
                imglist.Images.Add("SITE", EmbeddedImage("icon_WBTS.jpg"))
            ElseIf (strTechnology.ToLower.Contains("4g")) Then
                imglist.Images.Add("BAND1", EmbeddedImage("icon_site_4gband1.bmp"))
                imglist.Images.Add("BAND2", EmbeddedImage("icon_site_4gband2.bmp"))
                imglist.Images.Add("BAND3", EmbeddedImage("icon_site_4gband3.bmp"))
                imglist.Images.Add("BAND4", EmbeddedImage("icon_site_4gband4.bmp"))
                imglist.Images.Add("BAND5", EmbeddedImage("icon_site_4gband5.bmp"))
                imglist.Images.Add("BAND6", EmbeddedImage("icon_site_4gband6.bmp"))
                imglist.Images.Add("BAND7", EmbeddedImage("icon_site_4gband7.bmp"))
                imglist.Images.Add("BAND8", EmbeddedImage("icon_site_4gband8.bmp"))
                imglist.Images.Add("BAND9", EmbeddedImage("icon_site_4gband9.bmp"))
                imglist.Images.Add("SITE", EmbeddedImage("icon_WBTS.jpg"))
            ElseIf (strTechnology.ToLower.Contains("5g")) Then
                imglist.Images.Add("BAND1", EmbeddedImage("icon_site_5gband1.bmp"))
                imglist.Images.Add("BAND2", EmbeddedImage("icon_site_5gband2.bmp"))
                imglist.Images.Add("BAND3", EmbeddedImage("icon_site_5gband3.bmp"))
                imglist.Images.Add("BAND4", EmbeddedImage("icon_site_5gband4.bmp"))
                imglist.Images.Add("BAND5", EmbeddedImage("icon_site_5gband5.bmp"))
                imglist.Images.Add("BAND6", EmbeddedImage("icon_site_5gband6.bmp"))
            ElseIf (strTechnology.ToLower.Contains("core")) Then
            ElseIf (strTechnology.ToLower.Contains("cdr")) Then
                imglist.Images.Add("BAND1", EmbeddedImage("icon_site_3gband1.bmp"))
                imglist.Images.Add("BAND2", EmbeddedImage("icon_site_3gband2.bmp"))
                imglist.Images.Add("BAND3", EmbeddedImage("icon_site_3gband3.bmp"))
                imglist.Images.Add("BAND4", EmbeddedImage("icon_site_3gband4.bmp"))
                imglist.Images.Add("BAND5", EmbeddedImage("icon_site_3gband5.bmp"))
                imglist.Images.Add("SITE", EmbeddedImage("icon_WBTS.jpg"))
            End If
        Catch
        End Try
    End Sub

    Public Shared Function GetKPIImages(ByRef imglistkpi As ImageList) As ImageList
        imglistkpi.Images.Add("CATEGORY", EmbeddedImage("icon_Category1.png"))
        imglistkpi.Images.Add("CHART", EmbeddedImage("icon_chart.jpg"))
        imglistkpi.Images.Add("KPI", EmbeddedImage("icon_Element1.png"))
        Return imglistkpi
    End Function

    Public Shared Function GetTiltTreeImages(ByRef imglistValidation As ImageList) As ImageList
        imglistValidation.Images.Add("WARNING", EmbeddedImage("icon_warning3.png"))
        imglistValidation.Images.Add("BLOCKED", EmbeddedImage("red_box.jpg"))
        Return imglistValidation
    End Function

End Class
