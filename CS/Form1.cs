using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Printing;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace dxExample
{
    public partial class BandedGridForm : Form
    {
        public BandedGridForm()
        {
            InitializeComponent();
            customGridControl1.DataSource = GetData(10);
            customBandedColumn2.CaptionVerticalOnExport = true;
            customBandedGridView1.HeaderHeightOnExport = 100;
        }


        DataTable GetData(int rows)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Info", typeof(string));
            for (int i = 0; i < rows; i++)
            {
                dt.Rows.Add(i, "Info" + i.ToString());
            }
            return dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            PrintingSystem ps = new PrintingSystem();
            PrintableComponentLink pcLink1 = new PrintableComponentLink();
            pcLink1.PrintingSystem = ps;
            pcLink1.Component = customGridControl1;
            pcLink1.Landscape = true;
            
            pcLink1.ShowPreview();
        }
    }
  
   
}
