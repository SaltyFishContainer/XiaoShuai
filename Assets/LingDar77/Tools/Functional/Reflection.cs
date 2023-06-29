using System.Collections.Generic;
using System.Reflection;

namespace Lingdar77.Functional
{
    public sealed class Code
    {
        public static List<System.Type> GetDerivedClasses<Type>()
        {
            var classes = new List<System.Type>();
            var types = Assembly.GetCallingAssembly().GetTypes();
            var baseType = typeof(Type);
            foreach (var type in types)
            {
                var otherBaseType = type.BaseType;
                while (otherBaseType != null)
                {
                    if (otherBaseType.Name == baseType.Name)
                    {
                        classes.Add(type);
                        break;
                    }
                    else
                    {
                        otherBaseType = otherBaseType.BaseType;
                    }
                }
            }
            return classes;
        }
    }
}