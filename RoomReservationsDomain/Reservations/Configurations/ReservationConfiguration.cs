using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDAL.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDAL.Reservations.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation", "Resevations");

            // Primary Key
            builder.HasKey(res => res.ReservationId);
            builder.Property(res => res.ReservationId).ValueGeneratedOnAdd();

            // Properties
            builder.Property(res => res.ArrivalDate).IsRequired();
            builder.Property(res => res.DepartureDate).IsRequired();
            builder.Property(res => res.TotalPrice).IsRequired().HasPrecision(18, 2);
            builder.Property(res => res.Name).IsRequired().HasMaxLength(250);
            builder.Property(res => res.Email).IsRequired().HasMaxLength(250);
            builder.Property(res => res.PhoneNumber).IsRequired().HasMaxLength(50);
            builder.Property(res => res.Timestamp).IsRequired();

            // Relationships
            builder.HasOne(res => res.Room).WithMany(r => r.Reservations).HasForeignKey(res => res.RoomId);
        }
    }
}
