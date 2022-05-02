using System;
using System.Windows;
using System.Windows.Threading;

namespace ServerApp
{
    public partial class LoadingScreen : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public LoadingScreen()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private int x;
        private void Timer_Tick(object sender, EventArgs e)
        {
            x++;
            if (x == 5)
            {
                timer.Stop();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Hide();
            }
        }
    }
}
