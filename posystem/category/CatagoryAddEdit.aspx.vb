Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Imports System.DBNull
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports System.Web.Services


Partial Class Part_CatagoryAddEdit
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Public ddl As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            FillDropDown()
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
            ds = GF.GetData(True, "id,classname", "s_class_master", "Is_DELETED<>1", False, "classname asc")
            cmbclass.DataTextField = "className"
            cmbclass.DataValueField = "id"
            cmbclass.DataSource = ds
            cmbclass.DataBind()
            cmbclass.Items.Insert(0, "Select")
            cmbclass.SelectedItem.Text = "Select"
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
    Private Sub filldata()
        Dim catid As String
        catid = Request.QueryString("id")
        lblid.Text = catid.ToString
        Dim ds As DataTable
        ds = GF.GetData(False, "CLASSID,CATEGORYNAME", "s_category_master", "id=" + catid.ToString)
        If ds.Rows.Count > 0 Then
            If IsDBNull(ds.Rows(0)("CATEGORYNAME")) = True Then
                txtcatagory.Text = ""
            Else
                txtcatagory.Text = ds.Rows(0)("CATEGORYNAME")
            End If

            If IsDBNull(ds.Rows(0)("classid")) = False Then
                Dim classid As Integer = ds.Rows(0)("classid")
                Dim classtype As String = GF.GetSingleValue("", False, "classname", "s_class_master", "id=" + classid.ToString)
                cmbclass.SelectedItem.Text = classtype.ToString
            End If
        End If
        btnSubmit.Text = "Update"
    End Sub

    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtcatagory.Text = "" Then
            ShowMessage("Please enter Category Name", MessageType.Warning)
            Exit Sub
        ElseIf cmbclass.SelectedItem.Text = "Select" Then
            ShowMessage("Please Select Class", MessageType.Warning)
            Exit Sub
        End If
        Dim categoryname As String = Replace(Trim$(txtcatagory.Text), "'", "''")
        Dim classtype As String = Replace(Trim$(cmbclass.SelectedItem.Text), "'", "''")
        Dim classid As Integer = cmbclass.SelectedItem.Value.ToString
        Dim msg As String = ""
        Dim Type As String = ""

        Try
            If btnSubmit.Text = "Save" Then
                Dim count As String = GF.GetSingleValue("", False, "classid", "s_category_master", "LTrim(RTrim(categoryname))='" + Trim(categoryname) + "'")

                If count = Convert.ToString(classid) Then
                    ShowMessage("Category name is allready exists", MessageType.Error)
                    Exit Sub
                End If
                Dim id As String = GF.AutoGenId("id", "s_category_master")
                msg = GF.InsertData("ID,CLASSID,CATEGORYNAME", id.ToString + "," + classid.ToString + ",'" + categoryname + "'", "s_category_master")
                If msg = "Success" Then
                    ShowMessage("Data Inserted Successfully", MessageType.Success)
                End If
                Type = "Add"
            Else
                Dim count As String = GF.GetSingleValue("", False, "classid", "s_category_master", "LTrim(RTrim(categoryname))='" + Trim(categoryname) + "'")
                If CInt(count) = classid Then
                    ShowMessage("Category name is allready exists", MessageType.Error)
                    Exit Sub
                End If
                ''oldname = ""
                ID = lblid.Text.ToString
                msg = GF.UpdateData("ID|CLASSID|CATEGORYNAME", ID.ToString + "|" + classid.ToString + "|'" + categoryname + "'", "s_category_master", "id=" + lblid.Text.ToString)
                If msg = "Success" Then
                    ShowMessage("Data Updated Successfully", MessageType.Success)
                End If
                Type = "Update"
            End If
            Response.Redirect("CatagoryMaster.aspx", False)
        Catch ex As Threading.ThreadAbortException
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub btncancle_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtcatagory.Text = ""
        cmbclass.SelectedItem.Text = "Select"
        updateaddedit.Update()
    End Sub
End Class
