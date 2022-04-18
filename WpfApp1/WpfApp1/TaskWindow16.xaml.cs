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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow16.xaml
    /// </summary>
    public partial class TaskWindow16 : Window
    {
        public TaskWindow16()
        {
            InitializeComponent();
        }
        private void res_hint_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (res.Text.Length == 0)
            {
                res_hint.Visibility = Visibility.Visible;
            }
            else
            {
                res_hint.Visibility = Visibility.Hidden;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<int> govno = new List<int>();
            for (int i = 100; i <= 999; i++)
            {
                int a = i % 10;
                int b = (i / 10) % 10;
                int c = (i / 100) % 10;
                int temp = (int)(Math.Pow(a, 3) + Math.Pow(b, 3) + Math.Pow(c, 3));
                if (i == temp)
                {
                    govno.Add(i);
                }
            }
            string v = string.Join(" ",govno);
            res.Text = v;
        }
    }
}
