Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Imports System.DBNull
Imports System.Data.OleDb
Partial Class Model_ModelAddEdit
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Filldropdown()
            Dim type As String = Request.QueryString("type")
            If type = "New" Then

            ElseIf type = "Edit" Then
                filldata()
            End If
        End If
    End Sub

  
    Public Sub FillDropDown()
        Try
            Dim ds As New DataTable()
            ds = GF.GetData(True, "id,producttype", "s_producttype_master", "Is_DELETED<>1", False, "producttype asc")
            cmbproduct.DataTextField = "producttype"
            cmbproduct.DataValueField = "id"
            cmbproduct.DataSource = ds
            cmbproduct.DataBind()
            cmbproduct.Items.Insert(0, "Select")
            cmbproduct.SelectedIndex = 0

            ds = GF.GetData(True, "id,stackname", "s_stack_master", "Is_DELETED<>1", False, "stackname asc")
            cmbstack.DataTextField = "stackname"
            cmbstack.DataValueField = "id"
            cmbstack.DataSource = ds
            cmbstack.DataBind()
            cmbstack.Items.Insert(0, "Select")
            cmbstack.SelectedIndex = 0

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Rotech")
        End Try
    End Sub
    Private Sub filldata()
        Dim catid As String
        catid = Request.QueryString("id")
        lblid.Text = catid.ToString
        Dim ds As DataTable
        ds = GF.GetData(False, "mm.ModelName as modelname,mm.producttypeid as productid,mm.stackid as stackid,pm.ProductType as productname,sm.StackName as stackname,mm.rate as rate", "s_producttype_master pm,s_stack_master sm,s_model_master mm", "mm.producttypeid= pm.id and mm.stackid= sm.id and mm.IS_DELETED=0 and mm.id=" + lblid.Text)
        If ds.Rows.Count > 0 Then
            If IsDBNull(ds.Rows(0)("modelname")) = True Then
                txtmodelname.Text = ""
            Else
                txtmodelname.Text = ds.Rows(0)("modelname")
            End If

            If IsDBNull(ds.Rows(0)("productid")) = True Then
                cmbproduct.Text = ""
            Else
                Dim productid As Integer = ds.Rows(0)("productid")
                cmbproduct.SelectedValue = productid.ToString
                'Dim classtype As String = GF.GetSingleValue("", False, "classname", "s_class_master", "id=" + classid.ToString)
                'cmbclass.SelectedItem.Text = classtype.ToString
            End If
            If IsDBNull(ds.Rows(0)("stackid")) = True Then
                cmbstack.Text = ""
            Else
                Dim stackid As Integer = ds.Rows(0)("stackid")
                cmbstack.SelectedValue = stackid.ToString
                'Dim classtype As String = GF.GetSingleValue("", False, "classname", "s_class_master", "id=" + classid.ToString)
                'cmbclass.SelectedItem.Text = classtype.ToString
            End If


        End If
        btnSave.Text = "Update"
    End Sub




    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtmodelname.Text = "" Then
            MsgBox("Please Enter Model Name", , GF.Title)
            txtmodelname.Focus()
            Exit Sub
        ElseIf cmbproduct.Text = "Select" Then
            MsgBox("Please Select Product type", , GF.Title)
            cmbproduct.Focus()
            Exit Sub
        End If

        Dim Modelname As String = Replace(Trim$(txtmodelname.Text), "'", "''")
        Dim producttype As String = Replace(Trim$(cmbproduct.SelectedItem.Text), "'", "''")
        Dim stack As Integer = cmbstack.SelectedItem.Value.ToString
        Dim msg As String = ""
        Dim Type As String = ""

        Try
            If btnSave.Text = "Save" Then
                Dim count As String = GF.GetSingleValue("", False, "id", "s_Model_master", "LTrim(RTrim(modelname))='" + Trim(Modelname) + "'")

                If String.IsNullOrEmpty(count) = False Then
                    MsgBox("Model name is allready exists", , GF.Title)
                    Exit Sub
                End If
                Dim id As String = GF.AutoGenId("id", "s_model_master")
                msg = GF.InsertData("ID,MODELNAME,PRODUCTTYPEID,STACKID,RATE", id.ToString + ",'" + txtmodelname.Text + "'," + cmbproduct.SelectedValue.ToString + "," + cmbstack.SelectedValue.ToString + ",'" + txtrate.text + "'", "s_model_master")
                If msg = "Success" Then
                    ' lblresult.Text = "Data Inserted Successfully"
                End If
                Type = "Add"
            Else
                Dim count As String = GF.GetSingleValue("", False, "id", "s_Model_master", "LTrim(RTrim(modelname))='" + Trim(Modelname) + "'")
                If String.IsNullOrEmpty(count) = False Then
                    MsgBox("Model name is allready exists", , GF.Title)
                    Exit Sub
                End If
                'oldname = ""
                ID = lblid.Text.ToString
                msg = GF.UpdateData("ID|MODELNAME|PRODUCTTYPEID|STACKID|RATE", ID.ToString + "|'" + txtmodelname.Text + "'|" + cmbproduct.SelectedValue.ToString + "|" + cmbstack.SelectedValue.ToString + "|'" + txtRate.Text + "'", "s_model_master", "id=" + lblid.Text.ToString)
                If msg = "Success" Then
                    '  lblresult.Text = "Data Updated Successfully"
                End If
                Type = "Update"
            End If
            Response.Redirect("ModelMaster.aspx", False)
        Catch ex As Threading.ThreadAbortException
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub

End Class
