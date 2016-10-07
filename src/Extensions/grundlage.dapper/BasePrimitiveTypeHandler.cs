using System;
using System.Data;
using Dapper;

namespace grundlage.dapper
{
    public abstract class BasePrimitiveTypeHandler<TValueType, TPrimitive>
        : SqlMapper.ITypeHandler
        where TValueType : class, ValueType<TPrimitive>
        where TPrimitive : IComparable<TPrimitive>, IEquatable<TPrimitive>, IComparable
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.DbType = MapToDbType();
            if (value == null || value is DBNull || ((value as TValueType) == null))
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                var v = (TValueType)value;
                SetDbParam(parameter, v);
            }
        }

        public object Parse(Type destinationType, object value)
        {
            if (value == null) return null;
            if (value == DBNull.Value) return ValueWhenDatabaseIsNull();

            var primitiveValue = (TPrimitive)Convert.ChangeType(value, typeof(TPrimitive));
            return ConstructInstance(primitiveValue);
        }

        protected virtual DbType MapToDbType()
        {
            var typeMap = DapperHelper.TypeMap;

            return typeMap[typeof(TPrimitive)];
        }

        protected virtual TValueType ConstructInstance(TPrimitive value)
        {
            return Activator.CreateInstance(typeof(TValueType), value) as TValueType;
        }

        protected virtual void SetDbParam(IDbDataParameter parameter, TValueType obj)
        {
            parameter.Value = obj.Unwrap();
        }

        protected virtual TValueType ValueWhenDatabaseIsNull()
        {
            return null;
        }
    }
}
