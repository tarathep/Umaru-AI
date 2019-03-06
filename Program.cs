using System;
using System.Windows.Forms;
using System.IO;

namespace Umaru_AI
{
    static class Program
    {       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Directory.CreateDirectory(@"Cache Files\\");//สร้าง ไดเรคโทรีี่เก็บไฟล
            if (File.Exists("Matrix.dll"))
            {
                Application.Run(new Main());//รันปกติ
            }
            else
            {
                byte[] myfile = Properties.Resources.Matrix;//แตกไฟลจาก resorce ก่อนค่อยรัน
                File.WriteAllBytes("Matrix.dll", myfile);
                Application.Run(new Main());
            }
            
        }
       
    }
}
