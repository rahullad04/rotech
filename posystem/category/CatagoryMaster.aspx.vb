Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class CatagoryMaster
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Dim Sortheader As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            resetmaingrid()
        End If
    End Sub


    Private Sub resetmaingrid()
        Try
            Dim ds As DataTable
            ds = GF.GetData(False, "cat.ID as catid,cls.classname as clsname,CATEGORYNAME ", "s_category_master cat,s_class_master cls", "cat.CLASSID=cls.ID AND cat.IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vscatagory.DataSource = ds
                vscatagory.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vscatagory.DataSource = ds
                vscatagory.DataBind()
                Dim columncount As Integer = vscatagory.Rows(0).Cells.Count
                vscatagory.Rows(0).Cells.Clear()
                vscatagory.Rows(0).Cells.Add(New TableCell())
                vscatagory.Rows(0).Cells(0).ColumnSpan = columncount
                vscatagory.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub
    
    Protected Sub vsCatagory_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vscatagory.DataKeys(e.RowIndex).Values("catid").ToString)
        Dim catagory As String = vscatagory.DataKeys(i).Values(1).ToString()

        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_category_master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = catagory + " Details Deleted successfully"
        End If
    End Sub

    

    'Protected Sub vscatagory_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
    '    Dim i As Integer = e.NewEditIndex
    '    Dim id As String = vscatagory.Rows(i).Cells(1).Text
    '    Response.Redirect("CatagoryAddEdit.aspx?type=Edit&id=" + id.ToString)
    'End Sub

    Protected Sub vscategory_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vscatagory.DataKeys(e.NewEditIndex).Values("catid").ToString)
        Response.Redirect("CatagoryAddEdit.aspx?type=Edit&id=" + id.ToString)

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


    Protected Sub vscategory_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vscatagory.HeaderRow
        Sortheader = e.SortExpression
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"

        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "cat.ID as catid,cls.classname as clsname,CATEGORYNAME ", "s_category_master cat,s_class_master cls", "cat.CLASSID=cls.ID AND cat.IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vscatagory.DataSource = sortedView
        vscatagory.DataBind()

    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            TryCast(e.Row.FindControl("lblRowNumber"), Label).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub
    'Protected Sub vscategory_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles vscatagory.RowCreated

    '    If e.Row.RowType = DataControlRowType.Header Then
    '        For Each tc As TableCell In e.Row.Cells

    '            Dim img As New System.Web.UI.WebControls.Image()

    '            img.ImageUrl = "/img/" & (If(dir = SortDirection.Ascending, "asc", "desc")) & ".gif"

    '            If tc.HasControls() Then
    '                Dim lnk As LinkButton = DirectCast(tc.Controls(0), LinkButton)
    '                If (lnk.Text = vscatagory.SortExpression) Then
    '                    If vscatagory.SortDirection = SortDirection.Ascending Then
    '                        ' lnk.Text = lnk.Text + imgasc
    '                        tc.Controls.Add(img)
    '                    Else
    '                        tc.Controls.Add(img)
    '                    End If
    '                End If

    '            End If
    '        Next
    '    End If
    'End Sub

End Class
