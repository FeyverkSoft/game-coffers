using System;
using System.Collections.Generic;
using System.Reflection;

namespace Coffers.Helpers
{
    public static class ObjectToDictionary
    {
        public static Dictionary<String, Object> ToDictionary(this Object o)
        {
            var dict = new Dictionary<String, Object>();
            foreach (var prop in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!dict.ContainsKey(prop.Name))
                    dict.Add(prop.Name, prop.GetValue(o));
            }
            return dict;
        }
    }
}
