using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Client.Song
{
    public interface ISongService
    {
        Action<List<Core.Models.Song>> OnPlay { get; set; }
        Action StopMusic { get; set; }
    }
}
