﻿#pragma checksum "..\..\..\Windows\LoginWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "BD80A50319BEFA36C463DA418F01E908BEA266232735F5B39E18688809AE78F7"
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
using Cinema.Languages;
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
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridSignUp;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnStateMin;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnExit;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLogin;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkRememberMe;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Windows\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button bttnSignIn;
        
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
            System.Uri resourceLocater = new System.Uri("/Cinema;component/windows/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\LoginWindow.xaml"
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
            
            #line 16 "..\..\..\Windows\LoginWindow.xaml"
            ((Cinema.LoginWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LogWindow_MouseDown);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\Windows\LoginWindow.xaml"
            ((Cinema.LoginWindow)(target)).Closed += new System.EventHandler(this.LogWindow_Closed);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\Windows\LoginWindow.xaml"
            ((Cinema.LoginWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.LogWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gridSignUp = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.bttnStateMin = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Windows\LoginWindow.xaml"
            this.bttnStateMin.Click += new System.Windows.RoutedEventHandler(this.bttnStateMin_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.bttnExit = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Windows\LoginWindow.xaml"
            this.bttnExit.Click += new System.Windows.RoutedEventHandler(this.bttnExit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtLogin = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\Windows\LoginWindow.xaml"
            this.txtLogin.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtLogin_TextChanged);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\Windows\LoginWindow.xaml"
            this.txtLogin.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtLogin_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 54 "..\..\..\Windows\LoginWindow.xaml"
            this.txtPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.txtPassword_PasswordChanged);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\Windows\LoginWindow.xaml"
            this.txtPassword.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.txtPassword_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.checkRememberMe = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.bttnSignIn = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Windows\LoginWindow.xaml"
            this.bttnSignIn.Click += new System.Windows.RoutedEventHandler(this.bttnSignIn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

