using GoodsStoreUWP.MVVM.ViewModels.ShopCart;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GoodsStoreUWP.MVVM.Views
{
    public sealed partial class ShopCartView : Page
    {
        private readonly ShopCartViewModel _viewModel;

        public ShopCartView()
        {
            this.InitializeComponent();
            ServiceProvider serviceProvider = App.GetServiceProvider();

            _viewModel = serviceProvider.GetService<ShopCartViewModel>();

            this.DataContext = _viewModel;
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemId = (button.CommandParameter as int?) ?? -1;
            if (itemId > -1)
            {
                _viewModel.IncreaseQuantity(itemId);
                UpdateCartList();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemId = (button.CommandParameter as int?) ?? -1;
            if (itemId > -1)
            {
                _viewModel.DecreaseQuantity(itemId);
                UpdateCartList();
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemId = (button.CommandParameter as int?) ?? -1;
            if (itemId > -1)
            { 
                _viewModel.RemoveProduct(itemId);
                UpdateCartList();
            }
        }
        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SortKey = "Name";
            _viewModel.IsAscending = !_viewModel.IsAscending;
            _viewModel.SortByName();
            UpdateCartList();
        }

        private void SortByPrice_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SortKey = "Price";
            _viewModel.IsAscending = !_viewModel.IsAscending;
            _viewModel.SortByPrice();
            UpdateCartList();
        }

        private void UpdateCartList()
        {
            CartListView.ItemsSource = null;
            CartListView.ItemsSource = _viewModel.ShopCartItems;
            TotalSumTextBlock.Text = _viewModel.TotalSum.ToString();
            TotalQuantityTextBlock.Text = _viewModel.TotalQuantity.ToString();
        }
    }
}
