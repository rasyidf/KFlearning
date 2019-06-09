using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace KFlearning.ApplicationServices
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, List<WeakReference>> _eventSubsribers =
            new Dictionary<Type, List<WeakReference>>();

        private readonly object _lockSubscriberDictionary = new object();

        public void Publish<T>(T eventToPublish) where T : EventArgs
        {
            Type subsriberType = typeof(IListener<>).MakeGenericType(typeof(T));
            List<WeakReference> subscribers = GetSubscriberList(subsriberType);
            var subsribersToBeRemoved = new List<WeakReference>();

            foreach (WeakReference weakSubsriber in subscribers)
                if (weakSubsriber.IsAlive)
                {
                    var subscriber = (IListener<T>)weakSubsriber.Target;
                    InvokeSubscriberEvent(eventToPublish, subscriber);
                }
                else
                {
                    subsribersToBeRemoved.Add(weakSubsriber);
                }

            if (!subsribersToBeRemoved.Any()) return;
            lock (_lockSubscriberDictionary)
            {
                foreach (WeakReference remove in subsribersToBeRemoved)
                    subscribers.Remove(remove);
            }
        }

        public void Subsribe(object subscriber)
        {
            lock (_lockSubscriberDictionary)
            {
                var weakReference = new WeakReference(subscriber);
                IEnumerable<Type> subsriberTypes = subscriber.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IListener<>));

                foreach (Type subsriberType in subsriberTypes)
                {
                    List<WeakReference> subscribers = GetSubscriberList(subsriberType);
                    subscribers.Add(weakReference);
                }
            }
        }

        private void InvokeSubscriberEvent<T>(T eventToPublish, IListener<T> subscriber) where T : EventArgs
        {
            Dispatcher syncContext = Dispatcher.CurrentDispatcher;
            syncContext.Invoke(() => subscriber.OnEvent(eventToPublish), DispatcherPriority.Normal);
        }

        private List<WeakReference> GetSubscriberList(Type subsriberType)
        {
            List<WeakReference> subsribersList;
            lock (_lockSubscriberDictionary)
            {
                bool found = _eventSubsribers.TryGetValue(subsriberType, out subsribersList);
                if (found) return subsribersList;

                subsribersList = new List<WeakReference>();
                _eventSubsribers.Add(subsriberType, subsribersList);
            }

            return subsribersList;
        }
    }
}
