using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework.Events
{
    public delegate void D_EventHandler(object arg);

    public class EventController : Singleton<EventController>
    {
        private Dictionary<ObservedEventType, D_EventHandler> listeners = new Dictionary<ObservedEventType, D_EventHandler>();

        public static void AddListener(ObservedEventType observedEventType, D_EventHandler callback)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out D_EventHandler eventCallback))
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

        public static void RemoveListener(ObservedEventType observedEventType, D_EventHandler callback)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out D_EventHandler eventCallback))
            {
                eventCallback -= callback;
                Instance.listeners[observedEventType] = eventCallback;
            }
        }

        public static void TriggerEvent(ObservedEventType observedEventType, object param = null)
        {
            if (Instance.listeners.TryGetValue(observedEventType, out D_EventHandler eventCallback))
                eventCallback?.Invoke(param);
        }
    }
}