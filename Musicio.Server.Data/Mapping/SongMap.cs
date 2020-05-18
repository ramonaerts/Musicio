using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class SongMap : EntityMappingConfiguration<Song>
    {
        public override void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
