using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public class DemeritPointsCalculatorTests
	{
		private DemeritPointsCalculator _demeritPointsCalculator;

		[SetUp]
		public void SetUp()
		{
			_demeritPointsCalculator = new DemeritPointsCalculator();
        }

		[Test]
		[TestCase(-1)]
		[TestCase(301)]
		public void CalculateDemeritPoints_SpeedIsOutOfRange_ReturnArgumentOutOfRangeException(int speed)
		{
			Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
		}

        [Test]
		[TestCase(0, 0)]
		[TestCase(1, 0)]
		[TestCase(65, 0)]
		[TestCase(70, 1)]
		[TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
        {
			var result = _demeritPointsCalculator.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}

