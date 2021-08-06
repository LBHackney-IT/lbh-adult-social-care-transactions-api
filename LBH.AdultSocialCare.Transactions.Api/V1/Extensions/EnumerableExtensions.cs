using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNotNullOrEmpty<T>(this object obj) => obj is IEnumerable<T> seq && seq.Any();

        public static bool IsNullOrEmpty<T>(this object obj) => !(obj is IEnumerable<T> seq && seq.Any());

        public static bool IsNotNullOrEmpty(object obj) => obj is IEnumerable seq && seq.GetEnumerator().MoveNext();

        public static bool IsNullOrEmpty(object obj) => !(obj is IEnumerable seq && seq.GetEnumerator().MoveNext());
    }
}
