using System;

namespace grundlage
{
    public class Handle : Primitive<string>
    {
        public Handle(string handle) : base(Scrub(handle))
        {
            
        }

        static string Scrub(string input)
        {
            if (input == null)
            {
                return null;
            }

            return input.Trim()
                .Replace("(", "")
                .Replace(")", "")
                .Replace("&", "")
                .Replace("'", "")
                .Replace(".", "")
                .Replace(" ", "-")
                .Replace("/", "-")
                .Replace("--", "-")
                .ToLower();
        }

        public static bool operator ==(Handle a, Handle b)
        {
            // ReSharper disable once BuiltInTypeReferenceStyle
            // ReSharper disable once ArrangeStaticMemberQualifier
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Unwrap() == b.Unwrap();
        }

        public static bool operator !=(Handle a, Handle b)
        {
            return !(a == b);
        }

        protected bool Equals(Handle other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (!obj.GetType().Equals(typeof(Handle))) return false;

            return Equals((Handle)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}