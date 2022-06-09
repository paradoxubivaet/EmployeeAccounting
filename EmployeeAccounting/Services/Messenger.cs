using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAccounting.Services
{
    public class Messenger : IMessenger
    {
        ConcurrentDictionary<Type, SynchronizedCollection<Subscription>> _subscriptions =
           new ConcurrentDictionary<Type, SynchronizedCollection<Subscription>>();
        ConcurrentDictionary<Type, object> _currentState = new ConcurrentDictionary<Type, object>();

        public void Send<TMessage>(TMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (!_subscriptions.ContainsKey(typeof(TMessage)))
                _subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());

            _currentState.AddOrUpdate(typeof(TMessage), (o) => message, (o, old) => message);
            foreach (var subscription in _subscriptions[typeof(TMessage)])
            {
                subscription.Action(message);
            }
        }

        public void Subscribe<TMessage>(object subscriber, Action<object> action)
        {
            if (!_subscriptions.ContainsKey(typeof(TMessage)))
                _subscriptions.TryAdd(typeof(TMessage), new SynchronizedCollection<Subscription>());

            var newSubscriber = new Subscription(subscriber, action);
            _subscriptions[typeof(TMessage)].Add(newSubscriber);
            if (_currentState.ContainsKey(typeof(TMessage)))
                newSubscriber.Action(_currentState[typeof(TMessage)]);
        }

        public void Unsubscribe<TMessage>(object subscriber)
        {
            if (!_subscriptions.ContainsKey(typeof(TMessage)))
                return;

            var subscription = _subscriptions[typeof(TMessage)].FirstOrDefault(s => s.Subscriber == subscriber);
            if (subscription != null)
                _subscriptions[typeof(TMessage)].Remove(subscription);
        }
    }
}
