using System;
using System.Collections.Generic;

namespace Core.Maybe
{
  static internal class MaybeIReadOnlyDictionary
  {
    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupGeneric<TK, T>(this IReadOnlyDictionary<TK, T> dictionary, TK key)
    {
      var getter = MaybeFunctionalWrappers.Wrap<TK, T>(dictionary.TryGetValue);
      return getter(key);
    }

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupObject<TK, T>(this IReadOnlyDictionary<TK, T?> dictionary, TK key) where T : class
    {
      return dictionary!.LookupGeneric<TK, T>(key);
    }

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupValue<TK, T>(this IReadOnlyDictionary<TK, T> dictionary, TK key) where T : struct
    {
      return dictionary.LookupGeneric<TK, T>(key);
    }

    [Obsolete("Use Lookup or LookupNullable")]
    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.JustGeneric() : Maybe<TValue>.Nothing;
    }
  }
}