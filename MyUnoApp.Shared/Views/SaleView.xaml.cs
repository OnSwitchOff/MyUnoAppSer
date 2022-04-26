using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.ComponentModel;
using MyUnoApp.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MyUnoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaleView : Page, INotifyPropertyChanged
    {
        private SaleViewModel viewModel;

        public SaleView()
        {
            this.InitializeComponent();

            viewModel = new SaleViewModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SaleViewModel ViewModel
        {
            get => this.viewModel;
            set
            {
                this.viewModel = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ViewModel)));
            }
        }

        public CommunityToolkit.Mvvm.Input.IRelayCommand PageClose
        {
            get => new CommunityToolkit.Mvvm.Input.RelayCommand(() =>
            {
                this.ViewModel.RaisePageClosingEvent();
            });
        }
    }
}
