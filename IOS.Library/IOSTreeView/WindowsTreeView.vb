Imports System.Windows.Forms
Imports System.Drawing


Public Class WindowsTreeView
    Dim Treeview_NodeFound As Boolean = False
    Dim TreeView_SearchFound As Integer

    Public Sub txtObject_TextChanged(ByRef tree As TreeView, ByVal text As String)
        Dim tn As TreeNode = tree.SelectedNode
        TreeView_SearchFound = 0
        Treeview_NodeFound = False

        If tree.Nodes.Count <> 0 Then
            If Not tn Is Nothing Then
                tn.BackColor = Color.White
            End If
            Dim tns() As TreeNode = tree.Nodes(0).Nodes.Find(text, True)

            If tns.Length > 0 Then
                tns(0).EnsureVisible()
                tns(0).TreeView.SelectedNode = tns(0)
                tns(0).BackColor = Color.Coral
                TreeView_SearchFound = 1
            Else
                TreeView_SearchWildCard(tree.Nodes(0), text, 0)
            End If
        End If
    End Sub

    Public Sub txtObject_KeyDown(ByRef tree As TreeView, ByVal text As String, ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim tn As TreeNode = tree.SelectedNode
        If Not tn Is Nothing Then
            If e.KeyCode = Keys.Enter Then
                Treeview_NodeFound = False

                If TreeView_SearchFound = 0 Then
                    TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn), True)
                    If Treeview_NodeFound = False Then
                        TreeView_SearchWildCard(tree.Nodes(0), text, Treeview_NodePosition(tree, tn))
                    End If
                Else
                    TreeView_SearchFound = 0
                    TreeView_SearchWildCard(tree.Nodes(0), text, 0, False)

                End If

                If tn.Index = tree.SelectedNode.Index Then
                    tn.EnsureVisible()
                    tn.BackColor = Color.Coral
                Else
                    tn.BackColor = Color.White
                End If
            End If

        End If
    End Sub



    Public Sub TreeView_SearchWildCard(ByVal nd As TreeNode, ByVal str As String, ByVal startindex As Integer, Optional ByVal ExactMatch As Boolean = False)
        nd.TreeView.SuspendLayout()
        If str.Length < 3 Then
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    Exit Sub
                End If
                If nd.Text.ToUpper = str.ToUpper Then
                    If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                        nd.EnsureVisible()
                        nd.TreeView.SelectedNode = nd
                        nd.BackColor = Color.Coral
                        Treeview_NodeFound = True
                        nd.TreeView.ResumeLayout(True)
                        Exit Sub
                    End If
                Else
                    nd.BackColor = Color.White
                End If
                TreeView_SearchWildCard(nd, str, startindex)
            Next
        Else
            For Each nd In nd.Nodes
                If Treeview_NodeFound = True Then
                    nd.TreeView.ResumeLayout(True)
                    Exit Sub
                End If
                If ExactMatch = False Then
                    If nd.Text.ToUpper.StartsWith(str.ToUpper) Then
                        If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                            nd.EnsureVisible()
                            nd.TreeView.SelectedNode = nd
                            nd.BackColor = Color.Coral
                            Treeview_NodeFound = True
                            nd.TreeView.ResumeLayout(True)
                            Exit Sub
                        End If
                    Else
                        nd.BackColor = Color.White
                    End If
                    TreeView_SearchWildCard(nd, str, startindex)
                Else
                    If nd.Text.ToUpper = str.ToUpper Then
                        If Treeview_NodePosition(nd.TreeView, nd) > startindex Then
                            nd.EnsureVisible()
                            nd.TreeView.SelectedNode = nd
                            nd.BackColor = Color.Coral
                            Treeview_NodeFound = True
                            nd.TreeView.ResumeLayout(True)
                            Exit Sub
                        End If
                    Else
                        nd.BackColor = Color.White
                    End If
                    TreeView_SearchWildCard(nd, str, startindex, True)
                End If
            Next
        End If
        nd.TreeView.ResumeLayout(True)
    End Sub
    Private Function Treeview_NodePosition(ByVal oTreeView As TreeView, ByVal oNode As TreeNode)
        Dim iPosInTree As Integer = 0
        Do
            Dim iNodeIndex As Integer = oNode.Index
            iPosInTree = iPosInTree + iNodeIndex + 1

            'Get the Parent Node or the TreeView if at the top.
            Dim oParentNode As Object = oNode.Parent
            If oParentNode Is Nothing Then
                oParentNode = oTreeView
            End If

            'Count the Nodes precding this one on the current level.
            Dim I As Integer
            For I = 0 To iNodeIndex - 1
                iPosInTree = iPosInTree + Treeview_NumberOfChildren(oParentNode.Nodes(I))
            Next

            'Go up to the next level.
            oNode = oNode.Parent
        Loop Until oNode Is Nothing
        Return iPosInTree
    End Function
    Function Treeview_NumberOfChildren(ByVal oNode As TreeNode)
        If oNode.LastNode Is Nothing Then
            Return 0 'No children
        End If
        Dim iNumChildren = oNode.LastNode.Index + 1
        Dim oSubNode As TreeNode
        For Each oSubNode In oNode.Nodes
            iNumChildren = iNumChildren + Treeview_NumberOfChildren(oSubNode)
        Next
        Return iNumChildren
    End Function

    Public Shared Sub InsertNode(ByRef tree As System.Windows.Forms.TreeView, ByVal nodeText As String)
        Dim roottn As TreeNode = New TreeNode
        roottn.Text = nodeText
        tree.Nodes.Add(roottn)
    End Sub
End Class
