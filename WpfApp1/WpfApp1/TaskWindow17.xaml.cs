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
    /// Логика взаимодействия для TaskWindow17.xaml
    /// </summary>
    public partial class TaskWindow17 : Window
    {
        public TaskWindow17()
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

            char[] numbers = res1.ToCharArray();
            char[] new_numbers;

            new_numbers = numbers;

            int len = numbers.Length;

            char last = numbers[len - 1];

            new_numbers[len-1] = numbers[0];
            new_numbers[0] = last;

            res.Text = String.Join("", new_numbers);
        }
    }
}