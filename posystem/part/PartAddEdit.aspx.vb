Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Imports System.DBNull
Imports System.Data.OleDb
Imports System.Web.Services
Imports System.Collections.Generic

Partial Class Part_PartAddEdit
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Public ddl As Boolean = False
    Public Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbclass.SelectedItem.Text = "Select" Then
            ShowMessage("Please select Classname", MessageType.Warning)
            Exit Sub
        ElseIf cmbcategory.SelectedItem.Text = "Select" Then
            ShowMessage("Please select Category Name", MessageType.Warning)
            Exit Sub
        ElseIf txtpartname.Text = "" Then
            ShowMessage("Enter partname ", MessageType.Warning)
            Exit Sub
        End If

        Dim partname As String = Replace(Trim$(txtpartname.Text), "'", "''")
        Dim classtype As String = Replace(Trim$(cmbclass.SelectedItem.Text), "'", "''")
        Dim category As String = Replace(Trim$(cmbcategory.SelectedItem.Text), "'", "''")
        Dim msg As String = ""
        Dim Type As String = ""
        Try
            If btnSubmit.Text = "Save" Then
                Dim classid As Integer = cmbclass.SelectedItem.Value
                Dim catid As Integer = cmbcategory.SelectedItem.Value
                Dim ds As DataTable
                ds = GF.GetData(False, "id,classid,catagoryid", "s_part_master", "LTrim(RTrim(partname))='" + Trim(partname) + "'")
                If ds.Rows.Count > 0 Then
                    Dim classid1 As Integer = Replace(Trim$(ds.Rows(0)("classid").ToString), "'", "''")
                    Dim catid1 As Integer = Replace(Trim$(ds.Rows(0)("catagoryid").ToString), "'", "''")
                    If classid = classid1 Then
                        If catid = catid1 Then
                            ShowMessage("Category name is allready exists", MessageType.Error)
                            Exit Sub
                        End If
                    End If
                End If
                Dim id As String = GF.AutoGenId("id", "s_part_master")
                msg = GF.InsertData("ID,CLASSID,CATAGORYID,PARTNAME", id.ToString + "," + classid.ToString + "," + catid.ToString + ",'" + partname + "'", "s_part_master")
                If msg = "Success" Then
                    ShowMessage("Data Inserted Successfully", MessageType.Success)
                End If
                Type = "Add"
            Else

                Dim classid As Integer = cmbclass.SelectedValue.ToString
                Dim catid As Integer = cmbcategory.SelectedValue.ToString
                Dim ds As DataTable
                ds = GF.GetData(False, "id,classid,catagoryid", "s_part_master", "LTrim(RTrim(partname))='" + Trim(partname) + "'")
                If ds.Rows.Count > 0 Then
                    Dim classid1 As Integer = Replace(Trim$(ds.Rows(0)("classid").ToString), "'", "''")
                    Dim catid1 As Integer = Replace(Trim$(ds.Rows(0)("catagoryid").ToString), "'", "''")
                    If classid = classid1 Then
                        If catid = catid1 Then
                            ShowMessage("Category name is allready exists", MessageType.Error)
                            Exit Sub
                        End If
                    End If
                End If
                'oldname = ""
                ID = lblid.Text.ToString
                msg = GF.UpdateData("CLASSID|CATAGORYID|PARTNAME", classid.ToString + "|" + catid.ToString + "|'" + partname + "'", "s_part_master", "id=" + lblid.Text.ToString)
                If msg = "Success" Then
                    ShowMessage("Data Updated Successfully", MessageType.Info)
                End If
                Type = "Update"
            End If
            Response.Redirect("PartMaster.aspx", False)
        Catch ex As Threading.ThreadAbortException
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

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
            cmbclass.DataTextField = "classname"
            cmbclass.DataValueField = "id"
            cmbclass.DataSource = ds
            cmbclass.DataBind()
            cmbclass.Items.Insert(0, New ListItem("Select", "0"))

            'ds = GF.GetData(True, "id,unittype", "s_unit", "Is_DELETED<>1", False, "unittype asc")
            'cmbUnit.DataTextField = "unittype"
            'cmbUnit.DataValueField = "id"
            'cmbUnit.DataSource = ds
            'cmbUnit.DataBind()
            'cmbUnit.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Private Sub filldata()
        Dim partid As String
        partid = Request.QueryString("id")
        lblid.Text = partid.ToString
        Dim ds As DataTable

        ds = GF.GetData(False, "pm.classid as clsid,pm.CATAGORYID as catid,cls.classname as clsname,cat.CATEGORYNAME as catname,pm.partname as partname", "s_category_master cat,s_class_master cls, s_part_master pm", "pm.classid= cls.id  and pm.catagoryid = cat.id AND pm.id=" + lblid.Text.ToString)

        If ds.Rows.Count > 0 Then
            If IsDBNull(ds.Rows(0)("clsname")) = True Then
                cmbclass.Text = ""
            Else
                cmbclass.SelectedItem.Text = ds.Rows(0)("clsname").ToString
                cmbclass.SelectedValue = ds.Rows(0)("clsid").ToString
                fillcategory_data(ds.Rows(0)("clsid").ToString)
            End If
            If IsDBNull(ds.Rows(0)("catname")) = True Then
                cmbcategory.Text = ""
            Else
                cmbcategory.SelectedItem.Text = ds.Rows(0)("catname").ToString
                cmbcategory.SelectedValue = ds.Rows(0)("catid").ToString
            End If
            If IsDBNull(ds.Rows(0)("partname")) = True Then
                txtpartname.Text = ""
            Else
                txtpartname.Text = ds.Rows(0)("partname")
            End If
            
           
        End If
        btnSubmit.Text = "Update"
    End Sub

    

    Protected Sub cmbclass_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            fillcategory_data(cmbclass.SelectedValue.ToString)
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
    Protected Sub btncancle_click(ByVal sender As Object, ByVal e As EventArgs)
        UpdatePanel2.Update()
    End Sub
End Class
