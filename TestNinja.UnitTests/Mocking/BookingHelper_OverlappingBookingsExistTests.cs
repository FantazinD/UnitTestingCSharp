using System;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
	[TestFixture]
	public class BookingHelper_OverlappingBookingsExistTests
	{
		private Mock<IBookingRepository> _bookingRepository;
		private Booking _existingBooking;

		[SetUp]
		public void SetUp()
		{
			_bookingRepository = new Mock<IBookingRepository>();
			_existingBooking = new Booking()
			{
				Id = 1,
				ArrivalDate = ArriveOn(2025, 1, 15),
				DepartureDate = DepartOn(2025, 1, 20),
				Reference = "a"
			};

            _bookingRepository.Setup(br => br.GetActiveBookings(2)).Returns(new List<Booking>() {
                _existingBooking
            }.AsQueryable());
        }

		[Test]
		public void NewBookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
		{
			var result = BookingHelper.OverlappingBookingsExist(new Booking()
			{
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, numOfDays: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

			Assert.That(result, Is.Empty);
		}

        [Test]
        public void NewBookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, numOfDays: 2)
            }, _bookingRepository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NewBookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnReferenceOfExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, numOfDays: 2),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnReferenceOfExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, numOfDays: 2),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnReferenceOfExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void NewBookingStartsInTheMiddleAndFinishesAfterAnExistingBooking_ReturnReferenceOfExistingBooking()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 2,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime Before(DateTime dateTime, int numOfDays = 1)
		{
			return dateTime.AddDays(-numOfDays);
		}

		private DateTime After(DateTime dateTime, int numOfDays = 1)
		{
			return dateTime.AddDays(numOfDays);
		}

		private DateTime ArriveOn(int year, int month, int day)
		{
			return new DateTime(year, month, day, 14, 0, 0);
        }

		private DateTime DepartOn(int year, int month, int day)
		{
            return new DateTime(year, month, day, 10, 0, 0);
        }
	}
}

