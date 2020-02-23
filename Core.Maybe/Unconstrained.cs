namespace Core.Maybe
{
  //bug consider special type "unconstrained maybe"
  public class Unconstrained
  {
    /// <summary>
    /// Returns <paramref name="a"/>.Value or returns default(<typeparamref name="T"/>)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <returns></returns>
    public static T OrElseDefault<T>(Maybe<T> a) =>
      a.HasValue ? a.Value() : default;
  }
}