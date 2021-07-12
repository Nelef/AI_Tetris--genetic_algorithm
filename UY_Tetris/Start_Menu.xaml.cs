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

namespace UY_Tetris
{
    /// <summary>
    /// Start_Menu.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Start_Menu : Page
    {
        public Start_Menu()
        {
            InitializeComponent();
        }

        private void Single_Play_Click(object sender, RoutedEventArgs e)//1P
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GAME_PAGE.Mode = 1;
        }

        private void _2P_Click(object sender, RoutedEventArgs e)//2P
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GAME_PAGE.Mode = 2;
            Application.Current.MainWindow.Width = 720;
        }
        
        private void vs2P_Click(object sender, RoutedEventArgs e)//vs2P
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GAME_PAGE.Mode = 3;
            Application.Current.MainWindow.Width = 720;
        }

        private void vsAI_Click(object sender, RoutedEventArgs e)//vsAI
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GAME_PAGE.Mode = 4;
            Application.Current.MainWindow.Width = 720;
        }

        private void AIGood_Click(object sender, RoutedEventArgs e)//AI발달
        {
            NavigationService.Navigate(new Uri("/GAME_PAGE.xaml", UriKind.Relative));
            GAME_PAGE.Mode = 5;
            Application.Current.MainWindow.Width = 720;
            Application.Current.MainWindow.Height = 600;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}