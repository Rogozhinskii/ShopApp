using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShopLibrary.Entityes.Base;
using ShopUI.Core.interfaces;
using System.Runtime.CompilerServices;

namespace ShopUI.Core.MVVM
{
    public class DialogViewModel : BindableBase, IDialogAware, INotificationDialog
    {
        public virtual string Title => "";
        protected IDialogService _dialogService;
        

        public event Action<IDialogResult> RequestClose;
        public virtual void RaiseRequestClose(IDialogResult result)
        {
            RequestClose?.Invoke(result);
        }

        protected readonly Dictionary<string, object> _values = new();

        /// <summary>
        /// Устанавливает значение в словарь свойств изменяемого объекта 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        protected virtual bool SetValue(object value, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out var oldValue) && Equals(oldValue, value))
                return false;
            _values[property] = value;
            return true;
        }

        protected virtual T GetValue<T>(T Default, [CallerMemberName] string property = null)
        {
            if (_values.TryGetValue(property, out var value))
                return (T)value;
            return Default;
        }

        /// <summary>
        /// Сохраняет значения изменившихся свойств редактируемого объекта
        /// </summary>
        /// <param name="namedEntity"></param>
        protected void SaveChanges(Entity entity)
        {
            var type = entity.GetType();
            foreach (var (propertyName, value) in _values)
            {
                var property = type.GetProperty(propertyName);
                if (property is null || !property.CanWrite) continue;
                property.SetValue(entity, value);
            }
            _values.Clear();
        }

        /// <summary>
        /// Отменяет запланированные изменения
        /// </summary>
        protected void CancelChanges()
        {
            var properties = _values.Keys.ToArray();
            _values.Clear();
            foreach (var property in properties)
                RaisePropertyChanged(property);
        }

        public virtual bool CanCloseDialog() =>
            true;

        public virtual void OnDialogClosed() { }

        public virtual void OnDialogOpened(IDialogParameters parameters) { }

        public void ShowNotificationDialog(DialogType dialogType, string message)
        {
            var parameters = new DialogParameters
            {
                { CommonTypesPrism.DialogMessage, message }
            };
            switch (dialogType)
            {
                case DialogType.NotificationDialog:
                    _dialogService.Show(CommonTypesPrism.NotificationDialog, parameters, null);
                    break;
                case DialogType.ErrorDialog:
                    _dialogService.Show(CommonTypesPrism.ErrorNotification, parameters, null);
                    break;
                default:
                    _dialogService.Show(CommonTypesPrism.ErrorNotification, parameters, null);
                    break;
            }
        }

    }
}
