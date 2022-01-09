using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShopUI.Core;
using ShopUI.Modules.Customers.Views;
using ShopUI.Modules.Products.Views;
using ShopUI.Services.Interfaces;

namespace ShopUI.Modules.Products
{
    public class CustomersModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public CustomersModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {            
            if(containerProvider != null)
            {                
                _regionManager.RegisterViewWithRegion(RegionNames.ProductsRegion, typeof(ProductsView));
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