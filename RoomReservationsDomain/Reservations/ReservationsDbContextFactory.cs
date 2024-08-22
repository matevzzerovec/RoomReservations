using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RoomReservationsDAL.Reservations
{
    public class ReservationsDbContextFactory : IDesignTimeDbContextFactory<ReservationsDbContext>
    {
        public ReservationsDbContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<ReservationsDbContext>();
            var connectionString = "hidden";

            builder.UseSqlServer(connectionString);

            return new ReservationsDbContext(builder.Options);
        }
    }
}