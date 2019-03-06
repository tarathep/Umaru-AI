using System.IO;

public class ConfigFile
{
    const string P = "Cache Files\\Settings";

    public static string SettingFileRead(string type) //รับชนิดค่า return ค่ากลับ  0 = off 1=on
    {        
        string output = "";
        
            if (File.Exists(P))
            {
                StreamReader FR = new StreamReader(P);
                string tmp = FR.ReadLine();
                FR.Close();
            if (type == "hangoutshow")
            {
                output = tmp[0].ToString();
            }
            else if (type == "tauto")
            {
                output = tmp[1].ToString();
            }
            else if (type == "botTalk")
            {
                output = tmp[2].ToString();
            }
            else if (type == "Cal")
            {
                output = tmp[3].ToString();
            }
            else if (type == "ThemeColor")
            {
                output = tmp[4].ToString(); // 0สีขาว 1สีแดง 2สีส้ม 3สีเหลือง 4สีเขียว 5สีฟ้า 6สีน้ำเงิน 7สีม่วง 8สีชมพู 
            }
            else
            {
                output = "0";
            }
            }
            else
            {
                StreamWriter FW = new StreamWriter(P);
                FW.WriteLine("00000");
                FW.Close();
            }                
        return output;
    }
    public static void SettingFileWrite(string type, string value) //รับชนิดค่า บันทึกค่า
    {
               
        if (File.Exists(P))
        {
            StreamReader FR = new StreamReader(P);
            string tmp = FR.ReadLine();
            FR.Close();
            if (type == "hangoutshow")
            {
                StreamWriter w1 = new StreamWriter(P);
                w1.WriteLine(value+""+""+tmp[1]+""+""+tmp[2]+ "" + tmp[3]+""+tmp[4]);
                w1.Close();
            }
            else if (type == "tauto")
            {
                StreamWriter w2 = new StreamWriter(P);
                w2.WriteLine(tmp [0]+""+""+ value +""+ tmp[2] + "" + tmp[3] + "" + tmp[4]);
                w2.Close();
            }
            else if (type == "botTalk")
            {
                StreamWriter w3 = new StreamWriter(P);
                w3.WriteLine(tmp [0]+""+tmp[1]+"" + value+""+tmp[3] + "" + tmp[4]);
                w3.Close();
            }
            else if(type == "Cal")
            {
                StreamWriter w4 = new StreamWriter(P);
                w4.WriteLine(tmp[0] + "" + tmp[1] + ""+tmp[2] +""+value + "" + tmp[4]);
                w4.Close();
            }
            else if (type == "ThemeColor")
            {
                // 0สีขาว 1สีแดง 2สีส้ม 3สีเหลือง 4สีเขียว 5สีฟ้า 6สีน้ำเงิน 7สีม่วง 8สีชมพู 
                StreamWriter w5 = new StreamWriter(P);
                w5.WriteLine(tmp[0] + "" + tmp[1] + "" + tmp[2] + "" + tmp[3] + "" + value);
                w5.Close();
            }
            else
            {
                StreamWriter FW = new StreamWriter(P);
                FW.WriteLine("00000");
                FW.Close();
            }
        }
        else
        {
            StreamWriter FW = new StreamWriter(P);
            FW.WriteLine("00000");
            FW.Close();
        }

    }
}
