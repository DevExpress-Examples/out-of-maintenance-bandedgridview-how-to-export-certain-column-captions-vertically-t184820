namespace dxExample
{
    partial class BandedGridForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.customGridControl1 = new dxExample.CustomGridControl();
            this.customBandedGridView1 = new dxExample.CustomBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.customBandedColumn1 = new dxExample.CustomBandedColumn();
            this.customBandedColumn2 = new dxExample.CustomBandedColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customBandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(647, 110);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(141, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "ShowPrintPreview";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // customGridControl1
            // 
            this.customGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customGridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.customGridControl1.Location = new System.Drawing.Point(0, 0);
            this.customGridControl1.MainView = this.customBandedGridView1;
            this.customGridControl1.Name = "customGridControl1";
            this.customGridControl1.Size = new System.Drawing.Size(624, 493);
            this.customGridControl1.TabIndex = 2;
            this.customGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customBandedGridView1});
            // 
            // customBandedGridView1
            // 
            this.customBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.customBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.customBandedColumn1,
            this.customBandedColumn2});
            this.customBandedGridView1.GridControl = this.customGridControl1;
            this.customBandedGridView1.HeaderHeightOnExport = 0;
            this.customBandedGridView1.Name = "customBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.Columns.Add(this.customBandedColumn1);
            this.gridBand1.Columns.Add(this.customBandedColumn2);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 150;
            // 
            // customBandedColumn1
            // 
            this.customBandedColumn1.Caption = "Column1";
            this.customBandedColumn1.CaptionVerticalOnExport = false;
            this.customBandedColumn1.FieldName = "ID";
            this.customBandedColumn1.Name = "customBandedColumn1";
            this.customBandedColumn1.Visible = true;
            // 
            // customBandedColumn2
            // 
            this.customBandedColumn2.Caption = "Column2 ";
            this.customBandedColumn2.CaptionVerticalOnExport = false;
            this.customBandedColumn2.FieldName = "Info";
            this.customBandedColumn2.Name = "customBandedColumn2";
            this.customBandedColumn2.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 493);
            this.Controls.Add(this.customGridControl1);
            this.Controls.Add(this.simpleButton1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.customGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customBandedGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private CustomGridControl customGridControl1;
        private CustomBandedGridView customBandedGridView1;
        private CustomBandedColumn customBandedColumn1;
        private CustomBandedColumn customBandedColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}

