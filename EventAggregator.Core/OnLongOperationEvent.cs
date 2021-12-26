using Prism.Events;
using System.Windows;

namespace EventAggregator.Core
{
    internal class OnLongOperationEvent : PubSubEvent<Visibility>
    {
    }
}
