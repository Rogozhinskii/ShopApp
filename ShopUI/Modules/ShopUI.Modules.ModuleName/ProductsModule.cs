using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShopUI.Core;
using ShopUI.Modules.ModuleName.Views;

namespace ShopUI.Modules.ModuleName
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
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}