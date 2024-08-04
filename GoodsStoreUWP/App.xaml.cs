using GoodsStoreUWP.Context;
using GoodsStoreUWP.Data.Repositories;
using GoodsStoreUWP.Models;
using GoodsStoreUWP.MVVM.ViewModels;
using GoodsStoreUWP.MVVM.ViewModels.Catalog;
using GoodsStoreUWP.MVVM.ViewModels.ShopCart;
using GoodsStoreUWP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GoodsStoreUWP
{
    sealed partial class App : Application
    {
        private static ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite("Data Source = Goods.db"));

            services.AddSingleton<Func<Type, ViewModel>>(
                        serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            services.AddScoped<IRepository<ShopCartItem>,ShopCartRepository>();
            services.AddScoped<IRepository<Product>,ProductRepository>();
            services.AddTransient<CatalogViewModel>();
            services.AddTransient<ShopCartViewModel>();


            _serviceProvider = services.BuildServiceProvider();
            DbInitializerService.Seed(_serviceProvider);

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Загрузить состояние из ранее приостановленного приложения
                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
        public static ServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

    }
}
