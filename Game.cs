using RaceGame.Pathfinding;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceGame
{
    /// <summary>
    /// Deze class managed de huidige game.
    /// Deze class is partial!
    /// </summary>
    public partial class Game
    {
        public Player player1;
        public Player player2;
        public byte[,] GameField;
        public int MapsizeX = 1008; // Uit de Window.cs designer gehaald
        public int MapsizeY = 648;
        //14x9 roads
        int MapsizeXR = 14;
        int MapsizeYR = 9;
        public Bitmap Background;
        protected Random random;
        public System.Drawing.Point[] Points;

        public Road[,] Roads;
        public Point[] CheckPoints;
        public bool[] CheckPointsPassedP1;
        public bool[] CheckPointsPassedP2;
        List<SpecRoad> SpecialRoadList = new List<SpecRoad>();
        public Point PitStopPoint;
        bool ValuesChanged = false;
        bool forceCheck = false;
        public List<Obstacle> ObstaclesList = new List<Obstacle>();

        DrawInfo Redpointer;
        DrawInfo Bluepointer;

        Point SpawnPointP1;
        Point SpawnPointP2;

        int FillPercentage = 50;
        int Iterations = 3;

        public int LastCheckPointP1 = 0;
        public int LastCheckPointP2 = 0;

        public Game(Enums.VehicleType player1Vehicle, Enums.VehicleType player2Vehicle)
        {
            Base.currentGame = this;
            GameField = new byte[MapsizeX, MapsizeY];
            random = new Random(DateTime.UtcNow.GetHashCode());
            CreateMap();
            CreatePlayers(player1Vehicle, player2Vehicle);
            Bluepointer = new DrawInfo(Bitmaps.Other.Bluepointer, SpawnPointP1.x, SpawnPointP1.y - 10, 16, 16, -90);
            Redpointer = new DrawInfo(Bitmaps.Other.Redpointer, SpawnPointP2.x, SpawnPointP2.y - 10, 16, 16, -90);
            Base.drawInfos.Add(Bluepointer);
            Base.drawInfos.Add(Redpointer);
            Base.gameTasks.Add(PLayerTrackers);
            Base.gameTasks.Add(WinCheck);
        }

        public void CreatePlayers(Enums.VehicleType player1Vehicle, Enums.VehicleType player2Vehicle)
        {
            player1 = null;
            player2 = null;
            player1 = new Player(1);
            player1.CreateVehicle(player1Vehicle, SpawnPointP1);
            player1.vehicle.StartDraw();
            player1.vehicle.StartWeaponDraw();
            player2 = new Player(2);
            player2.CreateVehicle(player2Vehicle, SpawnPointP2);
            player2.vehicle.StartDraw();
            player2.vehicle.StartWeaponDraw();
            Base.gameTasks.Add(player1.vehicle.MoveVehicle);
            Base.gameTasks.Add(player2.vehicle.MoveVehicle);
        }

        public void CreateMap()
        {
            Roads = new Road[MapsizeXR, MapsizeYR];
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Roads[x, y] = new Road(x, y, RoadType.NULL);
                }
            }
            Point[] _points = GeneratePointSet();
            CheckPoints = _points;

            for (int i = 0; i < _points.Length; i++)
            {
                int SX = 0;
                int SY = 0;
                if (i != _points.Length - 1)
                {
                    SX = _points[i + 1].x;
                    SY = _points[i + 1].y;
                }
                else
                {
                    SX = _points[0].x;
                    SY = _points[0].y;
                }
                List<Point> P = FindRoadPath(Roads[_points[i].x, _points[i].y], Roads[SX, SY], RoadType.NULL);
                List<Road> CurrentPath = new List<Road>();
                for (int j = 0; j < P.Count; j++)
                {
                    CurrentPath.Add(Roads[P[j].x, P[j].y]);
                }
                SetRoadType(P);
            }

            for (int i = 1; i < SpecialRoadList.Count; i += 2)
            {
                if (i == SpecialRoadList.Count - 1)
                {
                    SetSpecialRoadType(SpecialRoadList[i], SpecialRoadList[0]);
                    //ER MOET MINIMAAL 1 BLOK TUSSEN DE ROADS ZITTEN!!!
                }
                else
                {
                    SetSpecialRoadType(SpecialRoadList[i], SpecialRoadList[i + 1]);
                }
            }

            //Pitstopmagic
            for (int i = 0; i < PitstopLocations.Count; ++i)
            {
                Roads[PitstopLocations[i].x, PitstopLocations[i].y].roadType = RoadType.PitstopSpecial;
            }

            Background = new Bitmap(MapsizeX, MapsizeY);
            CellularAutomata();

            for (int x = 0; x < MapsizeX; ++x)
            {
                for (int y = 0; y < MapsizeY; ++y)
                {
                    Background.SetPixel(x, y, GameField[x, y] == 1 ? Color.Green : Color.FromArgb(255, 93, 171, 67));
                }
            }
            for (int x = 0; x < MapsizeXR; x++)
            {
                for (int y = 0; y < MapsizeYR; y++)
                {
                    Bitmap T;
                    if (Roads[x, y].roadType != RoadType.PitstopSpecial)
                    {
                        switch (Roads[x, y].roadType)
                        {
                            case RoadType.horizontalStraight:
                                T = Bitmaps.Roads.HorizontalStraight;
                                break;
                            case RoadType.verticalStraight:
                                T = Bitmaps.Roads.VerticalStraight;
                                break;
                            case RoadType.bottomleftCorner:
                                T = Bitmaps.Roads.LeftBottom;
                                break;
                            case RoadType.bottomrightCorner:
                                T = Bitmaps.Roads.RightBottom;
                                break;
                            case RoadType.topleftCorner:
                                T = Bitmaps.Roads.LeftTop;
                                break;
                            case RoadType.toprightCorner:
                                T = Bitmaps.Roads.RightTop;
                                break;
                            default:
                                T = null;
                                break;
                        }
                        for (int x2 = 0; x2 < 72; x2++)
                        {
                            for (int y2 = 0; y2 < 72; y2++)
                            {
                                if (T != null && T.GetPixel(x2, y2).A != 0)
                                {
                                    Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                }
                            }
                        }
                    }
                    else
                    {
                        //Pitstop code
                        if (pitstopType == PitStopType.Horizontal)
                        {
                            T = Bitmaps.Roads.HorizontalPitstop;
                            if (Roads[x, y].X == 6 && Roads[x, y].Y == 0)
                            {
                                for (int x2 = 0; x2 < 216; ++x2)
                                {
                                    for (int y2 = 0; y2 < 144; ++y2)
                                    {
                                        if (T.GetPixel(x2, y2).A != 0)
                                            Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (pitstopType == PitStopType.Vertical)
                            {
                                T = Bitmaps.Roads.VerticalPitstop;
                                if (Roads[x, y].X == 0 && Roads[x, y].Y == 3)
                                {
                                    for (int x2 = 0; x2 < 144; ++x2)
                                    {
                                        for (int y2 = 0; y2 < 216; ++y2)
                                        {
                                            if (T.GetPixel(x2, y2).A != 0)
                                                Background.SetPixel(x * 72 + x2, y * 72 + y2, T.GetPixel(x2, y2));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("INVALID PITSTOPTYPE GAME.CS");
                            }
                        }
                    }
                }
            }
            for (int x = 0; x < 16; ++x)
            {
                for (int y = 0; y < 16; ++y)
                {
                    if (Bitmaps.Other.Wrench.GetPixel(x, y).A != 0)
                        Background.SetPixel(PitStopPoint.x - 8 + x, PitStopPoint.y - 8 + y, Bitmaps.Other.Wrench.GetPixel(x, y));
                }
            }
            CheckPointsPassedP1 = new bool[CheckPoints.Length];
            CheckPointsPassedP2 = new bool[CheckPoints.Length];
            forceCheck = true;
            Finish();
            Base.drawInfos.Add(new DrawInfo(Background, MapsizeX / 2, MapsizeY / 2, MapsizeX, MapsizeY, 270, 0));
            Base.gameTasks.Add(CheckPoint);
            Base.gameTasks.Add(CheckFinish);
            GenerateObstacles();
        }

        void CellularAutomata()
        {
            Random RND = new Random();
            for (int x = 0; x < MapsizeX; ++x)
            {
                for (int y = 0; y < MapsizeY; ++y)
                {
                    GameField[x, y] = RND.Next(0, 101) < FillPercentage ? (byte)1 : (byte)0;
                }
            }

            for (int i = 0; i < Iterations; i++)
            {
                for (int x = 0; x < MapsizeX; ++x)
                {
                    for (int y = 0; y < MapsizeY; ++y)
                    {
                        int Count = GetNeighbours(x, y);
                        if (Count > 4)
                        {
                            GameField[x, y] = 1;
                        }
                        else
                        {
                            if (Count < 4)
                            {
                                GameField[x, y] = 0;
                            }
                            else
                            {
                                GameField[x, y] = (byte)RND.Next(0, 2);
                            }
                        }
                    }
                }
            }
        }

        int GetNeighbours(int xCoord, int yCoord)
        {
            int Count = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue;

                    int CheckX = xCoord + x;
                    int CheckY = yCoord + y;

                    if (CheckX >= 0 && CheckX < MapsizeX && CheckY >= 0 && CheckY < MapsizeY)
                    {
                        Count += GameField[CheckX, CheckY];
                    }
                }
            }
            return Count;
        }

        void CheckPoint()
        {
            for (int i = 1; i < CheckPoints.Length; ++i)
            {
                int Cx = CheckPoints[i].x * 72 + 36;
                int Cy = CheckPoints[i].y * 72 + 36;


                if (player1.vehicle.drawInfo.x > Cx - 36 && player1.vehicle.drawInfo.x < Cx + 36 && player1.vehicle.drawInfo.y > Cy - 36 && player1.vehicle.drawInfo.y < Cy + 36)
                {
                    CheckPointsPassedP1[i] = true;
                    ValuesChanged = true;
                    LastCheckPointP1 = i;
                }
                else
                {
                    if (player2.vehicle.drawInfo.x > Cx - 36 && player2.vehicle.drawInfo.x < Cx + 36 && player2.vehicle.drawInfo.y > Cy - 36 && player2.vehicle.drawInfo.y < Cy + 36)
                    {
                        CheckPointsPassedP2[i] = true;
                        ValuesChanged = true;
                        LastCheckPointP2 = i;
                    }
                    else
                    {
                        ValuesChanged = false;
                    }
                }

                Direction Dir = Direction.NULL;

                if (i != CheckPoints.Length - 1)
                {
                    if (CheckPoints[i].x == CheckPoints[i + 1].x && CheckPoints[i].y < CheckPoints[i + 1].y)
                    {
                        Dir = Direction.Bottom;
                    }
                    else
                    {
                        if (CheckPoints[i].x == CheckPoints[i + 1].x && CheckPoints[i].y > CheckPoints[i + 1].y)
                        {
                            Dir = Direction.Top;
                        }
                        else
                        {
                            if (CheckPoints[i].y == CheckPoints[i + 1].y && CheckPoints[i].x > CheckPoints[i + 1].x)
                            {
                                Dir = Direction.Left;
                            }
                            else
                            {
                                if (CheckPoints[i].y == CheckPoints[i + 1].y && CheckPoints[i].x < CheckPoints[i + 1].x)
                                {
                                    Dir = Direction.Right;
                                }
                                else
                                {
                                    Console.WriteLine("ERRURRR2");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (CheckPoints[i].x == CheckPoints[0].x && CheckPoints[i].y < CheckPoints[0].y)
                    {
                        Dir = Direction.Bottom;
                    }
                    else
                    {
                        if (CheckPoints[i].x == CheckPoints[0].x && CheckPoints[i].y > CheckPoints[0].y)
                        {
                            Dir = Direction.Top;
                        }
                        else
                        {
                            if (CheckPoints[i].y == CheckPoints[0].y && CheckPoints[i].x > CheckPoints[0].x)
                            {
                                Dir = Direction.Left;
                            }
                            else
                            {
                                if (CheckPoints[i].y == CheckPoints[0].y && CheckPoints[i].x < CheckPoints[0].x)
                                {
                                    Dir = Direction.Right;
                                }
                                else
                                {
                                    Console.WriteLine("ERRURRR2");
                                }
                            }
                        }
                    }
                }

                Bitmap B = null;

                if (CheckPointsPassedP1[i] && CheckPointsPassedP2[i])
                {
                    //Beide groen
                    switch (Dir)
                    {
                        case Direction.Left:
                            B = Bitmaps.Other.OneGreenTwoGreenLeft;
                            break;
                        case Direction.Right:
                            B = Bitmaps.Other.OneGreenTwoGreenRight;
                            break;
                        case Direction.Bottom:
                            B = Bitmaps.Other.OneGreenTwoGreenDown;
                            break;
                        case Direction.Top:
                            B = Bitmaps.Other.OneGreenTwoGreenUp;
                            break;
                        default:
                            Console.WriteLine("ERRUR");
                            B = null;
                            break;
                    }
                }
                else
                {
                    if (CheckPointsPassedP1[i] == false && CheckPointsPassedP2[i])
                    {
                        //Player 1 blauw, Player 2 groen
                        switch (Dir)
                        {
                            case Direction.Left:
                                B = Bitmaps.Other.OneBlueTwoGreenLeft;
                                break;
                            case Direction.Right:
                                B = Bitmaps.Other.OneBlueTwoGreenRight;
                                break;
                            case Direction.Bottom:
                                B = Bitmaps.Other.OneBlueTwoGreenDown;
                                break;
                            case Direction.Top:
                                B = Bitmaps.Other.OneBlueTwoGreenUp;
                                break;
                            default:
                                Console.WriteLine("ERRUR");
                                B = null;
                                break;
                        }
                    }
                    else
                    {
                        if (CheckPointsPassedP1[i] && CheckPointsPassedP2[i] == false)
                        {
                            //Player 1 groen, Player 2 blue
                            switch (Dir)
                            {
                                case Direction.Left:
                                    B = Bitmaps.Other.OneGreenTwoBlueLeft;
                                    break;
                                case Direction.Right:
                                    B = Bitmaps.Other.OneGreenTwoBlueRight;
                                    break;
                                case Direction.Bottom:
                                    B = Bitmaps.Other.OneGreenTwoBlueDown;
                                    break;
                                case Direction.Top:
                                    B = Bitmaps.Other.OneGreenTwoBlueUp;
                                    break;
                                default:
                                    Console.WriteLine("ERRUR");
                                    B = null;
                                    break;
                            }
                        }
                        else
                        {
                            if (!CheckPointsPassedP1[i] && !CheckPointsPassedP2[i])
                            {
                                //Beide blauw
                                switch (Dir)
                                {
                                    case Direction.Left:
                                        B = Bitmaps.Other.OneBlueTwoBlueLeft;
                                        break;
                                    case Direction.Right:
                                        B = Bitmaps.Other.OneBlueTwoBlueRight;
                                        break;
                                    case Direction.Bottom:
                                        B = Bitmaps.Other.OneBlueTwoBlueDown;
                                        break;
                                    case Direction.Top:
                                        B = Bitmaps.Other.OneBlueTwoBlueUp;
                                        break;
                                    default:
                                        Console.WriteLine("ERRUR");
                                        B = null;
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("BAD STUFF");
                            }
                        }
                    }
                }

                if (forceCheck || ValuesChanged)
                {
                    Random R = new Random();
                    switch (R.Next(0, 200))
                    {
                        case 0:
                            AudioFiles.Horn1.Play();
                            break;
                        case 1:
                            AudioFiles.Horn2.Play();
                            break;
                        case 2:
                            AudioFiles.Horn3.Play();
                            break;
                        default:
                            break;
                    }

                    for (int x = 0; x < 32; ++x)
                    {
                        for (int y = 0; y < 32; ++y)
                        {
                            if (B.GetPixel(x, y).A != 0)
                                Background.SetPixel(CheckPoints[i].x * 72 - 16 + x + 36, CheckPoints[i].y * 72 - 16 + y + 36, B.GetPixel(x, y));
                        }
                    }
                }
            }
            forceCheck = false;
        }

        void Finish()
        {
            if (pitstopType == PitStopType.Horizontal)
            {
                int Cx = CheckPoints[0].x * 72 + 54;
                int Cy = CheckPoints[0].y * 72;

                SpawnPointP1 = new Point(Cx - 26, Cy + 18 + 36);
                SpawnPointP2 = new Point(Cx - 26, Cy - 18 + 36);

                for (int x = 0; x < 18; ++x)
                {
                    for (int y = 0; y < 72; ++y)
                    {
                        if (Bitmaps.Other.HorizontalFinish.GetPixel(x, y).A != 0)
                            Background.SetPixel(Cx + x + 18, Cy + y, Bitmaps.Other.HorizontalFinish.GetPixel(x, y));
                    }
                }
            }
            else
            {
                int Cx = CheckPoints[0].x * 72 - 18;
                int Cy = CheckPoints[0].y * 72 + 76;

                SpawnPointP1 = new Point(Cx - 18 + 54, Cy - 36);
                SpawnPointP2 = new Point(Cx + 18 + 54, Cy - 36);

                for (int x = 0; x < 72; ++x)
                {
                    for (int y = 0; y < 18; ++y)
                    {
                        if (Bitmaps.Other.VerticalFinish.GetPixel(x, y).A != 0)
                            Background.SetPixel(Cx + x + 18, Cy + y, Bitmaps.Other.VerticalFinish.GetPixel(x, y));
                    }
                }
            }
        }

        void CheckFinish()
        {
            int Cx = CheckPoints[0].x * 72 + 72;
            int Cy = CheckPoints[0].y * 72 + 36;

            if (player1.vehicle.drawInfo.x > Cx - 36 && player1.vehicle.drawInfo.x < Cx + 36 && player1.vehicle.drawInfo.y > Cy - 36 && player1.vehicle.drawInfo.y < Cy + 36)
            {
                int CheckPointCount = 0;
                for (int i = 0; i < CheckPointsPassedP1.Length; ++i)
                {
                    if (CheckPointsPassedP1[i])
                    {
                        CheckPointCount++;
                    }
                }
                if (CheckPointCount == CheckPointsPassedP1.Length - 1) //-1 voor finish correctie
                {
                    Base.currentGame.player1.LapCounter++;
                    CheckPointsPassedP1 = new bool[CheckPoints.Length];
                    forceCheck = true;
                }

            }
            if (player2.vehicle.drawInfo.x > Cx - 36 && player2.vehicle.drawInfo.x < Cx + 36 && player2.vehicle.drawInfo.y > Cy - 36 && player2.vehicle.drawInfo.y < Cy + 36)
            {
                int CheckPointCount = 0;
                for (int i = 0; i < CheckPointsPassedP2.Length; ++i)
                {
                    if (CheckPointsPassedP2[i])
                    {
                        CheckPointCount++;
                    }
                }
                if (CheckPointCount == CheckPointsPassedP2.Length - 1) //-1 voor finish correctie
                {
                    Base.currentGame.player2.LapCounter++;
                    CheckPointsPassedP2 = new bool[CheckPoints.Length];
                    forceCheck = true;
                }
            }
        }

        static protected int GetDistance(int x, int y, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow(Math.Abs(x - x2), 2) + Math.Pow(Math.Abs(y - y2), 2));
        }

        public void CheckHealth()
        {
            if (Base.currentGame.player1.vehicle.health <= 0)
            {
                if (GetDistance((int)Base.currentGame.player2.vehicle.drawInfo.x, (int)Base.currentGame.player2.vehicle.drawInfo.y, Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1].x * 72 + 36, Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1].y * 72 + 36)<72)
                {
                    if (Base.currentGame.LastCheckPointP1 != 0)
                    {
                        Base.currentGame.player1.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1 - 1].x * 72 + 36;
                        Base.currentGame.player1.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1 - 1].y * 72 + 36;
                    }
                    else
                    {
                        Base.currentGame.player1.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.CheckPoints.Length - 1].x * 72 + 36;
                        Base.currentGame.player1.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.CheckPoints.Length - 1].y * 72 + 36;
                    }
                }
                else
                {
                    Base.currentGame.player1.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1].x * 72 + 36;
                    Base.currentGame.player1.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP1].y * 72 + 36;
                }
                Base.currentGame.player1.vehicle.health = Base.currentGame.player1.vehicle.maxHealth;
            }
            if (Base.currentGame.player2.vehicle.health <= 0)
            {
                if (GetDistance((int)Base.currentGame.player1.vehicle.drawInfo.x, (int)Base.currentGame.player1.vehicle.drawInfo.y, Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2].x * 72 + 36, Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2].y * 72 + 36) < 72)
                {
                    if (Base.currentGame.LastCheckPointP2 != 0)
                    {
                        Base.currentGame.player2.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2 - 1].x * 72 + 36;
                        Base.currentGame.player2.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2 - 1].y * 72 + 36;
                    }
                    else
                    {
                        Base.currentGame.player2.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.CheckPoints.Length - 1].x * 72 + 36;
                        Base.currentGame.player2.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.CheckPoints.Length - 1].y * 72 + 36;
                    }
                }
                else
                {
                    Base.currentGame.player2.vehicle.drawInfo.x = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2].x * 72 + 36;
                    Base.currentGame.player2.vehicle.drawInfo.y = Base.currentGame.CheckPoints[Base.currentGame.LastCheckPointP2].y * 72 + 36;
                }
                Base.currentGame.player2.vehicle.health = Base.currentGame.player2.vehicle.maxHealth;
            }
        }

        public void WinCheck()
        {
            if (player1.vehicle.fuel == 0 || player2.LapCounter == Base.windowHandle.TotalLaps)
            {
                //Player2 wins
                Base.gameTasks.Remove(WinCheck);
                Base.drawInfos.Add(new DrawInfo(Bitmaps.Other.p2Win, Bitmaps.Other.p1Win.Width / 2, Bitmaps.Other.p1Win.Height / 2, Bitmaps.Other.p1Win.Width, Bitmaps.Other.p1Win.Height, -90));
                Base.windowHandle.disableInput = true;
                Base.windowHandle.button1.Visible = true;
                Base.currentGame.player1.vehicle.throttle = false;
                Base.currentGame.player1.vehicle.speed = 0;
                Base.currentGame.player2.vehicle.throttle = false;
                Base.currentGame.player2.vehicle.speed = 0;
                AudioFiles.Win.Play();
            }

            if (player2.vehicle.fuel == 0 || player1.LapCounter == Base.windowHandle.TotalLaps)
            {
                //Player1 wins
                Base.gameTasks.Remove(WinCheck);
                Base.drawInfos.Add(new DrawInfo(Bitmaps.Other.p1Win, Bitmaps.Other.p2Win.Width / 2, Bitmaps.Other.p2Win.Height / 2, Bitmaps.Other.p2Win.Width, Bitmaps.Other.p2Win.Height, -90));
                Base.windowHandle.disableInput = true;
                Base.windowHandle.button1.Visible = true;
                Base.currentGame.player1.vehicle.throttle = false;
                Base.currentGame.player1.vehicle.speed = 0;
                Base.currentGame.player2.vehicle.throttle = false;
                Base.currentGame.player2.vehicle.speed = 0;
                AudioFiles.Win.Play();
            }

        }
    }
}
