using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RokakEsNyulakLib
{
    public class Rabbit
    {
        public int HungerLevel { get; set; }
        public bool IsAlive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Rabbit(int x, int y)
        {
            HungerLevel = 5;
            IsAlive = true;
            X = x;
            Y = y;
        }

        public void Eat(Field[,] field)
        {
            if (field[X, Y].GrassState == 1) // Zsenge fű
            {
                HungerLevel++;
                field[X, Y].GrassState = 0; // Fű állapotának csökkentése
            }
            else if (field[X, Y].GrassState == 2) // Kifejlett fű
            {
                HungerLevel += 2;
                field[X, Y].GrassState = 0; // Fű állapotának csökkentése
            }
        }

        public void DecreaseHunger()
        {
            HungerLevel--;
            if (HungerLevel <= 0)
            {
                IsAlive = false; // Nyúl elpusztul, ha éhen hal
            }
        }

        // Nyúl mozgása egy szomszédos üres mezőre véletlenszerűen
        public void Move(Field[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            // Lehetséges irányok (fel, le, balra, jobbra)
            List<int[]> directions = new List<int[]>
            {
                new int[] { -1, 0 }, // fel
                new int[] { 1, 0 },  // le
                new int[] { 0, -1 }, // balra
                new int[] { 0, 1 }   // jobbra
            };

            Random r = new Random();

            // Véletlenszerű sorrendben próbálkozunk
            directions = directions.OrderBy(d => r.Next()).ToList();

            foreach (var dir in directions)
            {
                int newX = X + dir[0];
                int newY = Y + dir[1];

                // Ellenőrizzük, hogy az új koordináták a rácson belül vannak-e
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                {
                    // Üres mezőre mozgás
                    if (grid[newX, newY].Rabbit == null)
                    {
                        grid[newX, newY].Rabbit = this; // Nyúl új pozíció
                        grid[X, Y].Rabbit = null; // Az eredeti mező most üres
                        X = newX;
                        Y = newY;
                        break; // Ha mozog, kilép a ciklusból
                    }
                }
            }
        }
    }
}
