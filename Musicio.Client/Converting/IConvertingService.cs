using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Musicio.Client.Converting
{
    public interface IConvertingService
    {
        Task<string> ImageToBase64(IFileListEntry image);
    }
}
