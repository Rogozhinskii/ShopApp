using Prism.Ioc;
using Prism.Modularity;
using ShopUI.Modules.Products.Views;
using ShopUI.Services.Interfaces;

namespace ShopUI.Modules.Products
{
    public class CustomersModule : IModule
    {
       

        public CustomersModule()
        {
           
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {            
            if(containerProvider != null)
            {
                containerProvider.Resolve<IRepositoryManager>();
            }
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