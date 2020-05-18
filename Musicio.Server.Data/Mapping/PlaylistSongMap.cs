using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class PlaylistSongMap : EntityMappingConfiguration<PlaylistSong>
    {
        public override void Configure(EntityTypeBuilder<PlaylistSong> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Song);
            builder.HasOne(e => e.Playlist)
                .WithMany(e => e.PlaylistSongs);
        }
    }
}
