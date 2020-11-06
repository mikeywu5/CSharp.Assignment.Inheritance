using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace CSharp.Assignments.Tests.Library
{
    [DebuggerNonUserCode]
    public class Class
    {
        public readonly Type Type;
        private Class _superclass;
        private readonly List<Class> _subclasses;
        private dynamic _object = null;
        public Class(Type type, ClassType classType = ClassType.Class)
        {
            _subclasses = new List<Class>();
            Type = type;
            switch (classType)
            {
                case ClassType.AbstractClass:
                    if (!Type.IsClass || !Type.IsAbstract)
                    {
                        throw new AccessViolationException($"{Type.Name} must be an abstract class.");
                    }
                    break;
                case ClassType.Interface:
                    if (!Type.IsInterface)
                    {
                        throw new AccessViolationException($"{Type.Name} must be an interface class.");
                    }
                    break;
                case ClassType.Sealed:
                    if (!Type.IsClass || !Type.IsSealed)
                    {
                        throw new AccessViolationException($"{Type.Name} must be a sealed class.");
                    }
                    break;

            }
        }

        public Class(Type type, Class superclass)
            : this(type)
        {
            superclass.SubClass(this);
        }

        /// <summary>
        /// Create a new instance (new keyword)
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic New(params object[] args)
        {
            // using static
            if (args == null)
            {
                _object = null;
                return null;
            }
            // creating an instance
            _object = Activator.CreateInstance(Type, args);
            return _object;
        }

        /// <summary>
        /// Gets the dynamic object.
        /// </summary>
        public dynamic Object
        {
            get { return _object; }
        }

        /// <summary>
        /// Gets the value from a member
        /// </summary>
        /// <param name="name">member name</param>
        /// <param name="memberType">member type (either field, property, or method)</param>
        /// <param name="type">polymorphic type</param>
        /// <returns></returns>
        public dynamic Get(string name, ClassMemberType memberType, Type type)
        {
            switch (memberType)
            {
                case ClassMemberType.Field:
                    var field = type.GetField(name);
                    if (field == null)
                    {
                        throw new MissingFieldException($"The field \"{name}\" of {this} does not existor or is inaccessible (non-public).");
                    }
                    if (!field.IsPublic)
                    {
                        throw new MemberAccessException($"The field \"{name}\" of {this} must be public.");
                    }
                    return field.GetValue(_object);
                case ClassMemberType.Property:
                    var property = type.GetProperty(name);
                    if (property == null)
                        throw new MissingMemberException($"The property \"{name}\" of {this}does not exist or is inaccessible (non-public).");
                    if (!property.CanRead)
                    {
                        throw new MemberAccessException($"The property \"{name}\" of {this} cannot be read.");
                    }
                    return property.GetValue(_object);
                case ClassMemberType.Method:
                    var method = type.GetMethod(name);
                    if (method == null)
                    {
                        throw new MissingMemberException($"The method obj.{name}() of {this} <method> does not exist or is inaccessible (non-public).");
                    }
                    return method.Invoke(_object, new object[0]);
            }
            throw new ApplicationException($"The member type {memberType} is unknown.");
        }

        /// <summary>
        /// Gets the value from a member
        /// </summary>
        /// <param name="name">member name</param>
        /// <param name="memberType">member type (either field, property, or method)</param>
        /// <param name="superclass">super class for polymorphic creation</param>
        /// <returns></returns>
        public dynamic Get(string name, ClassMemberType memberType, Class superclass = null)
        {
            Type type = superclass == null ? Type : superclass.Type;
            return Get(name, memberType, type);
        }

        /// <summary>
        /// Sets the value to a member
        /// </summary>
        /// <param name="name">member's name</param>
        /// <param name="value">value to set</param>
        /// <param name="memberType">member type (either: field, property, or method)</param>
        /// <param name="superclass">super class for polymorphic creation</param>
        public void Set(string name, object value, ClassMemberType memberType, Type type)
        {
            switch (memberType)
            {
                case ClassMemberType.Field:
                    var field = type.GetField(name);
                    if (field == null)
                        throw new MissingFieldException($"The field \"{name}\" of {this} does not exist or is inaccessible (non-public).");
                    if (!field.IsPublic || field.IsInitOnly)
                    {
                        throw new MemberAccessException($"The field \"{name}\" of {this} must be public and not be readonly.");
                    }
                    field.SetValue(_object, value);
                    break;
                case ClassMemberType.Property:
                    var property = type.GetProperty(name);
                    if (property == null)
                        throw new MissingMemberException($"The property \"{name}\" of {type.Name} does not exist.");
                    if (property.GetSetMethod(true) == null || !property.CanWrite)
                    {
                        throw new MemberAccessException($"The property \"{name}\" of {type.Name} cannot be set.");
                    }
                    property.SetValue(_object, value);
                    break;
                case ClassMemberType.Method:

                    var method = type.GetMethod(name);
                    if (method == null)
                    {
                        throw new MissingMemberException($"The method obj.{name}(value) of {type.Name} does not exist or is inaccessible (non-public).");
                    }
                    method.Invoke(_object, new object[] { value });
                    break;
                default:
                    throw new ApplicationException($"The member type {memberType} is unknown.");
            }
        }

        /// <summary>
        /// Sets the value to a member
        /// </summary>
        /// <param name="name">member's name</param>
        /// <param name="value">value to set</param>
        /// <param name="memberType">member type (either: field, property, or method)</param>
        /// <param name="superclass">super class for polymorphic creation</param>
        public void Set(string name, object value, ClassMemberType memberType, Class superclass = null)
        {
            Type type = superclass == null ? Type : superclass.Type;
            Set(name, value, memberType, type);
        }

        /// <summary>
        /// Sets the value to a member
        /// </summary>
        /// <param name="name">member's name</param>
        /// <param name="value">value to set</param>
        /// <param name="superclass">super class for polymorphic creation</param>
        public void Set(string name, object value, Class superclass = null)
        {
            Type type = superclass == null ? Type : superclass.Type;
            Set(name, value, type);
        }

        /// <summary>
        /// Sets the value to a member
        /// </summary>
        /// <param name="name">member's name</param>
        /// <param name="value">value to set</param>
        /// <param name="type">type being invoked on</param>
        public void Set(string name, object value, Type type)
        {
            Set(name, value, ClassMemberType.Property, type);
        }

        /// <summary>
        /// obj.Get() or obj.Get
        /// </summary>
        /// <param name="name">name of the member (property or method)</param>
        /// <param name="memberType">whether this is using a method or a property</param>
        /// <param name="superclass">Member is in the Superclass (optional); null for the current class</param>
        public dynamic Get(string name, out ClassMemberType memberType, Class superclass = null)
        {
            Type type = superclass == null ? Type : superclass.Type;
            try
            {
                memberType = ClassMemberType.Property;
                return Get(name, memberType, superclass);
            }
            catch (MissingMemberException)
            {
                try
                {
                    memberType = ClassMemberType.Method;
                    return Get(name, memberType, superclass);
                }
                catch (MissingMethodException)
                {
                    throw new MissingMemberException($"The member obj.{name}() <method> or obj.{name} <property> of {this} does not exist or is inaccessible (non-public).");
                }
            }
        }

        /// <summary>
        /// Sets the data (no logic involved) using a field or a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isProperty">Returns true if it's a property.</param>
        /// <param name="superclass">Member is in the Superclass (optional); null for the current class</param>
        /// <returns></returns>
        public dynamic GetData(string name, out ClassMemberType memberType, Class superclass = null)
        {
            try
            {
                memberType = ClassMemberType.Field;
                return Get(name, memberType, superclass);
            }
            catch (MissingFieldException)
            {
                try
                {
                    memberType = ClassMemberType.Property;
                    return Get(name, memberType, superclass);
                }
                catch (MissingMemberException)
                {
                    throw new MissingMemberException($"The data member \"{name}\" of {this} (field or property) does not exist or is inaccessible (non-public).");
                }
            }
        }


        /// <summary>
        /// Sets the data (no logic involved) either with field or with a property
        /// </summary>
        /// <param name="name">name of the property or the field</param>
        /// <param name="superclass">Member is in the Superclass (optional); null for the current class</param>
        /// <param name="value"></param>
        public void SetData(string name, object value, out ClassMemberType memberType, Class superclass = null)
        {
            try
            {
                memberType = ClassMemberType.Property;
                Set(name, value, memberType, superclass);
            }
            catch (MissingMemberException)
            {
                memberType = ClassMemberType.Field;
                Set(name, value, memberType, superclass);
            }
        }

        /// <summary>
        /// Tests the setter type of the member name.
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public ClassMemberType SetterType(string memberName)
        {
            var property = Type.GetProperty(memberName);
            if (property != null && property.CanWrite)
            {
                return ClassMemberType.Property;
            }
            var field = Type.GetField(memberName);
            if (field != null && field.IsPublic && !field.IsInitOnly)
            {
                return ClassMemberType.Field;
            }
            return ClassMemberType.None;
        }

        /// <summary>
        /// Attaching a subclass to this class
        /// </summary>
        /// <param name="subclass"></param>
        public void SubClass(params Class[] subclasses)
        {
            if (subclasses == null) throw new NullReferenceException("subclasses");

            foreach (var c in subclasses)
            {
                // checking inheritance levels
                if (c.Type.BaseType != Type)
                {
                    throw new TypeAccessException($"The class {c} must extend class {this}.");
                }
                _subclasses.Add(c);
                c._superclass = this;
            }
        }

        /// <summary>
        /// Calling a method
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, params object[] args)
        {
            return Call(methodName, Type, args);
        }

        /// <summary>
        /// Polymorphically calling a method
        /// </summary>
        /// <param name="superclass">The superclass that the method will be called upon</param>
        /// <param name="methodName">The method's name</param>
        /// <param name="args">The arguments to the method</param>
        /// <returns></returns>
        public dynamic Call(string methodName, Class superclass, params object[] args)
        {
            return Call(methodName, superclass.Type, args);
        }

        /// <summary>
        /// Call with standard output
        /// </summary>
        /// <param name="superclass"></param>
        /// <param name="stdout"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, out string stdout, params object[] args)
        {
            return Call(methodName, Type, out stdout, args);
        }

        /// <summary>
        /// Call with standard input and output
        /// </summary>
        /// <param name="superclass"></param>
        /// <param name="stdin"></param>
        /// <param name="stdout"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, Class superclass, string stdin, out string stdout, params object[] args)
        {
            return Call(methodName, superclass.Type, stdin, out stdout, args);
        }

        /// <summary>
        /// Call with standard output
        /// </summary>
        /// <param name="superclass"></param>
        /// <param name="stdout"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, Class superclass, out string stdout, params object[] args)
        {
            return Call(methodName, superclass.Type, out stdout, args);
        }

        /// <summary>
        /// Polymorphically calling a method
        /// </summary>
        /// <param name="type">Type calling type</param>
        /// <param name="methodName">The method's name</param>
        /// <param name="args">The arguments to the method</param>
        /// <returns></returns>
        public dynamic Call(string methodName, Type type, params object[] args)
        {
            var method = type.GetMethod(methodName, GetTypes(args));
            if (method == null)
            {
                throw new MissingMethodException($"The method {methodName} of {type.Name} is missing.");
            }
            if (!method.IsPublic)
            {
                throw new MethodAccessException($"The method {methodName} of {type.Name} must be public.");
            }
            return method.Invoke(_object, args);
        }

        public static Type[] GetTypes(params object[] args)
        {
            Type[] types = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                types[i] = args[i] == null ? typeof(object) : args[i].GetType();
            }
            return types;
        }

        /// <summary>
        /// Call with only the standard output
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stdout"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, Type type, out string stdout, params object[] args)
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                var o = Call(methodName, type, args);
                stdout = Lines(sw.ToString());
                return o;
            }
        }

        /// <summary>
        /// Call with standard input and output.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stdin"></param>
        /// <param name="stdout"></param>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public dynamic Call(string methodName, Type type, string stdin, out string stdout, params object[] args)
        {
            using (StringReader sr = new StringReader(string.Join("\n", stdin)))
            using (StringWriter sw = new StringWriter())
            {
                Console.SetIn(sr);
                Console.SetOut(sw);
                var o = Call(methodName, Type, args);
                stdout = Lines(sw);
                return o;
            }
        }

        /// <summary>
        /// Fetches all the members that match the binding flags.
        /// </summary>
        /// <returns>true if sucess, false if not</returns>
        public MemberInfo[] Members(string memberName, BindingFlags bindingFlags)
        {
            var members = Type.GetMember(memberName, bindingFlags);
            return members;
        }

        public static string TypeAssemblyName
        {
            get
            {
                string className = typeof(Class).Namespace;
                return className.Substring(0, className.LastIndexOf('.'));
            }
        }

        public static string Lines(IEnumerable<string> o)
        {
            return Lines(string.Join("\n", o));
        }

        public static string Lines(object o)
        {
            return o.ToString().Replace("\r", "").TrimEnd('\n', '\r');
        }

        public override string ToString()
        {
            return Type.Name;
        }

        public string ToFullString()
        {
            if (_superclass == null)
            {
                return Type.Name.ToString();
            }
            else
            {
                return $"{_superclass.ToFullString()}\\{Type.Name.ToString()}";
            }
        }

        public Class[] GetSubclasses()
        {
            return _subclasses.ToArray();
        }
    }

    public enum ClassMemberType
    {
        None,
        Field,
        Method,
        Property
    }

    public enum ClassType
    {
        Class,
        Interface,
        AbstractClass,
        Sealed
    }
}
