using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Context;
using ShopUI.Core.MVVM;


namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Модель простосмтря строк подключений и их состояний
    /// </summary>
    internal class ConnectionInfoDialogViewModel:DialogViewModel
    {
        private readonly ShopAppDB _db;
                
        public ConnectionInfoDialogViewModel(ShopAppDB db)
        {
            _db = db;
        }

        /// <summary>
        /// Значение строки подключения
        /// </summary>
        public string ConnectionString => _db.Database.GetDbConnection().ConnectionString;

        /// <summary>
        /// Состояние соединения
        /// </summary>
        public string ConnectionState => _db.Database.GetDbConnection().State.ToString();

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand =>
           _closeCommand ??= _closeCommand = new(ExecuteCloseCommand);
        void ExecuteCloseCommand()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }


    }
}
