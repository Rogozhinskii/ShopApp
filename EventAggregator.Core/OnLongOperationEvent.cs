using Prism.Events;
using System.Windows;

namespace EventAggregator.Core
{
    public class OnLongOperationEvent : PubSubEvent<Visibility>
    {
    }
}
