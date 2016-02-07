
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class UnitMaster
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
            ds = GF.GetData(False, "ID,UNITTYPE", "s_Unit", "IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsUnit.DataSource = ds
                vsUnit.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsUnit.DataSource = ds
                vsUnit.DataBind()
                Dim columncount As Integer = vsUnit.Rows(0).Cells.Count
                vsUnit.Rows(0).Cells.Clear()
                vsUnit.Rows(0).Cells.Add(New TableCell())
                vsUnit.Rows(0).Cells(0).ColumnSpan = columncount
                vsUnit.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Protected Sub vsunit_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        vsUnit.EditIndex = -1
        resetmaingrid()
        lblresult.Text = ""
    End Sub

    Protected Sub vsUnit_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then

            Dim txtunittype As TextBox = DirectCast(vsUnit.FooterRow.FindControl("txtunit"), TextBox)
            If txtunittype.Text = "" Then
                lblresult.Text = "You can not enter null value"
                Exit Sub

            End If


            Dim unitid As Integer = GF.AutoGenId("id", "s_unit")
            Dim msg As String = GF.InsertData("Id,unittype", unitid.ToString + ",'" + txtunittype.Text + "'", "s_unit")
            If msg = "Success" Then
                resetmaingrid()
                lblresult.Text = txtunittype.Text + " Details inserted successfully"
            End If
        End If
    End Sub
    Protected Sub vsUnit_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)


        Dim i As Integer = e.RowIndex
        'Get the value of column from the DataKeys using the RowIndex.
        Dim id As Integer = Convert.ToInt32(vsUnit.DataKeys(i).Values(0))
        Dim unit As String = vsUnit.DataKeys(i).Values(1).ToString()

        'Dim i As Integer = e.RowIndex

        'Dim id As String = vsUnit.Rows(e.RowIndex).Cells(1).Text
        'Dim unit As String = vsUnit.Rows(i).Cells(2).Text.ToString
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_unit", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = unit + " Details Deleted successfully"
        End If

    End Sub

    Protected Sub vsunit_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        vsUnit.EditIndex = e.NewEditIndex
        resetmaingrid()
    End Sub

    'Protected Sub vsUnit_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles vsUnit.RowEditing
    '    Dim i As Integer = e.NewEditIndex
    '    resetmaingrid()
    'End Sub


    Protected Sub vsUnit_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(vsUnit.DataKeys(e.RowIndex).Values("id").ToString())
        Dim txtunittype As TextBox = DirectCast(vsUnit.Rows(e.RowIndex).FindControl("txtunittype"), TextBox)
        Dim msg As String = GF.UpdateData("UNITTYPE", "'" + txtunittype.Text + "'", "s_unit", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            lblresult.Text = txtunittype.Text + " Details Updated successfully"
        End If
    End Sub


    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsUnit.PageIndex = e.NewPageIndex
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


    Protected Sub vsunit_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsUnit.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,UNITTYPE", "s_Unit", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsUnit.DataSource = sortedView
        vsUnit.DataBind()
    End Sub
End Class
