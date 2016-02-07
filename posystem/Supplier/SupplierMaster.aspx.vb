Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class SupplierMaster
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
            ds = GF.GetData(False, "ID,NAME,City,PHONE,Cnname,cnphoneno,Email", "s_supplier_master", "IS_DELETED=0")

           
            If (ds.Rows.Count > 0) Then
                vssupplier.DataSource = ds
                vssupplier.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vssupplier.DataSource = ds
                vssupplier.DataBind()
                Dim columncount As Integer = vssupplier.Rows(0).Cells.Count
                vssupplier.Rows(0).Cells.Clear()
                vssupplier.Rows(0).Cells.Add(New TableCell())
                vssupplier.Rows(0).Cells(0).ColumnSpan = columncount
                vssupplier.Rows(0).Cells(0).Text = "No Records Found"
            End If
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Protected Sub vssupplier_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles vssupplier.RowDeleting
        Dim i As Integer = e.RowIndex
        Dim id As String = vssupplier.Rows(i).Cells(1).Text
        Dim sname As String = vssupplier.Rows(i).Cells(2).Text
        Dim ans As Boolean = MsgBox("Are you sure you want to delete " + sname + " Details?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, GF.Title)
        If ans = 1 Then
            Dim msg As String
            msg = GF.UpdateData("IS_DELETED", "1", "s_supplier_master", "id=" + id.ToString)
            If msg = "Success" Then
                MsgBox("Selected Data deleted sucessfully !", MsgBoxStyle.Information, GF.Title)
            End If
        Else
            Exit Sub
        End If
        resetmaingrid()
    End Sub

    Protected Sub vssupplier_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vssupplier.RowEditing
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vssupplier.DataKeys(e.NewEditIndex).Values("id").ToString)
        Response.Redirect("SupplierAddEdit.aspx?type=Edit&id=" + id.ToString)
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vssupplier.PageIndex = e.NewPageIndex
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


    Protected Sub vsSupplier_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vssupplier.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,NAME,ADDRESS,TIN,CST,PHONE,MOBILE,ECCno,Email", "s_supplier_master", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vssupplier.DataSource = sortedView
        vssupplier.DataBind()
    End Sub

End Class
