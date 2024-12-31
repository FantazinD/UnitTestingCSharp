using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class CustomerControllerTests
	{
		private CustomerController _customerController;

		[SetUp]
        public void Setup()
        {
            _customerController = new CustomerController();
        }

        [Test]
		public void GetCustomer_IdIsZero_ReturnsNotFound()
		{
			var result = _customerController.GetCustomer(0);

			/* First Option */
			// result is exactly a NotFound object
			Assert.That(result, Is.TypeOf<NotFound>());

			/* Second Option */
			// result can be a NotFound object OR any of its derivatives/children
			// Assert.That(result, Is.InstanceOf<NotFound>());
		}

		[Test]
		public void GetCustomer_IdIsNotZero_ReturnsOk()
		{
			var result = _customerController.GetCustomer(1);

			Assert.That(result, Is.TypeOf<Ok>());
        }
	}
}

