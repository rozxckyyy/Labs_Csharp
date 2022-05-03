using SuperSimpleTcp;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        public void Block()
        {
            IP_Client.IsReadOnly = true;
            Port_Client.IsReadOnly = true;
        }
        public void Unblock()
        {
            IP_Client.IsReadOnly = false;
            Port_Client.IsReadOnly = false;
        }

        SimpleTcpClient Client;

        public bool ServerIsConnect = false;
        public bool DeleteRequest;
        public string FullAdress { get; set; }

        string time = DateTime.Now.ToShortTimeString();
        public void Button_Connect_Server(object sender, RoutedEventArgs e)
        {
            string[] ArrayAdress = { IP_Client.Text, ":", Port_Client.Text };
            string regexIP = @"^((1\d\d|2([0-4]\d|5[0-5])|\d\d?)\.?){4}$";
            string regexPort = @"^([0-9]{1,4})";

            if ((IP_Client.Text.Length != 0 & Port_Client.Text.Length != 0) && (IP_Client.Text.Length <= 16 & Port_Client.Text.Length <= 4))
            {
                if (Regex.IsMatch(IP_Client.Text, regexIP) is true & Regex.IsMatch(Port_Client.Text, regexPort) is true)
                {
                    try
                    {
                        FullAdress = string.Concat(ArrayAdress);
                        Client = new SimpleTcpClient(FullAdress);

                        Client.Connect();

                        ServerIsConnect = Client.IsConnected;
                    }
                    catch { ServerIsConnect = false; }
                    finally
                    {
                        Load();
                        ServerIsConnect = true;
                        Connect_Server.IsEnabled = false;
                        Disconnect_Server.IsEnabled = true;

                        Messages.Items.Clear();

                        Block();
                    }
                }
                else Messages.Items.Add($"{time}{Environment.NewLine}$Enter correct IP and Port");
            }
            else Messages.Items.Add($"{time}{Environment.NewLine}$Enter correct IP and Port");
        }
        public void Button_Disconnect_Server(object sender, RoutedEventArgs e)
        {
            Messages.Items.Clear();
            ServerIsConnect = false;
            Connect_Server.IsEnabled = true;
            Disconnect_Server.IsEnabled = false;
            Unblock();
            Client.Disconnect();
            
        }
        private void Button_Send_Enter(object sender, KeyEventArgs e)
        {
            if (Client.IsConnected)
            {
                if (!string.IsNullOrEmpty(Send_Message.Text))
                {
                    if (e.Key == Key.Enter)
                    {
                        Client.Send(Send_Message.Text);
                        Messages.Items.Add($"{time}{Environment.NewLine}{Send_Message.Text}");
                        UpdateScrollBar(Messages);
                        Send_Message.Text = string.Empty;
                        e.Handled = true;
                    }
                }
            }
        }
        private void Load()
        {
            Client.Events.Connected += Events_Connected;
            Client.Events.Disconnected += Events_Disconnected;
            Client.Events.DataReceived += Events_DataReceived;
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (ServerIsConnect & e.IpPort == FullAdress)
            {
                try
                {
                    $"{Encoding.UTF8.GetString(e.Data)}".Contains($"[{FullAdress}]");
                }
                finally
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        Messages.Items.Add($"{time}{Environment.NewLine}[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
                    }));
                }
            }
        }

        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            if (ServerIsConnect)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Messages.Items.Add($"{time}{Environment.NewLine}[{e.IpPort}] disconnected");
                }));
            }
        }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
            if (ServerIsConnect)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Messages.Items.Add($"{time}{Environment.NewLine}[{e.IpPort}] connected");
                }));
            }
        }
        private void UpdateScrollBar(ListBox listBox)
        {
            if (listBox != null)
            {
                var border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }
    }
}