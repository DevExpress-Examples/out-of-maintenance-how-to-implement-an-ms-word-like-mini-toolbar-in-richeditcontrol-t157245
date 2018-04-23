using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Controls;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.ViewInfo;

namespace RTFRibbonMini {
    public class MyRibbonControl : RibbonControl {

        public MyRibbonControl() {
        }
        protected override RibbonBarManager CreateBarManager() {
            return new MyRibbonBarManager(this);
        }
    }



    public class MyRibbonBarManager : RibbonBarManager {
        public MyRibbonBarManager(RibbonControl ribbonControl)
            : base(ribbonControl) {

        }
        protected override BarSelectionInfo CreateSelectionInfo() {
            return new AdvancedBarSelectionInfo(this);
        }
    }



    public class AdvancedBarSelectionInfo : RibbonSelectionInfo {
        public AdvancedBarSelectionInfo(RibbonBarManager manager)
            : base(manager) {
        }
        protected override void CheckAndClosePopups(BarItemLink newLink) {
            BarPopupCollection popups = OpenedPopups;
            PopupMenuBarControl miniToolBar = popups.OfType<PopupMenuBarControl>().FirstOrDefault();
            if(miniToolBar != null)
                return;

            base.CheckAndClosePopups(newLink);
        }

        protected override void PressLinkCore(BarItemLink link, bool isArrow) {
            for(int n = OpenedPopups.Count - 1; n >= 0; n--) {
                IPopup popup = OpenedPopups[n] as IPopup;
                if(popup.ContainsLink(link)) {

                    for(int i = 0; i < Manager.Ribbon.MiniToolbars.Count; i++) {
                        Manager.Ribbon.MiniToolbars[i].Hide();
                    }
                }
            }
            base.PressLinkCore(link, isArrow);
        }
    }
}
