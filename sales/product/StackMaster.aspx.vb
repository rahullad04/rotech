Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class StackMaster
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            resetmaingrid()
        End If
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            TryCast(e.Row.FindControl("lblRowNumber"), Label).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub
    Private Sub resetmaingrid()
        Try
            Dim ds As DataTable
            ds = GF.GetData(False, "ID,stackname", "s_stack_Master", "IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsstack.DataSource = ds
                vsstack.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsstack.DataSource = ds
                vsstack.DataBind()
                Dim columncount As Integer = vsstack.Rows(0).Cells.Count
                vsstack.Rows(0).Cells.Clear()
                vsstack.Rows(0).Cells.Add(New TableCell())
                vsstack.Rows(0).Cells(0).ColumnSpan = columncount
                vsstack.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Protected Sub vsstack_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        vsStack.EditIndex = -1
        resetmaingrid()
        lblresult.Text = ""
    End Sub

    Protected Sub vsstack_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then
            Dim txtstackname As TextBox = DirectCast(vsStack.FooterRow.FindControl("txtstack"), TextBox)


            Dim stackid As Integer = GF.AutoGenId("id", "s_stack_Master")
            Dim msg As String = GF.InsertData("Id,stackname", stackid.ToString + ",'" + txtstackname.Text + "'", "s_stack_Master")
            If msg = "Success" Then
                resetmaingrid()
                lblresult.Text = txtstackname.Text + " Details inserted successfully"
            End If
        End If
    End Sub
    Protected Sub vsstack_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vsStack.DataKeys(e.RowIndex).Values("id").ToString())
        Dim stackname As String = vsStack.DataKeys(i).Values(1).ToString
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_stack_Master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = stackname + " Details Deleted successfully"
        End If

    End Sub

    Protected Sub vsstack_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        vsStack.EditIndex = e.NewEditIndex
        resetmaingrid()
    End Sub

    'Protected Sub vsstack_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vsstack.RowEditing
    '    Dim i As Integer = e.NewEditIndex
    '    resetmaingrid()
    'End Sub


    Protected Sub vsstack_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(vsStack.DataKeys(e.RowIndex).Values("id").ToString())
        Dim txtstackname As TextBox = DirectCast(vsStack.Rows(e.RowIndex).FindControl("txtstackname"), TextBox)
        Dim msg As String = GF.UpdateData("stackname", "'" + txtstackname.Text + "'", "s_stack_Master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = txtstackname.Text + " Details Updated successfully"
        End If
    End Sub


    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsStack.PageIndex = e.NewPageIndex
        resetmaingrid()
    End Sub

    Public Property dir() As SortDirection
        Get
            If ViewState("dirState") Is Nothing Then
                ViewState("dirState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("dirState"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("dirState") = value
        End Set
    End Property


    Protected Sub vsstack_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsStack.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,stackname", "s_stack_Master", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsStack.DataSource = sortedView
        vsStack.DataBind()
    End Sub
End Class
