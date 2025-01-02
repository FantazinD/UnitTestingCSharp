using System;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class VideoServiceTests
	{
		private VideoService _videoService;
		private Mock<IFileReader> _fileReader;

		[SetUp]
		public void SetUp()
		{
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }

		[Test]
		public void ReadVideoTitle_FileIsEmpty_ReturnErrorMessage()
		{
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

			var result = _videoService.ReadVideoTitle();

			Assert.That(result, Does.Contain("error").IgnoreCase);
		}
	}
}

