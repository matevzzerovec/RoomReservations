using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsBLL.Modules
{
    internal static class ConverterModule
    {
        public static byte[] ConvertToBytes(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0) return null;

            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
