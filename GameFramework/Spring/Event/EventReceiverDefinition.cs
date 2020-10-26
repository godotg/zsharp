using System;
using System.Reflection;

namespace Spring.Event
{
    public class EventReceiverDefinition : IEventReceiver
    {
        private object bean;

        // 被ReceiveEvent注解的方法
        private MethodInfo method;

        public void invoke(IEvent eve)
        {
            method.Invoke(bean, new object[] {eve});
        }
    }
}