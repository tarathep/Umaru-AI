using Matrix.Xml;

namespace Umaru_AI.Settings
{
    /*   
    This class shows how agsXMPP could also be used read and write custom xml files.
    Here we use it for the application settings which are stored in xml files.
     
    This works similar to the .NET serialization, but is much easier to use.
    All you have to do is derive from XmppXElement and write down your properties
    
    creating you own Xmpp extensions works exactly in the same way
    */
    public class Settings : XmppXElement
    {
        public Settings() : base("ag-software:settings", "Settings")
        {            
        }

        public Login Login
        {
            get { return Element<Login>(); }
            set { Replace(value); }
        }
    }
}