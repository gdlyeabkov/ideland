﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "51AE5DDAE7F825A9578FE2B45620946C42C667EA50BF2BDF86405AFD05F966A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Ideland;
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


namespace Ideland {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem saveFileMenuItem;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem closeProjectBtn;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel tabs;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock startActiveTab;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel explorer;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel editor;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel terminal;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel executedCommands;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock currentDir;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox terminalInput;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl editorTabs;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel lines;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sourceCode;
        
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
            System.Uri resourceLocater = new System.Uri("/Ideland;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 13 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenProjectHandler);
            
            #line default
            #line hidden
            return;
            case 2:
            this.saveFileMenuItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 14 "..\..\MainWindow.xaml"
            this.saveFileMenuItem.Click += new System.Windows.RoutedEventHandler(this.SaveFileHandler);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 16 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetAppearanceHandler);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetAppearanceHandler);
            
            #line default
            #line hidden
            return;
            case 5:
            this.closeProjectBtn = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.closeProjectBtn.Click += new System.Windows.RoutedEventHandler(this.CloseProjectHandler);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 20 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.QuitHandler);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 23 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.FindInFilesHandler);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 26 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTerminalHandler);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tabs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.startActiveTab = ((System.Windows.Controls.TextBlock)(target));
            
            #line 34 "..\..\MainWindow.xaml"
            this.startActiveTab.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SelectTabHandler);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            this.startActiveTab.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TabHoverHandler);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            this.startActiveTab.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TabHoutHandler);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SelectTabHandler);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.TabHoverHandler);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.TabHoutHandler);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 36 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SelectTabHandler);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.TabHoverHandler);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.TabHoutHandler);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 37 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SelectTabHandler);
            
            #line default
            #line hidden
            
            #line 37 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.TabHoverHandler);
            
            #line default
            #line hidden
            
            #line 37 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.TabHoutHandler);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 38 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SelectTabHandler);
            
            #line default
            #line hidden
            
            #line 38 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.TabHoverHandler);
            
            #line default
            #line hidden
            
            #line 38 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.TabHoutHandler);
            
            #line default
            #line hidden
            return;
            case 15:
            this.explorer = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 16:
            this.editor = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 17:
            this.terminal = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 18:
            
            #line 62 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.KillTerminalHandler);
            
            #line default
            #line hidden
            return;
            case 19:
            this.executedCommands = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 20:
            this.currentDir = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.terminalInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\MainWindow.xaml"
            this.terminalInput.KeyUp += new System.Windows.Input.KeyEventHandler(this.ExecuteCommandHandler);
            
            #line default
            #line hidden
            return;
            case 22:
            this.editorTabs = ((System.Windows.Controls.TabControl)(target));
            return;
            case 23:
            this.lines = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 24:
            this.sourceCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 79 "..\..\MainWindow.xaml"
            this.sourceCode.KeyUp += new System.Windows.Input.KeyEventHandler(this.CodeHotKeysHandler);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

