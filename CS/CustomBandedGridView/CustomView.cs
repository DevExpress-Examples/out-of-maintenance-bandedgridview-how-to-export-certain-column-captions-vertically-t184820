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
    public class CustomView : GridView {
        public const string SViewName = "CustomView";
        public CustomView() : this(null) { }
        public CustomView(GridControl grid) : base(grid) {
            HeaderHeightOnExport = 50;
        }
        public int HeaderHeightOnExport { get; set; }
        protected override string ViewName { get { return SViewName; } }

        protected override BaseViewPrintInfo CreatePrintInfoInstance(PrintInfoArgs args) {
            return new CustomPrintInfo(args);
        }
    }
    public class CustomPrintInfo : GridViewPrintInfo {
        public CustomPrintInfo(PrintInfoArgs args) : base(args) { }

        public override void PrintHeader(BrickGraphics graph) {
            base.PrintHeader(graph);
            if(!View.OptionsPrint.PrintHeader) return;
            Point indent = new Point(Indent, HeaderY);
            Rectangle r = Rectangle.Empty;
            bool usePrintStyles = View.OptionsPrint.UsePrintStyles;
            SetDefaultBrickStyle(graph, Bricks["HeaderPanel"]);

            int HeaderHeight = 50;
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

                if(caption.Contains(Environment.NewLine))
                    itb.Style.StringFormat = BrickStringFormat.Create(itb.Style.TextAlignment, true);
                itb.Style.StringFormat = itb.Style.StringFormat.ChangeFormatFlags(StringFormatFlags.DirectionVertical | StringFormatFlags.DirectionRightToLeft);

                if(AppearancePrint.HeaderPanel.TextOptions.WordWrap == WordWrap.NoWrap && View.OptionsPrint.UsePrintStyles) {
                    using(Graphics g = this.View.GridControl.CreateGraphics()) {
                        SizeF s = g.MeasureString(itb.Text, itb.Font, 1000, itb.StringFormat.Value);
                        if(s.Width + 5 >= r.Width) {
                            itb.Text = "";
                            itb.TextValue = "";
                        }
                    }
                }
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
}
