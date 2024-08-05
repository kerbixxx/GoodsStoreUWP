using CommunityToolkit.Mvvm.Input;
using GoodsStoreUWP.Data.Repositories;
using GoodsStoreUWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;

namespace GoodsStoreUWP.MVVM.ViewModels.ShopCart
{
    public class ShopCartViewModel : ViewModel
    {
        private readonly IRepository<ShopCartItem> _cartRepository;
        private ObservableCollection<ShopCartItem> _items;
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand SortByNameCommand { get; }
        public ICommand SortByPriceCommand { get; }
        public ObservableCollection<ShopCartItem> ShopCartItems { get; set; }
        public ShopCartViewModel(IRepository<ShopCartItem> cartRepository)
        {
            _cartRepository = cartRepository;
            InitializeShopCart();
            LoadSortStateAsync();
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
                    ShopCartItems.Remove(obj);
                }
                CalculateTotals();
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
            CalculateTotals();
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
                var newItem = new ShopCartItem { ProductId = productId };
                _cartRepository.Add(newItem);
                ShopCartItems.Add(newItem);
            }
            CalculateTotals();
        }
        public void RemoveProduct(int shopcartitemId)
        {
            var obj = _cartRepository.GetAll().FirstOrDefault(c => c.Id == shopcartitemId);
            if (obj != null)
            {
                _cartRepository.Remove(obj);
                ShopCartItems.Remove(obj);
            }
            CalculateTotals();
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


        public void SortByPrice()
        {
            if (!string.IsNullOrEmpty(SortKey) && SortKey == "Price")
            {
                if (IsAscending)
                {
                    ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Price).ToList());
                }
                else
                {
                    ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderByDescending(i => i.Product.Price).ToList());
                }
            }
            else
            {
                ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Name).ToList());
            }
            CalculateTotals();
            SaveSortStateAsync();
        }

        public void SortByName()
        {
            if (!string.IsNullOrEmpty(SortKey) && SortKey == "Name")
            {
                if (IsAscending)
                {
                    ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Name).ToList());
                }
                else
                {
                    ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderByDescending(i => i.Product.Name).ToList());
                }
            }
            else
            {
                ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Price).ToList());
            }
            CalculateTotals();
            SaveSortStateAsync();
        }

        public void SaveSortStateAsync()
        {
            var sortSettings = new
            {
                Key = SortKey,
                Ascending = IsAscending
            };

            var json = JsonConvert.SerializeObject(sortSettings);

            ApplicationData.Current.LocalSettings.Values["SortSettings"] = json;
        }

        public void LoadSortStateAsync()
        {
            var savedJson = ApplicationData.Current.LocalSettings.Values["SortSettings"] as string;

            if (!string.IsNullOrEmpty(savedJson))
            {
                var settings = JsonConvert.DeserializeObject<dynamic>(savedJson);

                SortKey = settings.Key.ToString();
                IsAscending = Convert.ToBoolean(settings.Ascending);

                if (SortKey == "Price")
                {
                    if (IsAscending)
                    {
                        ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Price).ToList());
                    }
                    else
                    {
                        ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderByDescending(i => i.Product.Price).ToList());
                    }
                }
                else
                {
                    ShopCartItems = new ObservableCollection<ShopCartItem>(ShopCartItems.OrderBy(i => i.Product.Name).ToList());
                }
                CalculateTotals();
            }
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

        private string _sortKey;
        public string SortKey
        {
            get { return _sortKey; }
            set
            {
                _sortKey = value;
                OnPropertyChanged(nameof(SortKey));
            }
        }

        private bool _isAscending;
        public bool IsAscending
        {
            get { return _isAscending; }
            set
            {
                _isAscending = value;
                OnPropertyChanged(nameof(IsAscending));
            }
        }

    }
}
