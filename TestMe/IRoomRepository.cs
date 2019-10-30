using System;
using System.Collections.Generic;

namespace TestMe
{
	public interface IRoomRepository
	{
		List<Room> SearchForRooms(int roomSize, DateTime availableFrom, DateTime availableTo);
	}
}
