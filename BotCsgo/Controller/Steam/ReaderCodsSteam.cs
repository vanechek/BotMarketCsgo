using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotCsgo.Controller.Steam
{
    class ReaderCodsSteam
    {
        public const string path = "cods.txt";
        public static List<string> GetSteamCode()
        {
            string code;
            List<string> codeList = new List<string>();
            using (StreamReader sr = File.OpenText(path))
            {
                for (int i = 0; i < 29; i++)
                {
                    code = sr.ReadLine();
                    codeList.Add(code);
                }
            }
            return codeList;
        }
        public static void DeleteCode()
        {
            string[] readText = File.ReadAllLines(path);
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                for (int i = 0; i < readText.Length; i++)
                {
                    if (i != 0)
                    {
                        sw.WriteLine(readText[i]);
                    }
                }
            }
        }
    }
}
