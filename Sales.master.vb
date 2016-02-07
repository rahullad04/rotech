
Partial Class Mainpage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("Uname") <> Nothing Then
                Dim uname As String = Session("Uname").ToString
                lblusername.Text = "Welcome " + uname + " !"
            End If
            Dim url As String = Request.Url.AbsolutePath
            If Request.Url.AbsolutePath.EndsWith("DashboardSales.aspx") Then
                lnksalesdashboard.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("StackMaster.aspx") Then
                lnkstack.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("ProductTypeMaster.aspx") Then
                lnkproduct.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("ModelMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("ModelAddEdit.aspx") Then
                lnkmodel.Attributes("class") = "active"
            ElseIf Request.Url.AbsolutePath.EndsWith("ClientMaster.aspx") Or Request.Url.AbsolutePath.EndsWith("ClientAddEdit.aspx") Then
                lnkclient.Attributes("class") = "active"
            End If
        End If
    End Sub
End Class

