// Ikt Projektmunka: The game of life


using RokakEsNyulakLib;
using System;

public class Program
{
    public static void Main()
    {
        Field[,] grid = new Field[10, 10]; // Létrehozunk egy 10x10-es rácsot
        Random random = new Random(); // Véletlenszám generátor

        // Rács inicializálása
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = new Field();
            }
        }

        // Nyúl és róka létrehozása
        Rabbit rabbit = new Rabbit();
        Fox fox = new Fox();

        // Nyúl és róka elhelyezése a rácson
        grid[5, 5].Rabbit = rabbit;
        grid[0, 0].Fox = fox;

        // Futassunk 10 kört a szimulációból
        for (int turn = 0; turn < 10; turn++)
        {
            Console.WriteLine($"Turn {turn + 1}:");

            // Nyúl aktuális pozíciója
            int rabbitX = 5, rabbitY = 5;

            // Nyúl mozgása és táplálkozása
            rabbit.Move(grid, rabbitX, rabbitY, random);
            rabbit.Eat(grid[rabbitX, rabbitY]);
            rabbit.DecreaseHunger();

            // Nyúl pozíciójának frissítése
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].Rabbit == rabbit)
                    {
                        rabbitX = i;
                        rabbitY = j;
                        break;
                    }
                }
            }

            // Róka aktuális pozíciója
            int foxX = 0, foxY = 0;

            // Róka mozgása és táplálkozása
            fox.Move(grid, foxX, foxY, random);
            fox.HuntRabbit(grid[foxX, foxY]);
            fox.DecreaseHunger();

            // Róka pozíciójának frissítése
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j].Fox == fox)
                    {
                        foxX = i;
                        foxY = j;
                        break;
                    }
                }
            }

            // Fű növekedése
            foreach (var field in grid)
            {
                field.GrowGrass();
            }

            // Rács állapotának kiírása
            PrintGrid(grid);
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
                    Console.Write("R "); // Nyúl
                }
                else if (grid[i, j].Fox != null)
                {
                    Console.Write("F "); // Róka
                }
                else
                {
                    Console.Write(". "); // Üres mező
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
