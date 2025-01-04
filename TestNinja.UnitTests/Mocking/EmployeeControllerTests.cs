using System;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class EmployeeControllerTests
	{
		private Mock<IEmployeeStorage> _employeeStorage;
		private EmployeeController _employeeController;

		public EmployeeControllerTests()
		{
		}

		[SetUp]
		public void SetUp()
		{
			_employeeStorage = new Mock<IEmployeeStorage>();
			_employeeController = new EmployeeController(_employeeStorage.Object);
		}

		[Test]
		public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
		{
            _employeeController.DeleteEmployee(1);

            _employeeStorage.Verify(es => es.DeleteEmployee(1));
		}

		[Test]
		public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
		{
            var result = _employeeController.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

    }
}

