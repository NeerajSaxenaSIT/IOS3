Imports System
Imports System.Collections
Imports MapInfo.Tools
Imports MapInfo.Geometry
Imports System.Windows.Forms

Imports MapInfo.Windows.Controls

Namespace MI.PSG.Windows.Tools.CustomTools

    Public Class PSGDistanceToolEventArgs
        Inherits EventArgs

        'Internal list of coordinates to keep track of the points submitted by the user
        Private _linePoints() As MapInfo.Geometry.DPoint
        'Internal logical variable to determine if drawing is finished
        Private _isFinished As Boolean
        'Internal variable to keep track of the coordsys used for the _linePoints
        Private _coordSys As MapInfo.Geometry.CoordSys

        Public Sub New(ByVal isFinished As Boolean, ByVal dPoints() As MapInfo.Geometry.DPoint, ByVal coordSys As MapInfo.Geometry.CoordSys)
            _isFinished = isFinished
            _linePoints = dPoints
            _coordSys = coordSys
        End Sub

        Public ReadOnly Property IsFinished()
            Get
                Return _isFinished
            End Get
        End Property

        Public Function GetCurrentLine() As MapInfo.Geometry.MultiCurve
            'return the feature
            Return New MapInfo.Geometry.MultiCurve(_coordSys, CurveSegmentType.Linear, _linePoints)
        End Function

        Public Function GetLastSegmentLength(ByVal distanceUnit As MapInfo.Geometry.DistanceUnit, ByVal distanceType As MapInfo.Geometry.DistanceType) As Double
            'create a curve object and turn its length
            If _linePoints.Length > 1 And Not _isFinished Then
                Dim _lastSegmentPoints(1) As MapInfo.Geometry.DPoint
                _lastSegmentPoints(0) = _linePoints(_linePoints.Length - 2)
                _lastSegmentPoints(1) = _linePoints(_linePoints.Length - 1)
                Dim curve As New MapInfo.Geometry.Curve(_coordSys, MapInfo.Geometry.CurveSegmentType.Linear, _lastSegmentPoints)
                Return curve.Length(distanceUnit, distanceType)
            End If
            Return 0
        End Function

        Public Function GetCurrentLineLength(ByVal distanceUnit As MapInfo.Geometry.DistanceUnit, ByVal distanceType As MapInfo.Geometry.DistanceType) As Double
            'create a curve object and return its length
            Dim curve As New MapInfo.Geometry.Curve(_coordSys, MapInfo.Geometry.CurveSegmentType.Linear, _linePoints)
            Return curve.Length(distanceUnit, distanceType)
        End Function

    End Class

    Public Delegate Sub PSGDistanceToolPointAddedEventHandler(ByVal sender As Object, ByVal e As PSGDistanceToolEventArgs)

    Public Class PSGDistanceTool2
        Inherits CustomPolylineMapTool

        'event for when points are added to the list
        Public Event PSGDistanceToolPointAdded As PSGDistanceToolPointAddedEventHandler

        'event for when the mouse tool is over the map
        Public Event PSGDistanceToolMove As PSGDistanceToolPointAddedEventHandler

        'Internal list of coordinates to keep track of the points submitted by the users
        Private _linePoints As ArrayList

        'Internal logical variable to determine when to reset the point list
        Private _isFinished As Boolean

        'Internal handle to grad a reference to the MapControl
        Private _handle As Integer

        'Internal variable to keep track of the map's coordsys
        Private _coordSys As MapInfo.Geometry.CoordSys


        Public Sub New(ByVal alterobject As Boolean, ByVal callcomplete As Boolean, ByVal drawrubberobject As Boolean, ByVal featureViewer As MapInfo.Mapping.FeatureViewer, ByVal hwnd As Integer, ByVal maptools As MapInfo.Tools.MapTools, ByVal iMouseToolProperties As MapInfo.Tools.IMouseToolProperties, ByVal iMapToolProperties As MapInfo.Tools.IMapToolProperties)

            MyBase.New(alterobject, callcomplete, drawrubberobject, featureViewer, hwnd, maptools, iMouseToolProperties, iMapToolProperties)
            'initialize internal variables
            _linePoints = New ArrayList
            _isFinished = True
            _handle = hwnd
            _coordSys = CType(featureViewer, MapInfo.Mapping.Map).GetDisplayCoordSys()
        End Sub

        Public Property CoordinateSystem() As MapInfo.Geometry.CoordSys
            Get
                Return _coordSys
            End Get
            Set(ByVal value As MapInfo.Geometry.CoordSys)
                _coordSys = value
            End Set
        End Property

        Public Overrides Sub OnKeyDown(ByVal sender As Object, ByVal kea As KeyEventArgs)
            Try
                Select Case kea.KeyCode
                    Case Keys.Enter
                        'get the mouse position in relation to the map
                        Dim mapControl As MapControl = CType(System.Windows.Forms.Control.FromHandle(New System.IntPtr(_handle)), MapControl)
                        Dim ptCursor As New System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y)
                        Dim ptMap As System.Drawing.Point = mapControl.PointToClient(ptCursor)
                        AddPoint(ptMap.X, ptMap.Y)
                        'end of the line - reset internal DPoints
                        _isFinished = True

                        Dim e As New PSGDistanceToolEventArgs(_isFinished, _linePoints.ToArray(GetType(MapInfo.Geometry.DPoint)), _coordSys)
                        RaiseEvent PSGDistanceToolPointAdded(Me, e)
                        MyBase.OnKeyDown(sender, kea)
                        Exit Sub
                    Case Keys.Escape
                        'TODO: Throw cancelled event?
                        'Cancelled Line - reset internal DPoints
                        _isFinished = True
                        MyBase.OnKeyDown(sender, kea)
                        Exit Sub
                    Case Else
                        MyBase.OnKeyDown(sender, kea)
                        Exit Sub
                End Select
            Catch
            End Try
        End Sub


        Public Overrides Sub OnMouseDown(ByVal sender As Object, ByVal mea As MouseEventArgs)
            Try
                'add a point to the list
                AddPoint(mea.X, mea.Y)
                If mea.Clicks = 2 Then
                    'end of line - reset internal DPoints
                    _isFinished = True
                End If

                'raise event if the PointAdded event has a subscription
                'Unlike in C#, it is not necessary to check for a subscription. The RaiseEvent will not throw an Exception
                Dim e As New PSGDistanceToolEventArgs(_isFinished, _linePoints.ToArray(GetType(MapInfo.Geometry.DPoint)), _coordSys)
                RaiseEvent PSGDistanceToolPointAdded(Me, e)
                MyBase.OnMouseDown(sender, mea)
            Catch
            End Try
        End Sub

        Public Overrides Sub OnMouseMove(ByVal sender As Object, ByVal mea As MouseEventArgs)
            'temporarily add a point to the list and raise event if the ToolMove event has a subscription
            If _linePoints.Count > 0 Then 'And Not (PSGDistanceToolMove Is Nothing) Then
                'add the point to the list
                AddPoint(mea.X, mea.Y)

                'Unlike in C#, it is not necessary to check for a subscription. The RaiseEvent will not throw an Exception
                Dim e As New PSGDistanceToolEventArgs(_isFinished, _linePoints.ToArray(GetType(MapInfo.Geometry.DPoint)), _coordSys)
                RaiseEvent PSGDistanceToolMove(sender, e)
                'Remove the point from the list
                _linePoints.RemoveAt(_linePoints.Count - 1)
            End If

            'call the base method
            MyBase.OnMouseMove(sender, mea)

        End Sub

        Private Sub AddPoint(ByVal mouseX As Integer, ByVal mouseY As Integer)
            Try
                If _isFinished = True Then
                    _linePoints.Clear()
                End If
                _isFinished = False
                Dim newPoint As New MapInfo.Geometry.DPoint(0, 0)
                MyBase.FeatureViewer.DisplayTransform.FromDisplay(New System.Drawing.PointF(mouseX, mouseY), newPoint)
                _linePoints.Add(newPoint)
            Catch
            End Try
        End Sub

    End Class

End Namespace


