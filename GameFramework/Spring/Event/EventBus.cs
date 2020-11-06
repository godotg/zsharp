using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFramework.Utils;
using Spring.Core;

namespace Spring.Event
{
    public class EventBus
    {
        private static readonly EventBus INSTANCE = new EventBus();

        private readonly Dictionary<Type, ICollection<IEventReceiver>> receiverMap =
            new Dictionary<Type, ICollection<IEventReceiver>>();

        private EventBus()
        {
        }

        public static EventBus GetInstance()
        {
            return INSTANCE;
        }

        public void Scan()
        {
            var allBeans = SpringContext.GetAllBeans();
            foreach (var bean in allBeans)
            {
                RegisterEventReceiver(bean);
            }
        }

        private void RegisterEventReceiver(object bean)
        {
            var clazz = bean.GetType();
            var methods = AssemblyUtils.GetMethodsByAnnoInPOJOClass(clazz, typeof(EventReceiver));
            foreach (var method in methods)
            {
                var receiverDefinition = EventReceiverDefinition.ValueOf(bean, method);
                var paramType = receiverDefinition.ParamType;
                if (!receiverMap.ContainsKey(paramType))
                {
                    receiverMap.Add(paramType, new LinkedList<IEventReceiver>());
                }

                receiverMap[paramType].Add(receiverDefinition);
            }
        }

        public void SyncSubmit(IEvent eve)
        {
            var list = receiverMap[eve.GetType()];
            if (CollectionUtils.IsEmpty(list))
            {
                return;
            }

            Submit(eve, list);
        }

        private void Submit(IEvent eve, ICollection<IEventReceiver> listReceiver)
        {
            foreach (var receiver in listReceiver)
            {
                try
                {
                    receiver.Invoke(eve);
                }
                catch (Exception e)
                {
                    // TODO:
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}