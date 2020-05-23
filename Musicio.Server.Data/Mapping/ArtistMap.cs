using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;

namespace Musicio.Server.Data.Mapping
{
    public class ArtistMap : EntityMappingConfiguration<Artist>
    {
        public override void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Albums).WithOne();
        }
    }
}
