using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace MyUnoApp.Extensions
{
    public class NavigationExtension
    {
        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationExtension), new PropertyMetadata(null));

        public static string GetNavigateTo(NavigationViewItem obj)
        {
            return (string)obj.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(NavigationViewItem obj, string value)
        {
            obj.SetValue(NavigateToProperty, value);
        }
    }
}
