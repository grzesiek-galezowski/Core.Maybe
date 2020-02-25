using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Core.Maybe
{
  public static class MaybeAsync
  {
    /// <summary>
    /// Flips Maybe and Task: instead of having Maybe<Task<T>>> (as in case of Select) we get Task<Maybe<T>> and have possibility to await on it.
    /// </summary>
    /// <typeparam name="T">source type</typeparam>
    /// <typeparam name="TR">async result type</typeparam>
    /// <param name="this">maybe to map</param>
    /// <param name="res">async mapper</param>
    /// <returns>Task of Maybe of TR</returns>
    public static async Task<Maybe<TR>> SelectAsync<T, TR>(this Maybe<T> @this, Func<T, Task<TR>> res) => @this.HasValue
      ? await res(@this.Value()).ToMaybeGenericAsync()
      : default;

    public static async Task<T> OrElseAsync<T>(this Task<Maybe<T>> @this, Func<Task<T>> orElse)
    {
      var res = await @this;
      return res.HasValue ? res.Value() : await orElse();
    }

    public static async Task<T> OrElse<T>(this Task<Maybe<T>> @this, T orElse)
    {
      var res = await @this;
      return res.HasValue ? res.Value() : orElse;
    }

    public static async Task<T> OrElse<T>(this Task<Maybe<T>> @this, Func<T> orElse)
    {
      var res = await @this;
      return res.HasValue ? res.Value() : orElse();
    }

    public static async Task<Maybe<T>> ToMaybeGenericAsync<T>(this Task<T> task) =>
      (await task).ToMaybeGeneric();

    public static async Task<Maybe<T>> ToMaybeValueAsync<T>(this Task<T> task) where T : struct =>
      await task.ToMaybeGenericAsync();

    public static async Task<Maybe<T>> ToMaybeNullableAsync<T>(this Task<T?> task) where T : struct =>
      (await task).ToMaybeNullable();

    //no way of making this cope with non-nullable references for now
    //public static async Task<Maybe<T>> ToMaybeObjectAsync<T>(this Task<T?> task) where T : class =>
    //  (await task).ToMaybeObject();

    public static async Task<Maybe<T>> JustGenericAsync<T>(this Task<T> value) =>
      (await value).JustGeneric();

    public static async Task<Maybe<T>> JustValueAsync<T>(this Task<T> value) where T : struct =>
      (await value).JustGeneric();

    public static async Task<Maybe<T>> JustNullableAsync<T>(this Task<T?> value) where T : struct =>
      (await value).JustNullable();

    //no way of making this cope with non-nullable references for now
    //public static async Task<Maybe<T>> JustObjectAsync<T>(this Task<T?> value) where T : class =>
    //  await value.JustGenericAsync();
  }
}
