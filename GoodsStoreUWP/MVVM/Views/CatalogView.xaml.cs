using GoodsStoreUWP.Data.Base;
using GoodsStoreUWP.Models;
using GoodsStoreUWP.MVVM.ViewModels.Catalog;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml.Controls;

namespace GoodsStoreUWP.MVVM.Views
{
    public sealed partial class CatalogView : Page
    {
        public CatalogView(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            var repository = serviceProvider.GetService<IRepository<Product, int>>();
            DataContext = new CatalogViewModel(repository);
        }
    }
}
