using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameProg_RPGMap_BenjaminMackey
{
    internal class Program
    {
        static bool runScreen = true;


        static char[,] map = new char[,] // dimensions defined by following data:
        {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`'},
        {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        };

        

        static int Clamp(int num, int maxNum, int MinNum)
        {
            if (num > maxNum) return maxNum;
            else if(num < MinNum) return MinNum;
            else return num;
        }

        static float Clamp(float num, float maxNum, float MinNum)
        {
            if (num > maxNum) return maxNum;
            else if (num < MinNum) return MinNum;
            else return num;
        }

        static void requestSizeChange(int magnifier)
        {
            Console.CursorVisible = false;
            magnifier = Clamp(magnifier, 3, 1);
            mapSize = magnifier;
            Console.Clear();
        }
        static int mapSize = 1;

        
        static async void DisplayMapToScreen()
        {
            while (runScreen)
            {
                Console.SetCursorPosition(4 * mapSize - 1, 2 * mapSize -1);
                Console.Write("+");
                for (int i = 0; i < map.GetLength(1) * mapSize; i++)
                {
                 
                    Console.Write("-");
                }
                Console.Write("+");
                for (int i = 0; i < map.GetLength(0); i++)
                {
                  
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        Console.SetCursorPosition((j + 4) *  mapSize, (i + 2) * mapSize);
                        switch (map[i, j])
                        {
                            case '~':
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case '`':
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case '^':
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case '*':
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                        }
                        ConsoleColor bounceback = Console.ForegroundColor;
                        for (int k = 0; k < mapSize; k++)
                        {
                            for (int h = 0; h < (mapSize); h++)
                            {
                                Console.ForegroundColor = bounceback;
                                Console.SetCursorPosition(((j + 4) * mapSize) + k, ((i + 2) * mapSize) + h);
                                Console.WriteLine(map[i, j]);
                                bounceback = Console.ForegroundColor;
                                if (j == map.GetLength(1) - 1 )
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.SetCursorPosition(((j + 4) * mapSize) + mapSize, ((i + 2) * mapSize) + h);
                                    Console.WriteLine("|");
                                }
                                else if(j == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.SetCursorPosition(((j + 4) * mapSize) - 1, ((i + 2) * mapSize) + h);
                                    Console.WriteLine("|");
                                }
                            }
                            
                        }
                        
                    }

                }
                Console.SetCursorPosition(4 * mapSize - 1, ((map.GetLength(0) + 2) * mapSize));
                Console.Write("+");
                for (int i = 0; i < map.GetLength(1) * mapSize; i++)
                {
                    
                    Console.Write("-");
                }
                Console.Write("+");
                await Task.Delay(100);
            }

        }

        static void InputRunner()
        {
            while(true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        requestSizeChange(Clamp(mapSize + 1, 3, 1));
                        break;
                    case ConsoleKey.DownArrow:
                        requestSizeChange(Clamp(mapSize - 1, 3, 1));
                        break;
                }
            }
            
        }

        static void Main(string[] args)
        {
            Console.WindowHeight = (map.GetLength(0) + 4) * 3;
            Console.WindowWidth = (map.GetLength(1) + 4) * 3;
            requestSizeChange(1);
            DisplayMapToScreen();
            InputRunner();

        }
    }
}
