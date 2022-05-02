using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using SuperSimpleTcp;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Collections.Generic;

namespace ServerApp
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
            IP_Server.IsReadOnly = true;
            Port_Server.IsReadOnly = true;
        }
        public void Unblock()
        {
            IP_Server.IsReadOnly = false;
            Port_Server.IsReadOnly = false;
        }

        SimpleTcpServer Server;

        public bool ServerIsWork = false;

        public string FullAdress { get; set; }

        public void Button_Start_Server(object sender, RoutedEventArgs e)
        {
            string[] ArrayAdress = {IP_Server.Text, ":", Port_Server.Text };
            string regexIP = @"^((1\d\d|2([0-4]\d|5[0-5])|\d\d?)\.?){4}$";
            string regexPort = @"^([0-9]{1,4})";

            if ((IP_Server.Text.Length != 0 & Port_Server.Text.Length != 0) && (IP_Server.Text.Length <= 16 & Port_Server.Text.Length <= 4))
            {
                if (Regex.IsMatch(IP_Server.Text, regexIP) is true & Regex.IsMatch(Port_Server.Text, regexPort) is true)
                {
                    try
                    {
                        FullAdress = string.Concat(ArrayAdress);
                    }
                    finally
                    {
                        Server = new SimpleTcpServer(FullAdress);

                        Messages.Text += $"{Environment.NewLine}$Server started"; //delete this and make 
                        Messages.Text = string.Empty;

                        Load();
                        Server.Start();

                        Stop_Server.IsEnabled = true;
                        Start_Server.IsEnabled = false;
                        ServerIsWork = Server.IsListening;

                        Block();
                    }
                }
                else Messages.Text += $"{Environment.NewLine}$Enter correct IP and Port";
            }
            else Messages.Text += $"{Environment.NewLine}$Enter correct IP and Port";
        }
        public void Button_Stop_Server(object sender, RoutedEventArgs e)
        {
            foreach (string item in lstIp.Items)
            {
                Server.Send(item, "$Server stopped");
            }
            lstIp.Items.Clear();
            Messages.Text = string.Empty;
            ServerIsWork = false;
            Start_Server.IsEnabled = true;
            Stop_Server.IsEnabled = false;
            Unblock();
            Server.Stop();
        }
        private void Button_Send_Enter(object sender, KeyEventArgs e)
        {
            if (Server.IsListening)
            {
                if (!string.IsNullOrEmpty(Send_Message.Text) && !lstIp.Items.IsEmpty)
                {
                    if (e.Key == Key.Enter)
                    {
                        try 
                        {
                            foreach (string item in lstIp.Items)
                            {
                                Server.Send(item,$"[{FullAdress}]: {Send_Message.Text}");
                            }
                        }
                        finally 
                        {
                            My_Messages.Text += $"{Environment.NewLine}{Send_Message.Text}";
                            Send_Message.Text = string.Empty;
                            e.Handled = true;
                        }
                    }
                }
            }
        }
        private void Load()
        {
            Server.Events.ClientConnected += Events_ClientConnected;
            Server.Events.ClientDisconnected += Events_ClientDisconnected;
            Server.Events.DataReceived += Events_DataReceived;
        }
        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (ServerIsWork)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Messages.Text += $"{Environment.NewLine}[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}";
                }));
                string tempIp = e.IpPort;
                foreach (string item in lstIp.Items)
                {
                    if (item != e.IpPort)
                    {
                        Server.Send(item, $"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
                    }
                }
            }
        }
        private void Events_ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            if (ServerIsWork)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Messages.Text += $"{Environment.NewLine}[{e.IpPort}] disconnected";
                    lstIp.Items.Remove(e.IpPort);
                }));
            }
        }
        private void Events_ClientConnected(object sender, ConnectionEventArgs e)
        {
            if (ServerIsWork)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Messages.Text += $"{Environment.NewLine}[{e.IpPort}] connected";
                    lstIp.Items.Add(e.IpPort);
                }));
            }
        }
    }
}