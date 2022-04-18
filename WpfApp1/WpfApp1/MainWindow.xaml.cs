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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
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

        private void Quest_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            switch (Quest.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Number 1":
                    TaskWindow1 taskWindow1 = new TaskWindow1();
                    taskWindow1.Show();
                    break;
                case "Number 15":
                    TaskWindow15 taskWindow15 = new TaskWindow15();
                    taskWindow15.Show();
                    break;
                case "Number 16":
                    TaskWindow16 taskWindow16 = new TaskWindow16();
                    taskWindow16.Show();
                    break;
                case "Number 17":
                    TaskWindow17 taskWindow17 = new TaskWindow17();
                    taskWindow17.Show();
                    break;
                case "Number 18":
                    TaskWindow18 taskWindow18 = new TaskWindow18();
                    taskWindow18.Show();
                    break;
                case "Number 19":
                    TaskWindow19 taskWindow19 = new TaskWindow19();
                    taskWindow19.Show();
                    break;
                     
            }
        }
    }
}
