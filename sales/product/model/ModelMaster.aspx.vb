Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class Model_ModelMaster
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
            ds = GF.GetData(False, "mm.id as id,mm.ModelName as modelname,mm.producttypeid as productid,mm.stackid as stackid,pm.ProductType as productname,sm.StackName as stack,mm.rate as rate,mm.BOMSRATE as  BOMSRATE", "s_producttype_master pm,s_stack_master sm,s_model_master mm", "mm.producttypeid= pm.id and mm.stackid= sm.id and mm.IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsmodel.DataSource = ds
                vsmodel.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsmodel.DataSource = ds
                vsmodel.DataBind()
                Dim columncount As Integer = vsmodel.Rows(0).Cells.Count
                vsmodel.Rows(0).Cells.Clear()
                vsmodel.Rows(0).Cells.Add(New TableCell())
                vsmodel.Rows(0).Cells(0).ColumnSpan = columncount
                vsmodel.Rows(0).Cells(0).Text = "No Records Found"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    'Protected Sub vsmodel_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
    '    If (e.CommandName.Equals("EditBOMS") = True) Then
    '        Dim i As Integer = vsmodel.SelectedRow.RowIndex.ToString
    '        Dim id As String = vsmodel.Rows(i).Cells(1).Text
    '        Dim mname As String = vsmodel.Rows(i).Cells(2).Text
    '    End If
    'End Sub
    'Protected Sub BOMS_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btn As Button = CType(sender, Button)
    '    Dim CommandName As String = btn.CommandName
    '    Dim CommandArgument As String() = btn.CommandArgument.Split(",")
    '    Dim CommandArgument1 As String = CommandArgument(0)
    '    Dim CommandArgument2 As String = CommandArgument(1)
    '    Dim CommandArgument3 As String = CommandArgument(2)
    'End Sub

    '' '' ''Protected Sub imgbutton_Click(ByVal sender As Object, ByVal e As EventArgs)
    '' '' ''    Dim imgbtn As ImageButton = CType(sender, ImageButton)
    '' '' ''    Dim CommandName As String = imgbtn.CommandName
    '' '' ''    Dim CommandArgument As String() = imgbtn.CommandArgument.Split(",")
    '' '' ''    Dim CommandArgument1 As String = CommandArgument(0)
    '' '' ''    'Dim CommandArgument2 As String = CommandArgument(1)
    '' '' ''    'Dim CommandArgument3 As String = CommandArgument(2)
    '' '' ''    Response.Redirect("BomsAdd.aspx?type=Edit&id=" + CommandArgument1)
    '' '' ''End Sub
    Protected Sub vsmodel_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles vsmodel.RowDeleting
        Dim i As Integer = e.RowIndex
        Dim id As String = vsmodel.Rows(i).Cells(1).Text
        Dim mname As String = vsmodel.Rows(i).Cells(2).Text
        Dim ans As Boolean = MsgBox("Are you sure you want to delete " + mname + " Details?", MsgBoxStyle.YesNo + MsgBoxStyle.Critical, GF.Title)
        If ans = 1 Then
            Dim msg As String
            msg = GF.UpdateData("IS_DELETED", "1", "s_model_master", "id=" + id.ToString)
            If msg = "Success" Then
                MsgBox("Selected Data deleted sucessfully !", MsgBoxStyle.Information, GF.Title)
            End If
        Else
            Exit Sub
        End If
        resetmaingrid()
    End Sub

    Protected Sub vsmodel_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vsmodel.DataKeys(e.NewEditIndex).Values("id").ToString)
        Response.Redirect("ModelAddEdit.aspx?type=Edit&id=" + id.ToString)
    End Sub


    ' '' ''Protected Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
    ' '' ''    Dim row As GridViewRow = DirectCast(DirectCast(sender, ImageButton).NamingContainer, GridViewRow)
    ' '' ''    DirectCast(row.NamingContainer, GridView).SelectedIndex = row.RowIndex
    ' '' ''End Sub


    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsmodel.PageIndex = e.NewPageIndex
        resetmaingrid()
    End Sub
    ' ''Protected Sub bomsclick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
    ' ''    Dim i As Integer = e.NewEditIndex
    ' ''    Dim id As String = Convert.ToInt32(vsmodel.DataKeys(e.NewEditIndex).Values("id").ToString)
    ' ''    Response.Redirect("BomsAdd.aspx?type=Edit&id=" + id.ToString)
    ' ''End Sub
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


    Protected Sub vsmodel_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsmodel.HeaderRow
        If dir = SortDirection.Ascending Then
            dir = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            dir = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim ds As DataTable
        ds = GF.GetData(False, "mm.id as id,mm.ModelName as modelname,mm.producttypeid as productid,mm.stackid as stackid,pm.ProductType as productname,sm.StackName as stack,mm.rate as rate", "s_producttype_master pm,s_stack_master sm,s_model_master mm", "mm.producttypeid= pm.id and mm.stackid= sm.id and mm.IS_DELETED=0")
        Dim sortedView As New DataView(ds)
        sortedView.Sort = Convert.ToString(e.SortExpression) & " " & sortingDirection
        vsmodel.DataSource = sortedView
        vsmodel.DataBind()
    End Sub
  
End Class
