using Prism.Events;
using ShopLibrary.Entityes;

namespace EventAggregator.Core
{
    /// <summary>
    /// Возникает после редактирования данных покупателя
    /// </summary>
    public class OnCustomerEdited:PubSubEvent<Customer>
    {
    }
}
