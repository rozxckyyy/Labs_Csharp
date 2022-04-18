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
using System.Net.Mail;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Microsoft.Toolkit.Uwp.Notifications;

namespace WpfApp4
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
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {

            IWebDriver driver = new ChromeDriver();

            Data.str = login.Text.Split('@');
            Data.smtp_str = "";
            switch (Data.str[1])
            {
                case "yandex.ru":
                    Data.smtp_str = "smtp.yandex.ru";

                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://passport.yandex.ru/");

                    try
                    {
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/div[1]/form/div[1]/span/input")).SendKeys(login.Text);
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/div[1]/form/div[3]/button")).Click();
                        Thread.Sleep(1500);
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/form/div[2]/span/input")).SendKeys(pass.Password);
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/div/div/div[2]/div[3]/div/div/div/form/div[3]/button")).Click();
                        Thread.Sleep(3000);
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/div[2]/nav/ul/li[1]/a"));
                        WindowState = WindowState.Minimized;
                        driver.Dispose();
                        Yandex yandex = new Yandex();
                        yandex.Show();
                        Data.pas = pass.Password;
                        Data.log = login.Text;
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Successful authorization");
                        ntf.Show();
                    }
                    catch
                    {
                        driver.Dispose();
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Wrong login or password");
                        ntf.Show();
                    }

                    break;
                case "gmail.com":
                    Data.smtp_str = "smtp.gmail.com";

                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://accounts.google.com/");

                    try
                    {
                        driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/div/div[1]/div/div[1]/input")).SendKeys(login.Text);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[1]/div/div/button")).Click();
                        Thread.Sleep(3500);
                        driver.FindElement(By.CssSelector(".whsOnd.zHQkBf")).SendKeys(pass.Password);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[1]/div/div/button")).Click();
                        Thread.Sleep(3500);
                        driver.FindElement(By.XPath("/html/body/div[2]/header/div[2]/div[2]/div[2]/form/div/div/div/div/div/div[1]/input[2]"));
                        Thread.Sleep(3000);
                        WindowState = WindowState.Minimized;
                        driver.Dispose();
                        Gmail gmail = new Gmail();
                        gmail.Show();
                        Data.pas = pass.Password;
                        Data.log = login.Text;
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Successful authorization");
                        ntf.Show();
                    }
                    catch
                    {
                        driver.Dispose();
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Wrong login or password");
                        ntf.Show();
                    }
                    break;
                case "mail.ru":
                    Data.smtp_str = "smtp.mail.ru";

                    driver.Manage().Window.Maximize();
                    driver.Navigate().GoToUrl("https://account.mail.ru/");

                    try
                    {
                        Thread.Sleep(3000);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div/form/div[2]/div/div[1]/div/div/div/div/div/div[1]/div/input")).SendKeys(login.Text); ;
                        driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div/form/div[2]/div/div[3]/div/div/div[1]/button")).Click();
                        Thread.Sleep(1500);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div/form/div[2]/div/div[2]/div/div/div/div/div/input")).SendKeys(pass.Password);
                        driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[2]/div/div/div/div/form/div[2]/div/div[3]/div/div/div[1]/div/button")).Click();
                        Thread.Sleep(7000);
                        WindowState = WindowState.Minimized;
                        driver.Dispose();
                        Mail mail = new Mail();
                        mail.Show();
                        Data.pas = pass.Password;
                        Data.log = login.Text;
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Successful authorization");
                        ntf.Show();
                    }
                    catch
                    {
                        driver.Dispose();
                        var ntf = new ToastContentBuilder();
                        ntf.AddAppLogoOverride(new Uri(@"C:\Users\gumbe\Desktop\WpfApp4\WpfApp4\source\logo.png"));
                        ntf.AddText("SmartMail");
                        ntf.AddText("Wrong login or password");
                        ntf.Show();
                    }
                    break;
                default:
                    break;
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
