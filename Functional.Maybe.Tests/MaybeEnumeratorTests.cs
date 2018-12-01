using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Maybe.Tests
{
	[TestClass]
	public class MaybeEnumeratorTests
	{
		[TestMethod]
		public void MaybeWithValueEnumerates()
		{
			var m = 1.ToMaybe();
			int c = 0;
			foreach (var val in m)
				c++;
			foreach (var val in m)
				c++;
			Assert.IsTrue(c == 2);
		}

		[TestMethod]
		public void EmptyDoesntEnumerate()
		{
			bool gotHere = false;
			foreach (var val in Maybe<bool>.Nothing)
				gotHere = true;
			Assert.IsFalse(gotHere);
		}
	}
}
