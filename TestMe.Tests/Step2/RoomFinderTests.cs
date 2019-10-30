using System;
using System.Linq;
using Xunit;

namespace TestMe.Tests.Step2
{
	public class RoomFinderTests
	{
		[Fact]
		public void TestNumberOfRooms()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			Assert.Equal(2, rooms.Count);
		}

		[Fact]
		public void ThrowOnInvalidNumberOfRooms()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(-1, DateTime.Now, DateTime.Now));
		}

		[Fact]
		public void RoomsHaveAPrice()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			Assert.True(rooms.All(room => room.Price > 0));
		}

		[Fact]
		public void ThrowOnAvailableFromInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(-1), DateTime.Now));
		}

		[Fact]
		public void ThrowOnAvailableToInThePast()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1)));
		}

		[Fact]
		public void ThrowOnAvailableToSmallerThanAvailableFrom()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());

			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(5), DateTime.Now.AddDays(0)));
		}
	}
}
