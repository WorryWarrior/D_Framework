using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities.Events
{
    public class EventController : Singleton<EventController>
    {
        private Dictionary<ObservedEventType, System.Action<object>> listeners = new Dictionary<ObservedEventType, System.Action<object>>();

        public static void AddListener(ObservedEventType observedEventType, System.Action<object> callback)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out System.Action<object> eventCallback))
            {
                eventCallback += callback;
                Instance.listeners[observedEventType] = eventCallback;
            }
            else
            {
                eventCallback += callback;
                Instance.listeners.Add(observedEventType, eventCallback);
            }
        }

        public static void RemoveListener(ObservedEventType observedEventType, System.Action<object> callback)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out System.Action<object> eventCallback))
            {
                eventCallback -= callback;
                Instance.listeners[observedEventType] = eventCallback;
            }
        }

        public static void TriggerEvent(ObservedEventType observedEventType, object param = null)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out System.Action<object> eventCallback))
                eventCallback.Invoke(param);
        }
    }
}