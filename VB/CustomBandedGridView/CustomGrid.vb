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
    <ToolboxItemAttribute(True)> _
    Public Class CustomGridControl
        Inherits GridControl

        Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
            MyBase.RegisterAvailableViewsCore(collection)
            collection.Add(New MyGridViewInfoRegistrator())
            collection.Add(New MyBandedGridViewInfoRegistrator())
        End Sub

    End Class
    Public Class MyGridViewInfoRegistrator
        Inherits GridInfoRegistrator

        Public Overrides ReadOnly Property ViewName() As String
            Get
                Return CustomView.SViewName
            End Get
        End Property
        Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
            Return New CustomBandedGridView(TryCast(grid, GridControl))
        End Function
    End Class
    Public Class MyBandedGridViewInfoRegistrator
        Inherits BandedGridInfoRegistrator

        Public Overrides ReadOnly Property ViewName() As String
            Get
                Return CustomBandedGridView.SViewName
            End Get
        End Property
        Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
            Return New CustomBandedGridView(TryCast(grid, GridControl))
        End Function
    End Class

End Namespace
