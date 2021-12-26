using Prism.Ioc;
using Prism.Modularity;
using ShopUI.Modules.NotificationTools.ViewModels;
using ShopUI.Modules.NotificationTools.Views;
using ShopUI.Services.Interfaces;

namespace ShopUI.Modules.NotificationTools
{
    public class NotificationToolsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IAuthenticationService>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(containerRegistry is not null)
            {
                containerRegistry.RegisterDialog<AuthenticationDialog, AuthenticationDialogViewModel>();
                containerRegistry.RegisterDialog<ErrorNotification, ErrorNotificationViewModel>();
                
            }
        }
    }
}