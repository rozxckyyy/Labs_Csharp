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
    /// Логика взаимодействия для TaskWindow15.xaml
    /// </summary>
    public partial class TaskWindow15 : Window
    {
        public TaskWindow15()
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
            string res1;
            res1 = read_n.Text;
            try
            {
                int a = Convert.ToInt32(res1);
                res1 = res1.Replace("0", "");
                res1 = res1.Replace("5", "");
                res.Text = res1;
            }
            catch
            {
                MessageBox.Show(
                    "ERROR",
                    "Сообщение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
        }
    }
}
