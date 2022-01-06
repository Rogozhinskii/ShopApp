using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Modules.NotificationTools;
using ShopUI.Modules.Products;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using ShopUI.Views;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace ShopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IDialogService _dialogService;
        protected override Window CreateShell()
        {
            _dialogService = Container.Resolve<IDialogService>() ?? throw new NullReferenceException(nameof(IDialogService));
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// все не обработанные исключения будут идти сюда.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DialogParameters dialogParameters = new()
            {
                { CommonTypesPrism.DialogMessage, e.Exception.Message }
            };
            _dialogService.ShowDialog(CommonTypesPrism.ErrorNotification, dialogParameters, result => { });
            e.Handled = true;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (containerRegistry != null)
            {                                              
                var connectionStrings = ConfigurationManager.ConnectionStrings;               
                var repositoryManager = new RepositoryManager(connectionStrings);                
                containerRegistry.RegisterInstance<IRepositoryManager>(repositoryManager);
                containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();

            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CustomersModule>();
            moduleCatalog.AddModule<NotificationToolsModule>();
        }
    }
}
