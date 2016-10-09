namespace grundlage.nhibernate
{
    using System;
    using System.Data;
    using NHibernate.SqlTypes;
    using NHibernate.Type;
    using NHibernate.UserTypes;

    public abstract class BasePrimitiveUserType<TUserType> :
        IUserType
        where TUserType : class
    {
        readonly NullableType _type;

        protected BasePrimitiveUserType(NullableType type)
        {
            _type = type;
        }

        public new bool Equals(object x, object y)
        {
            bool result = false;

            if (x == null && y == null) return true;
            if (x == null || y == null) return false;

            var l = x as TUserType;
            var r = y as TUserType;

            if (l != null && r != null)
            {
                result = l.Equals(r);
            }
            if (l == null && r == null) result = true;

            return result;
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            TUserType result = null;
            var dbresult = _type.NullSafeGet(rs, names[0]);

            if (dbresult != null)
            {

                result = ConstructInstance(dbresult);
            }

            return result;
        }

        protected virtual TUserType ConstructInstance(object dbresult)
        {
            return Activator.CreateInstance(typeof(TUserType), dbresult) as TUserType;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            var l = value as TUserType;
            if (l == null)
                ((IDbDataParameter)cmd.Parameters[index]).Value = DBNull.Value;
            else
                SetDbParam(cmd, GetValue(l), index);
        }

        protected virtual void SetDbParam(IDbCommand cmd, string value, int index)
        {
            _type.Set(cmd, value, index);
        }
        protected virtual string GetValue(TUserType value)
        {
            var ut = (ValueType<string>)value;
            return ut.Unwrap();
        }

        public object DeepCopy(object value)
        {
            return value;
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public SqlType[] SqlTypes
        {
            get { return new[] { _type.SqlType }; }
        }

        public Type ReturnedType
        {
            get { return typeof(TUserType); }
        }

        public bool IsMutable
        {
            get { return false; }
        }
    }
}
