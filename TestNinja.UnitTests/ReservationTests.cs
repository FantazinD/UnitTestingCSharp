using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public sealed class ReservationTests
	{
		private Reservation _reservation;

		[SetUp]
		public void SetUp()
		{
			_reservation = new Reservation();
        }

		[Test]
		public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
		{
			bool result = _reservation.CanBeCancelledBy(new User() { IsAdmin = true });

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public void CanBeCancelledBy_UserIsReserver_ReturnsTrue()
		{
			var user = new User();
			_reservation.MadeBy = user;

            bool result = _reservation.CanBeCancelledBy(user);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public void CanBeCancelledBy_UserIsNotReserver_ReturnsFalse()
		{
			_reservation.MadeBy = new User();

            bool result = _reservation.CanBeCancelledBy(new User());

			Assert.That(result, Is.EqualTo(false));
		}
	}
}

