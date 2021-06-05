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

namespace Captcha
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainForm { get; set; }
        public static UserControl PreviousPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainForm = this;
            ToolBar.MouseLeftButtonDown += new MouseButtonEventHandler(layoutRoot_MouseLeftButtonDown);
        }

        void layoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login auth = new Login();
            Hide();
            auth.Show();
        }


        public System.Timers.Timer aTimer;
        public static int TimeRemained = 500;
        public int Seconds = TimeRemained;
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void MinBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(2);
        }

        private void Registation_Click(object sender, RoutedEventArgs e)
        {
            // MainFrame.Content = new RegistrationUC();
        }
    }
}
