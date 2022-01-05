using Prism.Events;
using ShopLibrary.Models;

namespace EventAggregator.Core
{
    public class OnSelectedCustomerChanged: PubSubEvent<Customer>
    {
    }
}
