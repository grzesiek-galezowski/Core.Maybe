using NUnit.Framework;

namespace Core.Maybe.NTests
{
	[TestFixture]
	public class MaybeSpecification
	{
		private class User
		{
			public string? Name { get; set; }
		}

		[Test]
		public void WhenSelectingNull_GettingNothing()
		{
			var user = new User { Name = null };

			var maybeUser = user.ToMaybeObject();

			Assert.AreEqual(Maybe<string>.Nothing, maybeUser.Select(_ => _.Name));

		}
	}
}
