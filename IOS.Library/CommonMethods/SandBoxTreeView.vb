Imports System.Windows.Forms
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Public Class SandBoxTreeView

    Public Shared Sub TreeView_AfterCheck(ByVal nd As TreeNode)
        If nd.Checked = True Then
            If nd.Level > 1 Then
                If nd.Parent.Checked = False Then
                    nd.Parent.Checked = True
                End If
            End If

            If nd.Nodes.Count > 0 And Treeview_GetCheck(nd.Nodes).Count = 0 Then
                For Each nds As TreeNode In nd.Nodes
                    If nds.Checked = False Then
                        nds.Checked = True
                    End If
                Next
            End If
        Else
            If nd.Nodes.Count > 0 Then
                For Each nds As TreeNode In nd.Nodes
                    If nds.Checked = True Then
                        nds.Checked = False
                    End If
                Next
            End If
        End If
    End Sub

    Public Shared Function Treeview_GetCheck(ByVal node As TreeNodeCollection) As List(Of TreeNode)
        Dim lN As New List(Of TreeNode)
        For Each n As TreeNode In node
            If n.Checked Then lN.Add(n)
            lN.AddRange(Treeview_GetCheck(n.Nodes))
        Next
        Return lN
    End Function

    Public Shared Function TreeListNodes_GetCheck(ByVal treeNodes As TreeListNodes) As List(Of TreeListNode)
        Dim lN As New List(Of TreeListNode)
        For Each n As TreeListNode In treeNodes
            If n.Checked Then lN.Add(n)
            lN.AddRange(TreeListNodes_GetCheck(n.Nodes))
        Next
        Return lN
    End Function

    Public Shared Sub Clear(ByRef tl As DevExpress.XtraTreeList.TreeList)
        tl.SuspendLayout()
        tl.Columns.Clear()
        tl.Nodes.Clear()
        tl.DataSource = Nothing
        tl.Refresh()
        tl.Update()
        tl.ResumeLayout()
    End Sub

End Class
