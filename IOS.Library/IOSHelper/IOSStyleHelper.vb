Imports MapInfo.Styles
Imports System.Drawing

Public Class IOSStyleHelper

    Public Shared Function GetSytle(ByVal tableName As TableNames) As Style
        If (tableName = TableNames.DT_Scan2G_Parallel Or tableName = TableNames.DT_Scan3G_Parallel Or tableName = TableNames.DT_Scan4G_Parallel Or tableName = TableNames.DT_UE2G_Parallel Or tableName = TableNames.DT_UE3G_Parallel Or tableName = TableNames.DT_UE4G_Parallel Or tableName = TableNames.DT_Compare Or tableName = TableNames.DT_EventGrid Or tableName = TableNames.CellFootPrint) Then
            Return New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(33, New MapInfo.Styles.Font("MapInfo Symbols", 6), 0, Color.Black, 8))
        ElseIf (tableName = TableNames.DT_Events Or tableName = TableNames.DT_Events_GetEvents) Then
            '' Return New BitmapPointStyle("",BitmapStyles.ApplyColor,Color.Green,24);
            Return New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(42, New MapInfo.Styles.Font("MapInfo Symbols", 20), 0, Color.Black, 36))
        ElseIf (tableName = TableNames.CNE_RAW_Map_2G Or tableName = TableNames.CNE_RAW_Map_3G Or tableName = TableNames.CNE_RAW_Map_4G Or tableName = TableNames.CNE_RAW_Map_5G) Then
            Return New MapInfo.Styles.CompositeStyle(New MapInfo.Styles.FontPointStyle(35, New MapInfo.Styles.Font("MapInfo Symbols", 20), 0, Color.Black, 8))
        End If
        Return Nothing
    End Function

End Class
