// Copyright (c) Winton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Winton.AspNetCore.Seo.Robots
{
    /// <summary>
    ///     A value type used to define a user agent. Acts as a wrapper around a string.
    /// </summary>
    public struct UserAgent : IEquatable<UserAgent>
    {
        /// <summary>
        ///     A value indicating any user agent. A wildcard value.
        /// </summary>
        public static UserAgent Any = (UserAgent)"*";

        private readonly string _value;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserAgent" /> struct from a <see cref="string" /> value.
        /// </summary>
        /// <param name="userAgent">The value of the user agent.</param>
        public UserAgent(string userAgent)
        {
            _value = userAgent;
        }

        /// <summary>
        ///     Casts a <see cref="UserAgent" /> to a <see cref="string" />.
        /// </summary>
        /// <param name="userAgent">The <see cref="UserAgent" /> to cast.</param>
        public static explicit operator string(UserAgent userAgent)
        {
            return userAgent._value;
        }

        /// <summary>
        ///     Casts a <see cref="string" /> to a <see cref="UserAgent" />.
        /// </summary>
        /// <param name="userAgent">The <see cref="string" /> to cast. Cannot be null.</param>
        public static explicit operator UserAgent(string userAgent)
        {
            if (userAgent == null)
            {
                throw new ArgumentNullException(nameof(userAgent));
            }

            return new UserAgent(userAgent);
        }

        /// <summary>
        ///     Casts a <see cref="string" /> to a nullable <see cref="UserAgent" />.
        /// </summary>
        /// <param name="userAgent">The <see cref="string" /> to cast. Can be null.</param>
        public static explicit operator UserAgent?(string userAgent)
        {
            return userAgent == null ? null as UserAgent? : new UserAgent(userAgent);
        }

        /// <summary>Equality operator for two <see cref="UserAgent" />s.</summary>
        /// <param name="left">The left-hand side of the operator.</param>
        /// <param name="right">The right-hand side of the operator.</param>
        /// <returns>True if the <paramref name="left" /> is equal to the <paramref name="right" />; otherwise, false.</returns>
        public static bool operator ==(UserAgent left, UserAgent right)
        {
            return left.Equals(right);
        }

        /// <summary>Inequality operator for two <see cref="UserAgent" />s.</summary>
        /// <param name="left">The left-hand side of the operator.</param>
        /// <param name="right">The right-hand side of the operator.</param>
        /// <returns>True if the <paramref name="left" /> is not equal to the <paramref name="right" />; otherwise, false.</returns>
        public static bool operator !=(UserAgent left, UserAgent right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return other is UserAgent && Equals((UserAgent)other);
        }

        /// <inheritdoc />
        public bool Equals(UserAgent other)
        {
            return string.Equals(_value, other._value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _value;
        }
    }
}