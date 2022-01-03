using ShopLibrary.Models;
using ShopUI.Core.Converters.Base;
using System.Globalization;

namespace ShopUI.Core.Converters
{
    public class IsCheckedConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if(value is Product product)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
