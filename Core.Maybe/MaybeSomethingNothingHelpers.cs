using System.Runtime.CompilerServices;

namespace Core.Maybe
{
	/// <summary>
	/// IsSomething, IsNothing and shorthands to create typed Nothing of correct type
	/// </summary>
	public static class MaybeSomethingNothingHelpers
	{
		/// <summary>
		/// Has a value inside
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSomething<T>(this Maybe<T> a) => a.HasValue;

		/// <summary>
		/// Has no value inside
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNothing<T>(this Maybe<T> a) => !a.IsSomething();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Maybe<T> NothingOf<T>(this Maybe<T> _) => default;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Maybe<T> NothingOf<T>(this T _) => default;
	}
}