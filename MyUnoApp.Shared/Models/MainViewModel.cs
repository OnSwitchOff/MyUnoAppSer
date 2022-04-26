using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyUnoApp.Models
{
    public class MainViewModel : ObservableObject
    {
        private NavigationViewItem selected;

        public NavigationViewItem Selected
        {
            get => selected;
            set => SetProperty(ref selected, value);
        }
    }
}
