Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class Client_ClientMaster
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Dim str As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            resetmaingrid()

        End If
    End Sub

    Private Sub resetmaingrid()
        Try

            Dim ds As DataTable
            Dim cmbname As String = String.Empty
           
            'If Trim(txtsearch.Text) <> "" Then
            '    If cmbsearch.SelectedIndex = 1 Then
            '        cmbname = "Name Like '" + Trim(txtsearch.Text) + "%'"
            '    ElseIf (cmbsearch.SelectedIndex = 2) Then
            '        cmbname = "City Like '" + Trim(txtsearch.Text) + "%'"
            '    End If
            'End If
            ds = GF.GetData(False, "clientid,name,address,city,phoneno,faxno,TIN,CST,email,Cnphone_no,cn_name,Terms", "s_client_master", cmbname + "  IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsclient.DataSource = ds
                vsclient.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsclient.DataSource = ds
                vsclient.DataBind()
                Dim columncount As Integer = vsclient.Rows(0).Cells.Count
                vsclient.Rows(0).Cells.Clear()
                vsclient.Rows(0).Cells.Add(New TableCell())
                vsclient.Rows(0).Cells(0).ColumnSpan = columncount
                vsclient.Rows(0).Cells(0).Text = "No Records Found"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub


  
    Protected Sub vsclient_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles vsclient.RowDeleting
        Dim i As Integer = e.RowIndex
        Dim id As String = vsclient.Rows(i).Cells(1).Text
        Dim name As String = vsclient.Rows(i).Cells(2).Text
        Dim ans As Boolean = MsgBox("Are you sure you want to delete " + name + " Details?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, GF.Title)
        If ans = 1 Then
            Dim msg As String
            msg = GF.UpdateData("IS_DELETED", "1", "s_client_master", "clientid=" + id.ToString)
            If msg = "Success" Then
                MsgBox("Selected Data deleted sucessfully !", MsgBoxStyle.Information, GF.Title)
            End If
        Else
            Exit Sub
        End If
        resetmaingrid()
    End Sub


    Protected Sub vsclient_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vsclient.RowEditing
        Dim i As Integer = e.NewEditIndex
        Dim id As String = vsclient.Rows(i).Cells(1).Text
        Response.Redirect("ClientAddEdit.aspx?type=Edit&id=" + id.ToString)
    End Sub

 




    'Protected Sub cmbsearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsearch.SelectedIndexChanged
    '    resetmaingrid()
    'End Sub

    'Protected Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
    '    resetmaingrid()
    'End Sub
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

    
    Protected Sub vsclient_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsclient.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"

        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "clientid,name,address,city,phoneno,faxno,TIN,CST,email,Cnphone_no,cn_name,Terms", "s_client_master", "  IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsclient.DataSource = sortedView
        vsclient.DataBind()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsclient.PageIndex = e.NewPageIndex
        resetmaingrid()
    End Sub
End Class
