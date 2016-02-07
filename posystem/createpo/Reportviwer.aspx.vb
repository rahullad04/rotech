Imports System.Data
Imports System.Configuration
Imports MySql.Data.MySqlClient
Imports System.DBNull
Imports System.Collections.Generic
Imports System.Web.UI
Imports GlobalFunctions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource

Imports System
Imports Microsoft.Data.Odbc
Imports System.Drawing.Printing

Partial Class Po_Reportviwer
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            FillDropDown()
            Dim id As String = Request.QueryString("id")
            If id <> Nothing Then
                cmbpono.SelectedValue = id
                viewreport(id)
            End If
        End If
    End Sub
    Public Sub FillDropDown()
        Try
            Dim ds As New DataTable
            ds = GF.GetData(True, "id,Pono", "s_po_master", "Is_DELETED<>1", False, "pono asc")
            cmbpono.DataTextField = "pono"
            cmbpono.DataValueField = "id"
            cmbpono.DataSource = ds
            cmbpono.DataBind()
            cmbpono.Items.Insert(0, New ListItem("Select", "0"))


            ds = GF.GetData(True, "id,Pono", "s_po_master", "Is_DELETED<>1", False, "pono asc")
            cmbpono.DataTextField = "pono"
            cmbpono.DataValueField = "id"
            cmbpono.DataSource = ds
            cmbpono.DataBind()
            cmbpono.Items.Insert(0, New ListItem("Select", "0"))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnviewreport_click()
        viewreport(cmbpono.SelectedValue.ToString)
    End Sub
    Protected Sub viewreport(ByVal poid As Integer)
        Dim myReport As New ReportDocument
        Dim myData As New DataSet
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim myAdapter As New MySqlDataAdapter

        Dim MyConString As String = "DRIVER={MySQL ODBC 5.2 ANSI Driver};" & _
   "SERVER=localhost;" & _
   "DATABASE=rotech;" & _
   "UID=root;" & _
   "PASSWORD=root;" & _
   "OPTION=3;"


        'conn = "DRIVER={MySQL ODBC 5.2 ANSI Driver};SERVER=localhost;DATABASE=rotech;UID=root;PASSWORD=root;OPTION=3;"
        Dim MyConnection As New OdbcConnection(MyConString)
        Try
            MyConnection.Open()
            Dim path As String = CrystalReportSource1.Report.FileName
            myReport.Load(Server.MapPath(path))

            Dim Str As String = "{s_po_master1.id} = {s_po_detail1.po_id} And {s_po_master1.id}=" + poid.ToString + " And {s_po_detail1.po_id} = " + poid.ToString
            myReport.RecordSelectionFormula = Str
            CrytViewer.ReportSource = myReport
            Dim pname As String = "Canon LBP2900"
            lblconn.Text = pname.ToString
        Catch MyOdbcException As OdbcException
            Console.WriteLine(MyOdbcException.ToString)
        End Try

    End Sub

    Protected Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim id As String = cmbpono.SelectedValue.ToString
        Dim myReport As New ReportDocument
        Dim myData As New DataSet
        Dim conn As New MySqlConnection
        Dim cmd As New MySqlCommand
        Dim myAdapter As New MySqlDataAdapter

        Dim MyConString As String = "DRIVER={MySQL ODBC 5.2 ANSI Driver};" & _
   "SERVER=localhost;" & _
   "DATABASE=rotech;" & _
   "UID=root;" & _
   "PASSWORD=root;" & _
   "OPTION=3;"
        Dim MyConnection As New OdbcConnection(MyConString)
        Try
            MyConnection.Open()
            Dim path As String = CrystalReportSource1.Report.FileName
            myReport.Load(Server.MapPath(path))
            Dim Str As String = "{s_po_master1.id} = {s_po_detail1.po_id} And {s_po_master1.id}=" + id.ToString + " And {s_po_detail1.po_id} = " + id.ToString
            myReport.RecordSelectionFormula = Str
            CrytViewer.ReportSource = myReport
            'Dim pname As String = "Canon LBP2900"
            'lblconn.Text = pname.ToString
            'myReport.PrintOptions.PrinterName = pname.ToString
            ''  myReport.PrintToPrinter(1, False, 0, 0)


           
            
            Dim pDoc As System.Drawing.Printing.PrintDocument = New System.Drawing.Printing.PrintDocument()
            Dim PrintLayout As CrystalDecisions.Shared.PrintLayoutSettings = New CrystalDecisions.Shared.PrintLayoutSettings()
            Dim printerSettings As System.Drawing.Printing.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            printerSettings.PrinterName = "Canon LBP2900"
            Dim pSettings As System.Drawing.Printing.PageSettings = New System.Drawing.Printing.PageSettings(printerSettings)
            myReport.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
            myReport.PrintOptions.PrinterDuplex = PrinterDuplex.Simplex
            myReport.PrintToPrinter(printerSettings, pSettings, False, PrintLayout)




                        

        Catch MyOdbcException As OdbcException
            Console.WriteLine(MyOdbcException.ToString)
        End Try


    End Sub
End Class
