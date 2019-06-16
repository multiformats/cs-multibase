using System;
using System.Collections.Generic;
using System.Text;

namespace Multiformats.Base.Extensions
{
    internal static class SpanExtensions
    {
        public static bool Any<T>(this ReadOnlySpan<T> span, Func<T, bool> predicate)
        {
            for (var i = 0; i < span.Length; ++i)
            {
                if (predicate(span[i]))
                    return true;
            }
            return false;
        }

        public static ReadOnlySpan<T> Replace<T>(this ReadOnlySpan<T> span, T find, T replace)
        {
            Span<T> copy = new T[span.Length];
            for (var i = 0; i < span.Length; ++i)
                copy[i] = span[i].Equals(find) ? replace : span[i];
            return copy;
        }
    }
}
