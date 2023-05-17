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
    Public Class CustomView
        Inherits GridView

        Public Const SViewName As String = "CustomView"
        Public Sub New()
            Me.New(Nothing)
        End Sub
        Public Sub New(ByVal grid As GridControl)
            MyBase.New(grid)
            HeaderHeightOnExport = 50
        End Sub
        Public Property HeaderHeightOnExport() As Integer
        Protected Overrides ReadOnly Property ViewName() As String
            Get
                Return SViewName
            End Get
        End Property

        Protected Overrides Function CreatePrintInfoInstance(ByVal args As PrintInfoArgs) As BaseViewPrintInfo
            Return New CustomPrintInfo(args)
        End Function
    End Class
    Public Class CustomPrintInfo
        Inherits GridViewPrintInfo

        Public Sub New(ByVal args As PrintInfoArgs)
            MyBase.New(args)
        End Sub

        Public Overrides Sub PrintHeader(ByVal graph As BrickGraphics)
            MyBase.PrintHeader(graph)
            If Not View.OptionsPrint.PrintHeader Then
                Return
            End If
            Dim indent As New Point(Me.Indent, HeaderY)
            Dim r As Rectangle = Rectangle.Empty
            Dim usePrintStyles As Boolean = View.OptionsPrint.UsePrintStyles
            SetDefaultBrickStyle(graph, Bricks("HeaderPanel"))

            Dim HeaderHeight As Integer = 50
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

                If caption.Contains(Environment.NewLine) Then
                    itb.Style.StringFormat = BrickStringFormat.Create(itb.Style.TextAlignment, True)
                End If
                itb.Style.StringFormat = itb.Style.StringFormat.ChangeFormatFlags(StringFormatFlags.DirectionVertical Or StringFormatFlags.DirectionRightToLeft)

                If AppearancePrint.HeaderPanel.TextOptions.WordWrap = WordWrap.NoWrap AndAlso View.OptionsPrint.UsePrintStyles Then
                    Using g As Graphics = Me.View.GridControl.CreateGraphics()
                        Dim s As SizeF = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value)
                        If s.Width + 5 >= r.Width Then
                            itb.Text = ""
                            itb.TextValue = ""
                        End If
                    End Using
                End If
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
End Namespace
