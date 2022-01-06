using Prism.Events;
using System.Windows;

namespace EventAggregator.Core
{
    /// <summary>
    /// Событие при выполнении длительной операции
    /// </summary>
    public class OnLongOperationEvent : PubSubEvent<Visibility>
    {
    }
}
