using GoodsStoreUWP.MVVM.ViewModels.Catalog;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;

namespace GoodsStoreUWP.MVVM.Views
{
    public sealed partial class CatalogView : Page
    {
        private readonly CatalogViewModel _viewModel;
        public CatalogView()
        {
            this.InitializeComponent();
            ServiceProvider serviceProvider = App.GetServiceProvider();

            _viewModel = serviceProvider.GetService<CatalogViewModel>();

            this.DataContext = _viewModel;
        }
    }
}
