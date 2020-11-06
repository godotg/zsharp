using System;
using System.Collections.Generic;

namespace GameFramework.Utils
{
    public static class CollectionUtils
    {
        public static  bool IsEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count <= 0;
        }
    }
}