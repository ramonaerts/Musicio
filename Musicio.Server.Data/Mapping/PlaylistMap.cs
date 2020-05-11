using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class PlaylistMap : EntityMappingConfiguration<Playlist>
    {
        public override void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
