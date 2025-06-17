using System;
using System.Threading;

class Program
{
    static char[][] level = new char[][]
    {
        "########################################".ToCharArray(),
        "#                                      #".ToCharArray(),
        "#        *                             #".ToCharArray(),
        "#      ####                            #".ToCharArray(),
        "#                 *                    #".ToCharArray(),
        "#                     ####             #".ToCharArray(),
        "#            *                         #".ToCharArray(),
        "########################################".ToCharArray()
    };

    static int width = 40;
    static int height = 8;

    static int playerX = 2;
    static int playerY = height - 2;
    static int velocityY = 0;
    static int coinCount = 0;

    static void Main()
    {
        Console.CursorVisible = false;
        while (true)
        {
            HandleInput();
            UpdatePhysics();
            Render();
            Thread.Sleep(100);
        }
    }

    static void HandleInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow)
            {
                if (playerX > 1 && level[playerY][playerX - 1] != '#')
                    playerX--;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (playerX < width - 2 && level[playerY][playerX + 1] != '#')
                    playerX++;
            }
            else if (key == ConsoleKey.Spacebar)
            {
                if (IsOnGround())
                    velocityY = -3;
            }
        }
    }

    static void UpdatePhysics()
    {
        if (velocityY < 0)
        {
            if (playerY > 1 && level[playerY - 1][playerX] != '#')
            {
                playerY--;
                velocityY++;
            }
            else
            {
                velocityY = 0;
            }
        }
        else
        {
            if (!IsOnGround())
            {
                if (playerY < height - 2 && level[playerY + 1][playerX] != '#')
                    playerY++;
            }
        }

        if (level[playerY][playerX] == '*')
        {
            coinCount++;
            level[playerY][playerX] = ' ';
        }
    }

    static bool IsOnGround()
    {
        return playerY + 1 >= height || level[playerY + 1][playerX] == '#';
    }

    static void Render()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x == playerX && y == playerY)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('M');
                }
                else
                {
                    char c = level[y][x];
                    if (c == '#')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (c == '*')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(c);
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.WriteLine($"Coins: {coinCount}");
    }
}
