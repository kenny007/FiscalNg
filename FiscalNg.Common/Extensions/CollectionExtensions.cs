using System.Collections.Generic;

namespace FiscalNg.Common.Extensions {
    /// <summary>
    /// Class holding collection extension methods
    /// </summary>
    public static class CollectionExtensions {
        /// <summary>
        /// Make collection from a single value
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> MakeCollection<T>(this T value)
        {
            yield return value;
        }
	}
}
