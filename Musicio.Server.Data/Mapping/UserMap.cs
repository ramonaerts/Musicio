using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Musicio.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Server.Data.Mapping
{
    public class UserMap : EntityMappingConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
