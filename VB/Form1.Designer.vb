Namespace dxExample
    Partial Public Class BandedGridForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.simpleButton1 = New DevExpress.XtraEditors.SimpleButton()
            Me.customGridControl1 = New dxExample.CustomGridControl()
            Me.customBandedGridView1 = New dxExample.CustomBandedGridView()
            Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
            Me.customBandedColumn1 = New dxExample.CustomBandedColumn()
            Me.customBandedColumn2 = New dxExample.CustomBandedColumn()
            DirectCast(Me.customGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.customBandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' simpleButton1
            ' 
            Me.simpleButton1.Location = New System.Drawing.Point(647, 110)
            Me.simpleButton1.Name = "simpleButton1"
            Me.simpleButton1.Size = New System.Drawing.Size(141, 23)
            Me.simpleButton1.TabIndex = 1
            Me.simpleButton1.Text = "ShowPrintPreview"
            ' 
            ' customGridControl1
            ' 
            Me.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default
            Me.customGridControl1.Dock = System.Windows.Forms.DockStyle.Left
            Me.customGridControl1.Location = New System.Drawing.Point(0, 0)
            Me.customGridControl1.MainView = Me.customBandedGridView1
            Me.customGridControl1.Name = "customGridControl1"
            Me.customGridControl1.Size = New System.Drawing.Size(624, 493)
            Me.customGridControl1.TabIndex = 2
            Me.customGridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.customBandedGridView1})
            ' 
            ' customBandedGridView1
            ' 
            Me.customBandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() { Me.gridBand1})
            Me.customBandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() { Me.customBandedColumn1, Me.customBandedColumn2})
            Me.customBandedGridView1.GridControl = Me.customGridControl1
            Me.customBandedGridView1.HeaderHeightOnExport = 0
            Me.customBandedGridView1.Name = "customBandedGridView1"
            ' 
            ' gridBand1
            ' 
            Me.gridBand1.Caption = "gridBand1"
            Me.gridBand1.Columns.Add(Me.customBandedColumn1)
            Me.gridBand1.Columns.Add(Me.customBandedColumn2)
            Me.gridBand1.Name = "gridBand1"
            Me.gridBand1.VisibleIndex = 0
            Me.gridBand1.Width = 150
            ' 
            ' customBandedColumn1
            ' 
            Me.customBandedColumn1.Caption = "Column1"
            Me.customBandedColumn1.CaptionVerticalOnExport = False
            Me.customBandedColumn1.FieldName = "ID"
            Me.customBandedColumn1.Name = "customBandedColumn1"
            Me.customBandedColumn1.Visible = True
            ' 
            ' customBandedColumn2
            ' 
            Me.customBandedColumn2.Caption = "Column2 "
            Me.customBandedColumn2.CaptionVerticalOnExport = False
            Me.customBandedColumn2.FieldName = "Info"
            Me.customBandedColumn2.Name = "customBandedColumn2"
            Me.customBandedColumn2.Visible = True
            ' 
            ' Form1
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(828, 493)
            Me.Controls.Add(Me.customGridControl1)
            Me.Controls.Add(Me.simpleButton1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.customGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.customBandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        #End Region

        Private WithEvents simpleButton1 As DevExpress.XtraEditors.SimpleButton
        Private customGridControl1 As CustomGridControl
        Private customBandedGridView1 As CustomBandedGridView
        Private customBandedColumn1 As CustomBandedColumn
        Private customBandedColumn2 As CustomBandedColumn
        Private gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    End Class
End Namespace

