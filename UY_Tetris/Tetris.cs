using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UY_Tetris
{
    public class Tetris
    {
        private Random random = new Random((int)DateTime.Now.Ticks);

        private int Width;
        private int Height;
        private int[,] Grid;//배열
        private int[,] OldGrid;
        private int[,] GuideGrid;
        private int[,] OldGuideGrid;

        private int[] MissBlock;//10번 연속 안나오는 블록 없도록

        private int CurrentBlock;//현재블록
        private int NextBlock = -1;  //다음블록
        private int CurrentDirection = 0;//현재 방향
        private int CurrentX = 4;//테트리스 x위치
        private int CurrentY = 0;//테트리스 y위치
        public int Score = 0;//점수표
        public int Level = 1;
        public static bool CanvasClear = false;//바탕청소할 시간?

        //테트리스 객체 초기화
        public Tetris(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            MissBlock = new int[7];//잊어버린블럭
            Grid = new int[Width, Height];
            OldGrid = new int[Width, Height];
            GuideGrid = new int[Width, Height];
            OldGuideGrid = new int[Width, Height];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    OldGrid[i, j] = 1;//전 배열과 비교하기 위해 1로 채움.

        }

        //새로운 블록 만들기
        public void NewBlock()
        {
            CurrentX = 3;
            CurrentY = 1;
            CurrentDirection = 3;

            CurrentBlock = NextBlock;//현재 블록 만들기.

            if (NextBlock == -1)//만약 처음이라면
                CurrentBlock = (int)random.Next(0, 7);//랜덤값으로 변경.

            if (ForgetBlock() != -1)//만약 10번 연속으로 안나온 블럭이 존재한다면?
            {
                int i = ForgetBlock();//잊어버린 블록부터 부르자.
                NextBlock = i;
                MissBlock[i] = 0;
            }
            else
            {
                NextBlock = (int)random.Next(0, 7);//랜덤 블록 만들기
                for (int i = 0; i < 7; i++)//넥스트블록 제외하고 잊어버린 블록 카운터 하나씩 올리기.
                    if (NextBlock != i)
                        MissBlock[i]++;
                MissBlock[NextBlock] = 0;
            }
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);//블록을 배열에다 놓기
            DrawGuideGrid();
        }

        //부조리 없니?
        private int ForgetBlock()
        {
            for (int i = 0; i < 7; i++)
                if (MissBlock[i] >= 7)
                    return i;
            return -1;
        }

        //블록을 배열에다 생성(newblock과 연계)
        private int[,] MergeCurrentBlockToBoard(int[,] Grid, int CurrentX, int CurrentY, int CurrentDirection)
        {
            //블록너비
            int[,] blockArray = GetBlockArray(CurrentBlock, CurrentDirection);

            int arrayLength = blockArray.Length;
            int size = 0;

            switch (arrayLength)
            {
                case 9:
                    size = 3;
                    break;
                case 16:
                    size = 4;
                    break;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (blockArray[i, j] == 1)//테트리스 블록이 있는 곳만.
                    {
                        Grid[CurrentX + i, CurrentY + j] = 10;//현재블록 위치를 10으로 둠.
                    }
                }
            }
            return Grid;
        }

        //테트로미노 7개 정의
        private int[,] GetBlockArray(int block, int direction)
        {
            switch (block)
            {
                case 0://ㅁ
                    return new int[4, 4] { { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
                case 1://ㅡ
                    switch (direction)
                    {
                        case 0:
                            return new int[4, 4] { { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
                        case 1:
                            return new int[4, 4] { { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 }, { 0, 0, 1, 0 } };
                        case 2:
                            return new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 1, 1, 1 }, { 0, 0, 0, 0 } };
                        case 3:
                            return new int[4, 4] { { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 }, { 0, 1, 0, 0 } };
                    }
                    break;
                case 2://Z
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 1, 1 }, { 1, 1, 0 }, { 0, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 0, 1 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
                        case 3:
                            return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    }
                    break;
                case 3://S
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 1, 1, 0 }, { 0, 1, 1 }, { 0, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 0, 1 }, { 0, 1, 1 }, { 0, 1, 0 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 0 }, { 0, 1, 1 } };
                        case 3:
                            return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 1, 0, 0 } };
                    }
                    break;
                case 4://J
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 0, 1 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 1, 0, 0 } };
                        case 3:
                            return new int[3, 3] { { 1, 1, 0 }, { 0, 1, 0 }, { 0, 1, 0 } };
                    }
                    break;
                case 5://L
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 1, 0, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 0, 1 } };
                        case 3:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 0 }, { 1, 1, 0 } };
                    }
                    break;
                case 6://ㅗ
                    switch (direction)
                    {
                        case 0:
                            return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 1 }, { 0, 0, 0 } };
                        case 1:
                            return new int[3, 3] { { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 0 } };
                        case 2:
                            return new int[3, 3] { { 0, 0, 0 }, { 1, 1, 1 }, { 0, 1, 0 } };
                        case 3:
                            return new int[3, 3] { { 0, 1, 0 }, { 1, 1, 0 }, { 0, 1, 0 } };
                    }
                    break;
            }
            return null;
        }

        //블록 굳히기
        private void FixBlock()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (Grid[i, j] == 10)//현재위치를
                    {
                        switch (CurrentBlock)
                        {
                            case 0://ㅁ
                                Grid[i, j] = 1;//굳히자
                                break;
                            case 1://ㅡ
                                Grid[i, j] = 2;//굳히자
                                break;
                            case 2://Z
                                Grid[i, j] = 3;//굳히자
                                break;
                            case 3://S
                                Grid[i, j] = 4;//굳히자
                                break;
                            case 4://J
                                Grid[i, j] = 5;//굳히자
                                break;
                            case 5://L
                                Grid[i, j] = 6;//굳히자
                                break;
                            case 6://ㅗ
                                Grid[i, j] = 7;//굳히자
                                break;
                        }
                    }
                }
            }
            DestroyBlock();//한줄이 만들어 졌는지 검사.
        }

        //한줄이 만들어 졌는지 검사.
        private void DestroyBlock()
        {
            for (int j = Height - 1; j >= 0; j--)//끝에서부터 검사.
            {
                for (int i = 0; i < Width; i++)
                {
                    if (Grid[i, j] == 0)//안채워진 부분이 있네?
                        break;//다음 부분 검사.

                    //마지막까지 왔는데 위 검사를 통과했으면
                    //한줄 완성했구나.
                    if (i == Width - 1)
                    {
                        Score++;//점수 올리고
                        //현재위치부터 모든 줄을 내리자.
                        for (int y = j; y > 0; y--)
                        {
                            for (i = 0; i < Width; i++)
                            {
                                Grid[i, y] = Grid[i, y - 1];
                            }
                        }
                        //그리고 처음부터 다시 검사해
                        j = Height;
                        break;
                    }
                }
            }
        }

        //회전
        public void Rotation()
        {
            int nextDirection = CurrentDirection;

            switch (CurrentDirection)
            {
                case 0:
                    nextDirection = 3;
                    break;
                case 1:
                    nextDirection = 0;
                    break;
                case 2:
                    nextDirection = 1;
                    break;
                case 3:
                    nextDirection = 2;
                    break;
            }

            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, nextDirection, CurrentX, CurrentY))
            {
                CurrentDirection = nextDirection;
            }
            else
            {
                if (CanAction(Grid, nextDirection, CurrentX - 1, CurrentY))
                {
                    CurrentX--;
                    CurrentDirection = nextDirection;
                }
                else if (CanAction(Grid, nextDirection, CurrentX + 1, CurrentY))
                {
                    CurrentX++;
                    CurrentDirection = nextDirection;
                }
                else if (CanAction(Grid, nextDirection, CurrentX, CurrentY - 1))
                {
                    CurrentY--;
                    CurrentDirection = nextDirection;
                }
                else if (CurrentBlock == 1 && nextDirection == 1)//일자 블록을 위한 예외처리
                {
                    CurrentX += 2;
                    CurrentDirection = nextDirection;
                }
            }
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();

        }

        //왼쪽으로 블록 이동
        public void MoveLeft()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX - 1, CurrentY))
                CurrentX--;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        //오른쪽으로 블록 이동
        public void MoveRight()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX + 1, CurrentY))
                CurrentX++;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            DrawGuideGrid();
        }

        //아래쪽으로 블록 이동()
        public void MoveDown()
        {
            RemoveCurrentBlock(Grid);

            if (CanAction(Grid, CurrentDirection, CurrentX, CurrentY + 1))
            {
                CurrentY++;
                MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            }
            else
            {
                MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
                FixBlock();
                NewBlock();
            }
        }

        //바닥으로 블록 이동
        public void MoveGround()
        {
            RemoveCurrentBlock(Grid);
            int i = CurrentY;
            while (true)
            {
                if (CanAction(Grid, CurrentDirection, CurrentX, i + 1))//다음 위치에 블록 가능?
                {
                    i++;//더갈수 있어?
                    continue;//그럼 더가
                }
                else
                {
                    CurrentY = i;
                    MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
                    FixBlock();
                    NewBlock();
                    break;
                }
            }
        }

        //현재 이동 가능한 상태인가?
        private bool CanAction(int[,] Grid, int nextDirection, int nextX, int nextY)
        {
            int[,] blockArray = GetBlockArray(CurrentBlock, nextDirection);
            int arrayLength = blockArray.Length;
            int size = 0;

            switch (arrayLength)
            {
                case 9:
                    size = 3;
                    break;
                case 16:
                    size = 4;
                    break;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (blockArray[i, j] == 1)
                    {
                        if (nextY + j >= Height)
                        {
                            return false;
                        }

                        if (nextX + i < 0)
                        {
                            return false;
                        }

                        if (nextX + i >= Width)
                        {
                            return false;
                        }

                        if (Grid[nextX + i, nextY + j] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //현재 블록 위치 삭제
        private void RemoveCurrentBlock(int[,] Grid)
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    if (Grid[i, j] == 10)
                        Grid[i, j] = 0;//현재 위치를 0으로 바꿈.
        }

        //게임오버
        public bool IsGameOver()
        {
            int y = 1;
            for (int x = 0; x < Width; x++)
            {
                if (Grid[x, y] != 0 && Grid[x, y] != 10)//게임오버 라인에 블록이 있다면,
                    return true;
            }
            return false;
        }

        //배열 그리기
        public void DrawBoard(GAME_PAGE game_page, bool player2, bool GuideLine)
        {
            int unitSize = 20;//테트리스 사이즈

            int marginLeft = 10;//왼쪽 공백
            int marginTop = 0;//위쪽 공백

            if (player2 == true)
                marginLeft += ((Width * 2 + 1) * unitSize);

            if (Grid[4, 2] != OldGrid[4, 2] || CanvasClear == true)//새로운 블록이 나올때마다 넥스트 그림 그리기. 또는 캔버스 청소 후 그리기.
            {
                DrawNext(game_page, marginLeft, marginTop, player2);
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 2; j < Height; j++)
                {
                    int x1 = marginLeft + (i * unitSize);
                    int y1 = marginTop + (j * unitSize) - unitSize;
                    if (CanvasClear == true)//10번에 한번 청소 후 모든 셀 다시 생성.
                    {
                        PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                    }
                    if (Grid[i, j] != OldGrid[i, j])//예전과 다르다면, 그려라.
                    {
                        PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                    }
                    if (OldGuideGrid[i, j] != 0 && GuideLine == true)//기존 가이드블록 제거.
                    {
                        PaintCell(game_page, unitSize, x1, y1, i, j, Grid);
                    }
                    if (GuideGrid[i, j] != 0 && GuideLine == true)//가이드블록 생성
                    {
                        PaintCell(game_page, unitSize, x1, y1, i, j, GuideGrid);
                    }
                    OldGrid[i, j] = Grid[i, j];
                }
            }
            OldGuideGrid = new int[Width, Height];
        }

        //가이드라인 그리기
        private void DrawGuideGrid()
        {
            OldGuideGrid = (int[,])GuideGrid.Clone();//배열복사
            RemoveCurrentBlock(Grid);
            RemoveCurrentBlock(GuideGrid);
            int i = CurrentY;
            while (true)
            {
                if (CanAction(Grid, CurrentDirection, CurrentX, i + 1))//다음 위치에 블록 가능?
                {
                    i++;//더갈수 있어?
                    continue;//그럼 더가
                }
                else//더이상 못가면..
                {
                    MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);//요건 원상복귀
                    MergeCurrentBlockToBoard(GuideGrid, CurrentX, i, CurrentDirection);//그리드에 생성.
                    break;
                }
            }
        }

        //넥스트 블록 그리기
        private void DrawNext(GAME_PAGE game_page, int marginLeft, int marginTop, bool player2)
        {
            int unitSize = 20;//테트리스 사이즈
            int size = 4;
            int[,] blockArray = GetBlockArray(NextBlock, 3);

            switch (NextBlock)
            {
                case 0://ㅁ
                    blockArray = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 1, 1, 0 }, { 0, 1, 1, 0 }, { 0, 0, 0, 0 } };
                    break;
                case 1://ㅡ
                    blockArray = new int[4, 4] { { 0, 2, 0, 0 }, { 0, 2, 0, 0 }, { 0, 2, 0, 0 }, { 0, 2, 0, 0 } };
                    break;
                case 2://Z
                    blockArray = new int[4, 4] { { 0, 3, 0, 0 }, { 0, 3, 3, 0 }, { 0, 0, 3, 0 }, { 0, 0, 0, 0 } };
                    break;
                case 3://S
                    blockArray = new int[4, 4] { { 0, 0, 4, 0 }, { 0, 4, 4, 0 }, { 0, 4, 0, 0 }, { 0, 0, 0, 0 } };
                    break;
                case 4://J
                    blockArray = new int[4, 4] { { 0, 5, 5, 0 }, { 0, 0, 5, 0 }, { 0, 0, 5, 0 }, { 0, 0, 0, 0 } };
                    break;
                case 5://L
                    blockArray = new int[4, 4] { { 0, 0, 6, 0 }, { 0, 0, 6, 0 }, { 0, 6, 6, 0 }, { 0, 0, 0, 0 } };
                    break;
                case 6://ㅗ
                    blockArray = new int[4, 4] { { 0, 0, 7, 0 }, { 0, 7, 7, 0 }, { 0, 0, 7, 0 }, { 0, 0, 0, 0 } };
                    break;
            }

            if (player2 == true)
            {
                marginLeft = marginLeft - ((Width + 6) * unitSize);
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x1 = marginLeft + (Width * unitSize) + (i * unitSize) + unitSize;
                    int y1 = 140 + marginTop + (j * unitSize) - unitSize;
                    PaintCell(game_page, unitSize, x1, y1, i, j, blockArray);
                }
            }
        }

        //네모 그리기
        private void PaintCell(GAME_PAGE game_page, int unitSize, int x1, int y1, int i, int j, int[,] Array)
        {
            Rectangle r = new Rectangle();
            r.Width = unitSize;
            r.Height = unitSize;
            r.StrokeThickness = 1;
            r.Stroke = Brushes.White;

            if (Array == GuideGrid)
            {
                r.StrokeThickness = 3;
                r.Stroke = Brushes.White;
                r.Margin = new System.Windows.Thickness(x1, y1, 0, 0);
                game_page.canvas.Children.Add(r);
                return;
            }
            else
                r.Fill = Brushes.Gray;

            switch (Array[i, j])
            {
                case 1://ㅁ 굳
                    r.Fill = Brushes.Yellow;
                    break;
                case 2://ㅡ 굳
                    r.Fill = Brushes.Cyan;
                    break;
                case 3://Z 굳
                    r.Fill = Brushes.Red;
                    break;
                case 4://S 굳
                    r.Fill = Brushes.LimeGreen;
                    break;
                case 5://J 굳
                    r.Fill = Brushes.Blue;
                    break;
                case 6://L 굳
                    r.Fill = Brushes.Orange;
                    break;
                case 7://ㅗ 굳
                    r.Fill = Brushes.Purple;
                    break;
                case 10://움직이는 블록
                    switch (CurrentBlock)
                    {
                        case 0://ㅁ
                            r.Fill = Brushes.Yellow;
                            break;
                        case 1://ㅡ
                            r.Fill = Brushes.Cyan;
                            break;
                        case 2://Z
                            r.Fill = Brushes.Red;
                            break;
                        case 3://S
                            r.Fill = Brushes.LimeGreen;
                            break;
                        case 4://J
                            r.Fill = Brushes.Blue;
                            break;
                        case 5://L
                            r.Fill = Brushes.Orange;
                            break;
                        case 6://ㅗ
                            r.Fill = Brushes.Purple;
                            break;
                    }
                    break;
                default://0 = 빈 블록
                    r.Stroke = Brushes.Black;
                    r.StrokeThickness = 0.5;
                    r.Fill = Brushes.Black;
                    break;
            }
            r.Margin = new System.Windows.Thickness(x1, y1, 0, 0);
            game_page.canvas.Children.Add(r);
        }

        ///////////////////////////////////////////////////////////////////
        // A I ////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////
        public void AIVirtualMove(double AIa, double AIb, double AIc, double AId)
        {
            int[,] GridClone = (int[,])Grid.Clone();//배열복사
            int[,] GridClone2 = (int[,])GridClone.Clone();
            double AIScore;
            int AIResultX = 0;//x위치
            int AIResultY = 0;//y위치
            int AIResultDirection = 0;//블록방향
            double AIresultTemp = -1000;//중요도값 저장용.값이 클수록 좋음.

            for (int directiontemp = 3; directiontemp >= 0; directiontemp--)//4가지 방향 다 해봅니다.
            {
                RemoveCurrentBlock(GridClone);//현재 블럭의 위치를 그리드에서 삭제.
                int xtemp = 4;//이동될 위치. 현재위치를 임시로 저장.
                int ytemp = CurrentY;

                //경우의 수 시작.
                while (true)
                {
                    if (CanAction(GridClone, directiontemp, xtemp - 1, ytemp))//왼쪽위치로 최대한 옮깁니다.
                    {
                        xtemp--;//더갈수 있어?
                        continue;//그럼 더가
                    }
                    else
                        break;
                }
                //가장 왼쪽으로 왔습니다.

                //이제 오른쪽으로 가면서 하나씩 내려꽃아 봅시다.
                while (true)
                {
                    while (true)
                    {
                        if (CanAction(GridClone, directiontemp, xtemp, ytemp + 1))//아래로 최대한 옮깁니다.
                        {
                            ytemp++;//더갈수 있어?
                            continue;//그럼 더가
                        }
                        else
                            break;
                    }
                    //이제 내려꽃았습니다.
                    //내려꽃기 전과 비교하여 지금 이 방법이 가장 최적인지 확인해봅니다.
                    GridClone2 = (int[,])GridClone.Clone();//원상복귀
                    //이 위치를 고정시킴(배열안에 추가됨.)
                    GridClone2 = (int[,])MergeCurrentBlockToBoard(GridClone2, xtemp, ytemp, directiontemp).Clone();

                    //이제 계산해서 전값과 비교해보자
                    AIScore = AIa * AIHeightValueSUM(GridClone2) + AIb * AILinesValue(GridClone2)
                             + AIc * AIHolesValue(GridClone2) + AId * AIBumpinessValue(GridClone2);

                    //현재가 더 좋다면 높은 점수가 나올 것이다.
                    if (AIScore >= AIresultTemp)
                    {
                        //새로운 최적값을 찾았다면 갱신한다.
                        AIresultTemp = AIScore;
                        AIResultX = xtemp;
                        AIResultY = ytemp;
                        AIResultDirection = directiontemp;
                    }
                    //이제 한칸씩 오른쪽으로 못갈때 까지 가봅니다.
                    //우선 원래 높이로 돌아갑시다.
                    ytemp = CurrentY;
                    if (CanAction(GridClone, directiontemp, xtemp + 1, ytemp))//다음 위치에 블록 가능?
                    {
                        xtemp++;
                        continue;//그럼 더가                    
                    }
                    else//더 이상 갈 수 없다면 마지막으로 한번 더 확인해 봅니다.
                    {
                        break;
                    }
                }
            }
            Grid = (int[,])GridClone.Clone();//배열 원상복귀.
            RemoveCurrentBlock(Grid);
            CurrentX = AIResultX;
            CurrentY = AIResultY;
            CurrentDirection = AIResultDirection;
            MergeCurrentBlockToBoard(Grid, CurrentX, CurrentY, CurrentDirection);
            FixBlock();
            NewBlock();
        }

        //높이의 합
        private int AIHeightValueSUM(int[,] GridClone)
        {
            int ValueScore = 0;

            for (int x = 0; x < Width; x++)
            {
                int EachHeight = Height - 2;
                for (int y = 2; y < Height; y++)
                {
                    if (GridClone[x, y] != 0)
                        break;
                    else
                        EachHeight--;
                }
                ValueScore += EachHeight;
            }
            return ValueScore;//높을수록 안좋다.
        }
        //완전한 라인이 만들어진 갯수.
        private int AILinesValue(int[,] GridClone)
        {
            int ValueScore = 0;
            for (int y = Height - 1; y >= 0; y--)//끝에서부터 검사.
            {
                for (int x = 0; x < Width; x++)
                {
                    if (GridClone[x, y] == 0)//안채워진 부분이 있네?
                        break;//다음 부분 검사.

                    //마지막까지 왔는데 위 검사를 통과했으면
                    //한줄 완성했구나.
                    if (x == Width - 1)
                    {
                        ValueScore++;//
                    }
                }
            }
            return ValueScore;//높을수록 좋다.
        }
        //구멍이 몇개 있는가?
        private int AIHolesValue(int[,] GridClone)
        {
            int ValueScore = 0;
            bool HoleCheck = false;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 2; y < Height; y++)
                {
                    if (GridClone[x, y] != 0)
                        HoleCheck = true;
                    if (HoleCheck == true && GridClone[x, y] == 0)
                        ValueScore++;
                }
                HoleCheck = false;
            }
            return ValueScore;//높을수록 안좋다.
        }
        //울퉁불퉁한 지수는?
        private int AIBumpinessValue(int[,] GridClone)
        {
            int ValueScore = 0;
            int EachHeight_Current = 0;
            int EachHeight_Old = 0;
            for (int x = 0; x < Width; x++)
            {
                int EachHeight = Height - 2;
                for (int y = 2; y < Height; y++)
                {
                    if (GridClone[x, y] != 0)
                        break;
                    else
                        EachHeight--;
                }
                EachHeight_Old = EachHeight_Current;
                EachHeight_Current = EachHeight;

                if (x > 0)
                {
                    if (EachHeight_Current - EachHeight_Old > 0)
                        ValueScore += EachHeight_Current - EachHeight_Old;
                    else
                        ValueScore += EachHeight_Old - EachHeight_Current;
                }
            }
            return ValueScore;//높을수록 안좋다.
        }
    }
}