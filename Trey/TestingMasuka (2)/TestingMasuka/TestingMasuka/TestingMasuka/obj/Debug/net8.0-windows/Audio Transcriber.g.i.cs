﻿#pragma checksum "..\..\..\Audio Transcriber.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0FCBFD2BDA6380A08580603608245E9CA7379F36"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Manufaktura.Controls.WPF;
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
using TestingMasuka;
using WPFSoundVisualizationLib;


namespace TestingMasuka {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menu_file;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem btnHelp;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRecord;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Manufaktura.Controls.WPF.NoteViewer noteViewer1;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoadFile;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Audio Transcriber.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFileSelected;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TestingMasuka;component/audio%20transcriber.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Audio Transcriber.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menu_file = ((System.Windows.Controls.MenuItem)(target));
            
            #line 18 "..\..\..\Audio Transcriber.xaml"
            this.menu_file.Click += new System.Windows.RoutedEventHandler(this.btnMenu_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 19 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnOpen_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 20 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 23 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 24 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnUndo_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 25 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnRedo_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 27 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnExportPDF_click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 28 "..\..\..\Audio Transcriber.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnExit_click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnHelp = ((System.Windows.Controls.MenuItem)(target));
            
            #line 29 "..\..\..\Audio Transcriber.xaml"
            this.btnHelp.Click += new System.Windows.RoutedEventHandler(this.btnHelp_click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnRecord = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\Audio Transcriber.xaml"
            this.btnRecord.Click += new System.Windows.RoutedEventHandler(this.btnRecord_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.noteViewer1 = ((Manufaktura.Controls.WPF.NoteViewer)(target));
            return;
            case 12:
            this.btnLoadFile = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Audio Transcriber.xaml"
            this.btnLoadFile.Click += new System.Windows.RoutedEventHandler(this.btnLoadFile_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.txtFileSelected = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\..\Audio Transcriber.xaml"
            this.txtFileSelected.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtFileSelected_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

