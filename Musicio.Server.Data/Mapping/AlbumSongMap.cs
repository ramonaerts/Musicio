using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class AlbumSongMap : EntityMappingConfiguration<AlbumSong>
    {
        public override void Configure(EntityTypeBuilder<AlbumSong> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Song);
            builder.HasOne(e => e.Album)
                .WithMany(e => e.AlbumSongs);
        }
    }
}
