using System;
using System.Collections.Generic;
using System.Linq;
using GameFramework;
using GameFramework.Utils;

namespace Spring.Core
{
    public class SpringContext
    {
        private static readonly Dictionary<Type, object> beanMap = new Dictionary<Type, object>();

        public static void Scan()
        {
            var types = Utility.Assembly.GetTypes();
            foreach (var type in types)
            {
                if (AssemblyUtils.TypeHasAttribute(type, typeof(Controller)))
                {
                    var obj = Activator.CreateInstance(type);
                    if (obj == null)
                    {
                        throw new Exception(StringUtils.Format("无法通过类型[{0}]创建实例", type.Name));
                    }

                    beanMap.Add(type, obj);
                }
            }

            foreach (var pair in beanMap)
            {
                var type = pair.Key;
                var obj = pair.Value;
                var fields = Utility.Assembly.allTypeFields(type);
                foreach (var fieldInfo in fields)
                {
                    if (Utility.Assembly.fieldHasAttribute(fieldInfo, typeof(Autowired)))
                    {
                        var autowiredObj = beanMap[fieldInfo.FieldType];
                        if (autowiredObj == null)
                        {
                            throw new Exception(StringUtils.Format("无法在[{0}]中注入[{1}]实例", type.Name,
                                fieldInfo.FieldType.Name));
                        }

                        fieldInfo.SetValue(obj, autowiredObj);
                    }
                }
            }
        }

        public static T GetBean<T>()
        {
            return (T) beanMap[typeof(T)];
        }

        public static List<object> GetAllBeans()
        {
            var list = new List<object>();
            foreach (var pair in beanMap)
            {
                list.Add(pair.Value);
            }

            return list;
        }
    }
}