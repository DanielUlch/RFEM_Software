﻿using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Infrastructure;
using RFEMSoftware.Simulation.Infrastructure.Models;
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
    /// Interaction logic for RPill3DForm.xaml
    /// </summary>
    public partial class RPill3DForm : UserControl, ISimView 
    {
        private RPill3DViewModel _ViewModel;


        public RPill3DForm(RPill3DViewModel viewModel)
        {
            InitializeComponent();

            _ViewModel = viewModel;

            this.DataContext = _ViewModel;

            dgCorrelationMatrix.ItemsSource = DataGridMatrixHelper.GetBindable2DArray(_ViewModel.CorrelationMatrix);
        }


        public string GetHoveredHelpTopic()
        {
            throw new NotImplementedException();
        }

        private void dgCorrelationMatrix_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn column = e.Column as DataGridTextColumn;
            int header;
            if (int.TryParse(column.Header.ToString(), out header))
            {
                column.Header = ((REarthSoilProperties)header).ToUIString();
            }

            Binding binding = column.Binding as Binding;
            binding.Path = new PropertyPath(binding.Path.Path + ".Value");
        }

        private void dgCorrelationMatrix_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = ((REarthSoilProperties)e.Row.GetIndex()).ToUIString();
        }
    }
}
