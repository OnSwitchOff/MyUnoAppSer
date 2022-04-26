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
using MyUnoApp.Models;
using MyUnoApp.Extensions;
using MyUnoApp.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MyUnoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Dictionary<string, SaleView> saleViewList;
        private NavigationViewItemBase? selectedItem;

        public MainPage()
        {
            this.InitializeComponent();

            this.saleViewList = new Dictionary<string, SaleView>();

            this.navigationView.ItemInvoked += this.NavigationView_ItemInvoked;
            this.navigationView.SelectionChanged += this.NavigationView_SelectionChanged;

            this.frame.Content = this.DefaultFrame;
            this.frame.Navigated += this.Frame_Navigated;
        }

        private DefaultView DefaultFrame { get; } = new DefaultView();

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is SaleView page && e.Parameter is SaleViewModel viewModel)
            {
                page.ViewModel = viewModel;
            }
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItemBase itemBase && itemBase.Tag == null)
            {
                this.selectedItem = itemBase;
            }
            else
            {
                this.selectedItem = null;
            }
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

            if (args.InvokedItemContainer is NavigationViewItem selectedItem)
            {
                var viewKey = selectedItem.GetValue(NavigationExtension.NavigateToProperty) as string;

                if (viewKey is not null)
                {
                    switch (viewKey)
                    {
                        case "MyUnoApp.Models.SaleViewModel":
                            if (this.selectedItem != null)
                            {
                                this.selectedItem.IsSelected = false;
                            }

                            if (selectedItem.Name == "NewSale")
                            {
                                this.frame.Navigate(typeof(SaleView));
                                if (this.frame.Content is SaleView saleView)
                                {
                                    saleView.ViewModel.PageClosing += this.PageClose;

                                    NavigationViewItem saleItem = new NavigationViewItem()
                                    {
                                        Content = "Sale",
                                        Icon = new BitmapIcon()
                                        {
                                            UriSource = new System.Uri("ms-appx:///Assets/settings.png"),
                                            ShowAsMonochrome = false,
                                            Width = 18,
                                            Height = 18,
                                            Margin = new Thickness(0),
                                        },
                                        Tag = saleView.ViewModel.PageId,
                                        IsSelected = true,
                                    };
                                    saleItem?.SetValue(NavigationExtension.NavigateToProperty, "MyUnoApp.Models.SaleViewModel");

                                    this.saleViewList.Add(saleView.ViewModel.PageId, saleView);
                                    this.navigationView.MenuItems.Add(saleItem);
                                    this.navigationView.SelectedItem = saleItem;
                                }
                            }
                            else
                            {
                                if (selectedItem.Tag != null &&
                                    selectedItem.Tag is string key &&
                                    this.saleViewList.ContainsKey(key))
                                {
                                    this.frame.Content = this.saleViewList[key];
                                }

                                return;
                            }

                            break;
                        case "MyUnoApp.Models.DocumentViewModel":
                            this.frame.Navigate(typeof(DocumentView));
                            break;
                        default:
                            this.frame.Content = this.DefaultFrame;
                            break;
                    }
                }
            }
        }

        private void PageClose(string pageId)
        {
            switch (this.frame.Content)
            {
                case SaleView sale:
                    for (int i = this.navigationView.MenuItems.Count - 1; i >= 0; i--)
                    {
                        if (this.navigationView.MenuItems[i] is NavigationViewItem item)
                        {
                            if (item.Tag != null && item.Tag.ToString() == pageId)
                            {
                                this.navigationView.MenuItems.Remove(item);
                                this.saleViewList.Remove(pageId);
                                sale.ViewModel.PageClosing -= this.PageClose;
                                break;
                            }
                        }
                    }

                    break;
                default:
                    if (this.frame.Content is Page page)
                    {
                        page.NavigationCacheMode = NavigationCacheMode.Disabled;
                    }

                    break;
            }

            if (this.saleViewList.Count > 0)
            {
                this.frame.Content = this.saleViewList.Values.Last();
                this.navigationView.SelectedItem = this.navigationView.MenuItems.Last();
            }
            else
            {
                this.frame.Content = this.DefaultFrame;
                this.navigationView.SelectedItem = null;
            }
        }

        public MainViewModel ViewModel { get; } = new MainViewModel();
    }
}
