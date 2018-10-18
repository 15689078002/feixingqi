using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace 飞行棋游戏
{
    class Program
    {
        public static int[] Maps = new int[100];
        public static int[] PlayerPos = new int[2];//玩家坐标
        public static string[] name = new string[2];//玩家姓名
        public static bool[] Flags = new bool[2]; //bool 默认false
        static void Main(string[] args)
        {
            GaneShow();
            Console.WriteLine("输入玩家A的姓名");
            name[0] = Console.ReadLine();


            while (name[0] == null)
            {
                Console.WriteLine("玩家A的姓名为空，重新输入");
                name[0] = Console.ReadLine();

            }

            Console.WriteLine("输入玩家B的姓名");
            name[1] = Console.ReadLine();

            while (name[0] == null)
            {
                Console.WriteLine("玩家B的姓名为空，重新输入");
                name[0] = Console.ReadLine();

            }
            //玩家姓名输入完毕 清屏重新输出
            Console.Clear();
            GaneShow();
            Console.WriteLine("{0}的士兵用A表示,{1}的士兵用B表示", name[0], name[1]);
            InitailMap();
            DrawMap();
            //游戏不结束条件
            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    Playgame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻的赢了玩家{1}",name[0],name[1]);
                    break;
                }
                if (Flags[1] == false)
                {
                    Playgame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (PlayerPos[1] >= 99)
                {
                    Console.WriteLine("玩家{1}无耻的赢了玩家{0}", name[0], name[1]);
                    break;
                }
            }//while




            Console.ReadKey();
        }
        public static void GaneShow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************            ***游戏规则***");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*********************            ***踩到时空隧道 前进十格***");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************            ***踩到时空隧道 前进十格***");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****飞行棋游戏******            ***踩到幸运轮盘 1交换位置 2 轰炸对方（使对方前进6格）*** ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*********************            ***踩到暂停 暂停一回合*** ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************            ***踩到方框 什么也不干***");
            Console.WriteLine("                                     ");
            Console.WriteLine("                                     ");


        }//主界面显示
        public static void InitailMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘
            for (int i = 0; i < luckyturn.Length; i++)
            {
                //拿到数组下标 
                int index1 = luckyturn[i];
                //地图中初始化为0
                Maps[index1] = 1;
            };

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷
            {
                for (int i = 0; i < landMine.Length; i++)
                {
                    int index2 = landMine[i];
                    Maps[index2] = 2;

                }
            }
            int[] pause = { 9, 27, 60, 93 ,3,8,77};//暂停
            for (int i = 0; i < pause.Length; i++)
            {
                //拿到数组下标 
                int index3 = pause[i];
                //地图中初始化为0
                Maps[index3] = 3;
            };

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                //拿到数组下标 
                int index4 = timeTunnel[i];
                //地图中初始化为0
                Maps[index4] = 4;
            };

        }//地图坐标设计
        public static void DrawMap()
        {

            //Console.ForegroundColor = ConsoleColor.Red;        
            Console.WriteLine("图例 ：{0}代表AB重叠 □代表普通 ⊙代表幸运大轮盘 ▲代表地雷  ◎代表暂停  〓代表时空");
            Console.WriteLine("{0}用A表示{1}用B表示",name[0],name[1]);
            //第一横行
            for (int i = 0; i < 30; i++)
            {
                //如果AB 在一个坐标 用<>表示
                DrawStringMap(i);
            }


            Console.WriteLine();
            //第一竖行
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <= 28; j++)
                {
                    Console.Write("  ");
                };
                DrawStringMap(i);

                Console.WriteLine();
            }
            //第三横行
            for (int i = 64; i >= 35; i--)
            {
                DrawStringMap(i);
            }
            Console.WriteLine();
            //第四竖行
            for (int i = 65; i < 70; i++)
            {
                DrawStringMap(i);
                for (int j = 1; j <= 29; j++)
                {
                    Console.Write("  ");
                };
                Console.WriteLine();
            }
            //第五横行
            for (int i = 70; i < 100; i++)
            {
                DrawStringMap(i);

            }
            Console.WriteLine();
        }

        public static void Playgame(int number)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);//随机数 1-6

            Console.WriteLine("{0}按任意键开始掷骰子", name[number]);
            Console.ReadKey(true);//为true不现实在屏幕
            Console.WriteLine("{0}掷出{1}", name[number], rNumber);
            PlayerPos[number] += rNumber;//玩家坐标相加
            ChangePos();

            Console.ReadKey(true);
            Console.WriteLine("{0}开始行动", name[number]);
            Console.ReadKey(true);
            Console.WriteLine("{0}行动完毕", name[number]);
            //玩家A踩到玩家B
            if (PlayerPos[0] == PlayerPos[1])
            {
                Console.WriteLine("玩家{0}到了玩家{1}", name[number], name[1 - number]);//1-number
                PlayerPos[1] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else//踩到关卡
            {

                switch (Maps[PlayerPos[number]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到方框，安全", name[number]);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到幸运大轮盘，选择1交换位置 选择2--轰炸对方使其后退6格", name[number]);
                        string input = Console.ReadLine();

                        while (true)
                        {
                            if (input == "1")
                            {

                                Console.WriteLine("玩家{0}和玩家{1}交换位置", name[number], name[1 - number]);
                                Console.ReadKey(true);
                                int temp = PlayerPos[number];
                                PlayerPos[number] = PlayerPos[1 - number];
                                PlayerPos[1 - number] = temp;
                                Console.WriteLine("交换完成！！！任意键继续");
                                ChangePos();
                                Console.ReadKey(true);
                                break;

                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("轰炸！！！玩家{0}退6格", name[1 - number]);
                                PlayerPos[1 - number] -= 6;
                                ChangePos();
                                Console.WriteLine("轰炸完毕，玩家{0}已经退6格", name[1 - number]);
                                break;
                            }

                            else
                            {
                                Console.WriteLine("只能输入1或者2");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退六格", name[number]);
                        PlayerPos[number] -= 6;
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", name[number]);
                        Flags[number] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", name[number]);
                        ChangePos();
                        Console.ReadKey(true);
                        break;
                }//switch
            }//else
           
            Console.Clear();
            DrawMap();


        }


        public static void DrawStringMap(int i)
        {
            if (PlayerPos[1] == PlayerPos[0] && PlayerPos[1] == i)
            {
                Console.Write("<>");//不能用Write Line
            }
            else if (PlayerPos[0] == i)
            {
                //shift+空格
                Console.Write("A");
            }
            else if (PlayerPos[1] == i)
            {
                Console.Write("B");
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("□");//记住write 不能用writeLine
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("⊙");
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("▲");
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("◎");
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("〓");
                        break;

                }
            }
        }//方框之类的显示
        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
            {
                PlayerPos[0] = 0;
            }
            if (PlayerPos[0] > 99)
            {
                PlayerPos[0] = 99;
            }
            if (PlayerPos[1] < 0)
            {
                PlayerPos[1] = 0;
            }
            if (PlayerPos[1] > 99)
            {
                PlayerPos[1] = 99;
            }
        }
        
    }
}
