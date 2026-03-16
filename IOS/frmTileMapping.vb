Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports GMap.NET
Imports GMap.NET.MapProviders
Imports GMap.NET.WindowsForms
Imports MapInfo.Geometry
Imports System.Net.NetworkInformation

Public Class frmTileMapping

#Region "Variables"

    Dim currentMarker As GMapMarker = Nothing
    Dim ground As GMapOverlay = Nothing
    Dim objects As GMapOverlay = Nothing
    Dim GoogleCreds As String = Nothing
    Public GoogleMapsIsStatic As Boolean = False
    Public imageZoom As Integer = Nothing
    Public controlZoom As Integer = Nothing
    Public CurrentMapProvider As GMapProvider = Nothing
    Public PanToolUsed As Boolean = False
    Dim MaxDOP As Int16 = 8

#End Region

#Region "Form Event"

    Private Sub frmTileMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = frmMDI
        Me.Size = frmMapWindow.MapControl1.Size
        Me.Location = frmMapWindow.Location

        If objProxy IsNot Nothing Then
            GMapProvider.WebProxy = objProxy ' System.Net.WebRequest.GetSystemWebProxy
            GMapProvider.TimeoutMs = 5000
        End If

		'GoogleAPI
		Dim toencrypt As String = ""

		Try
			Dim encrypter As Aes256Base64Encrypter = New Aes256Base64Encrypter
			Dim GoogleKey As String = GetConfigClientKeyValue("GoogleKey")
            GoogleCreds = encrypter.Decrypt(GoogleKey, "c3lls3ns")
        Catch ex As Exception
			GoogleCreds = Nothing
		End Try

        'Dim internetavailable As Boolean = True
        'Try
        '    'check internet connection
        '    If bInternetAvailable = False Then
        '        frmMapWindow.SetStatus("Tileserver Unreachable, check Internet connection! Switching to Local Cache...")
        '        frmMapWindow.Map_StatusStrip.Visible = False
        '        frmMapWindow.ToolStripProgressBar1.Visible = False
        '        internetavailable = False
        '    End If
        'Catch ex As Exception
        '    frmMapWindow.SetStatus("Tileserver Unreachable, check Internet connection! Switching to Local Cache...")
        '    frmMapWindow.Map_StatusStrip.Visible = False
        '    frmMapWindow.ToolStripProgressBar1.Visible = False
        '    internetavailable = False
        'End Try

        Try
            GoogleMapsIsStatic = GetConfigClientKeyValue("GoogleMapsIsStatic")
            'Dim GoogleMapsIsStaticValue As String = System.Configuration.ConfigurationManager.AppSettings("GoogleMapsIsStatic").ToString
            'If GoogleMapsIsStaticValue = "1" Then
            '    GoogleMapsIsStatic = True
            'Else
            '    GoogleMapsIsStatic = False
            'End If
        Catch ex As Exception

        End Try


        'SECONDARY CACHE PROVIDER FOR TILESERVERS
        Dim GMAPCacheSecondaryServer As String = GetConfigClientKeyValue("GMAPCacheConnString")

        Dim cacheMSSQL As GMap.NET.CacheProviders.MsSQLPureImageCache = Nothing

        If Not GMAPCacheSecondaryServer Is Nothing AndAlso GMAPCacheSecondaryServer.Trim <> "" Then
            cacheMSSQL = New CacheProviders.MsSQLPureImageCache
            cacheMSSQL.ConnectionString = GMAPCacheSecondaryServer
            GMapControl1.Manager.SecondaryCache = cacheMSSQL
        End If


        With GMapControl1.Manager
            .UseRouteCache = True
            .UseGeocoderCache = True
            .UsePlacemarkCache = True
            .UseMemoryCache = True
            'If internetavailable = True Then
            '    .Mode = AccessMode.ServerAndCache
            'Else
            '    .Mode = AccessMode.CacheOnly
            'End If
            .Mode = AccessMode.ServerOnly
        End With

		With GMapControl1
			.MaxZoom = 33
			.MinZoom = 2
            .Zoom = 7
            '    .MapProvider = GMapProviders.GoogleMap
            .ShowTileGridLines = False
			.MapScaleInfoEnabled = False
			.CacheLocation = GetUserDataPath() & "\Cache\"

		End With
		Me.GMapControl1.CacheLocation = GetUserDataPath() & "\Cache\"
    End Sub

#End Region

#Region "Helper"

    Public Shared Function CheckForInternetConnection() As Boolean
        Try
            Return My.Computer.Network.Ping("www.google.com", 1000)
        Catch
            Return False
        End Try
    End Function

	Public Function CheckURIAddress(Optional ByVal URL As String = "https://maps.googleapis.com") As Boolean
        Return True
        Try
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Start")

			Dim channel As String = "&channel=" + IOS.Configuration.IOSAppConfigManage.DeploymentName

            Dim apiKey = GoogleCreds  '.Split(";")(0).Substring(3)
            'Dim privkey As String = GoogleCreds.Split(";")(1).Substring(4)
            Dim remoteURI = "https://maps.googleapis.com/maps/api/staticmap?center=52.0843912898369,4.28742359812703&zoom=10&size=100x100&style=feature:all|element:labels|visibility:on&key=" & apiKey & channel
            URL = remoteURI 'GoogleSign(remoteURI, privkey)

            ' OLD not working URL = "https://maps.googleapis.com/maps/api/staticmap?center=52.0843912898369,4.28742359812703"

            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(URL)
            _logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "WebRequest Create")

			If objProxy IsNot Nothing Then
				request.Proxy = objProxy
				_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "WebRequest WebProxy Assign to request")
			End If
            request.Timeout = 3000
            request.Method = "GET"
            request.AllowAutoRedirect = False
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.83 Safari/537.1"
            request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5"
            request.Headers.Add("Accept-Language", "en-us,en;q=0.5")
            request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7")
            request.Headers.Add("Keep-Alive", "3000")
            Dim response As System.Net.WebResponse = request.GetResponse()
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "WebRequest Response Complete")

		Catch ex As System.Net.WebException
			Select Case ex.Status
				Case Net.WebExceptionStatus.Timeout
					_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & ex.Message)
					Return False
				Case Net.WebExceptionStatus.ProtocolError
					If DirectCast(ex.Response, System.Net.HttpWebResponse).StatusCode = 400 Then
						Return True
					Else
						_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & ex.Message)
						Return False
					End If
				Case Else
					_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & ex.Message)
					Return False
			End Select
		Catch ex1 As Exception
			_logger.SetError(System.Reflection.MethodBase.GetCurrentMethod().Name & " - " & "Failed - " & ex1.Message)
			Return False
		End Try
		Return True
	End Function

	Public Sub FocusMapOnPoint(ByVal pt As PointLatLng, ByVal rect As RectLatLng)
        If Not PanToolUsed Then
            GMapControl1.SetZoomToFitRect(rect)
        End If
        PanToolUsed = False
        GMapControl1.Position = pt
    End Sub

    Public Sub TileMapping_GeoCoder(ByVal strsearch As String)
		'Dim status As GeoCoderStatusCode = GeoCoderStatusCode.Unknow
		Dim posLat As Double = Nothing
		Dim posLng As Double = Nothing
		Dim Status As String = ""
        Try
            Dim apiKey = GoogleCreds '.Split(";")(0).Substring(3)
            'Dim privkey As String = GoogleCreds.Split(";")(1).Substring(4)

            Dim remoteURI = "https://maps.googleapis.com/maps/api/geocode/xml?address=" & Replace(strsearch, " ", "+") & "&key=" & apiKey
            'remoteURI = GoogleSign(remoteURI, privkey)

            Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(remoteURI)
            request.Timeout = 2000
            If objProxy IsNot Nothing Then
                request.Proxy = objProxy
            End If

            Using respone As System.Net.WebResponse = request.GetResponse()
                Using dataStream As Stream = respone.GetResponseStream()

                    Dim doc As New XmlDocument
                    doc.XmlResolver = Nothing
                    doc.Load(dataStream)

                    Dim nodes As XmlNodeList
                    Dim statusNode As XmlNode

                    statusNode = doc.SelectSingleNode("//GeocodeResponse/status")
                    Status = statusNode.InnerText

                    nodes = doc.SelectNodes("//GeocodeResponse/result/geometry")

                    For Each nd As XmlNode In nodes
                        Dim location As String = Nothing
                        Dim elevation As String = Nothing
                        For Each chnd As XmlNode In nd.ChildNodes
                            If chnd.Name = "location" Then
                                posLat = CDbl(chnd.ChildNodes(0).InnerText)
                                posLng = CDbl(chnd.ChildNodes(1).InnerText)

                                If objFrmSvcCheck IsNot Nothing Then
                                    objFrmSvcCheck.posLat = posLat
                                    objFrmSvcCheck.posLng = posLng
                                End If

                            End If
                        Next
                    Next
                    doc = Nothing
                End Using
            End Using

        Catch ex As Exception
            frmMapWindow.SetStatus("Error Reaching Google!")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try

        If Status <> "OK" Then
            MsgBox("Google Maps Geocoder can't find: '" + strsearch & "  Msg: " & Status.ToString, MessageBoxButtons.OK)
        Else
            If GMapControl1.Enabled = True And TileMenu_Status(frmMapWindow.tlb_Layer_GM) <> "None" Then
                TileMap_Bounds(GMapControl1.ViewArea.Lng, GMapControl1.ViewArea.Lat, GMapControl1.ViewArea.Right, GMapControl1.ViewArea.Bottom)
            End If
            frmMapWindow.Location_Map(strsearch, posLng, posLat)
        End If
    End Sub

    Public Function TileMapping_GeoProfile(ByVal p() As DPoint, ByVal totaldist As Double) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("Location", GetType(System.String))
        dt.Columns.Add("Elevation", GetType(System.Double))

        If bInternetAvailable = False Then
            frmMapWindow.SetStatus("No Internet connection available!")
            'Return Nothing
            If dt.Rows.Count = 0 Then
                'adding dummy data
                For i = 0 To 499
                    Dim nr As DataRow = dt.NewRow
                    Dim newX As Double = p(0).x + (p(1).x - p(0).x) * i / 499
                    Dim newy As Double = p(0).y + (p(1).y - p(0).y) * i / 499

                    nr("location") = newy.ToString & ";" & newX.ToString
                    nr("elevation") = 0
                    dt.Rows.Add(nr)
                Next
            End If
            Return dt
        End If

        Try
            '       Dim pol As System.Net.Cache.RequestCachePolicy = New System.Net.Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.Reload)
            '       Dim myWebClient As New MyCustomWebClient(3000) 'System.Net.WebClient




            '     myWebClient.CachePolicy = pol
            Dim pntstring As String = Nothing
            For Each pnt As DPoint In p
                pntstring = pntstring & pnt.y & "," & pnt.x & "|"
            Next
            pntstring = pntstring.TrimEnd("|")

            Dim apikey = GoogleCreds  '.Split(";")(0).Substring(3)
            'Dim privkey As String = GoogleCreds.Split(";")(1).Substring(4)

            Dim remoteURI = "https://maps.googleapis.com/maps/api/elevation/xml?path=" & pntstring & "&samples=" & Math.Min(500, (Math.Round(totaldist * 1000 / 10, 0))) & "&key=" & apikey    'samples = 10 meter resolution
            'remoteURI = GoogleSign(remoteURI, privkey)

            'new code for mwingz
            Dim request As System.Net.HttpWebRequest = CType(System.Net.WebRequest.Create(remoteURI), System.Net.HttpWebRequest)
            request.Method = "GET"
            request.UserAgent = "CellSens/1.0 (contact: support@cellsens.com)" ' Customize this
            request.Accept = "application/xml"
            request.Referer = "cellsens" ' Optional: Set to your app's domain
            request.Timeout = 5000 ' Set timeout to 10 seconds
            request.KeepAlive = False ' Disable keep-alive for simplicity
            request.CachePolicy = New System.Net.Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.Reload)

            If objProxy IsNot Nothing Then
                request.Proxy = objProxy
            End If


            Dim Doc As New XmlDocument()


            Using response As System.Net.HttpWebResponse = CType(request.GetResponse(), System.Net.HttpWebResponse)
                If response.StatusCode = System.Net.HttpStatusCode.OK Then
                    ' Read the response stream
                    Using stream As Stream = response.GetResponseStream()
                        ' Load the XML response directly from the stream
                        Doc.Load(stream)

                    End Using
                Else
                    frmMapWindow.SetStatus($"Error: HTTP Status Code {response.StatusCode}")
                    UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", $"Error: HTTP Status Code {response.StatusCode}")
                End If
            End Using


            '  OLD   Dim file As New System.IO.StreamReader(myWebClient.OpenRead(remoteURI))
            '  OLD   Dim doc As New XmlDocument
            Doc.XmlResolver = Nothing
            '  OLD   doc.Load(file)

            Dim nodes As XmlNodeList

            nodes = doc.SelectNodes("//ElevationResponse/result")

            For Each nd As XmlNode In nodes
                Dim location As String = Nothing
                Dim elevation As String = Nothing
                For Each chnd As XmlNode In nd.ChildNodes
                    If chnd.Name = "location" Then
                        location = chnd.ChildNodes(0).InnerText & ";" & chnd.ChildNodes(1).InnerText
                    ElseIf chnd.Name = "elevation" Then
                        elevation = CDbl(chnd.InnerText)
                    End If
                Next

                If Not location Is Nothing And Not elevation Is Nothing Then
                    Dim nr As DataRow = dt.NewRow
                    nr("location") = location
                    nr("elevation") = elevation
                    dt.Rows.Add(nr)
                End If
            Next
        Catch ex As Exception
            frmMapWindow.SetStatus("Google API Timed Out, So Dummy Data Drawn")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
        If dt.Rows.Count = 0 Then
            'adding dummy data
            For i = 0 To 499
                Dim nr As DataRow = dt.NewRow
                Dim newX As Double = p(0).x + (p(1).x - p(0).x) * i / 499
                Dim newy As Double = p(0).y + (p(1).y - p(0).y) * i / 499

                nr("location") = newy.ToString & ";" & newX.ToString
                nr("elevation") = 0
                dt.Rows.Add(nr)
            Next
        End If
        Return dt
    End Function

    Public Sub TileMapping_GeoStaticMap(ByVal pt As PointLatLng, ByVal proj As PureProjection, ByVal Area As RectLatLng, ByVal zoom As Integer)
        Try
            Application.UseWaitCursor = True
            Application.DoEvents()
            Dim pol As System.Net.Cache.RequestCachePolicy = New System.Net.Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.Reload)
            Dim myWebClient As New System.Net.WebClient 'the webclient
            If objProxy IsNot Nothing Then
                myWebClient.Proxy = objProxy
            End If

            myWebClient.CachePolicy = pol
            Dim bigImage As String = GetUserDataPath() + "\GMap-Image.png"

            'current(Area)
            Dim topLeftPx As Global.GMap.NET.GPoint = proj.FromLatLngToPixel(Area.LocationTopLeft, zoom)
            Dim rightBottomPx As Global.GMap.NET.GPoint = proj.FromLatLngToPixel(Area.LocationRightBottom, zoom)
            Dim PxDelta As Global.GMap.NET.GPoint = New Global.GMap.NET.GPoint(rightBottomPx.X - topLeftPx.X, rightBottomPx.Y - topLeftPx.Y)

            Dim maptypespecial As String = ""
            If CurrentMapProvider.Name.Contains("Satellite") Then
                maptypespecial = "maptype=satellite&"
            End If
            If CurrentMapProvider.Name.Contains("Terrain") Then
                maptypespecial = "maptype=terrain&"
            End If

            Dim channel As String = "&channel=" + IOS.Configuration.IOSAppConfigManage.DeploymentName
            'myWebClient.Headers.Add("referer", "cellsens")

            Dim apiKey = GoogleCreds  '.Split(";")(0).Substring(3)
            'Dim privkey As String = GoogleCreds.Split(";")(1).Substring(4)
            Dim remoteURI = "https://maps.googleapis.com/maps/api/staticmap?" & maptypespecial & "center=" & pt.Lat & "," & pt.Lng & "&zoom=" & zoom & "&size=" & PxDelta.X & "x" & PxDelta.Y & "&style=feature:all|element:labels|visibility:on&key=" & apiKey & channel
            'remoteURI = GoogleSign(remoteURI, privkey)

            If Not CType(frmMapWindow.tlb_Layer_GM.DropDownItems(0), ToolStripMenuItem).Checked = True Then
                Dim request As System.Net.WebRequest = System.Net.WebRequest.Create(remoteURI)
                request.Timeout = 3000
                If objProxy IsNot Nothing Then
                    request.Proxy = objProxy
                End If

                ' request.Headers.Add("referer", "cellsens")
                Dim httpRequest As System.Net.HttpWebRequest = CType(request, System.Net.HttpWebRequest)
                httpRequest.Referer = "cellsens"

                Using respone As System.Net.HttpWebResponse = httpRequest.GetResponse()
                    Using dataStream As Stream = respone.GetResponseStream()
                        Dim data As New MemoryStream
                        dataStream.CopyTo(data)
                        Using FStream As New FileStream(bigImage, FileMode.Create)
                            data.WriteTo(FStream)
                        End Using
                    End Using
                End Using

                ''   Dim RdByte As Byte() = myWebClient.DownloadData(remoteURI)
                'Using FStream As New FileStream(bigImage, FileMode.Create)
                '    FStream.Write(RdByte, 0, RdByte.Length)
                'End Using
            End If

            imageZoom = zoom
            PageRequestCount = PageRequestCount + 1

            Application.UseWaitCursor = False
            Application.DoEvents()
        Catch ex As Exception
            frmMapWindow.SetStatus("Error Reaching Google!")
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
            reloadGoogleMaps = False
        End Try

        Application.UseWaitCursor = False
        Application.DoEvents()

        frmMapWindow.Mapcontrol_ActivateMapViewChange(False)
        TileMap_PushToMapxtreme(True)
        frmMapWindow.ToolStripProgressBar1.Visible = False
    End Sub

    Public Function TileMapping_GeoElevationPoint(ByVal p As DPoint) As Double
        Dim dt As New DataTable

        If bInternetAvailable = False Then
            frmMapWindow.SetStatus("No Internet connection available!")
            Return -9999
        End If

        Try
            Dim pol As System.Net.Cache.RequestCachePolicy = New System.Net.Cache.RequestCachePolicy(Net.Cache.RequestCacheLevel.Reload)
            Dim myWebClient As New MyCustomWebClient(3000) 'System.Net.WebClient 
            If objProxy IsNot Nothing Then
                myWebClient.Proxy = objProxy ' System.Net.WebRequest.GetSystemWebProxy
            End If

            myWebClient.CachePolicy = pol
            Dim pntstring As String = Nothing
            pntstring = p.y & "," & p.x

            Dim apiKey = GoogleCreds  '.Split(";")(0).Substring(3)
            'Dim privkey As String = GoogleCreds.Split(";")(1).Substring(4)

            Dim remoteURI = "https://maps.googleapis.com/maps/api/elevation/xml?locations=" & pntstring & "&key=" & apiKey
            'remoteURI = GoogleSign(remoteURI, privkey)

            Dim file As New System.IO.StreamReader(myWebClient.OpenRead(remoteURI))

            Dim doc As New XmlDocument
            doc.XmlResolver = Nothing
            doc.Load(file)

            Dim nodes As XmlNodeList

            nodes = doc.SelectNodes("//ElevationResponse/result")
            dt.Columns.Add("Location", GetType(System.String))
            dt.Columns.Add("Elevation", GetType(System.Double))

            For Each nd As XmlNode In nodes
                Dim location As String = Nothing
                Dim elevation As String = Nothing
                For Each chnd As XmlNode In nd.ChildNodes
                    If chnd.Name = "location" Then
                        location = chnd.ChildNodes(0).InnerText & ";" & chnd.ChildNodes(1).InnerText
                    ElseIf chnd.Name = "elevation" Then
                        elevation = CDbl(chnd.InnerText)
                    End If
                Next

                If Not elevation Is Nothing Then
                    doc = Nothing
                    file.Close()
                    file.Dispose()

                    Return elevation
                End If
            Next
        Catch ex As Exception

        End Try
        Return 0
    End Function

    Public Function GoogleSign(ByVal url As String, ByVal keyString As String) As String
        Dim encoding As ASCIIEncoding = New ASCIIEncoding()

        'URL-safe decoding
        Dim privateKeyBytes As Byte() = Convert.FromBase64String(keyString.Replace("-", "+").Replace("_", "/"))

        Dim objURI As Uri = New Uri(url)
        Dim encodedPathAndQueryBytes As Byte() = encoding.GetBytes(objURI.LocalPath & objURI.Query)

        'compute the hash
        Dim algorithm As HMACSHA1 = New HMACSHA1(privateKeyBytes)
        Dim hash As Byte() = algorithm.ComputeHash(encodedPathAndQueryBytes)

        'convert the bytes to string and make url-safe by replacing '+' and '/' characters
        Dim signature As String = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_")

        'Add the signature to the existing URI.
        Return objURI.Scheme & "://" & objURI.Host & objURI.LocalPath & objURI.Query & "&signature=" & signature
    End Function

    Public Sub TileMapping_None()
        'remove table from map
        Mapcontrol_CloseTable("IOS_TileMap")
        'set 0-pppjh0= event to disable gmap tracking
        GMapControl1.Enabled = False
        frmMapWindow.Mapcontrol_ActivateMapViewChange(False)
    End Sub

    Public Sub TileMapping_On(ByVal mt As GMapProvider)
        Try
            CurrentMapProvider = mt


            If CurrentMapProvider.Name.ToString.Contains("KPN") Then
                MaxDOP = 1
            Else
                MaxDOP = 8
            End If


            'closing of existing raster tables
            'If IsInternetAvailable() = False Then 'CheckURIAddress() = False Then
            '    frmMapWindow.SetStatus("No Internet connection available!")
            '    Exit Sub
            'End If

            'set GMapcontrol1 to maptype
            GMapControl1.Enabled = True
            If mt.Name.Contains("OpenStreet") Then
                MapProviders.GMapProvider.UserAgent = "CellSens"
            End If

            If frmMapWindow.StaticAndGoogle = False Then
                With GMapControl1
                    If mt.MaxZoom Is Nothing Then
                        .MaxZoom = 20
                    Else
                        .MaxZoom = mt.MaxZoom
                    End If

                    .MinZoom = mt.MinZoom
                    '     .Zoom = 7
                    .MapProvider = mt
                    .ShowTileGridLines = False
                    .MapScaleInfoEnabled = False
                    '   .CacheLocation = GetUserDataPath() & "\Cache\"
                End With
            End If


            'GMapControl1.MapProvider = mt
            'GMapControl1.MaxZoom = 33

            frmMapWindow.MapControl1.Invalidate()
            'set GMapcontrol1 to view of mapcontrol

            Dim pt1 As DPoint = frmMapWindow.TransformPointToWGS84(frmMapWindow.MapControl1.Map.Bounds.DefiningPoints(0))
            Dim pt2 As DPoint = frmMapWindow.TransformPointToWGS84(frmMapWindow.MapControl1.Map.Bounds.DefiningPoints(1))
            Dim rct As RectLatLng = New RectLatLng(pt1.y, pt1.x, pt2.x - pt1.x, pt2.y - pt1.y)
            FocusMapOnPoint(New PointLatLng(frmMapWindow.TransformPointToWGS84(frmMapWindow.MapControl1.Map.Center).y, frmMapWindow.TransformPointToWGS84(frmMapWindow.MapControl1.Map.Center).x), rct)
            Application.DoEvents()

            'launch getimage & push to mapxtreme
            TileMap_Update()
            frmMapWindow.MapControl1.Update()

            'set mapvieweventchanged parameter to on
            frmMapWindow.Mapcontrol_ActivateMapViewChange(True)
            Me.Visible = True
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Public Sub TileMap_Update()
        Try
            'If IsInternetAvailable() = False Then
            '    frmMapWindow.SetStatus("No Internet connection available!")
            '    Exit Sub
            'End If
            If frmMapWindow.StaticAndGoogle Then
                If reloadGoogleMaps = True Then
                    TileMapping_GeoStaticMap(GMapControl1.Position, CurrentMapProvider.Projection, GMapControl1.ViewArea, GMapControl1.Zoom)
                End If
            Else
                TileMap_GetImage(GMapControl1.MapProvider, GMapControl1.Position, GMapControl1.ViewArea, GMapControl1.MapProvider.Projection, GMapControl1.Zoom)
            End If
        Catch ex As Exception
            UserActionTracking(System.Reflection.MethodBase.GetCurrentMethod().Name, "Error", ex.Message)
        End Try
    End Sub

    Private Sub TileMap_GetImage(ByVal MapTypeChoosen As GMapProvider, ByVal pt As PointLatLng, ByVal Area As RectLatLng, ByVal proj As PureProjection, ByVal zoom As Integer)
        Dim bigImage As String = GetUserDataPath() + "\GMap-Image.png"
        Dim tileArea As List(Of Global.GMap.NET.GPoint) = New List(Of Global.GMap.NET.GPoint)

        'defining tilearea
        tileArea.Clear()
        tileArea.AddRange(GMapControl1.MapProvider.Projection.GetAreaTileList(Area, zoom, 1))
        tileArea.TrimExcess()

        'current(Area)
        Dim topLeftPx As Global.GMap.NET.GPoint = proj.FromLatLngToPixel(Area.LocationTopLeft, zoom)
        Dim rightBottomPx As Global.GMap.NET.GPoint = proj.FromLatLngToPixel(Area.LocationRightBottom, zoom)
        Dim PxDelta As Global.GMap.NET.GPoint = New Global.GMap.NET.GPoint(rightBottomPx.X - topLeftPx.X, rightBottomPx.Y - topLeftPx.Y)

        Dim padding As Integer = 22

        'frmMapWindow.Map_StatusStrip.Visible = True
        frmMapWindow.ToolStripProgressBar1.Visible = True



        Try
            Using bmpDestination As Bitmap = New Bitmap(CInt(PxDelta.X + padding * 2), CInt(PxDelta.Y + padding * 2))
                Using gfx As Graphics = Graphics.FromImage(bmpDestination)

                    gfx.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                    gfx.CompositingMode = Drawing2D.CompositingMode.SourceOver
                    gfx.Clear(Color.White)
                    Dim i As Integer = 0

                    Dim po As New ParallelOptions
                    po.MaxDegreeOfParallelism = MaxDOP '  Convert.ToInt32(Math.Ceiling((Environment.ProcessorCount * 0.6) * 2.0))


                    '  WriteString_Log("Time:" & Now().ToString & " - TileArea: " & tileArea.Count)


                    Parallel.ForEach(tileArea, po, Sub(p)
                                                       Dim tp As GMapProvider = MapTypeChoosen
                                                       Dim ex As Exception = Nothing
                                                       Dim tile As WindowsForms.GMapImage = CType(GMaps.Instance.GetImageFrom(tp, p, zoom, ex), WindowsForms.GMapImage)
                                                       If Not ex Is Nothing Then
                                                           WriteString_Log("Tile Error: " & p.ToString & " " & ex.Message)
                                                       End If

                                                       If Not tile Is Nothing Then
                                                           Using tile
                                                               Dim x As Integer = p.X * proj.TileSize.Width - topLeftPx.X + padding
                                                               Dim y As Integer = p.Y * proj.TileSize.Width - topLeftPx.Y + padding
                                                               SyncLock gfx
                                                                   'WriteString_Log("Time" & Now().ToString & " - TileXY DrawImage: " & x.ToString & " " & y.ToString)
                                                                   gfx.DrawImage(tile.Img, x, y, proj.TileSize.Width, proj.TileSize.Height)
                                                               End SyncLock
                                                           End Using
                                                       End If
                                                   End Sub)


                    frmMapWindow.ToolStripProgressBar1.Value = 100

                    'get tiles & combine into one
                    'For Each p As Global.GMap.NET.GPoint In tileArea
                    '                                   Dim tp As GMapProvider = MapTypeChoosen
                    '                                   Dim ex As Exception = Nothing
                    '                                   Dim tile As WindowsForms.GMapImage = CType(GMaps.Instance.GetImageFrom(tp, p, zoom, ex), WindowsForms.GMapImage)
                    '                                   If Not tile Is Nothing Then
                    '                                       Using tile
                    '                                           Dim x As Integer = p.X * proj.TileSize.Width - topLeftPx.X + padding
                    '                                           Dim y As Integer = p.Y * proj.TileSize.Width - topLeftPx.Y + padding
                    '                                           gfx.DrawImage(tile.Img, x, y, proj.TileSize.Width, proj.TileSize.Height)
                    '                                       End Using
                    '                                   End If
                    '                                   i = i + 1
                    '                                   frmMapWindow.ToolStripProgressBar1.Value = Math.Round(i / tileArea.Count * 100, 0)
                    '                               Next

                End Using

                'WriteString_Log("Time" & Now().ToString & " - SaveImage")

                bmpDestination.Save(bigImage, Imaging.ImageFormat.Png)
            End Using
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
        End Try
        frmMapWindow.Mapcontrol_ActivateMapViewChange(False)
        TileMap_PushToMapxtreme()
        frmMapWindow.ToolStripProgressBar1.Visible = False
        frmMapWindow.Map_StatusStrip.Visible = False
    End Sub

    Private Sub TileMap_PushToMapxtreme(Optional ByVal IsStatic As Boolean = False)
        Try
            If GMapControl1.Zoom = imageZoom Or IsStatic = False Then
                Dim MyLayer As MapInfo.Mapping.FeatureLayer = Nothing
                Dim fsm As MapInfo.Mapping.FeatureOverrideStyleModifier = Nothing
                Mapcontrol_CloseTable("IOS_TileMap")
                Dim bmwidth As Integer = GMapControl1.Width
                Dim bmheight As Integer = GMapControl1.Height

                Dim bmppoints(4) As MapInfo.Geometry.DPoint

                If IsStatic = False Then
                    bmppoints(0) = New MapInfo.Geometry.DPoint(22, 22)
                    bmppoints(1) = New MapInfo.Geometry.DPoint(22, bmheight + 22)
                    bmppoints(2) = New MapInfo.Geometry.DPoint(bmwidth + 22, 22)
                    bmppoints(3) = New MapInfo.Geometry.DPoint(bmwidth + 22, bmheight + 22)
                Else
                    bmppoints(0) = New MapInfo.Geometry.DPoint(0, 0)
                    bmppoints(1) = New MapInfo.Geometry.DPoint(0, bmheight + 0)
                    bmppoints(2) = New MapInfo.Geometry.DPoint(bmwidth + 0, 0)
                    bmppoints(3) = New MapInfo.Geometry.DPoint(bmwidth + 0, bmheight + 0)
                End If

                Dim bnds As MapInfo.Geometry.DRect = New MapInfo.Geometry.DRect(GMapControl1.ViewArea.LocationTopLeft.Lng, GMapControl1.ViewArea.LocationTopLeft.Lat, GMapControl1.ViewArea.LocationRightBottom.Lng, GMapControl1.ViewArea.LocationRightBottom.Lat)
                Dim bmprealpoints(4) As MapInfo.Geometry.DPoint
                bmprealpoints(0) = frmMapWindow.TransformPointToWGS84(New MapInfo.Geometry.DPoint(bnds.x1, bnds.y2))
                bmprealpoints(1) = frmMapWindow.TransformPointToWGS84(New MapInfo.Geometry.DPoint(bnds.x1, bnds.y1))
                bmprealpoints(2) = frmMapWindow.TransformPointToWGS84(New MapInfo.Geometry.DPoint(bnds.x2, bnds.y2))
                bmprealpoints(3) = frmMapWindow.TransformPointToWGS84(New MapInfo.Geometry.DPoint(bnds.x2, bnds.y1))

                If Not CType(frmMapWindow.tlb_Layer_GM.DropDownItems(0), ToolStripMenuItem).Checked = True Then
                    createTabRaster(GetUserDataPath() + "\IOS_TileMap.tab", bmppoints, bmprealpoints)
                    RemoveHandler frmMapWindow.MapControl1.Map.ViewChangedEvent, AddressOf frmMapWindow.MapControl_MapViewChanged
                    frmMapWindow.Mapcontrol_LoadRaster(GetUserDataPath() + "\IOS_TileMap.tab", fsm)
                    frmMapWindow.MapControl1.Map.SetView(bnds, csysWGS84)
                    AddHandler frmMapWindow.MapControl1.Map.ViewChangedEvent, AddressOf frmMapWindow.MapControl_MapViewChanged
                End If
            Else
                TileMap_Update()
            End If
        Catch ex As Exception
            Console.WriteLine("PushToMapxtreme: " & ex.Message.ToString)
        End Try
    End Sub

    Private Sub createTabRaster(ByVal fn As String, ByVal pbmp() As DPoint, ByVal preal() As DPoint)
        If File.Exists(fn) Then
            File.Delete(fn)
        End If
        ' Displays the same value with a blank as the separator.
        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        nfi.NumberDecimalSeparator = "."
        nfi.NumberDecimalDigits = 8
        Using sw As StreamWriter = File.CreateText(fn)
            sw.WriteLine("!table")
            sw.WriteLine("!version 300")
            sw.WriteLine("!charset WindowsLatin1 ")
            sw.WriteLine("")
            sw.WriteLine("Definition Table")
            sw.WriteLine("  File " & Chr(34) & "GMap-Image.png" & Chr(34))
            sw.WriteLine("  Type " & Chr(34) & "RASTER" & Chr(34))
            sw.WriteLine("  (" & preal(0).x.ToString("N", nfi) & "," & preal(0).y.ToString("N", nfi) & ") (" & pbmp(0).x.ToString & "," & pbmp(0).y.ToString & ") Label " & Chr(34) & "Pt 1" & Chr(34) & ",")
            sw.WriteLine("  (" & preal(1).x.ToString("N", nfi) & "," & preal(1).y.ToString("N", nfi) & ") (" & pbmp(1).x.ToString & "," & pbmp(1).y.ToString & ") Label " & Chr(34) & "Pt 2" & Chr(34) & ",")
            sw.WriteLine("  (" & preal(2).x.ToString("N", nfi) & "," & preal(2).y.ToString("N", nfi) & ") (" & pbmp(2).x.ToString & "," & pbmp(2).y.ToString & ") Label " & Chr(34) & "Pt 3" & Chr(34) & ",")
            sw.WriteLine("  (" & preal(3).x.ToString("N", nfi) & "," & preal(3).y.ToString("N", nfi) & ") (" & pbmp(3).x.ToString & "," & pbmp(3).y.ToString & ") Label " & Chr(34) & "Pt 4" & Chr(34))
            sw.WriteLine("  CoordSys Earth Projection 1, 157")
            sw.WriteLine("  Units " & Chr(34) & "degree" & Chr(34))
            sw.Close()
        End Using
    End Sub

#End Region

End Class

Public Class Aes256Base64Encrypter

    Dim unicode As UnicodeEncoding = New UnicodeEncoding

    Public Function Decrypt(ByVal encryptedText As String, ByVal secretKey As String) As String
        Dim plainText As String = Nothing
        Using inputStream As MemoryStream = New MemoryStream(System.Convert.FromBase64String(encryptedText))
            Dim algorithm As RijndaelManaged = getAlgorithm(secretKey)
            Using cryptoStream As CryptoStream = New CryptoStream(inputStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read)
                Dim outputBuffer(0 To CType(inputStream.Length - 1, Integer)) As Byte
                Dim readBytes As Integer = cryptoStream.Read(outputBuffer, 0, CType(inputStream.Length, Integer))
                plainText = unicode.GetString(outputBuffer, 0, readBytes)
            End Using
        End Using
        Return plainText
    End Function

    Public Function Encrypt(ByVal plainText As String, ByVal secretKey As String) As String
        Dim encryptedPassword As String = Nothing
        Using outputStream As MemoryStream = New MemoryStream()
            Dim algorithm As RijndaelManaged = getAlgorithm(secretKey)
            Using cryptoStream As CryptoStream = New CryptoStream(outputStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write)
                Dim inputBuffer() As Byte = unicode.GetBytes(plainText)
                cryptoStream.Write(inputBuffer, 0, inputBuffer.Length)
                cryptoStream.FlushFinalBlock()
                encryptedPassword = System.Convert.ToBase64String(outputStream.ToArray())
            End Using
        End Using
        Return encryptedPassword
    End Function

    Private Function getAlgorithm(ByVal secretKey As String) As RijndaelManaged
        Const salt As String = "put a salt key here"
        Const keySize As Integer = 256
        Dim keyBuilder As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(secretKey, unicode.GetBytes(salt))
        Dim algorithm As RijndaelManaged = New RijndaelManaged()
        algorithm.KeySize = keySize
        algorithm.IV = keyBuilder.GetBytes(CType(algorithm.BlockSize / 8, Integer))
        algorithm.Key = keyBuilder.GetBytes(CType(algorithm.KeySize / 8, Integer))
        algorithm.Padding = PaddingMode.PKCS7
        Return algorithm
    End Function

End Class
