using System;
using System.Linq;
using Xunit;

namespace TestMe.Tests.Step3
{
	public class RoomFinderTests
	{
		[Fact]
		public void ShouldReturnCorrectNumberOfRooms()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			Assert.Equal(2, rooms.Count);
		}

		[Fact]
		public void ShouldReturnedRoomsHaveAPrice()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			Assert.True(rooms.All(room => room.Price > 0));
		}

		[Fact]
		public void ShouldNotAllowNegativeRoomSizeSearch()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(-1, DateTime.Now, DateTime.Now));
		}

		[Fact]
		public void ShouldNotAllowStartDateInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(-1), DateTime.Now));
		}

		[Fact]
		public void ShouldNotAllowEndDateInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1)));
		}

		[Fact]
		public void ShouldNotAllowEndDateSmallerThanStartDate()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(5), DateTime.Now.AddDays(0)));
		}
	}
}
