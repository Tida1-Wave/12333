using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Captcha
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {


        public DataBase db;
        private Captcha captcha;
        public Login()
        {
            InitializeComponent();
            captcha = new Captcha();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += timer_Tick;
            aTimer.AutoReset = true;
            db = new DataBase();
            db.Connection_Open();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainForm.MainFrame.Content = MainWindow.PreviousPage;
        }


        public int attemptsCount = 0;
        public bool IsCaptchaActive = false;
        public bool AccessToEnter = false;


        public int seconds = 10;
        public DispatcherTimer timer = new DispatcherTimer();
        private static System.Timers.Timer aTimer;

        string exception;
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            String Login = LoginTB.Text;
            String Password = PasswordTB.Password;

            AccessToEnter = db.IsAccessAllowed(Login, Password) ? true : false;
            exception = AccessToEnter ? "" : "Неверный логин или пароль";

            if (!AccessToEnter)
            {
                attemptsCount++;
                if (attemptsCount >= 1)
                {
                    IsCaptchaActive = true;
                }
            }

            if (IsCaptchaActive)
            {
                CaptchaTB.Visibility = Visibility.Visible;
                LoginBtn.Margin = new Thickness(0, 350, 0, 0);

                if (CaptchaTB.Text != captcha.CaptchaText && attemptsCount > 1)
                {
                    exception = "Неверный ввод капчи";
                    LoginBtn.IsEnabled = false;
                    seconds = 10;
                    aTimer.Start();
                }
                if (CaptchaTB.Text == captcha.CaptchaText)
                {
                    IsCaptchaActive = false;
                }
                ErrorMsgLbl.Content = exception;
                CaptchaImage.Source = HelperClass.ToBitmapImage(captcha.CreateImage(400, 200));
            }
            if (AccessToEnter && !IsCaptchaActive)
            {
                HelperClass.CurrentUserId = db.GetClientId(Login);
                //HelperClass.CurrentUserLogin = db.GetLogin(HelperClass.CurrentUserId);
                HelperClass.CurrentUserRole = Login.Contains("admin") ? "admin" : "user";
                MainWindow mainWindow = new MainWindow();
                mainWindow.TimerLbl.Visibility = Visibility.Visible;
                mainWindow.MainFrame.Visibility = Visibility.Visible;
                mainWindow.MainFrame.Content = new Window0();
                this.Close();
                mainWindow.Show();
            }


        }

        private void timer_Tick(object sender, EventArgs e)
        {
            seconds--;
            Dispatcher.Invoke(() => TimeRespondLbl.Content = "Подождите " + seconds + " секунд...");

            if (seconds == 1)
            {
                Dispatcher.Invoke(() => TimeRespondLbl.Content = "Вперед...");
            }
            if (seconds <= 0)
            {
                Dispatcher.Invoke(() => LoginBtn.IsEnabled = true);
                Dispatcher.Invoke(() => TimeRespondLbl.Content = "");
                timer.Stop();
            }

        }

        private void ShowPassCB_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPassCB.IsChecked == true)
            {
                HiddenPasswordTB.Visibility = Visibility.Visible;
                HiddenPasswordTB.Text = PasswordTB.Password;
            }
            if (ShowPassCB.IsChecked == false)
            {
                PasswordTB.Password = HiddenPasswordTB.Text;
                HiddenPasswordTB.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (PasswordTB.Password.Length > 0)
            {
                passwordchecker.Visibility = Visibility.Hidden;
            }
            else passwordchecker.Visibility = Visibility.Visible;
        }

        private void PasswordTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordchecker.Visibility = PasswordTB.Password != "" ? Visibility.Hidden : Visibility.Visible;
        }

        private void HiddenPasswordTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordTB.Password = HiddenPasswordTB.Text;
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
        }

        private void CaptchaTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}