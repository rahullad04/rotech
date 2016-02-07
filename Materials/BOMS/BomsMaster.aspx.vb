Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class Materials_BOMS_BomsMaster
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
            ds = GF.GetData(False, "bm.id as id,bm.ModelId as modelid,mm.ModelName as modelname,bm.rate as bomsrate,pm.ProductType as productname,sm.StackName as stack", "s_boms_master bm,s_model_master mm,s_producttype_master pm,s_stack_master sm", "bm.modelid= mm.id and mm.producttypeid= pm.id and mm.stackid= sm.id and mm.IS_DELETED=0")
            If (ds.Rows.Count > 0) Then
                vsboms.DataSource = ds
                vsboms.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsboms.DataSource = ds
                vsboms.DataBind()
                Dim columncount As Integer = vsboms.Rows(0).Cells.Count
                vsboms.Rows(0).Cells.Clear()
                vsboms.Rows(0).Cells.Add(New TableCell())
                vsboms.Rows(0).Cells(0).ColumnSpan = columncount
                vsboms.Rows(0).Cells(0).Text = "No Records Found"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Protected Sub vsboms_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vsboms.DataKeys(e.NewEditIndex).Values("id").ToString)
        Response.Redirect("BomsAdd.aspx?type=Edit&id=" + id.ToString)
    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        vsboms.PageIndex = e.NewPageIndex
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


    Protected Sub vsboms_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortingDirection As String = String.Empty
        Dim headerRow As GridViewRow = vsboms.HeaderRow
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
        vsboms.DataSource = sortedView
        vsboms.DataBind()
    End Sub
End Class
