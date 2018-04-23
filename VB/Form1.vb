Imports DevExpress.Utils
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.Handler
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Printing
Imports DevExpress.XtraPrinting
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms

Namespace dxExample
    Partial Public Class BandedGridForm
        Inherits Form

        Public Sub New()
            InitializeComponent()
            customGridControl1.DataSource = GetData(10)
            customBandedColumn2.CaptionVerticalOnExport = True
            customBandedGridView1.HeaderHeightOnExport = 100
        End Sub


        Private Function GetData(ByVal rows As Integer) As DataTable
            Dim dt As New DataTable()
            dt.Columns.Add("ID", GetType(Integer))
            dt.Columns.Add("Info", GetType(String))
            For i As Integer = 0 To rows - 1
                dt.Rows.Add(i, "Info" & i.ToString())
            Next i
            Return dt
        End Function

        Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
            Dim ps As New PrintingSystem()
            Dim pcLink1 As New PrintableComponentLink()
            pcLink1.PrintingSystem = ps
            pcLink1.Component = customGridControl1
            pcLink1.Landscape = True

            pcLink1.ShowPreview()
        End Sub
    End Class


End Namespace
