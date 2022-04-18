using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-83PQJ9Q\\MSQLEXPRESS2;Trusted_Connection=Yes;DataBase=cargo_base;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        string z, x, c;
        int v;
        public struct cfg
        {
            public string name { get; set; }
            public string date { get; set; }
            public string number { get; set; }
            public int price { get; set; }
        }
        void add(string z, string x, string c, int v, List<cfg> info)
        {
            cfg data = new cfg();
            data.name = z;
            data.date = x;
            data.number = c;
            data.price = v;
            info.Add(data);
        }
        private void date_hint_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (set_date.Text.Length == 0)
            {
                date_hint.Visibility = Visibility.Visible;
            }
            else
            {
                date_hint.Visibility = Visibility.Hidden;
            }
        }

        private void calc_date_hint_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (calc_date.Text.Length == 0)
            {
                calc_hint.Visibility = Visibility.Visible;
            }
            else
            {
                calc_hint.Visibility = Visibility.Hidden;
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = Select("SELECT * FROM [dbo].[cargo_table]");
            List<cfg> info = new List<cfg>();

            string date = set_date.Text;
            char[] chars = date.ToCharArray();

            for (int i = 0; i < dt_user.Rows.Count; i++) // 01.01.2022 [6-9]
            {
                z = Convert.ToString(dt_user.Rows[i][0]);
                x = Convert.ToString(dt_user.Rows[i][1]);
                c = Convert.ToString(dt_user.Rows[i][2]);
                v = Convert.ToInt32(dt_user.Rows[i][3]);
                StringBuilder ch = new StringBuilder(x);

                if (chars.Length < 4)
                {
                    MessageBox.Show(
                   "Введите дату",
                   "Сообщение",
                   MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    return;
                }
                else if ((ch[6] == chars[0] & ch[7] == chars[1] & ch[8] == chars[2] & ch[9] == chars[3]) && ((ch[3] == '0' & ch[4] == '1') | (ch[3] == '0' & ch[4] == '2') | (ch[3] == '0' & ch[4] == '3')))
                {
                    add(z, x, c, v, info);
                }
/*                else if (ch[6] != chars[0] | ch[7] != chars[1] | ch[8] != chars[2] | ch[9] != chars[3])
                {
                    MessageBox.Show(
                    "Груз с такой датой не найден",
                    "Сообщение",
                    MessageBoxButton.OK,dasdasdas
                    MessageBoxImage.Error);
                    return;
                }*/
            }
            grid.ItemsSource = info;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = Select("SELECT * FROM [dbo].[cargo_table]");
            List<cfg> info = new List<cfg>();

            string date = set_date.Text;
            char[] chars = date.ToCharArray();
            int count = 0;
            int cnt = 0;

            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                v = Convert.ToInt32(dt_user.Rows[i][3]);
                cnt++;
                count += v;
            }
            int result = count / cnt;
            calc_date.Text = result.ToString();
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {

            DataTable dt_user = Select("SELECT * FROM [dbo].[cargo_table]");

            grid.ItemsSource = dt_user.DefaultView;

            grid.Items.SortDescriptions.Add(new SortDescription("cargo_date", ListSortDirection.Descending));
        }
    }
}
