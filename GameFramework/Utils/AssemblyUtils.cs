//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameFramework.Utils
{
    /// <summary>
    /// 程序集相关的实用函数。
    /// </summary>
    public static class AssemblyUtils
    {
        private static readonly Assembly[] allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        private static readonly Dictionary<string, Type> cachedTypes = new Dictionary<string, Type>();


        /// <summary>
        /// 获取已加载的程序集。
        /// </summary>
        /// <returns>已加载的程序集。</returns>
        public static Assembly[] GetAssemblies()
        {
            return allAssemblies;
        }

        public static bool TypeHasAttribute(Type type, Type attribute)
        {
            var attributes = type.GetCustomAttributes(true);
            foreach (var attr in attributes)
            {
                if (attr.GetType().Equals(attribute))
                {
                    return true;
                }
            }

            return false;
        }

        public static FieldInfo[] AllTypeFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                                                      | BindingFlags.NonPublic | BindingFlags.Instance
                                                      | BindingFlags.Default);
        }

        public static bool MemberHasAttribute(MemberInfo fieldInfo, Type attribute)
        {
            var attributes = fieldInfo.GetCustomAttributes();
            foreach (var attr in attributes)
            {
                if (attr.GetType().Equals(attribute))
                {
                    return true;
                }
            }

            return false;
        }


        public static MethodInfo[] GetMethodsByAnnoInPOJOClass(Type type, Type attribute)
        {
            var list = new List<MethodInfo>();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic
                                                                       | BindingFlags.NonPublic |
                                                                       BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (MemberHasAttribute(method, attribute))
                {
                    list.Add(method);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取已加载的程序集中的所有类型。
        /// </summary>
        /// <returns>已加载的程序集中的所有类型。</returns>
        public static Type[] GetTypes()
        {
            var results = new List<Type>();
            foreach (var assembly in allAssemblies)
            {
                results.AddRange(assembly.GetTypes());
            }

            return results.ToArray();
        }

        /// <summary>
        /// 获取已加载的程序集中的所有类型。
        /// </summary>
        /// <param name="results">已加载的程序集中的所有类型。</param>
        public static void GetTypes(List<Type> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (var assembly in allAssemblies)
            {
                results.AddRange(assembly.GetTypes());
            }
        }

        /// <summary>
        /// 获取已加载的程序集中的指定类型。
        /// </summary>
        /// <param name="typeName">要获取的类型名。</param>
        /// <returns>已加载的程序集中的指定类型。</returns>
        public static Type GetType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new GameFrameworkException("Type name is invalid.");
            }

            Type type = null;
            if (cachedTypes.TryGetValue(typeName, out type))
            {
                return type;
            }

            type = Type.GetType(typeName);
            if (type != null)
            {
                cachedTypes.Add(typeName, type);
                return type;
            }

            foreach (var assembly in allAssemblies)
            {
                type = Type.GetType(StringUtils.Format("{0}, {1}", typeName, assembly.FullName));
                if (type != null)
                {
                    cachedTypes.Add(typeName, type);
                    return type;
                }
            }

            return null;
        }
    }
}