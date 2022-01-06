using Prism.Events;
using ShopLibrary.Models;

namespace EventAggregator.Core
{
    /// <summary>
    /// Событие вызывается при удалении покупателя из бд
    /// </summary>
    public class OnCustomerDeleted:PubSubEvent<Customer>
    {
    }
}
