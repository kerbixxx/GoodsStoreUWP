using CommunityToolkit.Mvvm.ComponentModel;
using GoodsStoreUWP.Data.Base;
using GoodsStoreUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.MVVM.ViewModels.Catalog
{
    public class CatalogViewModel : ViewModel
    {
        private readonly IRepository<Product, int> _productRepository;
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        public CatalogViewModel(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _productRepository.GetAll();
            Products = new ObservableCollection<Product>(products);
        }

    }
}
