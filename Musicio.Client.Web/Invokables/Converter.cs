using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlazorInputFile;

namespace Musicio.Client.Web.Invokables
{
    public class Converter
    {
        public async Task<string> ImageToBase64(IFileListEntry image)
        {
            var ms = new MemoryStream();
            await image.Data.CopyToAsync(ms);
            var bytes = ms.ToArray();
            string base64string = Convert.ToBase64String(bytes, 0, bytes.Length);

            Console.WriteLine(base64string);

            return base64string;
        }
    }
}
