using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStoreUWP.Services
{
    public static class ServiceProviderHolder
    {
        public static ServiceProvider Instance { get; private set; }

        public static void SetInstance(ServiceProvider serviceProvider)
        {
            Instance = serviceProvider;
        }

        public static T GetService<T>()
        {
            return Instance.GetService<T>();
        }
    }

}
