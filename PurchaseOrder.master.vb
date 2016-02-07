Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports GlobalFunctions
Partial Class Mainpage
    Inherits System.Web.UI.MasterPage
    Dim gf As GlobalFunctions
    Dim uname As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

            If Session("Uname") <> Nothing Then
                Dim uname As String = Session("Uname").ToString
                lblusername.Text = "Welcome " + uname + " !"

            End If
            Dim url As String = Request.Url.AbsolutePath
            If Request.Url.AbsolutePath.EndsWith("UnitMaster.aspx") Then
                lnkunit.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("TransportMaster.aspx") Then
                lnktrans.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("ClassMaster.aspx") Then
                lnkclass.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("DashboardPurchase.aspx") Then
                lnkdashboard.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("CatagoryMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("CatagoryAddEdit.aspx") Then
                lnkcategory.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("PartMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("partAddEdit.aspx") Then
                lnkpart.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("Createpo.aspx") Then
                lnkcreatepo.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("Reportviwer.aspx") Then
                lnkreprintpo.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("SupplierMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("SupplierAddEdit.aspx") Then
                lnksupplier.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("Supplierreport.aspx") Then
                Lnksupplierreport.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("Reportviwer.aspx") Then
                lnkreprintpo.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("BomsMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("BomsAdd.aspx") Then
                lnkboms.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("Reportviewer.aspx") Then
                lnlreprintBOMS.Attributes("class") = "active"
            End If
        End If
    End Sub

    Protected Function SetCssClass(ByVal page As String) As String
        Return If(Request.Url.AbsolutePath.ToLower().EndsWith(page.ToLower()), "active", "")
    End Function
End Class

