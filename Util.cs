using System;
using System.Collections;
using System.IO;
using System.Text;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;

namespace Umaru_AI //project name
{
    public class Util
    {

        private static string _settingsFolder;
        private static string _settingsFilename;

        private static Hashtable _chatForms = new Hashtable();


        public static Hashtable ChatForms
        {
            get { return _chatForms; }
        }

        private static string SettingsFolder
        {
            get
            {
                if (_settingsFolder == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"UmaruHangout");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    _settingsFolder = path;
                }
                return _settingsFolder;
            }
        }

        private static string SettingsFilename
        {
            get
            {
                if (_settingsFilename == null)
                    _settingsFilename = Path.Combine(SettingsFolder, "settings.xml");
                
                return _settingsFilename;
            }
        }
        
        public static Settings.Settings LoadSettings()
        {
            XmppXElement set = null;
            if (File.Exists(SettingsFilename))
            {
                set = XmppXElement.LoadXml(File.ReadAllText(SettingsFilename));
                
            }
            if (set is Settings.Settings)
                    return set as Settings.Settings;
            
            return new Settings.Settings();
        }

        public static void SaveSettings(Settings.Settings set)
        {
            set.Save(SettingsFilename);
        }

        public static int GetRosterImageIndex(Presence pres)
        {
            if (pres.Type == PresenceType.unavailable)
                return 0;

            switch (pres.Show)
            {
                case Show.chat:
                    return 1;
                case Show.away:
                    return 2;
                case Show.xa:
                    return 2;
                case Show.dnd:
                    return 3;
                default:
                    return 1;
            }
        }

       
        public static string HumanReadableFileSize(long lBytes)
        {
            var sb = new StringBuilder();
            string strUnits = "Bytes";
            float fAdjusted = 0.0F;

            if (lBytes > 1024)
            {
                if (lBytes < 1024 * 1024)
                {
                    strUnits = "KB";
                    fAdjusted = Convert.ToSingle(lBytes) / 1024;
                }
                else
                {
                    strUnits = "MB";
                    fAdjusted = Convert.ToSingle(lBytes) / 1048576;
                }
                sb.AppendFormat("{0:0.0} {1}", fAdjusted, strUnits);
            }
            else
            {
                fAdjusted = Convert.ToSingle(lBytes);
                sb.AppendFormat("{0:0} {1}", fAdjusted, strUnits);
            }

            return sb.ToString();
        }

    }
}
