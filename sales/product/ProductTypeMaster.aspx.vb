Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class ProductTypeMaster
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
            ds = GF.GetData(False, "ID,PRODUCTTYPE", "s_producttype_Master", "IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsProducttype.DataSource = ds
                vsProducttype.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsProducttype.DataSource = ds
                vsProducttype.DataBind()
                Dim columncount As Integer = vsProducttype.Rows(0).Cells.Count
                vsProducttype.Rows(0).Cells.Clear()
                vsProducttype.Rows(0).Cells.Add(New TableCell())
                vsProducttype.Rows(0).Cells(0).ColumnSpan = columncount
                vsProducttype.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Protected Sub vsProducttype_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        vsProducttype.EditIndex = -1
        resetmaingrid()
        lblresult.Text = ""
    End Sub

    Protected Sub vsProducttype_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then
            Dim txtproducttype As TextBox = DirectCast(vsProducttype.FooterRow.FindControl("txtproduct"), TextBox)

            Dim productid As Integer = GF.AutoGenId("id", "s_producttype_Master")
            Dim msg As String = GF.InsertData("Id,producttype", productid.ToString + ",'" + txtproducttype.Text + "'", "s_producttype_Master")
            If msg = "Success" Then
                resetmaingrid()
                lblresult.Text = txtproducttype.Text + " Details inserted successfully"
            End If
        End If
    End Sub
    Protected Sub vsProducttype_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vsProducttype.DataKeys(e.RowIndex).Values("id").ToString())
        Dim producttype As String = vsProducttype.DataKeys(i).Values(1).ToString
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_producttype_Master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = producttype + " Details Deleted successfully"
        End If

    End Sub

    Protected Sub vsProducttype_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        vsProducttype.EditIndex = e.NewEditIndex
        resetmaingrid()
    End Sub

    'Protected Sub vsProducttype_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vsProducttype.RowEditing
    '    Dim i As Integer = e.NewEditIndex
    '    resetmaingrid()
    'End Sub


    Protected Sub vsProducttype_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(vsProducttype.DataKeys(e.RowIndex).Values("id").ToString())
        Dim txtproducttype As TextBox = DirectCast(vsProducttype.Rows(e.RowIndex).FindControl("txtproducttype"), TextBox)
        Dim msg As String = GF.UpdateData("UNITTYPE", "'" + txtproducttype.Text + "'", "s_producttype_Master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = txtproducttype.Text + " Details Updated successfully"
        End If
    End Sub


    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsProducttype.PageIndex = e.NewPageIndex
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


    Protected Sub vsProducttype_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsProducttype.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,PRODUCTTYPE", "s_producttype_Master", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsProducttype.DataSource = sortedView
        vsProducttype.DataBind()
    End Sub
End Class
