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
using System.Windows.Threading;

namespace UY_Tetris
{
    /// <summary>
    /// GAME_PAGE.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GAME_PAGE : Page
    {
        private double[] AI = { 0, -0.4865, 0.7735, -0.9865, -0.1865 };//상수 score, 총높이, 만들어 지는 선, 구멍의 개수, 울퉁불퉁 정도
        private double[] AI2 = new double[5];
        public double[,] AI_Good = new double[5, 5];//결과가 좋은 순서대로 행렬에 집어넣음.
        public double GameSpeed = 500;
        public static int Mode = 1;//게임 모드
        public static bool Guide = true;
        public DispatcherTimer timer = new DispatcherTimer();  // 타이머 객체생성
        public DispatcherTimer AItimer = new DispatcherTimer();  // 타이머 객체생성
        public DispatcherTimer gamespeedtimer = new DispatcherTimer();  // 게임속도타이머 객체생성
        public Tetris Tetris = new Tetris(10, 22);            // 테트리스 객체 생성.
        public Tetris Tetris2 = new Tetris(10, 22);            // 테트리스 객체2 생성.

        public static string MainControl = null; // 컨트롤.
        public string OldControl = null; //계속 눌림 방지.
        private int score;
        private int count;
        private int gamecount = 1;
        private bool EyeMode = true;

        public GAME_PAGE()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 500);    // 화면표시 시간간격 = 1000 = 1초
            timer.Tick += new EventHandler(timer_Tick);          // 이벤트 추가 
            timer.Start();

            gamespeedtimer.Interval = TimeSpan.FromMilliseconds(GameSpeed);    // 내리기 시간간격 = 0.5초에 1번
            gamespeedtimer.Tick += new EventHandler(gametimer_Tick);          // 이벤트 추가
            gamespeedtimer.Start();

            AItimer.Interval = TimeSpan.FromMilliseconds(2000);    // 화면표시 시간간격 = 1000 = 1초
            AItimer.Tick += new EventHandler(AItimer_Tick);          // 이벤트 추가
            AItimer.Start();

            Tetris.NewBlock();
            if (Mode != 1)
                Tetris2.NewBlock();

            if (Mode == 5)
            {
                AI_Good[0, 0] = 1;

                for (int i = 1; i < 5; i++)//처음값은 AI_GOOD 0행에 넣어줌.
                    AI_Good[0, i] = AI[i];

                while (true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Random rnd = new Random();//랜덤으로 +- 0.2사이 값 넣어줌.
                        AI2[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;//가장 좋은것에서 랜덤.
                    }
                    if (AI2[2] <= 1 && AI2[2] >= 0 && AI2[1] >= -1 && AI2[1] <= 0 && AI2[3] >= -1 && AI2[3] <= 0 && AI2[4] >= -1 && AI2[4] <= 0)//2번째 상수=양수.. 이외 모두 음수로. 0~1 사이.
                        break;
                }

                Gamecount.Text = gamecount.ToString();
                _1p_a.Text = AI[1].ToString("F4");
                _1p_b.Text = AI[2].ToString("F4");
                _1p_c.Text = AI[3].ToString("F4");
                _1p_d.Text = AI[4].ToString("F4");
                _2p_a.Text = AI2[1].ToString("F4");
                _2p_b.Text = AI2[2].ToString("F4");
                _2p_c.Text = AI2[3].ToString("F4");
                _2p_d.Text = AI2[4].ToString("F4");
                one0.Text = AI_Good[0, 0].ToString();
                one1.Text = AI_Good[0, 1].ToString("F4");
                one2.Text = AI_Good[0, 2].ToString("F4");
                one3.Text = AI_Good[0, 3].ToString("F4");
                one4.Text = AI_Good[0, 4].ToString("F4");
            }
            level1.Text = Tetris.Level.ToString();
            level2.Text = Tetris2.Level.ToString();
        }

        private void timer_Tick(object sender, EventArgs e)//0.001초에 1번
        {
            if (OldControl != MainControl)
            {
                OldControl = MainControl;
                switch (MainControl)
                {
                    case "Up":
                        Tetris.Rotation();
                        break;
                    case "Down":
                        Tetris.MoveDown();
                        break;
                    case "Left":
                        Tetris.MoveLeft();
                        break;
                    case "Right":
                        Tetris.MoveRight();
                        break;
                    case "M":
                        Tetris.MoveGround();
                        break;
                    case "H":
                        Tetris.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
                        break;
                    case "W":
                        Tetris2.Rotation();
                        break;
                    case "S":
                        Tetris2.MoveDown();
                        break;
                    case "A":
                        Tetris2.MoveLeft();
                        break;
                    case "D":
                        Tetris2.MoveRight();
                        break;
                    case "G":
                        Tetris2.MoveGround();
                        break;
                }
            }

            score++;

            if (score / 1000 == 1 && Mode != 5)
            {
                score = Tetris.Score;
                level1.Text = Tetris.Level++.ToString();
                level2.Text = Tetris2.Level++.ToString();
                GameSpeed = GameSpeed / 10 * 9;
                gamespeedtimer.Interval = TimeSpan.FromMilliseconds(GameSpeed);
                score = 0;
            }

            if (count >= 15)//주기적으로 캔버스 청소(메모리 누수 막기 위함)
            {
                canvas.Children.Clear();
                Tetris.CanvasClear = true;
            }

            if (EyeMode == true)//화면 끄기.
            {
                if (Mode != 1)//플레이어2가 활성화 됬을때만 화면에 그리기.
                    Tetris2.DrawBoard((GAME_PAGE)this, true, Guide);
                Tetris.DrawBoard((GAME_PAGE)this, false, Guide);
            }

            if (count >= 15)//주기적으로 캔버스 청소(메모리 누수 막기 위함.)
            {
                count = 0;
                Tetris.CanvasClear = false;
            }



            if (Mode == 5)//AI 발달모드
            {
                Tetris.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
                Tetris2.AIVirtualMove(AI2[1], AI2[2], AI2[3], AI2[4]);
            }

            //게임오버시
            if (Tetris.IsGameOver() || Tetris2.IsGameOver())
            {
                if (Mode == 1)
                {
                    MessageBox.Show("게임 오버");
                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if(Mode == 2)
                {
                    if (Tetris.IsGameOver())//메인이 죽으면
                    {
                        Tetris = new Tetris(10, 22);
                        Tetris.NewBlock();
                    }
                    else if (Tetris2.IsGameOver())//서브가 죽으면
                    {
                        Tetris2 = new Tetris(10, 22);
                        Tetris2.NewBlock();
                    }
                }
                else if(Mode == 3)
                {
                    if (Tetris.IsGameOver())//메인이 죽으면
                        MessageBox.Show("2P Win!");
                    else
                        MessageBox.Show("1P Win!");

                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if (Mode == 4)
                {
                    if (Tetris.IsGameOver())//메인이 죽으면
                        MessageBox.Show("You Lose..");
                    else
                        MessageBox.Show("1P Win!");

                    timer.Stop();
                    gamespeedtimer.Stop();
                }
                else if (Mode == 5)//AI 발달모드
                {
                    if (Tetris.IsGameOver())//메인이 죽으면
                    {
                        AI[0] = Tetris.Score;
                        for (int j = 4; j >= 0; j--)//5개 행에 결과 저장.
                        {
                            if (AI_Good[j, 0] <= Tetris.Score)
                            {
                                for (int i = 0; i < 5; i++)//score,a,b,c,d 값
                                {
                                    if (j + 1 < 5)
                                        AI_Good[j + 1, i] = AI_Good[j, i];
                                    AI_Good[j, i] = AI[i];
                                }
                            }
                        }
                        while (true)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Random rnd = new Random();//랜덤으로 +- 0.2사이 값 넣어줌.
                                AI[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;//가장 좋은것에서 랜덤.
                            }
                            if (AI[2] <= 1 && AI[2] >= 0 && AI[1] >= -1 && AI[1] <= 0 && AI[3] >= -1 && AI[3] <= 0 && AI[4] >= -1 && AI[4] <= 0)//2번째 상수=양수.. 이외 모두 음수로. 0~1 사이.
                                break;
                        }
                        Tetris = new Tetris(10, 22);
                        Tetris.NewBlock();
                    }
                    else if (Tetris2.IsGameOver())//서브가 죽으면
                    {
                        AI2[0] = Tetris2.Score;
                        for (int j = 4; j >= 0; j--)//5개 행에 결과 저장.
                        {
                            if (AI_Good[j, 0] <= Tetris2.Score)
                            {
                                for (int i = 0; i < 5; i++)//score,a,b,c,d 값
                                {
                                    if (j + 1 < 5)
                                        AI_Good[j + 1, i] = AI_Good[j, i];
                                    AI_Good[j, i] = AI2[i];
                                }
                            }
                        }
                        while (true)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Random rnd = new Random();//랜덤으로 +- 0.2사이 값 넣어줌.
                                AI2[i] = AI_Good[0, i] + rnd.NextDouble() / 5 * 2 - 0.2;//가장 좋은것에서 랜덤.
                            }
                            if (AI2[2] <= 1 && AI2[2] >= 0 && AI2[1] >= -1 && AI2[1] <= 0 && AI2[3] >= -1 && AI2[3] <= 0 && AI2[4] >= -1 && AI2[4] <= 0)//2번째 상수=양수.. 이외 모두 음수로. 0~1 사이.
                                break;
                        }
                        Tetris2 = new Tetris(10, 22);
                        Tetris2.NewBlock();
                    }

                    //timer.Start();
                    //gamespeedtimer.Start();

                    gamecount++;//횟수증가
                    Gamecount.Text = gamecount.ToString();
                    _1p_a.Text = AI[1].ToString("F4");
                    _1p_b.Text = AI[2].ToString("F4");
                    _1p_c.Text = AI[3].ToString("F4");
                    _1p_d.Text = AI[4].ToString("F4");
                    _2p_a.Text = AI2[1].ToString("F4");
                    _2p_b.Text = AI2[2].ToString("F4");
                    _2p_c.Text = AI2[3].ToString("F4");
                    _2p_d.Text = AI2[4].ToString("F4");
                    one0.Text = AI_Good[0, 0].ToString();
                    one1.Text = AI_Good[0, 1].ToString("F4");
                    one2.Text = AI_Good[0, 2].ToString("F4");
                    one3.Text = AI_Good[0, 3].ToString("F4");
                    one4.Text = AI_Good[0, 4].ToString("F4");
                    two0.Text = AI_Good[1, 0].ToString();
                    two1.Text = AI_Good[1, 1].ToString("F4");
                    two2.Text = AI_Good[1, 2].ToString("F4");
                    two3.Text = AI_Good[1, 3].ToString("F4");
                    two4.Text = AI_Good[1, 4].ToString("F4");
                    three0.Text = AI_Good[2, 0].ToString();
                    three1.Text = AI_Good[2, 1].ToString("F4");
                    three2.Text = AI_Good[2, 2].ToString("F4");
                    three3.Text = AI_Good[2, 3].ToString("F4");
                    three4.Text = AI_Good[2, 4].ToString("F4");
                    four0.Text = AI_Good[3, 0].ToString();
                    four1.Text = AI_Good[3, 1].ToString("F4");
                    four2.Text = AI_Good[3, 2].ToString("F4");
                    four3.Text = AI_Good[3, 3].ToString("F4");
                    four4.Text = AI_Good[3, 4].ToString("F4");
                    five0.Text = AI_Good[4, 0].ToString();
                    five1.Text = AI_Good[4, 1].ToString("F4");
                    five2.Text = AI_Good[4, 2].ToString("F4");
                    five3.Text = AI_Good[4, 3].ToString("F4");
                    five4.Text = AI_Good[4, 4].ToString("F4");
                }
            }
            Score.Text = Tetris.Score.ToString();
            Score2.Text = Tetris2.Score.ToString();
        }

        private void gametimer_Tick(object sender, EventArgs e)//게임속도
        {
            Tetris.MoveDown();
            if (Mode != 1)
                Tetris2.MoveDown();

            count++;//캔버스 청소를 위한 카운트
        }

        private void AItimer_Tick(object sender, EventArgs e)//AI 속도
        {
            if (Mode == 4)//AI vs모드
            {
                Tetris2.AIVirtualMove(AI[1], AI[2], AI[3], AI[4]);
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            Application.Current.MainWindow.Width = 400;
            Application.Current.MainWindow.Height = 500;
            timer.Stop();
            gamespeedtimer.Stop();
            Keyboard.ClearFocus();
        }

        private void Sound_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.SoundMode == true)
            {
                MainWindow.SoundMode = false;
                Sound_source.Source = new BitmapImage(new Uri(@"\Source\mute_button.png", UriKind.Relative));
            }
            else
            {
                MainWindow.SoundMode = true;
                Sound_source.Source = new BitmapImage(new Uri(@"\Source\sound_button.png", UriKind.Relative));
            }
            Keyboard.ClearFocus();
        }

        private void Eye_Click(object sender, RoutedEventArgs e)
        {
            if (EyeMode == true)
            {
                EyeMode = false;
                Eye_Image.Source = new BitmapImage(new Uri(@"\Source\CloseEye.png", UriKind.Relative));
                count = 20;
                GameSpeed = 1;
                timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 500);
            }
            else
            {
                EyeMode = true;
                Eye_Image.Source = new BitmapImage(new Uri(@"\Source\OpenEye.png", UriKind.Relative));
                count = 20;
                GameSpeed = 500;
                timer.Interval = TimeSpan.FromMilliseconds(GameSpeed / 30);
            }
        }

        private void Guide_Click(object sender, RoutedEventArgs e)
        {
            if (Guide == true)
            {
                count = 20;
                Guide = false;
            }
            else
                Guide = true;
        }
    }
}