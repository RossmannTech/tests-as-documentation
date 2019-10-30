using System;
using System.Collections.Generic;

namespace TestMe
{
	public class SqlRoomRepository : IRoomRepository
	{
		public List<Room> SearchForRooms(int roomSize, DateTime availableFrom, DateTime availableTo)
		{
			var random = new Random();
			var rooms = new List<Room>();

			for (var i = 0; i <= random.Next(0, 2); i++)
			{
				rooms.Add(new Room
				{
					AvailableFrom = availableFrom.AddDays(random.Next(0,2)),
					AvailableTo = availableTo.AddDays(random.Next(2,5)),
					Price = random.Next(100,200),
					RoomSize = random.Next(roomSize, roomSize + 2)
				});
			}

			return rooms;
		}
	}
}
