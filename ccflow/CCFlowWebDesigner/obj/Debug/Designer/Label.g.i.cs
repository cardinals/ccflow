﻿#pragma checksum "D:\ccflow\CCFlowWebDesigner\Designer\Label.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "88E9DF646CF171528D44FE422FEB21AD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.239
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Ccflow.Web.UI.Control.Workflow.Designer {
    
    
    public partial class NodeLabel : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Media.Animation.Storyboard sbDisplay;
        
        internal System.Windows.Media.Animation.Storyboard sbClose;
        
        internal System.Windows.Controls.TextBlock txtLabelName;
        
        internal System.Windows.Controls.TextBox tbLabelName;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Designer/Label.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.sbDisplay = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbDisplay")));
            this.sbClose = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbClose")));
            this.txtLabelName = ((System.Windows.Controls.TextBlock)(this.FindName("txtLabelName")));
            this.tbLabelName = ((System.Windows.Controls.TextBox)(this.FindName("tbLabelName")));
        }
    }
}

