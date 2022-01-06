using Prism.Services.Dialogs;
using ShopUI.Core.MVVM;
using ShopUI.Services.Interfaces;
using System.Collections.ObjectModel;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Модель простосмтря строк подключений и их состояний
    /// </summary>
    internal class ConnectionInfoDialogViewModel:DialogViewModel
    {
        private readonly IRepositoryManager _repositoryManager;
        public ConnectionInfoDialogViewModel(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private ObservableCollection<object> _repositories;

        public ObservableCollection<object> Repositories
        {
            get { return _repositories; }
            set { SetProperty(ref _repositories, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters) //todo переделать
        {
            Repositories = new ObservableCollection<object>(_repositoryManager.Repositories);
        }

    }
}
