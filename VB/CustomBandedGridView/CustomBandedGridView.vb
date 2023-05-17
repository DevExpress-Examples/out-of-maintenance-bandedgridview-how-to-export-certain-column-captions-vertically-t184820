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
    Public Class CustomBandedGridView
        Inherits BandedGridView

        Public Const SViewName As String = "CustomBandedView"
        Protected Overrides ReadOnly Property ViewName() As String
            Get
                Return SViewName
            End Get
        End Property
        Public Sub New(ByVal ownerGrid As GridControl)
            MyBase.New(ownerGrid)
                HeaderHeightOnExport = 50
        End Sub
        Public Sub New()

        End Sub
        Public Property HeaderHeightOnExport() As Integer
        Protected Overrides Function CreateColumnCollection() As GridColumnCollection
            Return New MyBandedColumnCollection(Me)
        End Function
        Protected Overrides Function CreatePrintInfoInstance(ByVal args As PrintInfoArgs) As BaseViewPrintInfo
            Return New CustomBandedPrintInfo(args)
        End Function

    End Class
    Public Class CustomBandedPrintInfo
        Inherits BandedGridViewPrintInfo

        Private vert As BrickStyle
        Public Sub New(ByVal args As PrintInfoArgs)
            MyBase.New(args)

        End Sub
        Protected Overrides Sub SetDefaultBrickStyle(ByVal graph As BrickGraphics, ByVal style As BrickStyle)
            MyBase.SetDefaultBrickStyle(graph, style)
        End Sub

        Protected Overrides Sub PrintBandHeader(ByVal graph As BrickGraphics)
            If Not View.OptionsPrint.PrintBandHeader Then
                Return
            End If
            Dim r As Rectangle = Rectangle.Empty
            Dim indent As New Point(Me.Indent, 0)
            Dim usePrintStyles As Boolean = View.OptionsPrint.UsePrintStyles
            SetDefaultBrickStyle(graph, DirectCast(Bricks("BandPanel").Clone(), BrickStyle))
            For Each band As PrintBandInfo In Bands
                r = band.Bounds
                r.Offset(indent)
                If (Not usePrintStyles) AndAlso band.Band IsNot Nothing Then
                    Dim temp As New AppearanceObject()
                    AppearanceHelper.Combine(temp, New AppearanceObject() { band.Band.AppearanceHeader, AppearancePrint.BandPanel })
                    SetDefaultBrickStyle(graph, Bricks.Create(temp, BorderSide.All, temp.BorderColor, 1))
                End If
                Dim brick = DrawTextBrick(graph, band.Band.GetTextCaption(), r)
                brick.Style = DirectCast(brick.Style.Clone(), BrickStyle)
                brick.Style.StringFormat.ChangeFormatFlags(StringFormatFlags.NoWrap Or StringFormatFlags.LineLimit)


            Next band
        End Sub
        Public Overrides Sub PrintHeader(ByVal graph As BrickGraphics)
            vert = New BrickStyle(Bricks("HeaderPanel"))
            vert.StringFormat = vert.StringFormat.ChangeFormatFlags(StringFormatFlags.DirectionVertical Or StringFormatFlags.DirectionRightToLeft)
            MyBase.PrintHeader(graph)
            If Not View.OptionsPrint.PrintHeader Then
                Return
            End If
            Dim indent As New Point(Me.Indent, HeaderY)
            Dim r As Rectangle = Rectangle.Empty
            Dim usePrintStyles As Boolean = View.OptionsPrint.UsePrintStyles
            SetDefaultBrickStyle(graph, Bricks("HeaderPanel"))

            Dim HeaderHeight As Integer = (TryCast(Me.View, CustomBandedGridView)).HeaderHeightOnExport

            Dim columnBricks As New Dictionary(Of PrintColumnInfo, ITextBrick)()
            For Each col As PrintColumnInfo In Columns
                If Not usePrintStyles Then
                    Dim temp As New AppearanceObject()
                    AppearanceHelper.Combine(temp, New AppearanceObject() { col.Column.AppearanceHeader, View.Appearance.HeaderPanel, AppearancePrint.HeaderPanel })
                    SetDefaultBrickStyle(graph, Bricks.Create(temp, BorderSide.All, temp.BorderColor, 1))
                End If
                r = col.Bounds
                r = New Rectangle(r.Location, New Size(r.Width, HeaderHeight))
                r.Offset(indent)
                Dim mi As MethodInfo = GetType(GridColumn).GetMethod("GetTextCaptionForPrinting", BindingFlags.NonPublic Or BindingFlags.Instance)
                Dim caption As String = DirectCast(mi.Invoke(col.Column, New Object() { }), String) 'col.Column.GetTextCaptionForPrinting();
                If Not col.Column.OptionsColumn.ShowCaption Then
                    caption = String.Empty
                End If
                Dim itb As ITextBrick = DrawTextBrick(graph, caption, r)
                If (TypeOf col.Column Is CustomBandedColumn) AndAlso ((TryCast(col.Column, CustomBandedColumn)).CaptionVerticalOnExport) Then
                    itb.Style = vert
                Else
                    SetDefaultBrickStyle(graph, Bricks("HeaderPanel"))
                End If
                If caption.Contains(Environment.NewLine) Then
                    itb.Style.StringFormat = BrickStringFormat.Create(itb.Style.TextAlignment, True)
                End If

                If AppearancePrint.HeaderPanel.TextOptions.WordWrap = WordWrap.NoWrap AndAlso View.OptionsPrint.UsePrintStyles Then
                    Using g As Graphics = Me.View.GridControl.CreateGraphics()
                        Dim s As SizeF = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value)
                        If s.Width + 5 >= r.Width Then
                            itb.Text = ""
                            itb.TextValue = ""
                        End If
                    End Using
                End If
                columnBricks.Add(col, itb)
                Using g As Graphics = Me.View.GridControl.CreateGraphics()
                    Dim s As SizeF = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value)
                    If s.Height + 5 >= r.Height Then
                        itb.Text = ""
                        itb.TextValue = ""
                    End If
                End Using
            Next col
        End Sub
    End Class

    Friend Class MyBandedColumnCollection
        Inherits BandedGridColumnCollection

        Public Sub New(ByVal view As ColumnView)
            MyBase.New(view)

        End Sub
        Protected Overrides Function CreateColumn() As GridColumn
            Return New CustomBandedColumn()
        End Function

    End Class
    Friend Class CustomBandedColumn
        Inherits BandedGridColumn

        Public Sub New()
            CaptionVerticalOnExport = False
        End Sub
        Protected Overrides Sub Assign(ByVal column As GridColumn)
            If TypeOf column Is CustomBandedColumn Then
                Me.CaptionVerticalOnExport = CType(column, CustomBandedColumn).CaptionVerticalOnExport
            End If
            MyBase.Assign(column)
        End Sub
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property CaptionVerticalOnExport() As Boolean
    End Class
End Namespace
