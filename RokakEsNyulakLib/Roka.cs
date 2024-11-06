using System;
using System.Collections.Generic;
using System.Linq;

namespace RokakEsNyulakLib
{
    public class Fox
    {
        public int HungerLevel { get; set; }
        public bool IsAlive { get; set; }

        public Fox()
        {
            HungerLevel = 10;
            IsAlive = true;
        }

        public void HuntRabbit(Field field)
        {
            if (field.Rabbit != null)
            {
                field.Rabbit = null; // Nyúl elpusztítása
                HungerLevel = Math.Min(HungerLevel + 3, 10); // Jóllakottság növelése
            }
        }

        public void DecreaseHunger()
        {
            HungerLevel--;
            if (HungerLevel <= 0)
            {
                IsAlive = false; // Róka elpusztul, ha éhen hal
            }
        }

        // Róka véletlenszerű mozgása
        public void Move(Field[,] grid, int x, int y, Random random)
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

            // Véletlenszerű sorrendben próbálkozunk
            directions = directions.OrderBy(d => random.Next()).ToList();

            foreach (var dir in directions)
            {
                int newX = x + dir[0];
                int newY = y + dir[1];

                // Ellenőrizzük, hogy az új koordináták a rácson belül vannak-e
                if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
                {
                    // Üres mezőre mozgás
                    if (grid[newX, newY].Rabbit == null && grid[newX, newY].Fox == null)
                    {
                        grid[newX, newY].Fox = this; // Róka új pozíció
                        grid[x, y].Fox = null; // Az eredeti mező most üres
                        break; // Ha mozog, kilép a ciklusból
                    }
                }
            }
        }
    }
}
