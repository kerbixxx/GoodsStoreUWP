using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace GoodsStoreUWP
{
    public sealed partial class MainPage : Page
    {
        private NavigationViewItem _lastItem;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == _lastItem)
            {
                return;
            }

            var clickedView = item.Tag?.ToString();
            if (!NavigateToView(clickedView)) return;
            _lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"GoodsStoreUWP.MVVM.Views.{clickedView}");

            if (string.IsNullOrEmpty(clickedView) || view == null)
            {
                return false;
            }

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "CatalogView")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

            ContentFrame.Navigate(typeof(MVVM.Views.CatalogView));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }
    }
}
