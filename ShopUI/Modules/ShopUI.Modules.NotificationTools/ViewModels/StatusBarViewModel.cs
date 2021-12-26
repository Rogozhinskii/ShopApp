using Prism.Mvvm;
using System.Windows;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    public class StatusBarViewModel:BindableBase
    {
        private Visibility _progressBarVisibility;

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { SetProperty(ref _progressBarVisibility, value); }
        }

    }
}
