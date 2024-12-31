using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
	[TestFixture]
	public sealed class ReservationTests
	{

		[Test]
		public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
		{
			var reservation = new Reservation();

			bool result = reservation.CanBeCancelledBy(new User() { IsAdmin = true });

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public void CanBeCancelledBy_UserIsReserver_ReturnsTrue()
		{
			var user = new User();
			var reservation = new Reservation() { MadeBy = user };

			bool result = reservation.CanBeCancelledBy(user);

			Assert.That(result, Is.EqualTo(true));
		}

		[Test]
		public void CanBeCancelledBy_UserIsNotReserver_ReturnsFalse()
		{
			var reservation = new Reservation() { MadeBy = new User() };

			bool result = reservation.CanBeCancelledBy(new User());

			Assert.That(result, Is.EqualTo(false));
		}
	}
}

