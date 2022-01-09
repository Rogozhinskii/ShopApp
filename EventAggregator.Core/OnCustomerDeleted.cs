using Prism.Events;
using ShopLibrary.Entityes;


namespace EventAggregator.Core
{
    /// <summary>
    /// Событие вызывается при удалении покупателя из бд
    /// </summary>
    public class OnCustomerDeleted:PubSubEvent<Customer>
    {
    }
}



