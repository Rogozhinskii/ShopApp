using Prism.Ioc;
using Prism.Modularity;
using ShopUI.Modules.NotificationTools.ViewModels;
using ShopUI.Modules.NotificationTools.Views;

namespace ShopUI.Modules.NotificationTools
{
    public class NotificationToolsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(containerRegistry is not null)
            {
                containerRegistry.RegisterDialog<AuthenticationDialog, AuthenticationDialogViewModel>();
            }
        }
    }
}