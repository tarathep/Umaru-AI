using System.IO;


namespace Umaru_AI
{
    class Note
    {
        const string path = "Cache Files\\NoteSave";
        
        public static void Create(string text)
        {
            Directory.CreateDirectory(@"Cache Files\\");
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(text);
            sw.Close();
        }

        public static string Read()
        {
            string tmp = "";
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                tmp = sr.ReadToEnd();
                sr.Close();
            }
            else
            {
                Directory.CreateDirectory(@"Cache Files\\");
                StreamWriter sw = new StreamWriter(path);
                sw.Close();
            }
            return tmp;
        }
    }
}
