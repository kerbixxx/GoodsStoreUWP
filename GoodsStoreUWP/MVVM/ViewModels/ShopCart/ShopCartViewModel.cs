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

        public ObservableCollection<ShopCartItem> ShopCartItems { get; set; }
        public ShopCartViewModel(IRepository<ShopCartItem> cartRepository)
        {
            _cartRepository = cartRepository;

            InitializeShopCart();
        }

        private void InitializeShopCart()
        {
            var shopcart = _cartRepository.GetAll();
            ShopCartItems = new ObservableCollection<ShopCartItem>(shopcart);
            CalculateTotals();
        }

        public void CalculateTotals()
        {
            TotalQuantity = ShopCartItems.Sum(item => item.Quantity);
            TotalSum = ShopCartItems.Sum(item => item.Product.Price * item.Quantity);
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

        public void DecreaseQuantity(int shopcartitemId)
        {
            var obj = _cartRepository.GetById(shopcartitemId);
            if (obj != null)
            {
                if (obj.Quantity > 1)
                {
                    obj.Quantity--;
                    _cartRepository.Update(obj);
                }
                else
                {
                    _cartRepository.Remove(obj);
                }
                InitializeShopCart();
            }
        }

        public void IncreaseQuantity(int shopcartitemId)
        {
            var obj = _cartRepository.GetById(shopcartitemId);
            if (obj != null)
            {
                obj.Quantity++;
                _cartRepository.Update(obj);
            }
            InitializeShopCart();
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

        public void RemoveProduct(int shopcartitemId)
        {
            var obj = _cartRepository.GetAll().FirstOrDefault(c => c.Id == shopcartitemId);
            if (obj != null)
            {
                _cartRepository.Remove(obj);
            }
            InitializeShopCart();
        }

        private decimal _totalSum;
        public decimal TotalSum
        {
            get { return _totalSum; }
            set
            {
                if (_totalSum != value)
                {
                    _totalSum = value;
                    OnPropertyChanged(nameof(TotalSum));
                }
            }
        }

        private int _totalQuantity;
        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                if (_totalQuantity != value)
                {
                    _totalQuantity = value;
                    OnPropertyChanged(nameof(TotalQuantity));
                }
            }
        }
    }
}
