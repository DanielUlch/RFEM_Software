﻿using RFEMSoftware.Simulation.Infrastructure.Models;
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

namespace RFEMSoftware.Simulation.Desktop.Forms
{
    /// <summary>
    /// Interaction logic for RFlow3DForm.xaml
    /// </summary>
    public partial class RFlow3DForm : UserControl, ISimView
    {
        private RFlow3DViewModel _ViewModel;
        public RFlow3DForm()
        {
            InitializeComponent();

            _ViewModel = new RFlow3DViewModel();

            this.DataContext = _ViewModel;

        }
        public RFlow3DForm(RFlow3D model)
        {
            InitializeComponent();

            _ViewModel = new RFlow3DViewModel(model);

            this.DataContext = _ViewModel;
        }

        public ISimViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
        }

        public string GetHoveredHelpTopic()
        {
            throw new NotImplementedException();
        }
    }
}
