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
    /// Логика взаимодействия для TaskWindow1.xaml
    /// </summary>
    public partial class TaskWindow1 : Window
    {
        public TaskWindow1()
        {
            InitializeComponent();
        }

        private void n_hint_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (read_n.Text.Length == 0)
            {
                n_hint.Visibility = Visibility.Visible;
            }
            else
            {
                n_hint.Visibility = Visibility.Hidden;
            }
        }

        private void x_hint_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (read_x.Text.Length == 0)
            {
                x_hint.Visibility = Visibility.Visible;
            }
            else
            {
                x_hint.Visibility = Visibility.Hidden;
            }
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
            uint n;
            double x;
            double result = 0;
            while (!uint.TryParse(read_n.Text, out n))
            {
                MessageBox.Show(
                    "ERROR",
                    "Сообщение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            while (!double.TryParse(read_x.Text, out x))
            {
                MessageBox.Show(
                    "ERROR",
                    "Сообщение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            for (int i = 1; i <= n; i++)
                result += Math.Pow(x, i) / Fact(i);

            res.Text = result.ToString();
        }
        private int Fact(int n)
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }
    }
}
