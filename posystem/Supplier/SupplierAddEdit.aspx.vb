Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class Supplier_SupplierAddEdit
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Dim oldname1 As String = String.Empty
    Public terms As String = String.Empty
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtname.Text = "" Then
            ShowMessage("Please Enter Supplier Name", MessageType.Warning)
            Exit Sub
        End If
        Dim sname As String = Replace(Trim$(txtname.Text), "'", "''")
        Dim address As String = Replace(Trim$(txtaddress.Text), "'", "''")
        Dim city As String = Replace(Trim$(txtcity.Text), "'", "''")
        Dim cnphoneno As String = Replace(Trim$(txtmobile.Text), "'", "''")
        Dim cnname As String = Replace(Trim$(txtcnname.Text), "'", "''")
        Dim phone As String = Replace(Trim$(txtphone.Text), "'", "''")
        Dim tin As String = Replace(Trim$(txttin.Text), "'", "''")
        Dim cst As String = Replace(Trim$(txtcst.Text), "'", "''")
        Dim eccno As String = Replace(Trim$(txteccno.Text), "'", "''")
        Dim email As String = Replace(Trim$(txtemail.Text), "'", "''")
        Dim cls As String = Replace(Trim$(txtclass.Text), "'", "''")
        Dim msg As String = ""
        Dim Type As String = ""

        Try
            If btnSubmit.Text = "Save" Then

                Dim count As String = GF.GetSingleValue("count", False, "id", "s_supplier_master", "LTrim(RTrim(name))='" + Trim(txtname.Text) + "'")
                If CInt(count) > 0 Then
                    ShowMessage("Supplier name is allready exists", MessageType.Warning)
                    Exit Sub
                End If
                Dim id As String = GF.AutoGenId("id", "s_supplier_master")
                msg = GF.InsertData("ID,NAME,ADDRESS,CITY,TIN,CST,ECCno,PHONE,Cnname,cnphoneno,EMAIL,class,terms", id + ",'" + sname + "','" + address + "','" + city + "','" + tin + "','" + cst + "','" + eccno + "','" + phone + "','" + cnname + "','" + cnphoneno + "','" + email + "','" + cls.ToString + "','" + terms + "'", "s_supplier_master")
                Type = "Add"
                ShowMessage("Insert " + msg, MessageType.Info)
                'If MsgBox("Do You Want To " + Type + " Supplier Model?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Rotech") = MsgBoxResult.Yes Then
                '    cmbclass.Focus()
                '    txtrate.Text = ""
                '    Exit Sub
                'End If


            Else

                Dim count As String = GF.GetSingleValue("count", False, "id", "s_supplier_master", "LTrim(RTrim(name))='" + Trim(txtname.Text) + "'")
                'If CInt(count) > 0 And oldname <> Trim(txtClientname.Text) Then
                '    MsgBox("Client name allready exists", , gf.title)
                '    Exit Sub
                'End If
                'oldname = ""
                msg = GF.UpdateData("NAME|ADDRESS|CITY|TIN|CST|ECCno|PHONE|Cnname|cnphoneno|EMAIL|class|terms|MODIFIED_DATE", "'" + sname + "'|'" + address + "'|'" + city + "'|'" + tin + "'|'" + cst + "'|'" + eccno + "'|'" + phone + "'|'" + cnname + "'|'" + cnphoneno + "'|'" + email + "'|'" + cls.ToString + "'|'" + terms + "'|'" + Format(Today.Date, "yyyy-MM-dd") + "'", "s_supplier_master", "id=" + lblid.Text.ToString)
                Type = "Update"
                ShowMessage("Update " + msg, MessageType.Info)
                'If MsgBox("Do You Want To " + Type + " Supplier Model?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Delete") = MsgBoxResult.Yes Then
                '    cmbclass.Focus()
                '    txtrate.Text = ""
                '    Exit Sub
                'End If
            End If


            'Response.Redirect("SupplierMaster.aspx", True)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim type As String = Request.QueryString("type")
            FillDropDown()
            If type = "Add" Then
                FillSuppliermodel()
            ElseIf type = "Edit" Then
                filldata()
                FillSuppliermodel()
            End If
        End If
    End Sub
    Public Sub FillDropDown()
        Try
            Dim ds As New DataTable()
            ds = GF.GetData(True, "id,classname", "s_class_master", "Is_DELETED<>1", False, "classname asc")
            cmbclass.DataTextField = "className"
            cmbclass.DataValueField = "id"
            cmbclass.DataSource = ds
            cmbclass.DataBind()
            cmbclass.Items.Insert(0, "Select")
            cmbclass.SelectedIndex = 0

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Private Function getTerms(ByVal data As String) As String
        If data = "against" Then
            against.Checked = True
            Return against.Text
        ElseIf data = "Immediate" Then
            Immediate.Checked = True
            Return Immediate.Text
        ElseIf data = "days15" Then
            days15.Checked = True
            Return days15.Text
        ElseIf data = "days30" Then
            days30.Checked = True
            Return days30.Text
        ElseIf data = "days45" Then
            days45.Checked = True
            Return days45.Text
        ElseIf data = "days60" Then
            days60.Checked = True
            Return days60.Text
        ElseIf data = "days90" Then
            days90.Checked = True
            Return days90.Text
        End If
        days90.Checked = True
        days90.Checked = False
        Return ""
    End Function
    Public Sub FillSuppliermodel()
        Try
            Dim sid As String
            sid = Request.QueryString("id")
            Dim ds As New DataTable
            If String.IsNullOrEmpty(sid) Then
                sid = 0
                ds = GF.GetData(False, "sp.ID as id,sp.PART_ID as partid,pm.PARTNAME as partname,clm.CLASSNAME as classname,catm.CATEGORYNAME as categoryname,sp.rate as Rate", "s_part_master pm,s_class_master clm,s_category_master catm,s_supplier_part sp", "sp.PART_ID=pm.ID and pm.CLASSID = clm.ID and pm.CATAGORYID = catm.ID AND pm.IS_DELETED <> 1 and sp.SUPPLIER_ID =" + sid.ToString)

                ds.Rows.Add(ds.NewRow())
                vssuppliermodel.DataSource = ds
                vssuppliermodel.DataBind()
                Dim columncount As Integer = vssuppliermodel.Rows(0).Cells.Count
                vssuppliermodel.Rows(0).Cells.Clear()
                vssuppliermodel.Rows(0).Cells.Add(New TableCell())
                vssuppliermodel.Rows(0).Cells(0).ColumnSpan = columncount
                vssuppliermodel.Rows(0).Cells(0).Text = "No Records Found"
            Else

                ds = GF.GetData(False, "sp.ID as id,sp.PART_ID as partid,pm.PARTNAME as partname,clm.CLASSNAME as classname,catm.CATEGORYNAME as categoryname,sp.rate as Rate", "s_part_master pm,s_class_master clm,s_category_master catm,s_supplier_part sp", "sp.PART_ID=pm.ID and pm.CLASSID = clm.ID and pm.CATAGORYID = catm.ID AND sp.IS_DELETED <> 1 and sp.SUPPLIER_ID =" + sid.ToString)
                If (ds.Rows.Count > 0) Then
                    vssuppliermodel.DataSource = ds
                    vssuppliermodel.DataBind()
                End If

            End If
            'FillDropDown()
            updatesuppliermodel.Update()
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    
    Private Sub filldata()
        Dim sid As String
        sid = Request.QueryString("id")
        lblid.Text = sid.ToString
        Dim ds As DataTable
        ds = GF.GetData(False, "NAME,ADDRESS,CITY,TIN,CST,ECCno,PHONE,Cnname,cnphoneno,EMAIL,Terms,Class", "S_supplier_master", "id=" + sid.ToString)
        If ds.Rows.Count > 0 Then
            If IsDBNull(ds.Rows(0)("NAME")) = True Then
                txtname.Text = ""
            Else
                txtname.Text = ds.Rows(0)("NAME")
            End If

            If IsDBNull(ds.Rows(0)("Address")) = True Then
                txtaddress.Text = ""
            Else
                txtaddress.Text = ds.Rows(0)("Address")
            End If

            If IsDBNull(ds.Rows(0)("City")) = True Then
                txtcity.Text = ""
            Else
                txtcity.Text = ds.Rows(0)("city")
            End If
            If IsDBNull(ds.Rows(0)("tin")) = True Then
                txttin.Text = ""
            Else
                txttin.Text = ds.Rows(0)("tin")
            End If

            If IsDBNull(ds.Rows(0)("Cst")) = True Then
                txtcst.Text = ""
            Else
                txtcst.Text = ds.Rows(0)("Cst")
            End If

            If IsDBNull(ds.Rows(0)("eccno")) = True Then
                txteccno.Text = ""
            Else
                txteccno.Text = ds.Rows(0)("eccno")
            End If

            If IsDBNull(ds.Rows(0)("Cnname")) = True Then
                txtcnname.Text = ""
            Else
                txtcnname.Text = ds.Rows(0)("Cnname")
            End If
            If IsDBNull(ds.Rows(0)("cnphoneno")) = True Then
                txtmobile.Text = ""
            Else
                txtmobile.Text = ds.Rows(0)("cnphoneno")
            End If

            If IsDBNull(ds.Rows(0)("phone")) = True Then
                txtphone.Text = ""
            Else
                txtphone.Text = ds.Rows(0)("phone")
            End If

            If IsDBNull(ds.Rows(0)("email")) = True Then
                txtemail.Text = ""
            Else
                txtemail.Text = ds.Rows(0)("email")
            End If
            If IsDBNull(ds.Rows(0)("Class")) = True Then
                txtclass.Text = ""
            Else
                txtclass.Text = ds.Rows(0)("Class")
            End If
        End If
        Dim term As String = getTerms(ds.Rows(0)("Terms"))

        FillSuppliermodel()
        btnSubmit.Text = "Update"
    End Sub

    Public Sub clearTextbox()
        'Try
        '    For Each txtBox As Control In Panel2.Controls

        '        If (txtBox.ID.StartsWith("txt")) AndAlso (TypeOf txtBox Is TextBox) Then
        '            Dim txt As TextBox = DirectCast(txtBox, TextBox)
        '            If String.IsNullOrEmpty(txt.Text) = False Then
        '                txt.Text = ""
        '            End If
        '        End If
        '    Next txtBox
        'Catch ex As Exception
        '    MsgBox(ex.Message, , GF.Title)
        'End Try
    End Sub
    Public Sub clearsuppliermodel()
        'cmbclass.SelectedItem.Text = "Select"
        'cmbcategory.SelectedItem.Text = "Select"
        'cmbpartname.SelectedItem.Text = "Select"
        If String.IsNullOrEmpty(txtrate.Text) = False Then
            txtrate.Text = ""
        End If
    End Sub
    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Try
            If (btnadd.Text = "Add") Then
                If txtrate.Text = "" Then
                    ShowMessage("Please Enter Price", MessageType.Warning)
                    txtrate.Focus()
                    Exit Sub
                End If
                AddUpdatesupplierModel("A")
            ElseIf (btnadd.Text = "Update") Then
                AddUpdatesupplierModel("E")
            End If

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Private Sub AddUpdatesupplierModel(ByVal Operation As String)
        Dim Message As String = String.Empty

        If (Operation = "A") Then
            If lblid.Text <> "" Then
                Dim str As String
                str = GF.GetSingleValue("COUNT", False, "partid", "s_supplier_part", "ltrim(rtrim(supplierid))=" + lblid.Text.ToString() + " and ltrim(rtrim(partid))=" + cmbpartname.SelectedValue.ToString)

                If String.IsNullOrEmpty(str) = False Then
                    ShowMessage("Please Fill First Supplier Detail and Save", MessageType.Info)
                    cmbpartname.SelectedIndex = 0
                    cmbpartname.Focus()
                    Exit Sub
                End If
                Dim supplier_partid As String = GF.AutoGenId("ID", "s_supplier_part")

                Message = GF.InsertData("ID,SUPPLIER_ID,PART_ID,Rate", supplier_partid + "," + lblid.Text + ", " + cmbpartname.SelectedValue.ToString + ", '" + txtrate.Text + "'", "s_Supplier_part")
                FillSuppliermodel()
                If MsgBox("Do You Want to add more part?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Add part") = MsgBoxResult.No Then
                    Response.Redirect("SupplierMaster.aspx", True)
                Else
                    cmbpartname.SelectedIndex = 0
                    cmbcategory.SelectedIndex = 0
                    cmbclass.SelectedIndex = 0
                    txtrate.Text = ""
                    btnadd.Text = "&Add"
                End If
            Else
                ShowMessage("Please Fill First Supplier Detail and Save", MessageType.Info)
                btnSubmit.Focus()
            End If
        ElseIf (Operation = "E") Then
            If oldname1 <> cmbclass.Text And btnadd.Text = "&Update" Then
                Dim str As String
                str = GF.GetSingleValue("COUNT", False, "partid", "s_supplier_part", "ltrim(rtrim(supplierid))=" + lblid.Text.ToString() + " and ltrim(rtrim(partid))=" + cmbpartname.SelectedValue.ToString)
               
                If String.IsNullOrEmpty(str) = False Then
                    ShowMessage("This Part is Allready Exists", MessageType.Warning)
                    cmbpartname.SelectedIndex = 0
                    cmbpartname.Focus()
                    Exit Sub
                End If
            End If

            Message = GF.UpdateData("PART_ID|Rate", cmbpartname.SelectedValue.ToString + "|'" + txtrate.Text + "'", "s_Supplier_part", "id=" + lblspid.Text.ToString + "")
            FillSuppliermodel()
            If MsgBox("Do You Want to Update more part?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Update part") = MsgBoxResult.No Then
                Response.Redirect("SupplierMaster.aspx", True)
            Else
                cmbpartname.SelectedIndex = 0
                cmbcategory.SelectedIndex = 0
                cmbclass.SelectedIndex = 0
                txtrate.Text = ""
                btnadd.Text = "&Add"
            End If
        End If
    End Sub
    Private Sub terms_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles against.CheckedChanged, days15.CheckedChanged, days30.CheckedChanged, days45.CheckedChanged, days60.CheckedChanged, days90.CheckedChanged, Immediate.CheckedChanged, against.CheckedChanged
        terms = sender.id
    End Sub
   
   

    Protected Sub cmbclass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
             fillcategory_data(cmbclass.SelectedValue.ToString)

        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbcategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim classid As Integer = cmbclass.SelectedItem.Value.ToString
            Dim categoryid As Integer = cmbcategory.SelectedItem.Value.ToString

            fillpart_data(classid, categoryid)
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub vssuppliermodel_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles vssuppliermodel.RowEditing
        Dim i As Integer = e.NewEditIndex
        Dim id As String = Convert.ToInt32(vssuppliermodel.DataKeys(e.NewEditIndex).Values("id").ToString)
        lblspid.Text = ID.ToString
        Dim ds As DataTable
        ds = GF.GetData(False, "clm.id as classid,sp.PART_ID as partid,pm.PARTNAME as partname,clm.CLASSNAME as classname,catm.id as catid,catm.CATEGORYNAME as categoryname,sp.rate as srate", "s_part_master pm,s_class_master clm,s_category_master catm,s_supplier_part sp", "sp.PART_ID=pm.ID and pm.CLASSID = clm.ID and pm.CATAGORYID = catm.ID AND pm.IS_DELETED <> 1 and sp.id=" + id.ToString)

        If ds.Rows.Count > 0 Then

            If IsDBNull(ds.Rows(0)("classid")) = True Then
                cmbclass.Text = ""
            Else
                cmbclass.SelectedValue = ds.Rows(0)("classid")
                fillcategory_data(ds.Rows(0)("classid").ToString)
            End If
            If IsDBNull(ds.Rows(0)("categoryname")) = True Then
                cmbcategory.SelectedItem.Text = ""
            Else
                cmbcategory.SelectedItem.Text = ds.Rows(0)("categoryname")
                fillpart_data(ds.Rows(0)("classid").ToString, ds.Rows(0)("catid").ToString)
            End If
            cmbpartname.SelectedValue = ds.Rows(0)("partid")
            If IsDBNull(ds.Rows(0)("srate")) = True Then
                txtrate.Text = ""
            Else
                txtrate.Text = ds.Rows(0)("srate")
            End If
        End If
        btnAdd.Text = "Update"
        oldname1 = ds.Rows(0)("partname")
        Updatesupplier.Update()
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
    Protected Sub vssuppliermodel_rowdeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim i As Integer = e.RowIndex
        Dim id As Integer = Convert.ToInt32(vssuppliermodel.DataKeys(e.RowIndex).Values("id").ToString)
        'Dim partid As String = vssuppliermodel.DataKeys(i).Values(1).ToString()

        Dim msg As String = GF.UpdateData("Is_Deleted", "1", "s_supplier_part", "Id=" + id.ToString)
        If msg = "Success" Then
            FillSuppliermodel()
            'lblresult.Text = catagory + " Details Deleted successfully"
        End If
    End Sub

End Class
