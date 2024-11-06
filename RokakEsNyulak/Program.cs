// Ikt Projektmunka: The game of life
// Pongrácz Gábor, Farkas Botond

using RokakEsNyulakLib;
using System;

public class Program
{
    public static void Main()
    {
        Field[,] grid = new Field[10, 10]; // Létrehozunk egy 10x10-es rácsot

        // Random
        Random random = new Random();

        // Rács inicializálása
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = new Field();
            }
        }

        // Nyúl és róka létrehozása
        int xR = random.Next(10);
        int yR = random.Next(10);
        Rabbit rabbit = new Rabbit(xR, yR);
        grid[xR, yR].Rabbit = rabbit;

        int xR2 = random.Next(10);
        int yR2 = random.Next(10);
        Rabbit rabbit2 = new Rabbit(xR2, yR2);
        grid[xR2, yR2].Rabbit = rabbit2;

        int xF = random.Next(10);
        int yF = random.Next(10);
        Fox fox = new Fox(xF, yF);
        grid[xF, yF].Fox = fox;

        // 10 szimulációs kör futtatása
        for (int turn = 0; turn < 50; turn++)
        {
            // Róka mozgása és táplálkozása
            if (fox.IsAlive == true)
            {
                if (rabbit.IsAlive == false) { fox.Move(grid, (rabbit.X + 1000, rabbit.Y + 1000), (rabbit2.X, rabbit2.Y)); }
                else if (rabbit2.IsAlive == false) { fox.Move(grid, (rabbit.X, rabbit.Y), (rabbit2.X + 1000, rabbit2.Y + 1000)); }
                else { fox.Move(grid, (rabbit.X, rabbit.Y), (rabbit2.X, rabbit2.Y)); }
                fox.HuntRabbit(grid);
                fox.DecreaseHunger(grid);
            }

            // Nyúl mozgása és táplálkozása
            if (rabbit.IsAlive == true)
            {
                rabbit.Eat(grid);
                rabbit.Move(grid);
                rabbit.DecreaseHunger();
            }

            // Nyúl mozgása és táplálkozása
            if (rabbit2.IsAlive == true)
            {
                rabbit2.Eat(grid);
                rabbit2.Move(grid);
                rabbit2.DecreaseHunger();
            }

            // Megjelenítés
            Console.Clear();
            Console.WriteLine($"Kör {turn + 1}:");

            // Rács állapotának kiírása
            PrintGrid(grid);

            // Fű növekedése
            foreach (var field in grid)
            {
                field.GrowGrass();
            }

            // Várakoztatás
            Thread.Sleep(300);
        }
    }

    // A rács aktuális állapotának megjelenítése konzolon
    public static void PrintGrid(Field[,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j].Rabbit != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("N "); // Nyúl
                    Console.ResetColor();
                }
                else if (grid[i, j].Fox != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("R "); // Róka 
                    Console.ResetColor();
                }
                else if (grid[i, j].GrassState == 0)
                {
                    Console.Write(". "); // Zsenge fűcsomó
                }
                else if (grid[i, j].GrassState == 1)
                {
                    Console.Write(", "); // Friss fűcsomó
                }
                else if (grid[i, j].GrassState == 2)
                {
                    Console.Write(": "); // Erős fűcsomó
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}