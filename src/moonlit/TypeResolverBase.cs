﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moonlit.Collections;

namespace Moonlit
{
    public abstract class TypeResolverBase : ITypeResolvor
    {
        private readonly bool _ignoreCase;
        private readonly Dictionary<string, Type> _types;
        private readonly Dictionary<string, string> _assemblyMaps
            = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        protected TypeResolverBase(bool ignoreCase)
        {
            _ignoreCase = ignoreCase;
            if (ignoreCase)
            {
                _types = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                _types = new Dictionary<string, Type>();
            }
            AddTypeAlias("object", typeof(object));

            AddTypeAlias("string", typeof(string));
            AddTypeAlias("datetime", typeof(DateTime));
            AddTypeAlias("int", typeof(Int32));
            AddTypeAlias("float", typeof(float));
            AddTypeAlias("long", typeof(long));
            AddTypeAlias("double", typeof(double));
            AddTypeAlias("boolean", typeof(bool));
            AddTypeAlias("bool", typeof(bool));
            AddTypeAlias("string", typeof(string));
            AddTypeAlias("decimal", typeof(decimal));

            AddTypeAlias("datetime?", typeof(DateTime?));
            AddTypeAlias("int?", typeof(Int32?));
            AddTypeAlias("float?", typeof(float?));
            AddTypeAlias("long?", typeof(long?));
            AddTypeAlias("double?", typeof(double?));
            AddTypeAlias("boolean?", typeof(bool?));
            AddTypeAlias("bool?", typeof(bool?));
            AddTypeAlias("decimal?", typeof(decimal?));
        }
        public Type ResolveType(string typeName)
        {
            if (typeName == null) throw new ArgumentNullException("typeName");

            typeName = typeName.Trim();

            Type type;

            if (_types.TryGetValue(typeName, out type))
            {
                return type;
            }
            if (typeName.EndsWith("[]"))
            {
                type = System.Array.CreateInstance(ResolveType(typeName.Substring(0, typeName.Length - 2)), 0).GetType();
                AddTypeAlias(typeName, type);
                return type;
            }

            typeName = GetFullTypeName(typeName);
            type = Type.GetType(typeName, false, _ignoreCase) ?? ResolveTypeCore(typeName, _ignoreCase);
            if (type != null)
            {
                AddTypeAlias(typeName, type);
                AddTypeAlias(type.FullName, type);
            }
            return type;
        }

        public IEnumerable<Type> ResolveTypes(Predicate<Type> predicate)
        {
            return ResolveTypes().Where(x => predicate(x));
        }

        protected abstract IEnumerable<Type> ResolveTypes();


        private string GetFullTypeName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            int pos1 = name.IndexOf(",", StringComparison.Ordinal);
            if (pos1 < 0)
                return name;
            int pos2 = name.IndexOf(",", pos1 + 1, StringComparison.Ordinal);
            if (pos2 >= 0)
                return name;

            string assemblyAlia = name.Substring(pos1 + 1).Trim();
            string assemblyName = default(string);
            if (_assemblyMaps.TryGetValue(assemblyAlia, out assemblyName))
            {
                return name.Substring(0, pos1 + 1) + assemblyName;
            }

            return name;
        }


        public void AddTypeAlias(string name, Type type)
        {
            _types[name] = type;
        }
        public void AddAssemblyAlias(string name, string assemblyName)
        {
            _assemblyMaps[name] = assemblyName;
        }

        protected abstract Type ResolveTypeCore(string typeName, bool ignoreCase);
    }
}