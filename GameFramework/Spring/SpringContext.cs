using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework;

namespace Spring
{
    public class SpringContext
    {
        private static readonly Dictionary<Type, object> componentMap = new Dictionary<Type, object>();

        public static void componentScan()
        {
            var types = Utility.Assembly.GetTypes();
            foreach (var type in types)
            {
                if (Utility.Assembly.typeHasAttribute(type, typeof(Controller)))
                {
                    var obj = Activator.CreateInstance(type);
                    if (obj == null)
                    {
                        throw new Exception("类型错误");
                    }

                    componentMap.Add(type, obj);
                }
            }

            foreach (var pair in componentMap)
            {
                var type = pair.Key;
                var obj = pair.Value;
                var fields = Utility.Assembly.allTypeFields(type);
                foreach (var fieldInfo in fields)
                {
                    if (Utility.Assembly.fieldHasAttribute(fieldInfo, typeof(Autowired)))
                    {
                        var autowiredObj = componentMap[fieldInfo.FieldType];
                        if (autowiredObj == null)
                        {
                            throw new Exception("实例错误");
                        }
                        fieldInfo.SetValue(obj, autowiredObj);
                    }
                }
            }
        }
        
        public static T getComponent<T>()
        {
            return (T) componentMap[typeof(T)];
        }
    }
}