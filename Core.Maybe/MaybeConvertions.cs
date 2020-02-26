using System;
using System.Collections.Generic;

namespace Core.Maybe
{
  /// <summary>
  /// Fluent exts for converting the values of Maybe to/from lists, nullables; casting and upshifting
  /// </summary>
  public static class MaybeConvertions
  {
    /// <summary>
    /// If <paramref name="a"/>.Value exists and can be successfully casted to <typeparamref name="TB"/>, returns the casted one, wrapped as Maybe&lt;TB&gt;, otherwise Nothing
    /// </summary>
    public static Maybe<TB> Cast<TA, TB>(this Maybe<TA> a) where TB : class =>
      from m in a
      let t = m as TB
      where t != null
      select t;

    /// <summary>
    /// If <paramref name="a"/> can be successfully casted to <typeparamref name="TR"/>, returns the casted one, wrapped as Maybe&lt;TR&gt;, otherwise Nothing
    /// </summary>
    public static Maybe<TR> MaybeCast<T, TR>(this T a) where TR : T =>
      MaybeFunctionalWrappers.Catcher<T, TR, InvalidCastException>(o => (TR) o)(a);

    /// <summary>
    /// If <paramref name="a"/>.Value is present, returns an enumerable of that single value, otherwise an empty one
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> a)
    {
      if (a.IsSomething())
        yield return a.Value();
    }

    /// <summary>
    /// Converts Maybe to corresponding Nullable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static T? ToNullable<T>(this Maybe<T> a) where T : struct =>
      a.IsSomething() ? a.Value() : (T?) null;

    /// <summary>
    /// Converts Nullable to corresponding Maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Maybe<T> ToMaybeNullable<T>(this T? a) where T : struct =>
      a?.ToMaybeGeneric() ?? default;

    /// <summary>
    /// Converts a struct to corresponding Maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Maybe<T> ToMaybeValue<T>(this T a) where T : struct =>
      a.ToMaybeGeneric();

    /// <summary>
    /// Returns <paramref name="a"/> wrapped as Maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Maybe<T> ToMaybeGeneric<T>(this T a) =>
      a == null ? default : new Maybe<T>(a);

    /// <summary>
    /// Returns <paramref name="a"/> wrapped as Maybe
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Maybe<T> ToMaybeObject<T>(this T? a) where T : class
    {
      return a!.ToMaybeGeneric();
    }

    public static Maybe<T> JustNullable<T>(this T? value) where T : struct
    {
      if (value.HasValue)
      {
        return value.Value.ToMaybeValue();
      }

      throw new ArgumentNullException(nameof(value), "Cannot create a Just<" + typeof(T) + "> from null");
    }

    public static Maybe<T> JustGeneric<T>(this T value)
    {
      if (value != null)
      {
        return value.ToMaybeGeneric();
      }

      throw new ArgumentNullException(nameof(value), "Cannot create a Just<" + typeof(T) + "> from null");
    }

    public static Maybe<T> JustObject<T>(this T? value) where T : class
    {
      return value!.JustGeneric();
    }

    public static Maybe<T> JustValue<T>(this T value) where T : struct
    {
      return value.JustGeneric();
    }

  }
}