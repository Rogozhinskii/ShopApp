using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    internal class AddEditDialogViewModel:DialogViewModel
    {
        private Product _originalRecord;
        private Product _product;
        public Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        //private string _email;
        //public string Email
        //{
        //    get { return _email; }
        //    set { SetProperty(ref _email, value); }
        //}

        //private int _productCode;
        //public int ProductCode
        //{
        //    get { return _productCode; }
        //    set { SetProperty(ref _productCode, value); }
        //}

        //private string _description;
        //public string Description
        //{
        //    get { return _description; }
        //    set { SetProperty(ref _description, value); }
        //}

        private DelegateCommand _saveChangesCommand;

        public DelegateCommand SaveChangesCommand =>
           _saveChangesCommand ??= _saveChangesCommand = new(ExecuteSaveChangesCommand);

        void ExecuteSaveChangesCommand()
        {
            DialogResult result=new DialogResult();
            result.Parameters.Add(CommonTypesPrism.EditableRecord, Product);
            RaiseRequestClose(result);
        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand =>
           _cancelCommand ??= _cancelCommand = new(ExecuteCancelCommand);

        void ExecuteCancelCommand()
        {
            var result=new DialogResult();
            result.Parameters.Add(CommonTypesPrism.EditableRecord, _originalRecord);
            RaiseRequestClose(result);
        }



        public override void OnDialogOpened(IDialogParameters parameters)
        {
            parameters.TryGetValue(CommonTypesPrism.EditableRecord,out _originalRecord);
            parameters.TryGetValue(CommonTypesPrism.EmailParam,out string email);
            if (_originalRecord != null){
                Product = (Product)_originalRecord.Clone();
            }
            else{
                if (email != null)
                    Product = new Product { Email = email };
                else
                    Product = new Product();
            }

        }

        public override void OnDialogClosed()
        {
            Product = null;           
        }



    }
}
