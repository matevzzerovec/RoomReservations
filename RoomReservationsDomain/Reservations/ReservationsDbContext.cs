using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using RoomReservationsDAL.Reservations.Configurations;
using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations
{
    public class ReservationsDbContext : DbContext
    {
        public ReservationsDbContext(DbContextOptions<ReservationsDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Room { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<RoomPicture> RoomPicture { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Auto apply all the entity configurations from provided assembly info, instead of calling them one-by-one
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationsDbContext).Assembly);
        }
    }
}
