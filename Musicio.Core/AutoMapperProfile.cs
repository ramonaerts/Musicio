using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateMap<Playlist, Models.Playlist>()
                .ForMember(mdl => mdl.Songs,
                    opt => opt.MapFrom(x => x.PlaylistSongs.Select(y => y.Song)));
            CreateMap<Song, Models.Song>();
        }
    }
}
