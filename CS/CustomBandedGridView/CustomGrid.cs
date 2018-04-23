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
    [ToolboxItemAttribute(true)]
    public class CustomGridControl : GridControl {

        protected override void RegisterAvailableViewsCore(InfoCollection collection) {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridViewInfoRegistrator());
            collection.Add(new MyBandedGridViewInfoRegistrator());
        }

    }
    public class MyGridViewInfoRegistrator : GridInfoRegistrator {
        public override string ViewName { get { return CustomView.SViewName; } }
        public override BaseView CreateView(GridControl grid) {
            return new CustomBandedGridView(grid as GridControl);
        }
    }
    public class MyBandedGridViewInfoRegistrator : BandedGridInfoRegistrator {
        public override string ViewName { get { return CustomBandedGridView.SViewName; } }
        public override BaseView CreateView(GridControl grid) {
            return new CustomBandedGridView(grid as GridControl);
        }
    }
    
}
