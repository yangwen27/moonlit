﻿using System;

namespace Moonlit
{
    public class DomainTypeResolver : TypeResolverBase
    {
        public DomainTypeResolver(bool ignoreCase) : base(ignoreCase)
        {
            
        }
        protected override Type ResolveTypeCore(string typeName, bool ignoreCase)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(typeName, false, ignoreCase);
                if (type != null)
                    return type;
            }
            return null;
        }
    }
}