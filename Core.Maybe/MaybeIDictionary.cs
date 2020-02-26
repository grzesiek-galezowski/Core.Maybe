using System;
using System.Collections.Generic;

namespace Core.Maybe
{
  static internal class MaybeIDictionary
  {
    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupGeneric<TK, T>(this IDictionary<TK, T> dictionary, TK key)
    {
      var getter = MaybeFunctionalWrappers.WrapGeneric<TK, T>(dictionary.TryGetValue);
      return getter(key);
    }

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupObject<TK, T>(this IDictionary<TK, T?> dictionary, TK key) where T : class
    {
      var getter = MaybeFunctionalWrappers.WrapObject<TK, T>(dictionary.TryGetValue);
      return getter(key);
    }

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupValue<TK, T>(this IDictionary<TK, T> dictionary, TK key) where T : struct
    {
      return dictionary.LookupGeneric<TK, T>(key);
    }

    [Obsolete("Use Lookup or LookupNullable")]
    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.JustGeneric() : Maybe<TValue>.Nothing;
    }
  }
}