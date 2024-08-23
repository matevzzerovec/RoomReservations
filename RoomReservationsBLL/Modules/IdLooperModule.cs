using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Modules
{
    internal static class IdLooperModule
    {
        internal static int GetNextId(int currentRoomId, List<int> roomIdList)
        {
            // If only element in list, return current RoomId
            if (roomIdList.Count <= 1) return currentRoomId;

            var indexOfCurrent = roomIdList.IndexOf(currentRoomId);

            if (indexOfCurrent + 1 == roomIdList.Count)
            {
                // If current element is last, return first element (loop)
                return roomIdList.First();
            }
            else
            {
                return roomIdList.ElementAt(indexOfCurrent + 1);
            }
        }

        internal static int GetPrevId(int currentRoomId, List<int> roomIdList)
        {
            // If only element in list, return current RoomId
            if (roomIdList.Count <= 1) return currentRoomId;

            var indexOfCurrent = roomIdList.IndexOf(currentRoomId);

            if (indexOfCurrent == 0)
            {
                // If current element first, return last element (loop)
                return roomIdList.Last();
            }
            else
            {
                return roomIdList.ElementAt(indexOfCurrent - 1);
            }
        }
    }
}
