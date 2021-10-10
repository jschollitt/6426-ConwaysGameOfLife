using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace CellularAutomata
{
    public interface IRule
    {
        public int Update(int[,] grid, int x, int y);
        public Color GetColor(int state);
    }

    public class ConwayRules : IRule
    {
        static Color ConwayAlive = Color.White;
        static Color ConwayDead = Color.Black;

        public int Update(int[,] grid, int x, int y)
        {
            // Conway neighbour alive count
            int aliveNeighbours = Rule.CountNeighboursOfState(grid, x, y, 1);

            // Conway conditional updates based on neighbour count
            if (grid[x, y] == 1) // alive
            {
                // 0, 1 = lonely = die
                // > 3 = overcrowded = die
                // 2, 3 = stay living
                if (aliveNeighbours < 2 || aliveNeighbours > 3)
                    return 0;
            }
            else
            {
                // 3 = new life
                // < 3 or > 3 = no change
                if (aliveNeighbours == 3)
                    return 1;
            }
            return grid[x, y];
        }

        public Color GetColor(int state)
        {
            switch (state)
            {
                case 1:
                    return ConwayAlive;
                default:
                    return ConwayDead;
            }
        }
    }

    public class BrianRules : IRule
    {
        static Color BrianOn = Color.White;
        static Color BrianOff = Color.Blue;
        static Color BrianReady = Color.Black;

        public int Update(int[,] grid, int x, int y)
        {
            switch (grid[x, y])
            {
                case 0: // off
                    if (Rule.CountNeighboursOfState(grid, x, y, 1) == 2)
                    {
                        return 1;
                    }
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 0;
                default:
                    return 0;
            }
        }

        public Color GetColor(int state)
        {
            switch (state)
            {
                case 1:
                    return BrianOn;
                case 2:
                    return BrianOff;
                default:
                    return BrianReady;
            }
        }
    }

    public class WireRules : IRule
    {
        static Color WireHead = Color.Blue;
        static Color WireTail = Color.Red;
        static Color WireConductor = Color.Yellow;
        static Color WireEmpty = Color.Black;

        public int Update(int[,] grid, int x, int y)
        {
            switch (grid[x, y])
            {
                case 1: // head
                    return 2;
                case 2: // tail
                    return 3;
                case 3: // conductor
                    int heads = Rule.CountNeighboursOfState(grid, x, y, 1);
                    if (heads == 1 || heads == 2)
                        return 1;
                    return 3;
                default: // empty
                    return 0;
            }
        }

        public Color GetColor(int state)
        {
            switch (state)
            {
                case 1:
                    return WireHead;
                case 2:
                    return WireTail;
                case 3:
                    return WireConductor;
                default:
                    return WireEmpty;
            }
        }
    }
}
