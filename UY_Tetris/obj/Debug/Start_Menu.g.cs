﻿#pragma checksum "..\..\Start_Menu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A70D179BD176197955C9863A85F9B9C98B0BC02DAE5C11769B2800BB48E5AAAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

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
using UY_Tetris;


namespace UY_Tetris {
    
    
    /// <summary>
    /// Start_Menu
    /// </summary>
    public partial class Start_Menu : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Single_Play;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Multi_Play;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Unlimited_Play;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button back;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AI_Good;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Start_Menu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button vsAI;
        
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
            System.Uri resourceLocater = new System.Uri("/UY_Tetris;component/start_menu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Start_Menu.xaml"
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
            this.Single_Play = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\Start_Menu.xaml"
            this.Single_Play.Click += new System.Windows.RoutedEventHandler(this.Single_Play_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Multi_Play = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\Start_Menu.xaml"
            this.Multi_Play.Click += new System.Windows.RoutedEventHandler(this._2P_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Unlimited_Play = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\Start_Menu.xaml"
            this.Unlimited_Play.Click += new System.Windows.RoutedEventHandler(this.vs2P_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.back = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\Start_Menu.xaml"
            this.back.Click += new System.Windows.RoutedEventHandler(this.back_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.AI_Good = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Start_Menu.xaml"
            this.AI_Good.Click += new System.Windows.RoutedEventHandler(this.AIGood_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.vsAI = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Start_Menu.xaml"
            this.vsAI.Click += new System.Windows.RoutedEventHandler(this.vsAI_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

