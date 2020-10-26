using System;
using System.Collections.Generic;

namespace Spring.Event
{
    public class EventBus
    {
        private readonly Dictionary<Type, LinkedList<IEventReceiver>> receiverMap = new Dictionary<Type, LinkedList<IEventReceiver>>();
    }
}