using System;
namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class ProductTests
	{

		[Test]
		public void GetPrice_CustomerIsGold_Apply30PctDiscout()
		{
			var product = new Product() { ListPrice = 100 };

			var result = product.GetPrice(new Customer() { IsGold = true });

			Assert.That(result, Is.EqualTo(70));
		}
	}
}

