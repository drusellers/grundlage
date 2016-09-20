using System;

namespace grundlage
{
    public interface ValueType<T>
        where T : IComparable<T>, IEquatable<T>, IComparable
    {
        T Unwrap();
    }
}
