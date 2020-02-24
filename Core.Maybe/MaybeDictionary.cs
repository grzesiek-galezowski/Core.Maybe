using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Core.Maybe
{
	public static class MaybeDictionary
	{
		/// <summary>
		/// Tries to get value from Dictionary safely
		/// </summary>
		[return: NotNull]
		public static Maybe<T> Lookup<TK, T>(this IDictionary<TK, T> dictionary, TK key)
		{
			var getter = MaybeFunctionalWrappers.Wrap<TK, T>(dictionary.TryGetValue);
			return getter(key);
		}

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupNullable<TK, T>(this IDictionary<TK, T?> dictionary, TK key) where T : class
    {
      return dictionary!.Lookup<TK, T>(key);
    }
		
		/// <summary>
		/// Tries to get value from Dictionary safely
		/// </summary>
		[return: NotNull]
		public static Maybe<T> Lookup<TK, T>(this IReadOnlyDictionary<TK, T> dictionary, TK key)
		{
			var getter = MaybeFunctionalWrappers.Wrap<TK, T>(dictionary.TryGetValue);
			return getter(key);
		}

    /// <summary>
    /// Tries to get value from Dictionary safely
    /// </summary>
    public static Maybe<T> LookupNullable<TK, T>(this IReadOnlyDictionary<TK, T?> dictionary, TK key) where T : class
    {
      return dictionary!.Lookup<TK, T>(key);
    }

		/// <summary>
		/// Tries to get value from Dictionary safely
		/// </summary>
		public static Maybe<T> Lookup<TK, T>(this Dictionary<TK, T> dictionary, TK key)
		{
			var getter = MaybeFunctionalWrappers.Wrap<TK, T>(dictionary.TryGetValue);
			return getter(key);
		}

		/// <summary>
		/// Tries to get value from Dictionary safely
		/// </summary>
		public static Maybe<T> LookupNullable<TK, T>(this Dictionary<TK, T?> dictionary, TK key) where T : class
    {
      return dictionary!.Lookup<TK, T>(key);
    }

    [Obsolete("Use Lookup or LookupNullable")]
    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.Just() : Maybe<TValue>.Nothing;
    }

    [Obsolete("Use Lookup or LookupNullable")]
    public static Maybe<TValue> MaybeValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      var result = dictionary.TryGetValue(key, out var value);
      return result ? value.Just() : Maybe<TValue>.Nothing;
    }

	}
}