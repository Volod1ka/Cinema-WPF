﻿#pragma checksum "..\..\..\Windows\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "647771E835C3C073E08DF8FB7A5F86B66418794A7995E069F70D6C728B026298"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Cinema;
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


namespace Cinema {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridTitleBar;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnExit;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnStateMin;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnStateChange;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnTemp;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuExit;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition gridLeft;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition gridRight;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lKey;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lName;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\Windows\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lDateTime;
        
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
            System.Uri resourceLocater = new System.Uri("/Cinema;component/windows/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\MainWindow.xaml"
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
            
            #line 11 "..\..\..\Windows\MainWindow.xaml"
            ((Cinema.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Windows\MainWindow.xaml"
            ((Cinema.MainWindow)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.Window_SizeChanged);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\Windows\MainWindow.xaml"
            ((Cinema.MainWindow)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 24 "..\..\..\Windows\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.bttnExit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.gridTitleBar = ((System.Windows.Controls.Grid)(target));
            
            #line 35 "..\..\..\Windows\MainWindow.xaml"
            this.gridTitleBar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.gridTitleBar_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bttnExit = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Windows\MainWindow.xaml"
            this.bttnExit.Click += new System.Windows.RoutedEventHandler(this.bttnExit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.bttnStateMin = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Windows\MainWindow.xaml"
            this.bttnStateMin.Click += new System.Windows.RoutedEventHandler(this.bttnStateMin_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bttnStateChange = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Windows\MainWindow.xaml"
            this.bttnStateChange.Click += new System.Windows.RoutedEventHandler(this.bttnStateChange_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.bttnTemp = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Windows\MainWindow.xaml"
            this.bttnTemp.Click += new System.Windows.RoutedEventHandler(this.bttnTemp_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.menuExit = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 9:
            this.gridLeft = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 10:
            this.gridRight = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 11:
            this.lKey = ((System.Windows.Controls.Label)(target));
            
            #line 66 "..\..\..\Windows\MainWindow.xaml"
            this.lKey.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lName_MouseDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.lName = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.lDateTime = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

