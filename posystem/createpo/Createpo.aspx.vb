Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.DBNull
Imports System.Collections.Generic
Imports System.Web.UI
Imports GlobalFunctions
Imports NumtoWord
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Partial Class Po_Createpo
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Dim nw As New NumtoWord
    Dim strpo As String = String.Empty
    Dim subtotal, totalduty As Integer
    Dim exise As String = 0
    Dim subtot, vat, addtx, otherchrge As Integer
    Dim i As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            lblpono.Text = createnewpo()
            FillDropDown()
            SetInitialRow()


        End If
    End Sub

    Private Function createnewpo() As String
        Dim pono As String
        'Dim date1 As Date = "01-04-2015"
        'Dim date2 As Date = "31-03-2016"

        'Dim year As String = date1.ToString("yy") & date2.ToString("yy")
        ' Dim strsql As String = GF.GetSingleValue("MAX", False, "pono", "s_po_master", "podate >= Datevalue('" + date1 + "') and podate < Datevalue('" + date2 + "')")
        Dim strsql As String = GF.GetSingleValue("MAX", False, "pono", "s_po_master", "")
        Dim upperno As Integer
        Dim lowerno As String
        If String.IsNullOrEmpty(strsql) = True Then
            upperno = 1516
            lowerno = "0001"
        Else
            upperno = Split(strsql, "/")(0)
            lowerno = Split(strsql, "/")(1)
            lowerno = CInt(lowerno) + 1
        End If

        If lowerno.Length = 1 Then
            lowerno = "000" + lowerno
        ElseIf lowerno.Length = 2 Then
            lowerno = "00" + lowerno
        ElseIf lowerno.Length = 3 Then
            lowerno = "0" + lowerno
        End If


        pono = upperno.ToString + "/" + lowerno.ToString
        Return pono
    End Function
    Private Sub SetInitialRow()

        Dim dt As New DataTable()
        Dim dr As DataRow = Nothing
        dt.TableName = "podetail"
        dt.Columns.Add(New DataColumn("partid", GetType(Integer)))
        dt.Columns.Add(New DataColumn("partname", GetType(String)))
        dt.Columns.Add(New DataColumn("qty", GetType(String)))
        dt.Columns.Add(New DataColumn("unit", GetType(String)))
        dt.Columns.Add(New DataColumn("rate", GetType(String)))
        dt.Columns.Add(New DataColumn("amount", GetType(String)))
        ViewState("podetail") = dt
        Dim id As Integer = GF.AutoGenId("id", "s_po_master")

        lblpoid.Text = id.ToString
        ' dt = GF.GetData(False, "partname,qty,unit,rate,amount", "s_po_detail", "po_id=" + lblpoid.Text.ToString)

        If (dt.Rows.Count > 0) Then
            vscreatepo.DataSource = dt
            vscreatepo.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            vscreatepo.DataSource = dt
            vscreatepo.DataBind()
            'Dim columncount As Integer = vscreatepo.Rows(0).Cells.Count
            'vscreatepo.Rows(0).Cells.Clear()
            'vscreatepo.Rows(0).Cells.Add(New TableCell())
            'vscreatepo.Rows(0).Cells(0).ColumnSpan = columncount
            'vscreatepo.Rows(0).Cells(0).Text = "No Records Found"
        End If
    End Sub
    Private Sub resetmaingrid()
        Try
            Dim curdt As DataTable = ViewState("podetail")
            vscreatepo.DataSource = curdt
            vscreatepo.DataBind()
        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

    Public Sub FillDropDown()
        Try
            Dim ds As DataTable
            ds = GF.GetData(True, "id,Name", "s_supplier_master", "Is_DELETED<>1", False, "Name asc")
            cmbsupplier.DataTextField = "Name"
            cmbsupplier.DataValueField = "id"
            cmbsupplier.DataSource = ds
            cmbsupplier.DataBind()
            cmbsupplier.Items.Insert(0, New ListItem("Select", "0"))

            ds = GF.GetData(True, "id,Transpotname", "s_transpoter", "Is_DELETED<>1", False, "Transpotname asc")
            cmbtranspoter.DataTextField = "Transpotname"
            cmbtranspoter.DataValueField = "id"
            cmbtranspoter.DataSource = ds
            cmbtranspoter.DataBind()
            cmbtranspoter.Items.Insert(0, New ListItem("Select", "0"))

            ds = GF.GetData(True, "id,Name", "s_Owner_master", "Is_DELETED<>1", False, "Name asc")
            cmbdeliveryat.DataTextField = "Name"
            cmbdeliveryat.DataValueField = "id"
            cmbdeliveryat.DataSource = ds
            cmbdeliveryat.DataBind()
            cmbdeliveryat.Items.Insert(0, New ListItem("Select", "0"))

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Protected Sub OnDataBound(ByVal sender As Object, ByVal e As EventArgs)
        bindsupplierpart()
        Dim ddlunit As DropDownList = TryCast(vscreatepo.FooterRow.FindControl("cmbunit"), DropDownList)
        ddlunit.DataSource = GF.GetData(False, "id,UNITTYPE", "s_unit", " Is_Deleted<>1")
        ddlunit.DataTextField = "UNITTYPE"
        ddlunit.DataValueField = "id"
        ddlunit.DataBind()
        ddlunit.Items.Insert(0, New ListItem("Select", "0"))
    End Sub
    Public Sub bindsupplierpart()
        If cmbsupplier.SelectedItem.Text <> "Select" Then
            Dim ddl1 As DropDownList = TryCast(vscreatepo.FooterRow.FindControl("cmbpartname"), DropDownList)
            ddl1.DataSource = GF.GetData(False, "ssp.PART_ID as partid, spm.PARTNAME as partname ", "s_supplier_part ssp,s_part_master spm", "ssp.PART_ID= spm.ID and ssp.SUPPLIER_ID= " + cmbsupplier.SelectedValue.ToString)
            ddl1.DataTextField = "partname"
            ddl1.DataValueField = "partid"
            ddl1.DataBind()
            ddl1.Items.Insert(0, New ListItem("Select", "0"))
            Dim ds As DataTable = GF.GetData(True, "terms", "s_supplier_master", "id =" + cmbsupplier.SelectedValue.ToString)
            txtterms.Text = ds.Rows(0)("terms")
        End If
    End Sub

    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Protected Sub vscreatepo_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If (e.CommandName.Equals("AddNew") = True) Then
            Dim cmbpartname As DropDownList = TryCast(vscreatepo.FooterRow.FindControl("cmbpartname"), DropDownList)
            Dim txtqty As System.Web.UI.WebControls.TextBox = TryCast(vscreatepo.FooterRow.FindControl("txtpartqty"), System.Web.UI.WebControls.TextBox)
            Dim cmbunit As DropDownList = TryCast(vscreatepo.FooterRow.FindControl("cmbunit"), DropDownList)
            If cmbpartname.Text = "" Then
                ShowMessage("Please Select Supplier Name", MessageType.Error)
                Exit Sub
            End If
            If cmbpartname.SelectedItem.Text = "Select" Then
                ShowMessage("Please Select Data for partname", MessageType.Error)
                Exit Sub
            ElseIf txtqty.Text = vbNull Then
                ShowMessage("Please Enter QTY", MessageType.Error)
                Exit Sub
            ElseIf cmbunit.SelectedItem.Text = "Select" Then
                ShowMessage("Please Select Data for Unit", MessageType.Error)
                Exit Sub
            End If

            If (ViewState("podetail") IsNot Nothing) Then
                Dim partid As Integer = Replace(Trim$(TryCast(vscreatepo.FooterRow.FindControl("cmbpartname"), DropDownList).SelectedValue.ToString), "'", "''")
                Dim partname As String = Replace(Trim$(TryCast(vscreatepo.FooterRow.FindControl("cmbpartname"), DropDownList).SelectedItem.Text), "'", "''")
                Dim partqty As String = TryCast(vscreatepo.FooterRow.FindControl("txtpartqty"), System.Web.UI.WebControls.TextBox).Text
                Dim partrate As String = TryCast(vscreatepo.FooterRow.FindControl("txtpartrate"), System.Web.UI.WebControls.TextBox).Text
                Dim amount As String = Convert.ToDouble(partqty) * Convert.ToDouble(partrate)
                Dim unit As String = TryCast(vscreatepo.FooterRow.FindControl("cmbunit"), DropDownList).SelectedItem.Text

                Dim curdt As DataTable = ViewState("podetail")
                Dim drcurdt As DataRow = Nothing
                Dim subtotal As Double = 0

                If curdt.Rows.Count > 0 Then
                    For i = 0 To curdt.Rows.Count - 1
                        drcurdt = curdt.NewRow()
                        drcurdt("partid") = partid
                        drcurdt("partname") = partname
                        drcurdt("qty") = partqty
                        drcurdt("unit") = unit
                        drcurdt("rate") = partrate
                        drcurdt("amount") = amount
                    Next
                    If curdt.Rows(0)(0).ToString() = "" Then
                        curdt.Rows(0).Delete()
                        curdt.AcceptChanges()
                    End If
                End If
                curdt.Rows.Add(drcurdt)
                For i = 0 To curdt.Rows.Count - 1
                    subtotal = subtotal + curdt.Rows(i)(5).ToString
                Next
                lblsubtotal.Text = subtotal.ToString
                ViewState("podetail") = curdt
                vscreatepo.DataSource = curdt
                vscreatepo.DataBind()
                Updategridview.Update()
                lblsubtotal.Text = subtotal.ToString
                Updatepanelcontrol.Update()
            End If
        End If
    End Sub

    Protected Sub vscreatepo_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim curdt As New DataTable
        curdt = ViewState("podetail")
        Dim i As Integer = e.RowIndex
        curdt.Rows.RemoveAt(i)
        curdt.AcceptChanges()
        ViewState("podetail") = curdt
        resetmaingrid()
    End Sub
   

    Protected Sub cmbsupplier_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        bindsupplierpart()
    End Sub


    Protected Sub cmbpartname_selectedindexchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim partid As String = TryCast(vscreatepo.FooterRow.FindControl("cmbpartname"), DropDownList).SelectedItem.Value
        Dim ds As DataTable
        ds = GF.GetData(False, "ID,rate", "s_supplier_part", "PART_ID = " + partid.ToString() + " and SUPPLIER_ID= " + cmbsupplier.SelectedValue.ToString + " and IS_DELETED <>1")
        Dim txtrate As System.Web.UI.WebControls.TextBox = TryCast(vscreatepo.FooterRow.FindControl("txtpartrate"), System.Web.UI.WebControls.TextBox)
        txtrate.Text = ds.Rows(0)("rate").ToString

    End Sub
    Protected Sub txtpartqty_textchanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtrate As System.Web.UI.WebControls.TextBox = TryCast(vscreatepo.FooterRow.FindControl("txtpartrate"), System.Web.UI.WebControls.TextBox)
        Dim txtqty As System.Web.UI.WebControls.TextBox = TryCast(vscreatepo.FooterRow.FindControl("txtpartqty"), System.Web.UI.WebControls.TextBox)
        Dim amt As Double = Convert.ToDouble(txtrate.Text) * Convert.ToDouble(txtqty.Text)
        Dim txtamt As System.Web.UI.WebControls.TextBox = TryCast(vscreatepo.FooterRow.FindControl("lblamt"), System.Web.UI.WebControls.TextBox)
        txtamt.Text = amt.ToString
    End Sub

    Protected Sub rblexise_selectedindexchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim txtval As Integer = rblexise.SelectedIndex
        If txtval = 0 Then
            exise = Math.Round((lblsubtotal.Text * 12.5) / 100)
            lblrate.Text = exise
        ElseIf txtval = 1 Then
            lblrate.Text = "As Applicable"
            exise = 0
        End If
        Updatepanelcontrol.Update()
    End Sub
    Protected Sub rblcal_selectedindexchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim taxcal As String = rblcal.SelectedItem.Text
        Dim taxval As Integer = rblcal.SelectedIndex
        Dim Vat, Addtax As Integer
        If (rblcal.SelectedIndex = 0) Then
            lblvat.Text = "VAT @12.5%"
            lbladl.Text = "Adl.Tax @2.5%"
        ElseIf (rblcal.SelectedIndex = 1) Then
            lblvat.Text = "C.S.T @2%"
            lbladl.Text = "Adl.Tax @0%"
        ElseIf (rblcal.SelectedIndex = 3) Then
            lblvat.Text = "VAT @4%"
            lbladl.Text = "Adl.Tax @1%"
        ElseIf (rblcal.SelectedIndex = 2) Then
            lblvat.Text = "C.S.T @15%"
            lbladl.Text = "Adl.Tax @0%"
        ElseIf (rblcal.SelectedIndex = 4) Then
            lblvat.Text = "VAT @0%"
            lbladl.Text = "Adl.Tax @0%"
        Else
            lblvat.Text = "Vat/CST@%"
            lbladl.Text = "Adl Tax@%"
        End If
        If taxval = 0 Then

            Vat = Math.Round((lblsubtotal.Text * 12.5) / 100)
            lblvatcst.Text = Vat
            Addtax = Math.Round((lblsubtotal.Text * 2.5) / 100)
            lbladltax.Text = Addtax
        ElseIf taxval = 1 Then
            Vat = Math.Round((lblsubtotal.Text * 2) / 100)
            lblvatcst.Text = Vat
            Addtax = 0
            lbladltax.Text = Addtax
        ElseIf taxval = 2 Then
            Vat = Math.Round((lblsubtotal.Text * 15) / 100)
            lblvatcst.Text = Vat
            lbladltax.Text = 0
        ElseIf taxval = 3 Then
            Vat = Math.Round((lblsubtotal.Text * 4) / 100)
            lblvatcst.Text = Vat
            Addtax = Math.Round((lblsubtotal.Text * 1) / 100)
            lbladltax.Text = Addtax
        ElseIf taxval = 4 Then
            Vat = 0
            Addtax = 0
            lblvatcst.Text = 0
            lbladltax.Text = 0
        ElseIf taxval = 5 Then
            Vat = Addtax = 0
            lbladltax.Text = "As Applicable"
            lblvatcst.Text = "As Applicable"
        End If
        Dim others As Double
        If txtotherchanrges.Text = Nothing Then
            others = 0
        Else
            others = CDbl(txtotherchanrges.Text)
        End If
        If rblexise.SelectedIndex = 1 Then
            exise = 0
        Else
            exise = CDbl(lblrate.Text)
        End If
        Dim total As Double
        total = CDbl(lblsubtotal.Text) + Vat + Addtax + others + exise
        lbltotal.Text = CDbl(total.ToString).ToString
        Updatepanelcontrol.Update()
    End Sub
    Protected Sub btncancle_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub formvalidation()
        If cmbsupplier.Text = "Select" Then
            Exit Sub
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '   Dim due_date As Date = Replace(Trim$(DateTime.Parse(Request.Form(txtduedate.UniqueID))), "'", "''")
        Try
            If cmbsupplier.SelectedItem.ToString = "Select" Then
                ShowMessage("Please select Suppliername", MessageType.Error)
                Exit Sub
                'ElseIf Due_Date.ToString = vbEmpty Then
                '    ShowMessage("Please select Due Date", MessageType.Error)
                '    Exit Sub

            End If
            Dim id As Integer = GF.AutoGenId("id", "s_po_master")
            Dim pono = createnewpo()
            Dim sid As String = cmbsupplier.SelectedValue.ToString
            Dim ds As DataTable = GF.GetData(False, "Name,Address,TIN,CST,Terms", "s_supplier_master", "id=" + sid.ToString)
            Dim sname As String = ds.Rows(0)("Name").ToString
            Dim saddress As String = ds.Rows(0)("Address").ToString
            Dim terms As String = Replace(Trim$(txtterms.Text), "'", "''")
            Dim stin As String = ds.Rows(0)("TIN").ToString
            Dim scst As String = ds.Rows(0)("cst").ToString
            Dim refno As String = Replace(Trim$(txtrefno.Text), "'", "''")
            Dim podate As Date = Today.Date
            Dim sp_instruct As String = Replace(Trim$(txtinstruct.Text), "'", "''")
            Dim due_date As String = Request.Form(txtduedate.UniqueID).ToString
            Dim refdate As String = Request.Form(txtrefdate.UniqueID).ToString


            Dim deliveryid As String = 1

            ds = GF.GetData(False, "Name,address", "s_owner_master", "id=" + deliveryid.ToString)
            Dim deliveryat As String = Replace(Trim$(ds.Rows(0)("Name").ToString), "'", "''")
            Dim deliveryaddress As String = Replace(Trim$(ds.Rows(0)("address").ToString), "'", "''")
            Dim pricebasis As String = Replace(Trim$(radioprice.SelectedValue.ToString), "'", "''")
            Dim transid As Integer = Replace(Trim$(cmbtranspoter.SelectedValue.ToString), "'", "''")
            Dim transname As String = Replace(Trim$(cmbtranspoter.SelectedItem.ToString), "'", "''")
            Dim signby As String = String.Empty



            Dim Rate1, Rate2 As String
            Rate1 = String.Empty
            Rate2 = String.Empty
            If (rblcal.SelectedIndex = 0) Then
                Rate1 = "VAT @12.5%"
                Rate2 = "Adl.Tax @2.5%"
            ElseIf (rblcal.SelectedIndex = 1) Then
                Rate1 = "C.S.T @2%"
                Rate2 = "Adl.Tax @0%"
            ElseIf (rblcal.SelectedIndex = 3) Then
                Rate1 = "VAT @4%"
                Rate2 = "Adl.Tax @1%"
            ElseIf (rblcal.SelectedIndex = 2) Then
                Rate1 = "C.S.T @15%"
                Rate2 = "Adl.Tax @0%"
            ElseIf (rblcal.SelectedIndex = 4) Then
                Rate1 = "VAT @0%"
                Rate2 = "Adl.Tax @0%"
            Else
                Rate1 = "VAT/CST@0%"
                Rate2 = "Adl Tax@%"
            End If


            Dim totalinwards As String = nw.AmtInWord(Convert.ToDouble(lbltotal.Text))
            'id, pono, supplierid,sname, saddress, Refrenceno, podate, Special_instruction,
            'Terms_of_payment, delivery_To, delivery_address,
            'due_date, Price_Basis, trans_id, trans_name, subtotal, lblvatcst,vatcst,lbladltax, adltax,total, Sign_by
            exise = lblrate.Text.ToString
            Dim columnname As String = "id, pono,supplierid,sname, saddress, Refrenceno,Refrencedate, podate, Special_instruction,Terms_of_payment, delivery_To, delivery_address,due_date, Price_Basis, trans_id, trans_name, subtotal,Exicesduty, lblvatcst,vatcst,lbladltax, adltax,othercharges,total,totalinwords,Sign_by"
            Dim columndata1 As String = id.ToString + ",'" + pono.ToString + "'," + sid.ToString + ",'" + sname.ToString + "','" + saddress + "','" + refno.ToString + "','" + refdate.ToString + "','" + Format(podate, "yyyy-MM-dd").ToString + "','" + sp_instruct.ToString + "',"
            Dim columndata2 As String = "'" + terms.ToString + "','" + deliveryat.ToString + "','" + deliveryaddress.ToString + "','" + due_date + "','" + pricebasis.ToString + "'," + transid.ToString + ",'" + transname.ToString + "',"
            Dim columndata3 As String = "'" + lblsubtotal.Text.ToString + "','" + exise.ToString + "','" + Rate1.ToString + "','" + lblvatcst.Text.ToString + "','" + Rate2 + "','" + lbladltax.Text.ToString + "','" + txtotherchanrges.Text.ToString + "','" + lbltotal.Text.ToString + "','" + totalinwards.ToString + "','" + signby.ToString + "'"
            Dim columndata As String = columndata1 + columndata2 + columndata3

            Dim msg As String = GF.InsertData(columnname, columndata, "s_po_master")
            If msg = "Success" Then
                Dim curdt As New DataTable
                Dim j As Integer
                curdt = ViewState("podetail")
                Dim partid, partname, qty, unit, rate, amount As String
                For i = 0 To curdt.Rows.Count - 1
                    Dim curdr As DataRow = curdt.Rows(i)
                    Dim podetailid As Integer = GF.AutoGenId("id", "s_po_detail")
                    partid = curdr("partid").ToString
                    partname = curdr("partname").ToString
                    qty = curdr("qty").ToString
                    unit = curdr("unit").ToString
                    rate = curdr("rate").ToString
                    amount = curdr("amount").ToString
                    Dim str As String = GF.GetSingleValue("", False, "CATEGORYNAME", "s_part_master pm,s_category_master cm", "cm.id= pm.CATAGORYID and  pm.ID =" + partid.ToString)

                    Dim msg1 As String = GF.InsertData("ID,po_id,catname,Partid,partname,Qty,unit,Rate,Amount", podetailid.ToString + "," + id.ToString + ",'" + str.ToString + "'," + partid + ",'" + partname + "','" + qty + "','" + unit + "','" + rate + "','" + amount.ToString + "'", "s_po_detail")
                Next
            Else
                ShowMessage("data not save", MessageType.Error)
            End If
            btnSuccess.Enabled = False
            Response.Redirect("Reportviwer.aspx?id=" + lblpoid.Text.ToString)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

End Class
