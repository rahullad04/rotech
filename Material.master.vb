
Partial Class Mainpage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("Uname") <> Nothing Then
                Dim uname As String = Session("Uname").ToString
                lblusername.Text = "Welcome " + uname + " !"

            End If
        End If
    End Sub
End Class

