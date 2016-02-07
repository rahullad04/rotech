Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions


Partial Class Part_PartMaster
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
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Private Sub resetmaingrid()
        Try
            Dim ds As DataTable
            ds = GF.GetData(False, " pm.ID as pmid,cls.classname as clsname,cat.CATEGORYNAME as catname,pm.partname as partname ", "s_category_master cat,s_class_master cls, s_part_master pm", "pm.classid= cls.id and pm.catagoryid = cat.id anD pm.IS_DELETED <> 1")
            If (ds.Rows.Count > 0) Then
                vspart.DataSource = ds
                vspart.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vspart.DataSource = ds
                vspart.DataBind()
                Dim columncount As Integer = vspart.Rows(0).Cells.Count
                vspart.Rows(0).Cells.Clear()
                vspart.Rows(0).Cells.Add(New TableCell())
                vspart.Rows(0).Cells(0).ColumnSpan = columncount
                vspart.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub vspart_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles vspart.RowDeleting
        Dim i As Integer = e.RowIndex
        Dim id As Integer = vspart.Rows(i).Cells(1).Text
        Dim part As String = vspart.Rows(i).Cells(4).Text
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_part_master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            ShowMessage(part + " Details Deleted successfully", MessageType.Info)
        End If
    End Sub

    Protected Sub vspart_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vspart.RowEditing
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vspart.DataKeys(e.NewEditIndex).Values("pmid").ToString)
        Response.Redirect("partAddEdit.aspx?type=Edit&id=" + id.ToString)
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vspart.PageIndex = e.NewPageIndex
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


    Protected Sub vspart_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vspart.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, " pm.ID as pmid,cls.classname as clsname,cat.CATEGORYNAME as catname,pm.partname as partname", "s_category_master cat,s_class_master cls, s_part_master pm", "pm.classid= cls.id and pm.catagoryid = cat.id AND pm.IS_DELETED <> 1")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vspart.DataSource = sortedView
        vspart.DataBind()
    End Sub
End Class
