using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShopUI.Modules.Products.Views;

namespace ShopUI.Modules.Products
{
    public class CustomersModule : IModule
    {
       

        public CustomersModule()
        {
           
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {            
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {            
            if (containerRegistry != null)
            {
                containerRegistry.RegisterForNavigation<CustomersView>();
            }

        }
    }
}