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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool SoundMode = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            GAME_PAGE.MainControl = e.Key.ToString();
        }

        private void onKeyUP(object sender, KeyEventArgs e)
        {
            GAME_PAGE.MainControl = null;
        }
    }
}
