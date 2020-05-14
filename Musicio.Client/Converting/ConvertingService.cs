using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Musicio.Client.Converting
{
    public class ConvertingService : IConvertingService
    {
        public async Task<string> ImageToBase64(IFileListEntry image)
        {
            var ms = new MemoryStream();
            await image.Data.CopyToAsync(ms);
            var bytes = ms.ToArray();

            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
