using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Musicio.Client.Web.Invokables
{
    public class FileEncoder
    {
        public FileEncoder()
        {
            
        }

        public string GetEncodedFile()
        {
            string path = @"C:\Users\Gebruiker\source\repos\Musicio\Musicio.Client.Web\wwwroot\audio\Sample.mp3";
            Console.WriteLine(path);
            byte[] bytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(bytes);
        }
    }
}
