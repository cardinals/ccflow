﻿#pragma checksum "D:\ccflow.SLPrj\Designer\FlowWebDesigner\Controls\ToolbarButton.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8A63792783D42C6DFE25846BEAFFA2AA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.235
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


namespace WF.Controls {
    
    
    public partial class ToolbarButton : System.Windows.Controls.Button {
        
        internal System.Windows.Media.Animation.Storyboard Activate;
        
        internal System.Windows.Media.Animation.Storyboard Deactivate;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WF;component/Controls/ToolbarButton.xaml", System.UriKind.Relative));
            this.Activate = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Activate")));
            this.Deactivate = ((System.Windows.Media.Animation.Storyboard)(this.FindName("Deactivate")));
        }
    }
}

