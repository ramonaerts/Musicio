using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class AlbumMap : EntityMappingConfiguration<Album>
    {
        public override void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.AlbumSongs);
        }
    }
}
