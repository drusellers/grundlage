using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;

namespace grundlage.dapper
{
    public static class DapperHelper
    {
        static readonly FieldInfo Fi = typeof(SqlMapper).GetField("typeMap", BindingFlags.Static
                                                                             | BindingFlags.GetField
                                                                             | BindingFlags.NonPublic);
        public static Dictionary<Type, DbType> TypeMap
        {
            get
            {
                var typeMap = (Dictionary<Type, DbType>)Fi.GetValue(null);

                return typeMap;
            }
        }
    }
}