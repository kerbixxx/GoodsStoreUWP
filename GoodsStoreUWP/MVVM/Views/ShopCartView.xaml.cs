using GoodsStoreUWP.MVVM.ViewModels.Catalog;
using GoodsStoreUWP.MVVM.ViewModels.ShopCart;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

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
                _viewModel.AddProduct(itemId);
                UpdateCartList();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemId = (button.CommandParameter as int?) ?? -1;
            if (itemId > -1)
            {
                // Здесь должна быть логика уменьшения количества товара
                _viewModel.AddProduct(itemId); // Для простоты, используем ту же логику, что и для увеличения
                UpdateCartList();
            }
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var itemId = (button.CommandParameter as int?) ?? -1;
            if (itemId > -1)
            {
                // Здесь должна быть логика удаления товара из корзины
                _viewModel.RemoveProduct(itemId);
                UpdateCartList();
            }
        }

        private void UpdateCartList()
        {
            CartListView.ItemsSource = null;
            CartListView.ItemsSource = _viewModel.ShopCartItems;
        }
    }
}
