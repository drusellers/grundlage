using System;
using System.Diagnostics;

namespace grundlage
{
    [DebuggerDisplay("{_primitiveValue}")]
    public class Primitive<T> : ValueType<T>
        where T : IComparable<T>, IEquatable<T>, IComparable
    {
        readonly T _primitiveValue;

        public Primitive(T primitiveValue)
        {
            _primitiveValue = primitiveValue;
        }

        public T Unwrap() => _primitiveValue;

        public override string ToString()
        {
            return _primitiveValue?.ToString() ?? "";
        }

        public override int GetHashCode()
        {
            return _primitiveValue.GetHashCode();
        }


        protected bool Equals(Primitive<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._primitiveValue, _primitiveValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Primitive<T>)) return false;
            return Equals((Primitive<T>)obj);
        }

        public int CompareTo(Primitive<T> other)
        {
            var left = (ValueType<T>)this;
            var right = (ValueType<T>)other;
            return left.Unwrap().CompareTo(right.Unwrap());
        }

        public int CompareTo(object other)
        {
            var left = (ValueType<T>)this;
            var right = (ValueType<T>)other;
            return left.Unwrap().CompareTo(right.Unwrap());
        }
    }
}