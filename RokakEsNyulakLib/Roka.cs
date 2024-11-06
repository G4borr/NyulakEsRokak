using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RokakEsNyulakLib
{
    public class Fox
    {
        public int HungerLevel { get; set; }
        public bool IsAlive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Fox(int x, int y)
        {
            HungerLevel = 20;
            IsAlive = true;
            X = x;
            Y = y;
        }

        // Nyúl levadászása
        public void HuntRabbit(Field[,] field)
        {
            if (field[X, Y].Rabbit != null)
            {
                field[X, Y].Rabbit.IsAlive = false;
                field[X, Y].Rabbit = null; // Nyúl elpusztítása
                HungerLevel += 10; // Jóllakottság növelése
                Debug.WriteLine("Róka evett");
            }
        }

        // Éhség csökkentése
        public void DecreaseHunger(Field[,] field)
        {
            HungerLevel--;
            if (HungerLevel <= 0)
            {
                IsAlive = false; // Róka elpusztul, ha éhen hal
                field[X, Y].Fox = null;
                Debug.WriteLine("Róka éhenhalt");
            }
        }

        // Róka mozgása
        public void Move(Field[,] grid, (int, int) nyulPos, (int, int) nyulPos2)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            int xMove = 0;
            int yMove = 0;

            // Számított távolság a nyúltól
            int dFrom1Rabbit = Math.Abs(X - nyulPos.Item1) + Math.Abs(Y - nyulPos.Item2);
            int dFrom2Rabbit = Math.Abs(X - nyulPos2.Item1) + Math.Abs(Y - nyulPos2.Item2);

            // Közelebbi nyúl követése 

            if (dFrom1Rabbit <= dFrom2Rabbit)
            {
                Debug.WriteLine("Követés: Rabbit1");
                if (X != nyulPos.Item1)
                {
                    if (X > nyulPos.Item1)
                    { xMove = -1; }
                    else
                    { xMove = 1; }
                }
                else
                {
                    if (Y > nyulPos.Item2)
                    { yMove = -1; }
                    else
                    { yMove = 1; }
                }
            }
            else {
                Debug.WriteLine("Követés: Rabbit2");
                if (X != nyulPos2.Item1)
                {
                    if (X > nyulPos2.Item1)
                    { xMove = -1; }
                    else
                    { xMove = 1; }
                }
                else
                {
                    if (Y > nyulPos2.Item2)
                    { yMove = -1; }
                    else
                    { yMove = 1; }
                }
            }

            int newX = X + xMove;
            int newY = Y + yMove;

            // Ellenőrizzük, hogy az új koordináták a rácson belül vannak-e
            if (newX >= 0 && newX < rows && newY >= 0 && newY < cols)
            {
                grid[newX, newY].Fox = this; // Róka új pozíció
                grid[X, Y].Fox = null; // Az eredeti mező most üres
                X = newX;
                Y = newY;
            }
        }
    }
}
