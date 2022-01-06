using Prism.Events;
using ShopLibrary.Models;

namespace EventAggregator.Core
{
    /// <summary>
    /// Возникает при смене выбранного покупателя
    /// </summary>
    public class OnSelectedCustomerChanged: PubSubEvent<Customer>
    {
    }
}
