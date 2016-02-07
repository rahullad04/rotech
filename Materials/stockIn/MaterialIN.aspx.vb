
Partial Class Materials_stockIn_MaterialIN
    Inherits System.Web.UI.Page

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            TryCast(e.Row.FindControl("lblRowNumber"), Label).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub
End Class
