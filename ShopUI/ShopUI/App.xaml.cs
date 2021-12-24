using Prism.Ioc;
using Prism.Modularity;
using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using ShopUI.Modules.NotificationTools;
using ShopUI.Modules.Products;
using ShopUI.Views;
using System.Configuration;
using System.Windows;

namespace ShopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (containerRegistry != null)
            {
                //var sqlProvide = ConfigurationManager.ConnectionStrings[1];
                IProviderFactoryService providerFactory = new ProviderFactoryService();
                

            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ProductsModule>();
            moduleCatalog.AddModule<NotificationToolsModule>();
        }
    }
}
