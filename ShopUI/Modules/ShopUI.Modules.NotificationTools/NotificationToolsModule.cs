using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ShopUI.Core;
using ShopUI.Modules.NotificationTools.ViewModels;
using ShopUI.Modules.NotificationTools.Views;
using ShopUI.Services.Interfaces;

namespace ShopUI.Modules.NotificationTools
{
    public class NotificationToolsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NotificationToolsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion,typeof(StatusBar));
            containerProvider.Resolve<IAuthenticationService>();
            containerProvider.Resolve<IRepositoryManager>();
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(containerRegistry is not null)
            {
                containerRegistry.RegisterDialog<AuthenticationDialog, AuthenticationDialogViewModel>();
                containerRegistry.RegisterDialog<ErrorNotification, ErrorNotificationViewModel>();
                containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
                containerRegistry.RegisterDialog<ConnectionInfoDialog, ConnectionInfoDialogViewModel>();
                containerRegistry.RegisterDialog<AddEditDialog, AddEditProductDialogViewModel>();
                containerRegistry.RegisterDialog<AddEditCustomerDialog, AddEditCustomerDialogViewModel>();
                containerRegistry.RegisterDialog<RegisterUserDialog, RegisterUserDialogViewModel>();
                
            }
        }
    }
}