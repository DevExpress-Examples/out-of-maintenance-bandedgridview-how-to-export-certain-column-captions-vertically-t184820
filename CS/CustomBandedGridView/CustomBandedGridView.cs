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

namespace dxExample {
    public class CustomBandedGridView : BandedGridView {
        public const string SViewName = "CustomBandedView";
        protected override string ViewName { get { return SViewName; } }
        public CustomBandedGridView(GridControl ownerGrid)
            : base(ownerGrid) {
                HeaderHeightOnExport = 50;
        }
        public CustomBandedGridView() {

        }
        public int HeaderHeightOnExport { get; set; }
        protected override GridColumnCollection CreateColumnCollection() {
            return new MyBandedColumnCollection(this);
        }
        protected override BaseViewPrintInfo CreatePrintInfoInstance(PrintInfoArgs args) {
            return new CustomBandedPrintInfo(args);
        }

    }
    public class CustomBandedPrintInfo : BandedGridViewPrintInfo {
        BrickStyle vert;
        public CustomBandedPrintInfo(PrintInfoArgs args)
            : base(args) {

        }
        protected override void SetDefaultBrickStyle(IBrickGraphics graph, BrickStyle style) {
            base.SetDefaultBrickStyle(graph, style);
        }

        protected override void PrintBandHeader(IBrickGraphics graph) {
            if(!View.OptionsPrint.PrintBandHeader) return;
            Rectangle r = Rectangle.Empty;
            Point indent = new Point(Indent, 0);
            bool usePrintStyles = View.OptionsPrint.UsePrintStyles;
            SetDefaultBrickStyle(graph, (BrickStyle)Bricks["BandPanel"].Clone());
            foreach(PrintBandInfo band in Bands) {
                r = band.Bounds;
                r.Offset(indent);
                if(!usePrintStyles && band.Band != null) {
                    AppearanceObject temp = new AppearanceObject();
                    AppearanceHelper.Combine(temp, new AppearanceObject[] { band.Band.AppearanceHeader, AppearancePrint.BandPanel });
                    SetDefaultBrickStyle(graph, Bricks.Create(temp, BorderSide.All, temp.BorderColor, 1));
                }
                var brick = DrawTextBrick(graph, band.Band.GetTextCaption(), r);
                brick.Style = (BrickStyle)brick.Style.Clone();
                brick.Style.StringFormat.ChangeFormatFlags(StringFormatFlags.NoWrap | StringFormatFlags.LineLimit);


            }
        }
        public override void PrintHeader(IBrickGraphics graph) {
            vert = new BrickStyle(Bricks["HeaderPanel"]);
            vert.StringFormat = vert.StringFormat.ChangeFormatFlags(StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft);
            base.PrintHeader(graph);
            if(!View.OptionsPrint.PrintHeader) return;
            Point indent = new Point(Indent, HeaderY);
            Rectangle r = Rectangle.Empty;
            bool usePrintStyles = View.OptionsPrint.UsePrintStyles;
            SetDefaultBrickStyle(graph, Bricks["HeaderPanel"]);

            int HeaderHeight = (this.View as CustomBandedGridView).HeaderHeightOnExport;
          
            Dictionary<PrintColumnInfo, ITextBrick> columnBricks = new Dictionary<PrintColumnInfo, ITextBrick>();
            foreach(PrintColumnInfo col in Columns) {
                if(!usePrintStyles) {
                    AppearanceObject temp = new AppearanceObject();
                    AppearanceHelper.Combine(temp, new AppearanceObject[] { col.Column.AppearanceHeader, View.Appearance.HeaderPanel, AppearancePrint.HeaderPanel });
                    SetDefaultBrickStyle(graph, Bricks.Create(temp, BorderSide.All, temp.BorderColor, 1));
                }
                r = col.Bounds;
                r = new Rectangle(r.Location, new Size(r.Width, HeaderHeight));
                r.Offset(indent);
                MethodInfo mi = typeof(GridColumn).GetMethod("GetTextCaptionForPrinting", BindingFlags.NonPublic | BindingFlags.Instance);
                string caption = (string)mi.Invoke(col.Column, new object[] { });//col.Column.GetTextCaptionForPrinting();
                if(!col.Column.OptionsColumn.ShowCaption) caption = string.Empty;
                ITextBrick itb = DrawTextBrick(graph, caption, r);
                if((col.Column is CustomBandedColumn) && ((col.Column as CustomBandedColumn).CaptionVerticalOnExport))
                    itb.Style = vert;
                else {
                    SetDefaultBrickStyle(graph, Bricks["HeaderPanel"]);
                }
                if(caption.Contains(Environment.NewLine))
                    itb.Style.StringFormat = BrickStringFormat.Create(itb.Style.TextAlignment, true);

                if(AppearancePrint.HeaderPanel.TextOptions.WordWrap == WordWrap.NoWrap && View.OptionsPrint.UsePrintStyles) {
                    using(Graphics g = this.View.GridControl.CreateGraphics()) {
                        SizeF s = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value);
                        if(s.Width + 5 >= r.Width) {
                            itb.Text = "";
                            itb.TextValue = "";
                        }
                    }
                }
                columnBricks.Add(col, itb);
                using(Graphics g = this.View.GridControl.CreateGraphics()) {
                    SizeF s = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value);
                    if(s.Height + 5 >= r.Height) {
                        itb.Text = "";
                        itb.TextValue = "";
                    }
                }
            }
        }
    }

    class MyBandedColumnCollection : BandedGridColumnCollection {
        public MyBandedColumnCollection(ColumnView view)
            : base(view) {

        }
        protected override GridColumn CreateColumn() {
            return new CustomBandedColumn();
        }

    }
    class CustomBandedColumn : BandedGridColumn {
        public CustomBandedColumn() {
            CaptionVerticalOnExport = false;
        }
        protected override void Assign(GridColumn column) {
            if(column is CustomBandedColumn)
                this.CaptionVerticalOnExport = ((CustomBandedColumn)column).CaptionVerticalOnExport;
            base.Assign(column);
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool CaptionVerticalOnExport { get; set; }
    }
}
