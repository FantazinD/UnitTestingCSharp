using System;
using System.Net;

namespace TestNinja.Mocking
{
	public interface IFileDownloader
	{
		void DownloadFile(string url, string destinationPath);
	}

	public class FileDownloader : IFileDownloader
	{
		public FileDownloader()
		{
		}

        public void DownloadFile(string url, string destinationPath)
        {
            var client = new WebClient();

			client.DownloadFile(url, destinationPath);
        }
    }
}

