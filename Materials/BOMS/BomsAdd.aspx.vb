Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Imports System.DBNull
Imports System.Data.OleDb
Imports System.Web.Services
Imports System.Collections.Generic
Partial Class Materials_BOMS_BomsAdd
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Public ddl As Boolean = False
    Public bomsid As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            FillDropDown()
            Dim id As String = Request.QueryString("id")
            Dim type As String = Request.QueryString("type")

            If type = "Add" Then
                lblbomid.Text = GF.AutoGenId("id", "s_boms_master")
                bomsid = lblbomid.Text
                resetbomgrid()
                resetmaingrid(lblbomid.Text)
            ElseIf type = "Edit" Then
                Dim modelid As String = GF.GetSingleValue("", False, "modelid", "s_boms_master", "id=" + id.ToString)
                Dim ds As DataTable = GF.GetData(False, "producttypeid", "s_model_master", "id=" + modelid.ToString)
                cmbproducttype.SelectedValue = ds.Rows(0)("Producttypeid").ToString
                ds = GF.GetData(True, "Modelname,id", "s_model_master", "is_deleted <>1 and producttypeid='" + cmbproducttype.SelectedValue.ToString + "'", False, "ModelName asc")
                cmbmodel.DataTextField = "Modelname"
                cmbmodel.DataValueField = "id"
                cmbmodel.DataSource = ds
                cmbmodel.DataBind()
                cmbmodel.Items.Insert(0, "Select")
                cmbmodel.SelectedValue = modelid.ToString
                lblmodelid.Text = modelid.ToString
                lblbomid.Text = id.ToString
                bomsid = id.ToString
                resetbomgrid()
                resetmaingrid(id)
            End If
        End If
    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            TryCast(e.Row.FindControl("lblRowNumber"), Label).Text = (e.Row.RowIndex + 1).ToString()
        End If
    End Sub
    Private Sub resetmaingrid(ByVal id As Integer)
        'Dim dt As DataTable = GF.GetData(True, "bm.rate as bomsrate,bd.classid AS classid,cm.classname AS class,bd.categoryid AS catid,catm.CATEGORYNAME AS Category,bd.partid AS partid,pm.partname AS partname,bd.qty AS qty,bd.Unit AS unit,bd.Rate AS rate,bd.sid AS sid", "s_boms_reference ref INNER JOIN s_boms_detail bd ON bd.bomid = ref.reference_bom_id LEFT JOIN s_boms_exclude_part ex ON ex.BOMID = ref.bomid  INNER JOIN s_class_master cm ON cm.id= bd.classid INNER JOIN s_category_master catm ON catm.id= bd.CATEGORYID INNER JOIN s_part_master pm ON pm.id= bd.PARTID LEFT JOIN s_boms_master bm ON bm.ID = ref.reference_bom_id", "ref.bomid = " + id.ToString + "  AND (ex.PARTID IS NULL OR bd.PARTID <> ex.PARTID)")
        Dim dt As DataTable
        dt = GF.GetData(True, "bm.rate as bomsrate,bd.classid AS classid,cm.classname AS class,bd.categoryid AS catid,catm.CATEGORYNAME AS Category,bd.partid AS partid,pm.partname AS partname,bd.qty AS qty,bd.Unit AS unit,bd.Rate AS rate,bd.sid AS sid", "s_boms_master bm,s_boms_detail bd,s_class_master cm,s_category_master catm,s_part_master pm", "bm.ID = bd.BOMID and cm.id=bd.CLASSID and catm.id = bd.categoryid and pm.id= bd.partid and bm.id =" + id.ToString)
        If (dt.Rows.Count > 0) Then
            vsBoms.DataSource = dt
            vsBoms.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            vsBoms.DataSource = dt
            vsBoms.DataBind()
        End If
        Dim lbltotal As Label = TryCast(vsBoms.FooterRow.FindControl("lbltotalrate"), Label)
        Dim totalrate As String = dt.Rows(0)("bomsrate").ToString
        lbltotal.Text = totalrate
    End Sub
    Private Sub resetbomgrid()
        Dim dt As DataTable = GF.GetData(True, "br.id as id,bm.ID as refbomid,bm.ModelId as MODELID,mm.ModelName as MODELNAME,bm.rate as bomsrate", "s_boms_master bm,s_boms_reference br,s_model_master mm", "bm.id =br.REFERENCE_BOM_ID and mm.id = bm.modelid and br.BOMID =" + lblbomid.Text.ToString)
        If (dt.Rows.Count > 0) Then
            vscopyboms.DataSource = dt
            vscopyboms.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            vscopyboms.DataSource = dt
            vscopyboms.DataBind()
        End If
        Uplboms.Update()
        'Dim lbltotal As Label = TryCast(vscopyboms.FooterRow.FindControl("lbltotalrate"), Label)
        ''Dim totalrate As String = GF.GetSingleValue("Sum", True, "Rate", "s_boms_temp", "modelid= '" + lblmodelid.Text.ToString + "' and Is_Deleted <>1")
        'Dim totalrate As String = dt.Rows(0)("bomsrate").ToString
        'lbltotal.Text = totalrate
    End Sub
    Public Sub FillDropDown()
        Try
            Dim ds As New DataTable()
            ds = GF.GetData(True, "id,classname", "s_class_master", "Is_DELETED<>1", False, "classname asc")
            cmbclass.DataTextField = "classname"
            cmbclass.DataValueField = "id"
            cmbclass.DataSource = ds
            cmbclass.DataBind()
            cmbclass.Items.Insert(0, New ListItem("Select", "0"))

            ds = GF.GetData(True, "id,unittype", "s_unit", "Is_DELETED<>1", False, "unittype asc")
            cmbunit.DataTextField = "unittype"
            cmbunit.DataValueField = "id"
            cmbunit.DataSource = ds
            cmbunit.DataBind()
            cmbunit.Items.Insert(0, New ListItem("Select", "0"))

            ds = GF.GetData(True, "Producttype,id", "s_producttype_master", "is_deleted <>1", False, "producttype asc")
            cmbproducttype.DataTextField = "Producttype"
            cmbproducttype.DataValueField = "id"
            cmbproducttype.DataSource = ds
            cmbproducttype.DataBind()
            cmbproducttype.Items.Insert(0, "Select")
            cmbproducttype.SelectedIndex = 0

            ds = GF.GetData(True, "bm.id,bm.modelid as mid,mm.modelname as modelname", "s_boms_master bm,s_model_master mm", "bm.modelid = mm.id", False, "modelname asc")
            cmbmodelname.DataTextField = "modelname"
            cmbmodelname.DataValueField = "mid"
            cmbmodelname.DataSource = ds
            cmbmodelname.DataBind()
            cmbmodelname.Items.Insert(0, "Select")
            cmbmodelname.SelectedIndex = 0


        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbclass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            fillcategory_data(cmbclass.SelectedValue.ToString)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbcategory_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            fillpart_data(cmbclass.SelectedValue.ToString, cmbcategory.SelectedValue.ToString)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbpartname_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim partid As Integer = cmbpartname.SelectedValue
            Dim ds As DataTable = GF.GetData(False, "s.id as sid,s.name as suppliername", "s_supplier_part sp,s_supplier_master s", "sp.SUPPLIER_ID= s.id and sp.PART_ID=" + partid.ToString)
            cmbsupplier.DataTextField = "suppliername"
            cmbsupplier.DataValueField = "sid"
            cmbsupplier.DataSource = ds
            cmbsupplier.DataBind()
            cmbsupplier.Items.Insert(0, "Select")
            cmbsupplier.SelectedIndex = 0
            uplsuppliername.Update()
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbsupplier_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim rate As Double = GF.GetSingleValue("", False, "Rate", "s_supplier_part", "SUPPLIER_ID= " + cmbsupplier.SelectedValue.ToString + " and PART_ID = " + cmbpartname.SelectedValue.ToString)
            txtrate.Text = rate
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbproducttype_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim ds As DataTable
            ds = GF.GetData(True, "id,modelname", "s_model_master", "is_deleted <>1  and id not in (select ModelId from s_boms_master) and producttypeid='" + cmbproducttype.SelectedValue.ToString + "'", False, "ModelName asc")
            cmbmodel.DataTextField = "Modelname"
            cmbmodel.DataValueField = "id"
            cmbmodel.DataSource = ds
            cmbmodel.DataBind()
            cmbmodel.Items.Insert(0, "Select")
            cmbmodel.SelectedIndex = 0
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbmodel_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim producttype As String = cmbproducttype.SelectedItem.Text
            Dim modelname As String = cmbmodel.SelectedItem.Text
            lblname.Text = "Modelname: " + producttype.ToString + ": " + modelname.ToString
            lblmodelid.Text = cmbmodel.SelectedValue.ToString
            Uplboms.Update()
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub cmbmodelname_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblbid.Text = GF.GetSingleValue("", True, "id", "s_boms_master", "modelid=" + cmbmodelname.SelectedValue.ToString)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub fillcategory_data(ByVal id As Integer)
        Dim ds As New DataTable()
        ds = GF.GetData(True, "id,categoryname", "s_category_master", "classid=" + id.ToString + " and Is_DELETED<>1", False, "categoryname asc")
        cmbcategory.DataTextField = "categoryname"
        cmbcategory.DataValueField = "id"
        cmbcategory.DataSource = ds
        cmbcategory.DataBind()
        cmbcategory.Items.Insert(0, "Select")
        cmbcategory.SelectedIndex = 0
    End Sub
    Protected Sub fillpart_data(ByVal classid As Integer, ByVal catid As Integer)
        Try
            Dim ds As New DataTable()
            ds = GF.GetData(True, "id,partname", "s_Part_master", "classid=" + classid.ToString + " and catagoryid= " + catid.ToString + " and Is_DELETED<>1", False, "partname asc")
            cmbpartname.DataTextField = "partname"
            cmbpartname.DataValueField = "id"
            cmbpartname.DataSource = ds
            cmbpartname.DataBind()
            cmbpartname.Items.Insert(0, "Select")
            cmbpartname.SelectedIndex = 0
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub btncancle_click(ByVal sender As Object, ByVal e As EventArgs)
        cmbclass.SelectedIndex = 0
        cmbcategory.Items.Clear()
        cmbpartname.Items.Clear()
        txtrate.Text = ""
        txtqty.Text = ""
        cmbunit.SelectedIndex = 0
        cmbsupplier.Items.Clear()
        UpdatePanel2.Update()
    End Sub
    Protected Sub vsboms_rowdeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim partid As Integer = Convert.ToInt32(vsBoms.DataKeys(i).Values("partid").ToString)
        Dim msg As String = GF.DeleteData("s_boms_detail", "bomid=" + lblbomid.Text.ToString + " and partid=" + partid.ToString)
        'Dim deleteid As Integer = GF.AutoGenId("id", "s_boms_exclude_part")
        'msg = GF.InsertData("ID,BOMID,PARTID,BOM_REFERENCE_ID", deleteid.ToString + "," + lblbomid.Text.ToString + "," + partid.ToString + "," + cmbmodelname.SelectedValue.ToString, "s_boms_exclude_part")
        resetmaingrid(lblbomid.Text.ToString)
    End Sub
    Protected Sub vscopybom_rowdeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vscopyboms.DataKeys(i).Values("id").ToString)
        Dim msg As String = GF.DeleteData("s_boms_reference", "id=" + id.ToString)
        'Dim deleteid As Integer = GF.AutoGenId("id", "s_boms_exclude_part")
        'msg = GF.InsertData("ID,BOMID,PARTID,BOM_REFERENCE_ID", deleteid.ToString + "," + lblbomid.Text.ToString + "," + partid.ToString + "," + cmbmodelname.SelectedValue.ToString, "s_boms_exclude_part")
        resetbomgrid()
    End Sub
    Protected Sub btncopy_click(ByVal sender As Object, ByVal e As EventArgs)
        ' Dim ds As DataTable = GF.GetData(False, "id,classid,CATEGORYID,partid,qty,unit,rate,sid,modelid", "s_boms_detail", "modelid=" + cmbmodelname.SelectedValue.ToString)

        Dim i As Integer
        Dim msg As String = String.Empty

        Dim id As Integer = GF.AutoGenId("id", "s_boms_reference")
        Dim refbomsid As String = GF.GetSingleValue("", False, "id", "s_boms_master", "modelid=" + cmbmodelname.SelectedValue.ToString)
        msg = GF.InsertData("ID,BOMID,REFERENCE_BOM_ID", id.ToString + "," + lblbomid.Text.ToString + "," + refbomsid.ToString, "s_boms_reference")

        Dim rate As Double = GF.GetSingleValue("", False, "rate", "s_boms_master", "id=" + refbomsid.ToString)
        msg = GF.GetSingleValue("count", False, "id", "s_boms_master", "id=" + lblbomid.Text.ToString)
        If msg = 0 Then
            bomsid = GF.AutoGenId("id", "s_boms_master")
            msg = GF.InsertData("Id,modelid,rate", lblbomid.Text.ToString + ",'" + cmbmodel.SelectedValue.ToString + "','" + rate.ToString + "'", "s_boms_master")
        Else
            Dim bomsrate As Double = GF.GetSingleValue("", False, "rate", "s_boms_master", "id=" + lblbomid.ToString)
            Dim newrate As Double = Convert.ToDouble(rate) + Convert.ToDouble(bomsrate)
            msg = GF.UpdateData("rate", "'" + newrate.ToString() + "'", "s_boms_master", "id=" + lblbomid.Text)
        End If
        resetbomgrid()
        resetmaingrid(lblbomid.Text.ToString)
        ShowMessage("Data copy successfully", MessageType.Success)
    End Sub

    Protected Sub btnSave_click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim ds As DataTable = GF.GetData(False, "ID,CLASSID,catid,PARTID,QTY,UNIT,RATE,SID,modelid", "s_boms_temp", "modelid=" + lblmodelid.Text.ToString)
        'Dim i As Integer
        'For i = 0 To ds.Rows.Count - 1
        '    Dim msg As String = String.Empty
        '    Dim id As Integer = GF.AutoGenId("id", "s_boms_detail")
        '    Dim classid As String = ds.Rows(i)("CLASSID").ToString
        '    Dim catid As String = ds.Rows(i)("catid").ToString
        '    Dim partid As String = ds.Rows(i)("partid").ToString
        '    Dim qty As Double = ds.Rows(i)("qty").ToString
        '    Dim unit As String = ds.Rows(i)("unit").ToString
        '    Dim rate As String = ds.Rows(i)("rate").ToString
        '    Dim sid As String = ds.Rows(i)("sid").ToString
        '    Dim modelid As String = ds.Rows(i)("modelid").ToString
        '    Dim coldata As String = id.ToString + "," + classid.ToString + "," + catid.ToString + "," + partid.ToString + ",'" + qty.ToString + "','" + unit.ToString + "','" + rate.ToString + "'," + sid.ToString + "," + modelid.ToString
        '    msg = GF.InsertData("id,classid,CATEGORYID,partid,qty,unit,rate,sid,modelid", coldata, "s_boms_detail")
        'Next
        'Dim totalrate As String = GF.GetSingleValue("Sum", True, "Rate", "s_boms_temp", "modelid= '" + lblmodelid.Text.ToString + "' and Is_Deleted <>1")
        'Dim abc As String = GF.UpdateData("BOmsRATE", totalrate, "s_model_master", "id=" + lblmodelid.Text.ToString)
        'Dim deletedata As String = GF.DeleteData("s_boms_temp", "modelid=" + lblmodelid.Text.ToString)
        'ShowMessage("Data insterted Successfully", MessageType.Success)
    End Sub
    Protected Sub btnview_click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("Reportviewer.aspx?id=" + lblbomid.Text.ToString)
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnSubmit.Enabled = True
        Try
            If cmbmodel.SelectedItem.Text = "select" Then
                ShowMessage("Please select Model name To create BOMS", MessageType.Error)
                Exit Sub
            End If
            If txtqty.Text = "" Then
                ShowMessage("Please Enter Value for QTY", MessageType.Error)
                Exit Sub
            ElseIf cmbunit.SelectedItem.Text = "Select" Then
                ShowMessage("Please select Value for unit", MessageType.Error)
                Exit Sub
            ElseIf cmbsupplier.SelectedItem.Text = "Select" Then
                ShowMessage("Please select Value for SupplierName", MessageType.Error)
                Exit Sub
            End If

            Dim modelid As String = cmbmodel.SelectedValue.ToString
            Dim clsid As String = cmbclass.SelectedValue.ToString
            Dim classname As String = cmbclass.SelectedItem.Text
            Dim catid As String = cmbcategory.SelectedValue.ToString
            Dim catname As String = cmbcategory.SelectedItem.ToString
            Dim partid As String = cmbpartname.SelectedValue.ToString
            Dim partname As String = cmbpartname.SelectedItem.ToString
            Dim qty As String = txtqty.Text
            Dim rate As Double = CDbl(txtrate.Text) * CDbl(qty)
            Dim unit As String = cmbunit.SelectedItem.ToString
            Dim sid As String = cmbsupplier.SelectedValue.ToString

            Dim str As String = GF.GetSingleValue("count", True, "bd.id", "inner join s_boms_detail bd on bd.bomid = ref.reference_bom_id", "ref.bomid = " + lblbomid.Text.ToString + " and bd.partid = " + partid.ToString)
            If str <> 0 Then
                ShowMessage("This Part is already Exists", MessageType.Error)
                cmbpartname.SelectedIndex = 0
                txtqty.Text = ""
                txtrate.Text = ""
                cmbunit.SelectedIndex = 0
                cmbsupplier.SelectedIndex = 0
                Exit Sub
            End If
            Dim msg As String
            Dim id As Integer = GF.AutoGenId("id", "s_boms_detail")
            Dim coldata As String = id.ToString + "," + lblbomid.Text.ToString + "," + clsid.ToString + "," + catid.ToString + "," + partid.ToString + ",'" + qty.ToString + "','" + unit.ToString + "','" + rate.ToString + "'," + sid.ToString + "," + modelid.ToString
            msg = GF.InsertData("id,bomid,classid,CATEGORYID,partid,qty,unit,rate,sid,modelid", coldata, "s_boms_detail")

            Dim totalrate As String = GF.GetSingleValue("Sum", True, "Rate", "s_boms_detail", "bomid=" + lblbomid.Text.ToString + " and Is_Deleted <>1")

            Dim count As String
            count = GF.GetSingleValue("count", False, "id", "s_boms_master", "id=" + lblbomid.Text.ToString)
            If count = 0 Then
                Dim bomrate As Double = Convert.ToDouble(totalrate)
                bomsid = GF.AutoGenId("id", "s_boms_master")
                msg = GF.InsertData("Id,modelid,rate", lblbomid.Text.ToString + ",'" + cmbmodel.SelectedValue.ToString + "','" + bomrate.ToString + "'", "s_boms_master")
            Else
                Dim bomsrate As Double = GF.GetSingleValue("", False, "rate", "s_boms_master", "id=" + lblbomid.Text.ToString)
                Dim newrate As Double = Convert.ToDouble(totalrate) + Convert.ToDouble(bomsrate)
                msg = GF.UpdateData("rate", "'" + newrate.ToString() + "'", "s_boms_master", "id=" + lblbomid.Text.ToString)
            End If

            count = GF.GetSingleValue("count", False, "id", "s_boms_reference", "BOMID=" + lblbomid.Text.ToString + " and REFERENCE_BOM_ID=" + lblbomid.Text.ToString)
            If count = 0 Then
                Dim refid As Integer = GF.AutoGenId("id", "s_boms_reference")
                msg = GF.InsertData("ID,BOMID,REFERENCE_BOM_ID", refid.ToString + "," + lblbomid.Text.ToString + "," + lblbomid.Text.ToString, "s_boms_reference")
            End If
            resetmaingrid(lblbomid.Text.ToString)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

  
End Class
