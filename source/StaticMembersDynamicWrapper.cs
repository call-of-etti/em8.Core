/**
 * Thanks to: David Ebbo
 * http://blogs.msdn.com/b/davidebb/archive/2009/10/23/using-c-dynamic-to-call-static-members.aspx
 */

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CoE.em8.Core
{
    public class StaticMembersDynamicWrapper : DynamicObject
    {
        private Type _type;
        public StaticMembersDynamicWrapper(Type type) { _type = type; }

        // Handle static properties
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            PropertyInfo prop = _type.GetProperty(binder.Name, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
            if (prop == null)
            {
                FieldInfo field = _type.GetField(binder.Name, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
                if (field == null)
                {
                    result = null;
                    return false;
                }
                else
                {
                    result = field.GetValue(null);
                    return true;
                }
            }

            result = prop.GetValue(null, null);
            return true;
        }

        // Handle static methods
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                MethodInfo method = _type.GetMethods
                (
                    BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public
                ).Where(m => m.Name == binder.Name).First((m) =>
                {
                    ParameterInfo[] pm = m.GetParameters();
                    if (pm.Length == args.Length)
                    {

                        for (int i = 0, max = pm.Length; i < max; i++)
                        {
                            Type a = pm[i].ParameterType;
                            Type b = args[i].GetType();

                            if (a.IsAssignableFrom(b) || b.IsAssignableFrom(a))
                                continue;
                            else
                                return false;
                        }

                        return true;
                    }
                    else
                        return false;
                });

                //MethodInfo method = _type.GetMethod(binder.Name, BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Public);
                if (method == null)
                {
                    result = null;
                    return false;
                }

                result = method.Invoke(null, args);
                return true;
            }
            catch(TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }


        // Proxy

        static readonly Dictionary<Type, StaticMembersDynamicWrapper> _typeLookup = new Dictionary<Type, StaticMembersDynamicWrapper>();

        public static dynamic Proxy(Type type)
        {
            if (!_typeLookup.TryGetValue(type, out StaticMembersDynamicWrapper smdw))
                _typeLookup[type] = smdw = new StaticMembersDynamicWrapper(type);

            return smdw;
        }
    }
}
