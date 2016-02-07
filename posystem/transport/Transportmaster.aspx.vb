Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class posystem_transport_Transportmaster
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
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
            ds = GF.GetData(False, "ID,Transpotname,DriverName,VehicalNo,contactno,contactname", "s_transpoter", "IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vstrans.DataSource = ds
                vstrans.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vstrans.DataSource = ds
                vstrans.DataBind()
                Dim columncount As Integer = vstrans.Rows(0).Cells.Count
                vstrans.Rows(0).Cells.Clear()
                vstrans.Rows(0).Cells.Add(New TableCell())
                vstrans.Rows(0).Cells(0).ColumnSpan = columncount
                vstrans.Rows(0).Cells(0).Text = "No Records Found"
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub vstrans_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        vstrans.EditIndex = -1
        resetmaingrid()
        lblresult.Text = ""
    End Sub
    Protected Sub vstrans_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then

            Dim txttrans As TextBox = DirectCast(vstrans.FooterRow.FindControl("txttrans"), TextBox)
            If txttrans.Text = "" Then
                ShowMessage("Please enter transpotname to save data", MessageType.Info)
                Exit Sub
            End If
            Dim txtdrive As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtdrive"), TextBox)
            Dim txtv As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtv"), TextBox)
            Dim txtcontact As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtcontact"), TextBox)
            Dim txtphone As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtphone"), TextBox)

            Dim id As Integer = GF.AutoGenId("id", "s_transpoter")
            Dim msg As String = GF.InsertData("ID,Transpotname,DriverName,VehicalNo,contactno,contactname", id.ToString + ",'" + txttrans.Text + "','" + txtdrive.Text + "','" + txtv.Text + "','" + txtcontact.Text + "','" + txtphone.Text + "'", "s_transpoter")
            If msg = "Success" Then
                resetmaingrid()
                ShowMessage(txttrans.Text + " Details inserted successfully", MessageType.Success)
            End If
        End If
    End Sub

    Protected Sub vstrans_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vstrans.DataKeys(i).Values(0))
        Dim transpotname As String = vstrans.DataKeys(i).Values(1).ToString()
        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_transpoter", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            ShowMessage(transpotname + " Details Deleted successfully", MessageType.Success)
        End If

    End Sub

    Protected Sub vstrans_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        vstrans.EditIndex = e.NewEditIndex
        resetmaingrid()
    End Sub



    Protected Sub vstrans_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(vstrans.DataKeys(e.RowIndex).Values("id").ToString())
        Dim txttrans As TextBox = DirectCast(vstrans.Rows(e.RowIndex).FindControl("txttranspot"), TextBox)
        Dim txtdrive As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtdriver"), TextBox)
        Dim txtv As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtvehical"), TextBox)
        Dim txtcontact As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtcontactname"), TextBox)
        Dim txtphone As TextBox = DirectCast(vstrans.FooterRow.FindControl("txtcontactno"), TextBox)

        Dim msg As String = GF.UpdateData("Transpotname,DriverName,VehicalNo,contactno,contactname ", "'" + txttrans.Text + "'|'" + txtdrive.Text + "'|'" + txtv.Text + "'|'" + txtcontact.Text + "'|'" + txtphone.Text + "'", "s_transpoter", "Id=" + id.ToString)
        If msg = "Success" Then
            resetmaingrid()
            ShowMessage(txttrans.Text + " Details Updated successfully", MessageType.Success)
        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vstrans.PageIndex = e.NewPageIndex
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


    Protected Sub vstrans_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vstrans.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,Transpotname,DriverName,VehicalNo,contactno,contactname", "s_transpoter", "IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vstrans.DataSource = sortedView
        vstrans.DataBind()
    End Sub
End Class
