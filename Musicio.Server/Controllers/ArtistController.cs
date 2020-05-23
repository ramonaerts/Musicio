using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicio.Core;

namespace Musicio.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IMapper _mapper;
        public ArtistController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ApiResult GetArtists()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{artistId}")]
        public ApiResult GetArtistById(int artistId)
        {
            throw new NotImplementedException();
        }
    }
}
