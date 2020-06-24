using BotCsgo.Service;
using BotCsgo.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BotCsgo.Helpers
{
    public class ViewModelLocator
    {
        private static ServiceProvider provider;
        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MyInventoryViewModel>();
            services.AddSingleton<BotPageModel>();
            services.AddTransient<BuyItemsViewModel>();
            services.AddTransient<FindItemWithViewModel>();

            services.AddSingleton<PageService>();
            provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                provider.GetRequiredService(item.ServiceType);
            }
        }
        public static MyInventoryViewModel MyInventoryViewModel => provider.GetRequiredService<MyInventoryViewModel>();
        public static BotPageModel BotPageModel => provider.GetRequiredService<BotPageModel>();
        public static FindItemWithViewModel FindItemWithViewModel => provider.GetRequiredService<FindItemWithViewModel>();
        public static BuyItemsViewModel BuyItemsViewModel => provider.GetRequiredService<BuyItemsViewModel>();
    }
}
