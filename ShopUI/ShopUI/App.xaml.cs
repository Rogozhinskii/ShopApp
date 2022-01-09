using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using ShopUI.Core;
using ShopUI.Modules.NotificationTools;
using ShopUI.Modules.NotificationTools.ViewModels;
using ShopUI.Modules.NotificationTools.Views;
using ShopUI.Modules.Products;
using ShopUI.Services;
using ShopUI.Services.Interfaces;
using ShopUI.Views;
using System;
using System.Configuration;
using System.Diagnostics;
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
            //DispatcherUnhandledException += App_DispatcherUnhandledException;
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// все не обработанные исключения будут идти сюда.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //DialogParameters dialogParameters = new()
            //{
            //    { CommonTypesPrism.DialogMessage, e.Exception.Message }
            //};
            //_dialogService.ShowDialog(CommonTypesPrism.ErrorNotification, dialogParameters, result => { });
            e.Handled = true;
        }

        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (containerRegistry != null)
            {   
                try
                {                    
                    //_dialogService = Container.Resolve<IDialogService>() ?? throw new NullReferenceException(nameof(IDialogService));
                    //var connectionStrings = ConfigurationManager.ConnectionStrings;
                    //var repositoryManager = new RepositoryManager(connectionStrings);
                    //containerRegistry.RegisterInstance<IRepositoryManager>(repositoryManager);
                    //containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
                }
                catch (Exception e)
                {
                    var result = MessageBox.Show(e.Message, "Fatal Error", MessageBoxButton.OK);
                    if(result == MessageBoxResult.OK)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                   
                }
                

            }
        }

        
        /// <summary>
        /// Подключение модулей
        /// </summary>
        /// <param name="moduleCatalog"></param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<CustomersModule>();
            //moduleCatalog.AddModule<NotificationToolsModule>();
        }
    }
}
