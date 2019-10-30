using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace TestMe.Tests.Step6
{
	public class RoomFinderTests
	{
		[Fact]
		public void ShouldReturnCorrectNumberOfRooms()
		{
			var mockRepository = new MockRepository(MockBehavior.Strict);
			var roomRepositoryMock = mockRepository.Create<IRoomRepository>();
			roomRepositoryMock.Setup(mock =>
					mock.SearchForRooms(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
				.Returns(new List<Room> { new Room { Price = 1 }, new Room { Price = 1 } });
			var subject = new RoomFinder(roomRepositoryMock.Object);

			var result = subject.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			result.Should().HaveCount(2);
		}

		[Fact]
		public void ShouldReturnedRoomsHaveAPrice()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			rooms.Should().OnlyContain(room => room.Price > 0);
		}

		[Fact]
		public void ShouldNotAllowNegativeRoomSizeSearch()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Action act = () => roomFinder.SearchAvailableRooms(-1, DateTime.Now, DateTime.Now);

			act.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void ShouldNotAllowStartDateInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Action act = () => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(-1), DateTime.Now);

			act.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void ShouldNotAllowEndDateInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Action act = () => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1));

			act.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void ShouldNotAllowEndDateSmallerThanStartDate()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Action act = () => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(5), DateTime.Now.AddDays(0));

			act.Should().Throw<InvalidOperationException>();
		}
	}
}
