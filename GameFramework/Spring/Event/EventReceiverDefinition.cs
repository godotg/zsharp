using System;
using System.Reflection;
using GameFramework.Utils;

namespace Spring.Event
{
    public class EventReceiverDefinition : IEventReceiver
    {
        private object bean;

        // 被ReceiveEvent注解的方法
        private MethodInfo method;

        // 接收的参数Type
        private Type paramType;

        public void Invoke(IEvent eve)
        {
            method.Invoke(bean, new object[] {eve});
        }


        private EventReceiverDefinition(object bean, MethodInfo method, Type paramType)
        {
            this.bean = bean;
            this.method = method;
            this.paramType = paramType;
        }

        public static EventReceiverDefinition ValueOf(object bean, MethodInfo method)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != 1)
            {
                throw new Exception(StringUtils.Format("[class:{0}] [method:{1}] must have one parameter!",
                    bean.GetType().Name, method.Name));
            }

            if (!typeof(IEvent).IsAssignableFrom(parameters[0].ParameterType))
            {
                throw new Exception(StringUtils.Format("[class:{}] [method:{}] must have one [IEvent] type parameter!",
                    bean.GetType().Name, method.Name));
            }

            return new EventReceiverDefinition(bean, method, parameters[0].ParameterType);
        }

        public Type ParamType => paramType;
    }
}