﻿#pragma checksum "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CB5C7C25792F33458897FDF26D55B363"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RFEMSoftware.Simulation.Desktop.Commands;
using RFEMSoftware.Simulation.Desktop.CustomControls;
using RFEMSoftware.Simulation.Desktop.Forms;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RFEMSoftware.Simulation.Desktop.Forms {
    
    
    /// <summary>
    /// RBear2DHistForm
    /// </summary>
    public partial class RBear2DHistForm : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border HistElement;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTitle;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateHistogram;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPopOutHistogram;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox gbFootingNumber;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbFootingOne;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbFootingTwo;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal RFEMSoftware.Simulation.Desktop.Forms.HistogramFormCore HistCore;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RFEMDesktopClient;component/forms/histogram%20views/rbear2dhistform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 18 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.HelpClickStub);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.HelpClickExeStub);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HistElement = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.txtTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.btnUpdateHistogram = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            this.btnUpdateHistogram.Click += new System.Windows.RoutedEventHandler(this.btnUpdateHistogram_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnPopOutHistogram = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            this.btnPopOutHistogram.Click += new System.Windows.RoutedEventHandler(this.btnPopOutHistogram_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.gbFootingNumber = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 7:
            this.rbFootingOne = ((System.Windows.Controls.RadioButton)(target));
            
            #line 99 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            this.rbFootingOne.Checked += new System.Windows.RoutedEventHandler(this.rbFootingOne_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.rbFootingTwo = ((System.Windows.Controls.RadioButton)(target));
            
            #line 112 "..\..\..\..\Forms\Histogram Views\RBear2DHistForm.xaml"
            this.rbFootingTwo.Checked += new System.Windows.RoutedEventHandler(this.rbFootingTwo_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.HistCore = ((RFEMSoftware.Simulation.Desktop.Forms.HistogramFormCore)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

