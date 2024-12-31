using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class HtmlFormatterTests
	{
		[Test]
		public void FormatAsBold_WhenCalled_ShouldEncloseArgumentWithStrongElement()
		{
			var formatter = new HtmlFormatter();

			var result = formatter.FormatAsBold("abc");

			// Specific
			Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

			// General
			Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
			Assert.That(result, Does.EndWith("</strong>"));
			Assert.That(result, Does.Contain("abc"));
		}
	}
}

