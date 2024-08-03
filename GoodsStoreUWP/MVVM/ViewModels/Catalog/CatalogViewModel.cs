using GoodsStoreUWP.Data.Base;
using GoodsStoreUWP.MVVM.ViewModels.Catalog;
using GoodsStoreUWP.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsStoreUWP.MVVM.ViewModels.Catalog
{
    public class CatalogViewModel : ViewModel
    {
        private readonly IRepository<Product, int> _productRepository;
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public CatalogViewModel(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
            InitializeProducts();
        }

        private void InitializeProducts()
        {
            var productsFromDb = _productRepository.GetAll();
            Products = new ObservableCollection<Product>(productsFromDb);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
