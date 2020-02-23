﻿using NUnit.Framework;

namespace Core.Maybe.NTests
{
	[TestFixture]
	public class MaybeEnumeratorTests
	{
		[Test]
		public void MaybeWithValueEnumerates()
		{
			var m = 1.ToMaybeGeneric().ToEnumerable();
			int c = 0;
			foreach (var val in m)
				c++;
			foreach (var val in m)
				c++;
			Assert.IsTrue(c == 2);
		}

		[Test]
		public void EmptyDoesntEnumerate()
		{
			bool gotHere = false;
			foreach (var val in Maybe<bool>.Nothing.ToEnumerable())
				gotHere = true;
			Assert.IsFalse(gotHere);
		}
	}
}