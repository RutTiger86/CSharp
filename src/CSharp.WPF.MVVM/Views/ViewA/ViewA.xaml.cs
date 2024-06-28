﻿using CSharp.WPF.MVVM.ViewModels.ViewA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharp.WPF.MVVM.Views.ViewA
{
    /// <summary>
    /// ViewA.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewA : BaseView
    {
        public ViewA(ViewAViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}