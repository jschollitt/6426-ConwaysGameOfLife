using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;

namespace CellularAutomata
{
    static class Rule
    {
        static ConwayRules conwayRules = new ConwayRules();
        static BrianRules brianRules = new BrianRules();
        static WireRules wireRules = new WireRules();
        public enum RuleSet
        {
            ConwaysGameOfLife,
            BriansBrain,
            WireWorld
        }

        public static RuleSet CurrentRule = RuleSet.ConwaysGameOfLife;

        public static void ChangeRule()
        {
            CurrentRule = (RuleSet)(((int)CurrentRule + 1) % Enum.GetNames(typeof(RuleSet)).Length);
            Console.WriteLine(CurrentRule);
        }
        public static int UpdateState(int[,] grid, int X, int Y)
        {
            switch(CurrentRule)
            {
                case RuleSet.ConwaysGameOfLife:
                    return conwayRules.Update(grid, X, Y);
                case RuleSet.BriansBrain:
                    return brianRules.Update(grid, X, Y);
                case RuleSet.WireWorld:
                    return wireRules.Update(grid, X, Y);
                default:
                    return grid[X, Y];
            }
        }

        public static Color GetStateColour(int state)
        {
            switch (CurrentRule)
            {
                case RuleSet.ConwaysGameOfLife:
                    return conwayRules.GetColor(state);
                case RuleSet.BriansBrain:
                    return brianRules.GetColor(state);
                case RuleSet.WireWorld:
                    return wireRules.GetColor(state);
                default:
                    return Color.Black;
            }
        }

        public static int CountNeighboursOfState(int[,] grid, int x, int y, int State, bool wrap = true)
        {
            int stateCount = 0;
            for (int j = y - 1; j <= y + 1; j++)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    int neighbourX = i;
                    int neighbourY = j;

                    if (wrap) // wrap coords to other side of grid
                    {
                        neighbourX = WrapIndex(neighbourX, grid.GetLength(0));
                        neighbourY = WrapIndex(neighbourY, grid.GetLength(1));
                    }
                    else // not wrapping, check for illegal array access
                    {
                        if (i < 0 || i >= grid.GetLength(0) || j < 0 || j >= grid.GetLength(0))
                        { 
                            continue;
                        }
                    }
                    
                    if (neighbourX == x && neighbourY == y) // wrap or not, don't count self
                    { 
                        continue;
                    }
                    else if (grid[neighbourX, neighbourY] == State) // valid neighbour, check if matching
                    {
                        stateCount++;
                    }
                }
            }
            return stateCount;
        }

        static int WrapIndex(int index, int dimensionSize)
        {
            if (index < 0) return index + dimensionSize;
            else if (index >= dimensionSize) return index % dimensionSize;
            else return index;
        }
    }
}
