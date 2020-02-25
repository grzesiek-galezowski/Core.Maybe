using System.Threading.Tasks;
using NUnit.Framework;

namespace Core.Maybe.NTests
{
	[TestFixture]
	public class MaybeAsyncTests
	{
		[Test]
		public async Task SelectAsyncTest()
		{
			Task<int> Two() => Task.FromResult(2);

			var onePlusTwo = await 1.ToMaybeGeneric().SelectAsync(async one => one + (await Two()));

			Assert.AreEqual(3, onePlusTwo.Value());
    }

    [Test]
		public async Task StructToMaybeAsyncTest()
		{
			var task = Task.FromResult(2);
			Assert.AreEqual((await task).ToMaybeValue(), await task.ToMaybeValueAsync());
		}

		[Test]
		public async Task NullableStructToMaybeAsyncTest()
		{
			var task = Task.FromResult<int?>(null);
			Assert.AreEqual((await task).ToMaybeNullable(), await task.ToMaybeNullableAsync());
		}

		//no way of making this cope with non-nullable references for now
    //[Test]
		//public async Task ObjectToMaybeAsyncTest()
		//{
		//	Task<string> task = Task.FromResult("2");
		//	//bug!!!
		//	Assert.AreEqual((await task).ToMaybeObject(), await task.ToMaybeObjectAsync());
    //}
	}
}
