using System;
using System.IO;
public class ConversationRW
{	
    public static string read(string inputR)
    {
        string outputMSG = "?";
        if (File.Exists("Cache Files\\Skill Conversation"))
        {
            StreamReader fileR = new StreamReader("Cache Files\\Skill Conversation");
            string line = "";

            while (line != inputR && line != null)
            {
                line = fileR.ReadLine();
            }
            line = fileR.ReadLine();
            while (line != "---end---" && line != null)
            {
                string[] data;  Random r = new Random();       
                //outputMSG = line; //ค่าที่แสดงออกมา
                data = line.Split('+');//data[0]= คำตอบแรก
                int ran = r.Next(0,data.Length);//สุ่ม 0 ถึง จำนวนทั้งหมดของคำตอบ
                outputMSG = data[ran];

                line = fileR.ReadLine();
            }
            fileR.Close();
        }
        else
        {
            StreamWriter write = new StreamWriter("Cache Files\\Skill Conversation");
            write.Close();
        }
        return outputMSG;
    }
    public static void write(string WriteQ, string WriteA)
    {
        StreamWriter fileW = new StreamWriter("Cache Files\\Skill Conversation", true);
        string[] W = new string[3];
        W[0] = WriteQ;
        W[1] = WriteA;
        W[2] = "---end---\n";
        for (int i = 0; i < 3; i++)
        {
            fileW.WriteLine(W[i]);
        }
        fileW.Close();
    }
}
