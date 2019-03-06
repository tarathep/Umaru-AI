using System.Drawing;
using System.Windows.Forms;
using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using EventArgs = System.EventArgs;
using Message = Matrix.Xmpp.Client.Message;
using System.Diagnostics;

namespace Umaru_AI
{
    public partial class HangoutChat : Form
    {      
        bool checkbot = false;
        int move;
        int mx;
        int my;        
        private XmppClient _xmppClient;
        private Jid _jid;
        private readonly string _nickname;

        public HangoutChat(Jid jid, XmppClient con, string nickname)
        {
            _jid = jid;
            _xmppClient = con;
            _nickname = nickname;

            InitializeComponent();

            Text = "Chat with " + nickname;
            label1.Text  = nickname;
            Util.ChatForms.Add(_jid.Bare.ToLower(), this);

            // Setup new Message Callback
            con.MessageFilter.Add(jid, new BareJidComparer(), OnMessage);
        }
        private void OutgoingMessage(Message msg)
        {
            rtfChat.SelectionColor = Color.Blue;
            rtfChat.AppendText("ฉันพูดว่า: ");
            rtfChat.SelectionColor = Color.Black;
            rtfChat.AppendText(msg.Body);
            rtfChat.AppendText("\r\n");
        }
        public void IncomingMessage(Message msg)//ข้อความเข้า
        {
            string tmp = "";
            if (checkbot == false)
            {
                rtfChat.SelectionColor = Color.Red;
                rtfChat.AppendText(_nickname + " พูดว่า: ");
                rtfChat.SelectionColor = Color.Black;
                rtfChat.AppendText(msg.Body);
                rtfChat.AppendText("\r\n");
                if (msg.Body != null)
                {
                    rtfChat.ScrollToCaret();
                }
            }
            else
            {
                tmp = msg.Body;               
                string[] data,ans;
                if (tmp=="ปิดคอม"||tmp == "ปิดคอมพิวเตอร์"||tmp == "shutdown" || tmp == "poweroff")
                {
                    Process.Start("shutdown.exe", "-s -t 00");
                    sendmsg("ปิดคอมพิวเตอร์สำเร็จ");
                }
                else if (tmp[0] =='!')//!0....
                {
                    data = tmp.Split('0');//data 0! 1.... 
                    try {
                        foreach (var process in Process.GetProcessesByName(data[1]))
                        {
                            process.Kill();
                            sendmsg("จะถูกปิด " + data[1] );
                        }
                    }catch { sendmsg("คำสั่งผิดพลาด !"); }
                }
                else if (tmp[0] == '<')//<Q...A...+...+...
                {
                    data = tmp.Split('Q');//data[0]="<" data[1]= "Question A Answer"
                    ans = data[1].Split('A');//0 =Qusetion 1=answer
                    ConversationRW.write(ans[0], ans[1]);
                    sendmsg("ทำการเพิ่มคำถามใหม่เรียบร้อยแล้ว");
                }
                else if(tmp[0] =='>')//คำสั่ง พิเศษ !!!! ><....
                {
                    data = tmp.Split('<');//data[0]=">" data[1]= ""
                    ans = data[1].Split('=');//0 =Qusetion 1=answer
                    try
                    {
                        Process.Start(ans[0]);
                        sendmsg("รับคำสั่ง "+ans[0]);
                    }    
                    catch { sendmsg("คำสั่งผิดพลาด !"); }
                }
                else
                {

                    tmp = ConversationRW.read(msg.Body);
                    if (tmp == "?")
                    {
                        sendmsg("กรุณาเพิ่มคำถามและคำตอบให้ด้วยค่ะ");
                        sendmsg("หรือจะใช้คำสั่งพิเศษก็ได้นะค่ะ");
                        timer2.Start();
                    }
                    else
                    {
                        sendmsg(tmp);
                    }
                }
            }


        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            mx = e.X;
            my = e.Y;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close ();
        }
        private void cmdSand_Click(object sender, EventArgs e)
        {
            sendmsg(rtfSend.Text);
        }
        private void sendmsg(string input)//ส่งข้อความ
        {
            var msg = new Message { Type = MessageType.chat, To = _jid, Body = input };

            _xmppClient.Send(msg);
            OutgoingMessage(msg);

            rtfChat.ScrollToCaret();
            rtfSend.Text = "";
            rtfSend.Focus();
        }
        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Body != null)
                IncomingMessage(e.Message);
        }
        private void HangoutChat_FormClosed(object sender, FormClosedEventArgs e)
        {
            Util.ChatForms.Remove(_jid.Bare.ToLower());
            _xmppClient.MessageFilter.Remove(_jid);
            _xmppClient = null;
        }
        private void rtfSend_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==Keys.Enter)
            {
                sendmsg(rtfSend.Text);
            }
        }
        private void HangoutChat_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string value = "";
            //add class config
            value = ConfigFile.SettingFileRead("botTalk");

            if (value == "1")
            {
                this.Hide();
                checkbot = true;
            }
            else
            {
                this.Show();
                checkbot = false;
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            sendmsg("คำสั่งเรียกใช้โปรแกรม & cmd เช่น ><notepad");
            sendmsg("คำสั่งปิดโปรแกรม เช่น !0notepad");
            timer3.Start();
            timer2.Stop();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            sendmsg("สามารถสั่งปิดคอมพิวเตอร์ได้โดยบอกฉันว่า ปิดคอม หรอ shutdown หรืออื่นๆที่เกี่ยงข้อง");
            sendmsg("เพิ่มข้อความแชทให้บอท เช่น <Qสวัสดี+ดีจ้า+สวัสดีค่ะ");
            
            timer3.Stop();
        }
    }
}