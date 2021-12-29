using ShopUI.Core.Converters.Base;
using System.Globalization;
using System.Text;

namespace ShopUI.Core.Converters
{
    public class FullNameConverter : MultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder sb = new();
            foreach (var item in values){
                sb.Append($"{item} ");
            }
            return sb.ToString();
        }
    }
}
