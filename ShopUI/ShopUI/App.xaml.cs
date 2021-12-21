using Prism.Ioc;
using Prism.Modularity;
using ShopUI.Modules.ModuleName;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using ShopUI.Views;
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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ProductsModule>();
        }
    }
}
