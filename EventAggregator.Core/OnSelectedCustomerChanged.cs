using Prism.Events;
using ShopLibrary.Entityes;


namespace EventAggregator.Core
{
    /// <summary>
    /// Возникает при смене выбранного покупателя
    /// </summary>
    public class OnSelectedCustomerChanged: PubSubEvent<Customer>
    {
    }
}
