using GoodsStoreUWP.Data.Repositories;
using GoodsStoreUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.MVVM.ViewModels.ShopCart
{
    public class ShopCartViewModel : ViewModel
    {
        private readonly IRepository<ShopCartItem> _cartRepository;
        private ObservableCollection<ShopCartItem> _items;

        public ObservableCollection<ShopCartItem> ShopCartItems
        {
            get => _items;
            set
            {
                _items = value;

            }
        }
        public ShopCartViewModel(IRepository<ShopCartItem> cartRepository)
        {
            _cartRepository = cartRepository;

            InitializeShopCart();
        }

        private void InitializeShopCart()
        {
            var shopcart = _cartRepository.GetAll();
            ShopCartItems = new ObservableCollection<ShopCartItem>(shopcart);
        }

        public void AddProduct(int productId)
        {
            var product = _cartRepository.GetAll().FirstOrDefault(c => c.ProductId == productId);
            if (product != null)
            {
                product.Quantity++;
            }
            else
            {
                _cartRepository.Add(new ShopCartItem { ProductId = productId });
            }
        }

        public ObservableCollection<ShopCartItem> GetCartItems()
        {
            return new ObservableCollection<ShopCartItem>(_cartRepository.GetAll());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RemoveProduct(int productId)
        {
            var product = _cartRepository.GetAll().FirstOrDefault(c => c.ProductId == productId);
            if (product != null)
            {
                _cartRepository.Remove(product);
            }
        }
    }
}
