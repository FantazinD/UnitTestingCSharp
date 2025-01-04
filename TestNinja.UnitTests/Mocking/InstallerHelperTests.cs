using System;
using System.Net;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class InstallerHelperTests
	{
		private InstallerHelper _installerHelper;
		private Mock<IFileDownloader> _fileDownloader;

		[SetUp]
		public void SetUp()
		{
			_fileDownloader = new Mock<IFileDownloader>();
			_installerHelper = new InstallerHelper(_fileDownloader.Object);
		}

		[Test]
		public void DownloadInstaller_DownloadFails_ReturnFalse()
		{
			_fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();
			var result = _installerHelper.DownloadInstaller(customerName: "a", installerName: "b");

            Assert.That(result, Is.False);
		}

		[Test]
		public void DownloadInstaller_DownloadSucceeds_ReturnTrue()
		{
			var result = _installerHelper.DownloadInstaller(customerName: "a", installerName: "b");

            Assert.That(result, Is.True);
		}
	}
}

