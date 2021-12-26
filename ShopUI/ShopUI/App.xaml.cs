using Prism.Ioc;
using Prism.Modularity;
using ShopLibrary.Authentication.Interfaces;
using ShopLibrary.Services;
using ShopLibrary.Services.Interfaces;
using ShopUI.Core;
using ShopUI.Modules.NotificationTools;
using ShopUI.Modules.Products;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
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
                var providersService = new ProviderFactoryService();                               
                var connectionStrings = ConfigurationManager.ConnectionStrings;               
                var repositoryManager = new RepositoryManager(providersService, connectionStrings);
                containerRegistry.RegisterInstance<IProviderFactoryService>(providersService);
                containerRegistry.RegisterInstance<IRepositoryManager>(repositoryManager);
                containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();

            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ProductsModule>();
            moduleCatalog.AddModule<NotificationToolsModule>();
        }
    }
}
