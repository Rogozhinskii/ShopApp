using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Modules.Products.Views;

namespace ShopUI.Modules.Products
{
    public class ProductsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ProductsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<ViewA>();
            if (containerRegistry != null)
            {
                containerRegistry.RegisterForNavigation<ProductsView>();
            }

        }
    }
}