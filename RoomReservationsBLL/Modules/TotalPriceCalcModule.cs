using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Modules
{
    internal static class TotalPriceCalcModule
    {
        internal static decimal CalcTotalPrice(DateTime arrivalDate, DateTime departureDate, decimal roomPrice)
        {
            if (arrivalDate.Date >= departureDate.Date)
            {
                return 0;
            }

            var totalDays = (departureDate.Date - arrivalDate.Date).Days;

            return totalDays * roomPrice;
        }
    }
}
