using System.Globalization;
using System.Windows.Data;

namespace ShopUI.Core.Converters.Base
{
    /// <summary>
    /// Базовый класс для конвертации множественных привязок
    /// </summary>
    public abstract class MultiValueConverterBase : IMultiValueConverter
    {
        /// <summary>
        /// Реализует прямое преобразование входных парамметров
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Реализует обратное преобразование входных  параметров
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование не предусмотрено");
        }
    }
}
