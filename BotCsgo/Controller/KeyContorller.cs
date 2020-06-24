using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Controller
{
    class KeyContorller
    {
        private const string Path = "apikey.txt";
        public static string LoadKey()
        {
            string text;
            using (FileStream fileIoLoad = new FileStream(Path, FileMode.OpenOrCreate))
            {
                byte[] buffer = new byte[fileIoLoad.Length];
                fileIoLoad.Read(buffer, 0, buffer.Length);
                text = Encoding.Default.GetString(buffer);
            }
            return text;
        }

        public static void SaveKey(string ApiKey)
        {
            using(FileStream fs = new FileStream(Path, FileMode.Create))
            {
                byte[] buffer = Encoding.Default.GetBytes(ApiKey);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public static void DeleteKey()
        {
            File.Delete(Path);
        }
    }
}
