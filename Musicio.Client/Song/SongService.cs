using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Client.Song
{
    public class SongService : ISongService
    {
        public Action<List<Core.Models.Song>> OnPlay { get; set; }
        public Action StopMusic { get; set; }
    }
}
