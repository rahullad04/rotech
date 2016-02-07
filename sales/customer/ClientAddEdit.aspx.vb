Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class Client_ClientAddEdit
    Inherits System.Web.UI.Page

    Public terms As String = String.Empty
    Dim GF As New GlobalFunctions
    Dim oldname As String = String.Empty
    Dim oldname1 As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim type As String = Request.QueryString("type")
            If type = "Add" Then
                clearclientmodel()

            ElseIf type = "Edit" Then
                filldata()
                FillClientmodel()

            End If
        End If
    End Sub

    Public Sub clearclientmodel()
        If String.IsNullOrEmpty(txtrate.Text) = False Then
            txtrate.Text = ""
        End If
    End Sub
    Public Sub FillClientmodel()
        Try
            Dim ds As DataTable
            ds = GF.GetData(False, "c.Client_modelID as ID,m.ModelName as ModelName,c.Client_Rate as Rate ,c.Assess_Rate as Assess_Rate,c.Discount_percentage as Discountper", "s_Client_Model c,s_Model_Master m", "c.modelid = m.ID AND c.ClientID = " + lblcid.Text.ToString())
            If (ds.Rows.Count > 0) Then
                vsclientmodel.DataSource = ds
                vsclientmodel.DataBind()
            Else
                ds.Rows.Add(ds.NewRow())
                vsclientmodel.DataSource = ds
                vsclientmodel.DataBind()
                Dim columncount As Integer = vsclientmodel.Rows(0).Cells.Count
                vsclientmodel.Rows(0).Cells.Clear()
                vsclientmodel.Rows(0).Cells.Add(New TableCell())
                vsclientmodel.Rows(0).Cells(0).ColumnSpan = columncount
                vsclientmodel.Rows(0).Cells(0).Text = "No Records Found"
            End If
            FillDropDown()
        Catch ex As Exception
            MsgBox(ex.Message, , GF.Title)
        End Try
    End Sub
    Private Sub FillDropDown()
        Try
            

        Catch ex As Exception
            MsgBox(ex.Message, , "Rotech")
        End Try
    End Sub

    Private Sub filldata()
        Dim cid As String
        cid = Request.QueryString("id")
        lblcid.Text = cid.ToString
        Dim ds As DataTable
        ds = GF.GetData(False, "clientid,name,address,city,phoneno,faxno,TIN,CST,email,Cnphone_no,cn_name,Terms", "S_client_master", "clientid=" + cid.ToString)

        If IsDBNull(ds.Rows(0)("Name")) = True Then
            txtname.Text = ""
        Else
            txtname.Text = ds.Rows(0)("Name")
        End If

        If IsDBNull(ds.Rows(0)("Address")) = True Then
            txtaddress.Text = ""
        Else
            txtaddress.Text = ds.Rows(0)("Address")
        End If
        If IsDBNull(ds.Rows(0)("City")) = True Then
            txtcity.Text = ""
        Else
            txtcity.Text = ds.Rows(0)("City")
        End If

        If IsDBNull(ds.Rows(0)("Phoneno")) = True Then
            txtphno.Text = ""
        Else
            txtphno.Text = ds.Rows(0)("Phoneno")
        End If

        If IsDBNull(ds.Rows(0)("FaxNo")) = True Then
            txtfaxno.Text = ""
        Else
            txtfaxno.Text = ds.Rows(0)("Faxno")
        End If

        If IsDBNull(ds.Rows(0)("TIN")) = True Then
            txttin.Text = ""
        Else
            txttin.Text = ds.Rows(0)("TIN")
        End If


        If IsDBNull(ds.Rows(0)("CST")) = True Then
            txtcst.Text = ""
        Else
            txtcst.Text = ds.Rows(0)("CST")
        End If

        If IsDBNull(ds.Rows(0)("Cn_name")) = True Then
            txtcnname.Text = ""
        Else
            txtcnname.Text = ds.Rows(0)("Cn_name")
        End If


        If IsDBNull(ds.Rows(0)("Cnphone_No")) = True Then
            txtcnphnno.Text = ""
        Else
            txtcnphnno.Text = ds.Rows(0)("Cnphone_No")
        End If

        lblcid.Text = ds.Rows(0)("ClientID")
        oldname = ds.Rows(0)("Name")
        If IsDBNull(ds.Rows(0)("Email")) = True Then
            txtEmail.Text = ""
        Else
            txtEmail.Text = ds.Rows(0)("Email")
        End If
        Dim term As String = getTerms(ds.Rows(0)("Terms"))

        FillClientmodel()
        btnSave.Text = "Update"
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

    Private Sub terms_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles against.CheckedChanged, days15.CheckedChanged, days30.CheckedChanged, days45.CheckedChanged, days60.CheckedChanged, days90.CheckedChanged, Immediate.CheckedChanged, against.CheckedChanged
        terms = sender.id
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim cname As String = Replace(Trim$(txtname.Text), "'", "''")
        Dim add As String = Replace(Trim$(txtaddress.Text), "'", "''")
        Dim city As String = Replace(Trim$(txtcity.Text), "'", "''")
        Dim phone As String = Replace(Trim$(txtphno.Text), "'", "''")
        Dim fax As String = Replace(Trim$(txtfaxno.Text), "'", "''")
        Dim tin As String = Replace(Trim$(txttin.Text), "'", "''")
        Dim cst As String = Replace(Trim$(txtcst.Text), "'", "''")
        Dim cnname As String = Replace(Trim$(txtcnname.Text), "'", "''")
        Dim cnphnno As String = Replace(Trim$(txtcnphnno.Text), "'", "''")
        Dim email As String = Replace(Trim$(txtemail.Text), "'", "''")
        Dim msg As String = ""
        Dim Type As String = ""

        Try
            If btnSave.Text = "Save" Then


                Dim count As String = GF.GetSingleValue("count", False, "ClientID", "s_Client_Master", "LTrim(RTrim(Name))='" + Trim(txtname.Text) + "'")
                If CInt(count) > 0 Then
                    MsgBox("Client name allready exists", , "Rotech")
                    Exit Sub
                End If
                Dim id As String = GF.AutoGenId("ClientID", "s_Client_master")
                lblcid.Text = id
                msg = GF.InsertData("ClientID,Name,Address,City,Phoneno,Faxno,TIN,CST,Terms,Cn_name,Cnphone_no,Email", id + ",'" + cname + "','" + add + "','" + city + "','" + phone + "','" + fax + "','" + tin + "','" + cst + "','" + terms + "','" + cnname + "','" + cnphnno + "','" + email + "'", "s_Client_Master")
                Type = "Add"
                If MsgBox("Do You Want To " + Type + " Client Model?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Rotech") = MsgBoxResult.Yes Then
                    cmbmodelname.Focus()
                    txtrate.Text = ""
                    Exit Sub
                End If


            Else

                Dim count As String = GF.GetSingleValue("count", False, "ClientID", "s_Client_Master", "LTrim(RTrim(Name))='" + Trim(txtname.Text) + "'")
                If CInt(count) > 0 And oldname <> Trim(txtname.Text) Then
                    MsgBox("Client name allready exists", , "Rotech")
                    Exit Sub
                End If
                oldname = ""
                msg = GF.UpdateData("Name|Address|City|Phoneno|Faxno|TIN|CST|Terms|Cn_name|Cnphone_no|Email", "'" + cname + "'|'" + add + "'|'" + city + "'|'" + phone + "'|'" + fax + "'|'" + tin + "'|'" + cst + "'|'" + terms + "'|'" + cnname + "'|'" + cnphnno + "'|'" + email + "'", "s_Client_Master", "ClientID=" + lblcid.Text.ToString)
                Type = "Update"
                If MsgBox("Do You Want To " + Type + " Client Model?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Delete") = MsgBoxResult.Yes Then
                    cmbmodelname.Focus()
                    txtrate.Text = ""
                Else
                    Exit Sub
                End If

            End If


            Response.Redirect("ClientMaster.aspx", False)
        Catch ex As Exception
            MsgBox(ex.Message, , "Rotech")
        End Try
    End Sub
End Class