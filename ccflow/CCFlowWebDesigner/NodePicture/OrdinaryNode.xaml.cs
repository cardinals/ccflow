﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BP.Picture
{
    public partial class OrdinaryNode : UserControl, IFlowNodePicture
    {
        public OrdinaryNode()
        {
            InitializeComponent();
        }
        public new double Opacity
        {
            set { picRect.Opacity = value; }
            get { return picRect.Opacity; }
        }
        public double PictureWidth
        {
            set
            {
                picRect.Width = value - 4;
            }
            get
            {
                return picRect.Width + 4;
            }
        }
        public double PictureHeight
        {
            set
            {
                picRect.Height = value - 4;
            }
            get { return picRect.Height + 4; }
        }
        public new Brush Background
        {
            set
            {
                picRect.Fill = value;
            }
            get { return picRect.Fill; }
        }
        public Visibility PictureVisibility
        {
            set
            {
                this.Visibility = value;
            }
            get
            {
                return this.Visibility;
            }
        }
        public void ResetInitColor()
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush = new SolidColorBrush();
            brush.Color = Colors.Green;
            picRect.Fill = brush;
        }

        public void SetWarningColor()
        {
            picRect.Fill = SystemConst.ColorConst.WarningColor;
            //  picCenter.Fill = SystemConst.ColorConst.WarningColor;  
        }

        public void SetSelectedColor()
        {
            picRect.Fill = SystemConst.ColorConst.SelectedColor;
            //  picCenter.Fill = SystemConst.ColorConst.SelectedColor;  

        }
        public PointCollection ThisPointCollection
        {
            get { return null; }
        }
    }
}
