using Prism.Services.Dialogs;
using ShopLibrary.Models;
using ShopUI.Core;
using ShopUI.Core.MVVM;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    internal class AddEditDialogViewModel:DialogViewModel
    {
        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private int _productCode;

        public int ProductCode
        {
            get { return _productCode; }
            set { SetProperty(ref _productCode, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var record = parameters.GetValue<Product>(CommonTypesPrism.recordForEdit);
            if (record != null)
            {
                Email=record.Email;
                ProductCode = record.ProductCode;
                Description = record.Description;

            }
        }



    }
}
