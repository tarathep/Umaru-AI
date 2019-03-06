using Matrix;
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
    public class Login : XmppXElement
    {
        public Login() : base( "ag-software:settings", "Login" )
        {         
        }

        public string User
        {
            get { return GetTag("User"); }
            set { SetTag("User", value); }
        }

        public string Server
        {
            get { return GetTag("Server"); }
            set { SetTag("Server", value); }
        }

        public string Password
        {
            get { return GetTag("Password"); }
            set { SetTag("Password", value); }
        }

        public string Resource
        {
            get { return GetTag("Resource"); }
            set { SetTag("Resource", value); }
        }

        public int Priority
        {
            get { return GetAttributeInt("Priority"); }
            set { SetTag("Priority", value); }
        }

        public int Port
        {
            get { return GetTagInt("Port"); }
            set { SetTag("Port", value); }
        }

        public bool Tls
        {
            get { return GetTagBool("Tls"); }
            set { SetTag("Tls", value); }
        }
    }
}