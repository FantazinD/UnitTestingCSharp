using System;
namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class VideoServiceTests
	{
		[Test]
		public void ReadVideoTitle_FileIsEmpty_ReturnErrorMessage()
		{
			var videoService = new VideoService();

			var result = videoService.ReadVideoTitle(new FakeFileReader());

			Assert.That(result, Does.Contain("error").IgnoreCase);
		}
	}
}

