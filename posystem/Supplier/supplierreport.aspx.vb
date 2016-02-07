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
Imports System.IO
Imports CrystalDecisions.Web
Partial Class posystem_Supplier_supplierreport
    Inherits System.Web.UI.Page
    Dim GF As New GlobalFunctions
    Dim filename As String
    'Protected strQueryString As String
    'Protected intLastPage As Integer
    'Protected intCurPage As Integer
    'Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    '    strQueryString = "..\report\SupplierDetail.rpt"
    '    '  Page.Title = Replace(Request.QueryString("rpt").ToString(), ".rpt", "")
    '    If Not Page.IsPostBack Then
    '        'Do nothing
    '    ElseIf Session(strQueryString) IsNot Nothing Then
    '        CrytViewer.ReportSource = Session(strQueryString)
    '    End If
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            radiobutton.SelectedIndex = 0
            filename = "..\report\SupplierDetail.rpt"
            viewreport(filename)
            CrystalReportSource1.Report.FileName = filename
            CrytViewer2.Visible = False
            CrytViewer3.Visible = False
        End If
    End Sub

    Protected Sub btnviewreport_click()
        viewreport(filename)
        uplreportview.Update()
    End Sub
    Protected Sub viewreport(ByVal filename As String)
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
            If radiobutton.SelectedIndex = 0 Then
                CrytViewer1.Visible = True
                CrytViewer2.Visible = False
                CrytViewer3.Visible = False
                filename = "..\report\SupplierDetail.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                CrytViewer1.ReportSource = myReport
            ElseIf radiobutton.SelectedIndex = 1 Then
                CrytViewer1.Visible = False
                CrytViewer2.Visible = True
                CrytViewer3.Visible = False
                filename = "..\report\Supplierdetailcitywise.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                CrytViewer2.ReportSource = myReport
            ElseIf radiobutton.SelectedIndex = 2 Then
                CrytViewer1.Visible = False
                CrytViewer2.Visible = False
                CrytViewer3.Visible = True
                filename = "..\report\supplierpart.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                Dim Str As String = "{s_category_master1.CATEGORYNAME} = 'Bearing' and {s_class_master1.CLASSNAME} = 'A'"
                myReport.RecordSelectionFormula = Str
                CrytViewer3.ReportSource = myReport
            End If

            Dim pname As String = "Canon LBP2900"
            ' lblconn.Text = pname.ToString
        Catch MyOdbcException As OdbcException
            Console.WriteLine(MyOdbcException.ToString)
        End Try

    End Sub

    Protected Sub btnprint_Click(ByVal sender As Object, ByVal e As System.EventArgs)

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
            If radiobutton.SelectedIndex = 0 Then
                CrytViewer1.Visible = True
                CrytViewer2.Visible = False
                CrytViewer3.Visible = False
                filename = "..\report\SupplierDetail.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                CrytViewer1.ReportSource = myReport
            ElseIf radiobutton.SelectedIndex = 1 Then
                CrytViewer1.Visible = False
                CrytViewer2.Visible = True
                CrytViewer3.Visible = False
                filename = "..\report\Supplierdetailcitywise.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                CrytViewer2.ReportSource = myReport
            ElseIf radiobutton.SelectedIndex = 2 Then
                CrytViewer1.Visible = False
                CrytViewer2.Visible = False
                CrytViewer3.Visible = True
                filename = "..\report\supplierpart.rpt"
                Dim path As String = filename
                myReport.Load(Server.MapPath(path))
                Dim Str As String = "{s_category_master1.CATEGORYNAME} = 'Bearing' and {s_class_master1.CLASSNAME} = 'A'"
                myReport.RecordSelectionFormula = Str
                CrytViewer3.ReportSource = myReport
            End If

            Dim pname As String = "Canon LBP2900"



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

  

    'Private Sub GetPageNums()
    '    intCurPage = Session(strQueryString + "_pagenum")
    '    intLastPage = Session(strQueryString + "_lastpagenum")
    '    ButtonsCheck()
    'End Sub
    'Private Sub SetCurPageNum(ByVal intPage As Integer)
    '    GetPageNums()
    '    Session(strQueryString + "_pagenum") = intPage
    '    ButtonsCheck()
    'End Sub



    'Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
    '    GetPageNums()
    '    If intCurPage < intLastPage Then
    '        SetCurPageNum(intCurPage + 1)
    '        CrytViewer.ShowNthPage(intCurPage + 1)
    '    Else
    '        CrytViewer.ShowNthPage(intCurPage)
    '    End If
    'End Sub
    'Protected Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
    '    GetPageNums()
    '    If intCurPage > 1 Then
    '        SetCurPageNum(intCurPage - 1)
    '        CrytViewer.ShowNthPage(intCurPage - 1)
    '    Else
    '        CrytViewer.ShowNthPage(intCurPage)
    '    End If
    'End Sub

    'Private Function GetCRPageNumber() As Integer

    '    Dim vi As ViewInfo = CrytViewer.ViewInfo

    '    Return vi.PageNumber

    'End Function

    'Private Function GetLastCRPageNumber() As Integer

    '    Dim vi As ViewInfo = CrytViewer.ViewInfo

    '    Return vi.LastPageNumber

    'End Function

    'Protected Sub ButtonsCheck()

    '    If intCurPage = 1 Then

    '        btnFirst.Enabled = False

    '        btnPrev.Enabled = False

    '    Else

    '        btnFirst.Enabled = True

    '        btnPrev.Enabled = True

    '    End If

    '    If intCurPage = intLastPage Then

    '        btnLast.Enabled = False

    '        btnNext.Enabled = False

    '    Else

    '        btnLast.Enabled = True

    '        btnNext.Enabled = True

    '    End If

    'End Sub



    'Protected Sub CrytViewer_Navigate(ByVal source As Object, ByVal e As CrystalDecisions.Web.NavigateEventArgs) Handles CrytViewer.Navigate

    '    SetCurPageNum(e.NewPageNumber)

    'End Sub



    'Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click

    '    CrytViewer.ShowFirstPage()

    '    GetPageNums()

    'End Sub

    'Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
    '    CrytViewer.ShowLastPage()
    '    GetPageNums()
    'End Sub

End Class
