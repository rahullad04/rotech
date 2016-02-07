Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class ClassMaster
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
            ds = GF.GetData(False, "ID,Classname", "s_class_master", "IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsclass.DataSource = ds
                vsclass.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsclass.DataSource = ds
                vsclass.DataBind()
                Dim columncount As Integer = vsclass.Rows(0).Cells.Count
                vsclass.Rows(0).Cells.Clear()
                vsclass.Rows(0).Cells.Add(New TableCell())
                vsclass.Rows(0).Cells(0).ColumnSpan = columncount
                vsclass.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Protected Sub vsclass_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        vsclass.EditIndex = -1
        resetmaingrid()
        lblresult.Text = ""
    End Sub

    Protected Sub vsclass_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then
            Dim txtclass As TextBox = DirectCast(vsclass.FooterRow.FindControl("txtclass"), TextBox)
            If txtclass.Text = "" Then
                lblresult.Text = "you cannot enter null value"
                Exit Sub
            End If

            Dim id As Integer = GF.AutoGenId("id", "s_class_master")
            Dim msg As String = GF.InsertData("Id,classname", id.ToString + ",'" + txtclass.Text + "'", "s_class_master")
            If msg = "Success" Then
                resetmaingrid()
                lblresult.Text = txtclass.Text + " Details inserted successfully"
            End If
        End If
    End Sub
    Protected Sub vsclass_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As String = vsclass.Rows(i).Cells(1).Text
        Dim classname As String = vsclass.Rows(i).Cells(2).Text
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_class_master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = classname + " Details Deleted successfully"
        End If

    End Sub

    Protected Sub vsclass_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        vsclass.EditIndex = e.NewEditIndex
        resetmaingrid()
    End Sub



    Protected Sub vsclass_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(vsclass.DataKeys(e.RowIndex).Values("id").ToString())
        Dim txtclassname As TextBox = DirectCast(vsclass.Rows(e.RowIndex).FindControl("txtclass"), TextBox)
        Dim msg As String = GF.UpdateData("classname", "'" + txtclassname.Text + "'", "s_class_master", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = txtclassname.Text + " Details Updated successfully"
        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsclass.PageIndex = e.NewPageIndex
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


    Protected Sub vsclass_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsclass.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,Classname", "s_class_master", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsclass.DataSource = sortedView
        vsclass.DataBind()
    End Sub
End Class
