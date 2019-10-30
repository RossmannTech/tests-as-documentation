using System;
using System.Collections.Generic;

namespace TestMe
{
	public class RoomFinder
	{
		private readonly IRoomRepository _roomRepository;

		public RoomFinder(IRoomRepository roomRepository)
		{
			_roomRepository = roomRepository;
		}

		public List<Room> SearchAvailableRooms(int roomSize, DateTime availableFrom, DateTime availableTo)
		{
			if (roomSize < 1)
			{
				throw new InvalidOperationException("Room size smaller than 1 is not possible");
			}

			if (availableFrom > availableTo)
			{
				throw new InvalidOperationException("Available From date cannot be after available To date.");
			}

			if (availableFrom == availableTo)
			{
				throw new InvalidOperationException("Room booking is available for at least one night");
			}

			if (availableFrom < DateTime.Today || availableTo < DateTime.Today)
			{
				throw new InvalidOperationException("Cannot book rooms in the past");
			}

			return _roomRepository.SearchForRooms(roomSize, availableFrom, availableTo);
		}
	}
}
