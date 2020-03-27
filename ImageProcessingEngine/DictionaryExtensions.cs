using System.Collections.Generic;
using System.Linq;

namespace ImageProcessingEngine
{
    internal static class DictionaryExtensions
    {
        #region Methods

        internal static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            var item = dictionary
                .FirstOrDefault(x => x.Value.Equals(value));

            return !item.Equals(null)
                ? item.Key
                : default(TKey);
        }

        #endregion
    }
}