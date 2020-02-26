using System;
using System.Collections.Generic;

namespace Core.Maybe
{
	public static class MaybeDictionary
	{
    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupGeneric<TK, T>(this Dictionary<TK, T> dictionary, TK key)
    {
      var getter = MaybeFunctionalWrappers.WrapGeneric<TK, T>(dictionary.TryGetValue);
      return getter(key);
    }


    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupObject<TK, T>(this Dictionary<TK, T?> dictionary, TK key) where T : class
    {
      var getter = MaybeFunctionalWrappers.WrapObject<TK, T>(dictionary.TryGetValue);
      return getter(key);

    }


    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupValue<TK, T>(this Dictionary<TK, T> dictionary, TK key) where T : struct
    {
      return dictionary.LookupGeneric<TK, T>(key);
    }
  }
}