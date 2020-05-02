using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Musicio.Core.Domain;

namespace Musicio.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, Models.User>();
        }
    }
}
