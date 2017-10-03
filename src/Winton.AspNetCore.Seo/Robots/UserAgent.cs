using System;

namespace Winton.AspNetCore.Seo.Robots
{
    public struct UserAgent : IEquatable<UserAgent>
    {
        public static UserAgent Any = (UserAgent)"*";

        private readonly string _value;

        public UserAgent(string userAgent)
        {
            _value = userAgent;
        }

        public static explicit operator string(UserAgent userAgent)
        {
            return userAgent._value;
        }

        public static explicit operator UserAgent(string userAgent)
        {
            if (userAgent == null)
            {
                throw new ArgumentNullException(nameof(userAgent));
            }

            return new UserAgent(userAgent);
        }

        public static explicit operator UserAgent?(string userAgent)
        {
            return userAgent == null ? null as UserAgent? : new UserAgent(userAgent);
        }

        public static bool operator ==(UserAgent left, UserAgent right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UserAgent left, UserAgent right)
        {
            return !left.Equals(right);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return other is UserAgent && Equals((UserAgent)other);
        }

        public bool Equals(UserAgent other)
        {
            return string.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}