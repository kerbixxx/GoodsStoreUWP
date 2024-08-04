using Prism.Commands;
using GoodsStoreUWP.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using GoodsStoreUWP.Data.Repositories;
using System.Linq;
using System;

namespace GoodsStoreUWP.MVVM.ViewModels.Catalog
{
    public class CatalogViewModel : ViewModel
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShopCartItem> _cartRepository;
        private ObservableCollection<Product> _products;
        public ICommand AddToCartCommand { get; private set; }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public CatalogViewModel(IRepository<Product> productRepository, IRepository<ShopCartItem> cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            InitializeProducts();

            CartItems = new ObservableCollection<ShopCartItem>(_cartRepository.GetAll());
            AddToCartCommand = new DelegateCommand<int?>(OnAddToCart);
        }

        private void OnAddToCart(int? productId)
        {

            if (!productId.HasValue) return;

            var product = _productRepository.GetById(productId.Value);
            var cartItem = _cartRepository.GetAll().FirstOrDefault(c => c.ProductId == product.Id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                _cartRepository.Add(new ShopCartItem { ProductId = product.Id, Quantity = 1 });
            }

            OnPropertyChanged(nameof(CartItems));
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

        private ObservableCollection<ShopCartItem> _cartItems;
        public ObservableCollection<ShopCartItem> CartItems
        {
            get => _cartItems;
            set
            {
                _cartItems = value;
                OnPropertyChanged(nameof(CartItems));
            }
        }
    }
}
