using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RoomReservationsDomain.Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomReservationsDomain.Reservations.Configurations
{
    public class RoomPictureConfiguration : IEntityTypeConfiguration<RoomPicture>
    {
        public void Configure(EntityTypeBuilder<RoomPicture> builder)
        {
            builder.ToTable("RoomPicture", "Resevations");

            // Primary Key
            builder.HasKey(x => x.RoomPictureId);
            builder.Property(x => x.RoomPictureId).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.PictureData).IsRequired();

            // Relationships
            builder.HasOne(rp => rp.Room).WithMany(r => r.RoomPictures).HasForeignKey(rp => rp.RoomId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
