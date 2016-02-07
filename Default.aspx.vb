Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

        End If
    End Sub
    Protected Sub btnlogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Try
            Dim str As String = String.Empty
            Dim str1 As String = String.Empty
            str = GF.GetSingleValue("COUNT", False, "Name", "s_user", "ltrim(rtrim(name))='" _
                  & UCase(Trim(txtusername.Text)) & "'and ltrim(rtrim(password))='" & UCase(Trim(txtpassword.Text)) & "'")
            Dim recokrdcount As Integer
            recokrdcount = CInt(str)
            str1 = GF.GetSingleValue("", False, "id", "s_user", "ltrim(rtrim(name))='" _
                  & UCase(Trim(txtusername.Text)) & "'and ltrim(rtrim(password))='" & UCase(Trim(txtpassword.Text)) & "'")
            Dim ds As DataTable = GF.GetData(True, "Fname,lname", "s_user", "iD=" + str1.ToString)
            Dim fname As String = ds.Rows(0)("Fname").ToString
            Dim lname As String = ds.Rows(0)("Lname").ToString
            Dim uname As String = fname + " " + lname
            If recokrdcount > 0 Then
                Session("Uname") = uname
                Response.Redirect("DashboardPurchase.aspx", False)
            Else
                lblerror.Text = "Invalid UserName or Password"
                txtusername.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblerror.Text = ex.Message
        End Try
    End Sub
End Class
