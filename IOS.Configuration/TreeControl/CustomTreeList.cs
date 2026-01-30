using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Handler;
using DevExpress.XtraTreeList.Nodes;

namespace IOS.Configuration.TreeListControl {
    public class CustomTreeList : TreeList {
        public CustomTreeList() {

        }
        protected CustomTreeList(object ignore)
            : base(ignore) {

        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e) {
            TreeListHitInfo hitInfo = this.CalcHitInfo(e.Location);
            if (hitInfo.Node != null) {
                FocusedNode = hitInfo.Node;
                if (hitInfo.Column != null)
                    FocusedColumn = hitInfo.Column;
            }
            base.OnMouseUp(e);
        }
        protected override DevExpress.XtraTreeList.Handler.TreeListHandler CreateHandler() {
            return new CustomTreeListHandler(this);
        }
        internal new TreeListNode PressedNode {
            get { return base.PressedNode; }
            set {
                base.PressedNode = value;
            }
        }
        protected override int FocusedRowIndex {
            get {
                return base.FocusedRowIndex;
            }
            set {
                CustomNodeDraggingState state = Handler.ControlState as CustomNodeDraggingState;
                if (state != null && state.isEndNodeDragging) return;
                base.FocusedRowIndex = value;
            }
        }
        new CustomTreeListHandler Handler {
            get { return base.Handler as CustomTreeListHandler; }
        }
    }
    public class CustomTreeListHandler : TreeListHandler {
        public CustomTreeListHandler(TreeList treeList)
            : base(treeList) {

        }
        protected override TreeListHandler.TreeListControlState CreateState(TreeListState state) {
            if (state == TreeListState.NodePressed)
                return new CustomNodePressedState(this);
            if (state == TreeListState.NodeDragging)
                return new CustomNodeDraggingState(this);
            return base.CreateState(state);
        }
        internal DevExpress.XtraTreeList.Handler.TreeListHandler.TreeListControlState ControlState {
            get { return this.fControlState; }
        }
    }
    public class CustomNodePressedState : TreeListHandler.NodePressedState {
        public CustomNodePressedState(TreeListHandler handler)
            : base(handler) {

        }
        protected override void ChangeSelection(DevExpress.XtraTreeList.ViewInfo.RowInfo pressRowInfo) {
            //base.ChangeSelection(pressRowInfo);
        }
    }
    public class CustomNodeDraggingState : TreeListHandler.NodeDraggingState {
        public CustomNodeDraggingState(TreeListHandler handler)
            : base(handler) {

        }
        internal bool isEndNodeDragging = false;
        //public override void DoEndNodeDragging(System.Drawing.Point p, TreeListNode nodeTo, bool copy) {
        //    try {
        //        isEndNodeDragging = true;
        //        base.DoEndNodeDragging(p, nodeTo, copy);
        //    }
        //    finally {
        //        isEndNodeDragging = false;
        //    }
        //}
      

    }
}
