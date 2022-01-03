using Prism.Commands;
using Prism.Services.Dialogs;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    internal class AddEditDialogViewModel:DialogViewModel
    {
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
            result.Parameters.Add(CommonTypesPrism.recordForEdit, Product);
            RaiseRequestClose(result);
        }



        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var record = parameters.GetValue<Product>(CommonTypesPrism.recordForEdit);
            if (record != null){
                Product = record;
            }
        }

        public override void OnDialogClosed()
        {
            Product = null;
            base.OnDialogClosed();
        }



    }
}
