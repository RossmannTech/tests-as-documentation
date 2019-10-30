using System;
using System.Linq;
using Xunit;

namespace TestMe.Tests.Intro
{
	public class UnitTest1
	{
		[Fact]
		public void Test()
		{
			var roomFinder = new RoomFinder(new SqlRoomRepository());
			var rooms = roomFinder.SearchAvailableRooms(2, DateTime.Now, DateTime.Now);

			Assert.Equal(2, rooms.Count);
			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(-1, DateTime.Now, DateTime.Now));
			Assert.True(rooms.All(room => room.Price > 0));
			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(-1), DateTime.Now));
			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1)));
			Assert.Throws<InvalidOperationException>(() => roomFinder.SearchAvailableRooms(2, DateTime.Now.AddDays(5), DateTime.Now.AddDays(0)));
		}
	}
}
