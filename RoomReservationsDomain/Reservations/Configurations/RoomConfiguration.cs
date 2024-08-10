using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomReservationsDomain.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDomain.Reservations.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room", "Resevations");

            // Primary Key
            builder.HasKey(x => x.RoomId);
            builder.Property(x => x.RoomId).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Price).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.ShortDescription).HasMaxLength(500);
            builder.Property(x => x.LastTimestamp).IsRequired();
            builder.Property(x => x.LastUser).IsRequired().HasMaxLength(150);

            // Relationships
            builder.HasMany(r => r.RoomPictures).WithOne(rp => rp.Room).HasForeignKey(rp => rp.RoomId);
            builder.HasMany(r => r.Reservations).WithOne(res => res.Room).HasForeignKey(res => res.RoomId);
        }
    }
}
