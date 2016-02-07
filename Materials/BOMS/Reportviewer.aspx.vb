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
Partial Class Materials_BOMS_Reportviewer
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            FillDropDown()
            Dim id As String = Request.QueryString("id")
            If id <> Nothing Then
                cmbModelname.SelectedValue = id
                viewreport(id)
            End If
        End If
    End Sub
    Public Sub FillDropDown()
        Try
            Dim ds As New DataTable
            ds = GF.GetData(True, "mm.ID as id,ModelName", "s_model_master mm,s_boms_master bm", "bm.modelid = mm.id", False, "modelname asc")
            cmbModelname.DataTextField = "Modelname"
            cmbModelname.DataValueField = "id"
            cmbModelname.DataSource = ds
            cmbModelname.DataBind()
            cmbModelname.Items.Insert(0, "Select")
            cmbModelname.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnviewreport_click()
        viewreport(cmbModelname.SelectedValue.ToString)
    End Sub
    Protected Sub viewreport(ByVal id As Integer)
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

            Dim refid As String = GF.GetSingleValue("", False, "REFERENCE_BOM_ID", "s_boms_reference", "BOMID=" + id.ToString)
            If refid.ToString <> "" Then
                Dim dt As DataTable = GF.GetData(False, "bm.ModelId,mm.modelname as modelname,bm.rate as rate", "s_boms_master bm,s_model_master mm", "mm.id = bm.modelid and bm.ID=" + refid.ToString)
                Dim refmodel As String = dt.Rows(0)("modelname").ToString
                Dim rate As String = dt.Rows(0)("rate").ToString
                myReport.SetParameterValue("refbomname", refmodel.ToString)
                myReport.SetParameterValue("refbomrate", rate.ToString)
            Else
                myReport.SetParameterValue("refbomname", "-")
                myReport.SetParameterValue("refbomrate", "0")
            End If

            Dim Str As String = "{s_boms_master1.ID}  =" + id.ToString
            myReport.RecordSelectionFormula = Str
            CrytViewer.ReportSource = myReport
        Catch MyOdbcException As OdbcException
            Console.WriteLine(MyOdbcException.ToString)
        End Try
    End Sub

    Protected Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim id As String = cmbModelname.SelectedValue.ToString
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
            Dim Str As String = "{s_boms_detail1.MODELID} =" + cmbModelname.SelectedValue.ToString
            myReport.RecordSelectionFormula = Str
            CrytViewer.ReportSource = myReport
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
