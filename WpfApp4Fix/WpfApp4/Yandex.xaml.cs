using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Win32;
using Microsoft.Toolkit.Uwp.Notifications;


namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для Yandex.xaml
    /// </summary>
    public partial class Yandex : Window
    {
        public Yandex()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Button_Turn(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SmtpClient client = new SmtpClient(Data.smtp_str, 587);
            MailMessage message = new MailMessage();
            MailAddress from = new MailAddress(Data.log);
            MailAddress to = new MailAddress(To.Text);
            MailMessage m = new MailMessage(from, to);
            m.Subject = Subject.Text;
            m.Body = Body.Text;
            m.IsBodyHtml = true;

            if (ForFile.Text != string.Empty)
            {
                m.Attachments.Add(new Attachment(Data.FileName));
                client.Credentials = new NetworkCredential(Data.log, Data.pas);
                client.EnableSsl = true;
                client.Send(m);
                client.Dispose();
                message.Dispose();
                var ntf = new ToastContentBuilder();
                ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                ntf.AddText("SmartMail");
                ntf.AddText("Message sent");
                ntf.Show();
            }
            else
            {
                client.Credentials = new NetworkCredential(Data.log, Data.pas);
                client.EnableSsl = true;
                client.Send(m);
                client.Dispose();
                message.Dispose();
                var ntf = new ToastContentBuilder();
                ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                ntf.AddText("SmartMail");
                ntf.AddText("Message sent");
                ntf.Show();
            }
        }

        private void Attach_Click(object sender, EventArgs e)
        {
            OpenFileDialog pathfile = new OpenFileDialog();
            pathfile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            pathfile.FilterIndex = 2;
            pathfile.RestoreDirectory = true;

            if (pathfile.ShowDialog() == true)
            {
                Data.FileName = pathfile.FileName;
                string text = File.ReadAllText(Data.FileName);
                ForFile.Text = "File downloaded";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ForFile.Text = "";
            Body.Text = "";
        }
    }
}
