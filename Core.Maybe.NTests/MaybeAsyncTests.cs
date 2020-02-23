using NUnit.Framework;
using System.Threading.Tasks;

namespace Functional.Maybe.Tests
{
	[TestFixture]
	public class MaybeAsyncTests
	{
		[Test]
		public async Task SelectAsyncTest()
		{
			Task<int> Two() => Task.FromResult(2);

			var onePlusTwo = await 1.ToMaybe().SelectAsync(async one => one + (await Two()));

			Assert.AreEqual(3, onePlusTwo.Value());
		}

		[Test]
		public async Task StructToMaybeAsyncTest()
		{
			var task = Task.FromResult(2);
			Assert.AreEqual((await task).ToMaybe(), await task.ToMaybeAsync());
		}

		[Test]
		public async Task NullableStructToMaybeAsyncTest()
		{
			var task = Task.FromResult<int?>(null);
			Assert.AreEqual((await task).ToMaybe(), await task.ToMaybeAsync());
		}

		[Test]
		public async Task ObjectToMaybeAsyncTest()
		{
			var task = Task.FromResult("2");
			Assert.AreEqual((await task).ToMaybe(), await task.ToMaybeAsync());
		}
	}
}
