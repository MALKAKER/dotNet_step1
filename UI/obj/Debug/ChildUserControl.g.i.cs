﻿#pragma checksum "..\..\ChildUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CEA052F9A05229014FA2F078102B1A59"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BE;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using UI;


namespace UI {
    
    
    /// <summary>
    /// Child
    /// </summary>
    public partial class ChildUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid inputChildDetails;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox iDTextBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateOfBirthDatePicker;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox pickLang;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isSpecial;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox childHMOComboBox;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateB;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\ChildUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removeB;
        
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
            System.Uri resourceLocater = new System.Uri("/UI;component/childusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChildUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 9 "..\..\ChildUserControl.xaml"
            ((UI.ChildUserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.inputChildDetails = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.firstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\ChildUserControl.xaml"
            this.firstNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.firstNameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\ChildUserControl.xaml"
            this.lastNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.lastNameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.iDTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 49 "..\..\ChildUserControl.xaml"
            this.iDTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.iDTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.dateOfBirthDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.pickLang = ((System.Windows.Controls.ListBox)(target));
            
            #line 54 "..\..\ChildUserControl.xaml"
            this.pickLang.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.pickLang_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.isSpecial = ((System.Windows.Controls.CheckBox)(target));
            
            #line 60 "..\..\ChildUserControl.xaml"
            this.isSpecial.Checked += new System.Windows.RoutedEventHandler(this.isSpecial_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.childHMOComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 61 "..\..\ChildUserControl.xaml"
            this.childHMOComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.childHMOComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 71 "..\..\ChildUserControl.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).Selected += new System.Windows.RoutedEventHandler(this.ListBoxItem_Selected);
            
            #line default
            #line hidden
            return;
            case 11:
            this.updateB = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\ChildUserControl.xaml"
            this.updateB.Click += new System.Windows.RoutedEventHandler(this.update_click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.removeB = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\ChildUserControl.xaml"
            this.removeB.Click += new System.Windows.RoutedEventHandler(this.remove_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

