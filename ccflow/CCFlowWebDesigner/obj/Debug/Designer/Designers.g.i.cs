﻿#pragma checksum "D:\ccflow\CCFlowWebDesigner\Designer\Designers.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "821CCF0D0604CB9AB97EDA997DB0B8A7"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Liquid;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using WF.Controls;


namespace Ccflow.Web.UI.Control.Workflow.Designer {
    
    
    public partial class Designers : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel stackPanelLeft;
        
        internal System.Windows.Controls.Image imageLogo;
        
        internal System.Windows.Controls.TabControl TbcFDS;
        
        internal System.Windows.Controls.TabItem TbItemFlow;
        
        internal System.Windows.Controls.Canvas CvsFlowTree;
        
        internal Liquid.Tree TvwFlow;
        
        internal Liquid.Menu MuFlowTree;
        
        internal System.Windows.Controls.TabItem Tbwf;
        
        internal Liquid.Tree TvwFlowDataEnum;
        
        internal System.Windows.Controls.TabItem TbSystemMaintain;
        
        internal Liquid.Tree TvwSysMenu;
        
        internal System.Windows.Controls.StackPanel stackPanel2;
        
        internal WF.Controls.Toolbar toolbar1;
        
        internal WF.Controls.TabControlEx tbDesigner;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Designer/Designers.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.stackPanelLeft = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanelLeft")));
            this.imageLogo = ((System.Windows.Controls.Image)(this.FindName("imageLogo")));
            this.TbcFDS = ((System.Windows.Controls.TabControl)(this.FindName("TbcFDS")));
            this.TbItemFlow = ((System.Windows.Controls.TabItem)(this.FindName("TbItemFlow")));
            this.CvsFlowTree = ((System.Windows.Controls.Canvas)(this.FindName("CvsFlowTree")));
            this.TvwFlow = ((Liquid.Tree)(this.FindName("TvwFlow")));
            this.MuFlowTree = ((Liquid.Menu)(this.FindName("MuFlowTree")));
            this.Tbwf = ((System.Windows.Controls.TabItem)(this.FindName("Tbwf")));
            this.TvwFlowDataEnum = ((Liquid.Tree)(this.FindName("TvwFlowDataEnum")));
            this.TbSystemMaintain = ((System.Windows.Controls.TabItem)(this.FindName("TbSystemMaintain")));
            this.TvwSysMenu = ((Liquid.Tree)(this.FindName("TvwSysMenu")));
            this.stackPanel2 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel2")));
            this.toolbar1 = ((WF.Controls.Toolbar)(this.FindName("toolbar1")));
            this.tbDesigner = ((WF.Controls.TabControlEx)(this.FindName("tbDesigner")));
        }
    }
}

