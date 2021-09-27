using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;

namespace Grid
{
    public enum CellState
    {
        Alive,
        Dead
    }

    public class CellShape
    {
        public RectangleShape Shape;
        public Color AliveColour = Color.White;
        public Color DeadColour = Color.Black;

        public CellShape(float Width, float Height)
        {
            Shape = new RectangleShape(new Vector2f(Width, Height));
            Shape.OutlineColor = Color.White;
            Shape.OutlineThickness = 1;
        }
        public void Draw(RenderTarget target, float X, float Y, CellState state)
        {
            Shape.Position = new Vector2f(X * Shape.Size.X, Y * Shape.Size.Y);
            Shape.FillColor = state == CellState.Alive ? AliveColour : DeadColour;
            target.Draw(Shape);
        }
    }
    public class Grid
    {
        CellState[,] cells, buffer;
        CellShape cellShape;

        bool bRun = false;
        float _width;
        float _height;
        int _cellsPerRow;


        public Grid(float Width, float Height, int CellsPerRow)
        {
            _width = Width;
            _height = Height;
            _cellsPerRow = CellsPerRow;

            Initialise();
        }

        protected void Initialise()
        {
            cells = new CellState[_cellsPerRow, _cellsPerRow];
            buffer = new CellState[_cellsPerRow, _cellsPerRow];
            cellShape = new CellShape(_width / _cellsPerRow, _height / _cellsPerRow);
            

            for (int y = 0; y < _cellsPerRow; y++)
            {
                for (int x = 0; x < _cellsPerRow; x++)
                {
                    cells[x, y] = CellState.Dead;
                }
            }
        }

        public void Update()
        {
            if (!bRun) return;

            // copy grid state to buffer
            buffer = (CellState[,])cells.Clone();

            // conway rules
            for (int y = 0; y < _cellsPerRow; y++)
            {
                for (int x = 0; x < _cellsPerRow; x++)
                {
                    ConwayCheck(x, y);
                }
                //Console.WriteLine("");
            }
            //Console.WriteLine("");

            // write buffer back to grid state
            cells = (CellState[,])buffer.Clone();
        }

        public void Draw(RenderTarget target)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    cellShape.Draw(target, x, y, cells[x, y]);
                }
            }
        }

        protected void ConwayCheck(int x, int y)
        {
            int aliveNeighbours = 0;

            // check neighbouring cells for life state
            for (int j = y - 1; j <= y + 1; j++)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    if (i < 0 || i >= cells.GetLength(0) || j < 0 || j >= cells.GetLength(0))
                    { }
                    else if (i == x && j == y)
                    { }
                    else if (cells[i, j] == CellState.Alive)
                        aliveNeighbours++;

                }
            }
            //Console.Write($"{aliveNeighbours},");
            //Console.WriteLine($"Cell[{x},{y}]. Neighbours: {aliveNeighbours}");

            // apply changes based on neighbour count
            if (cells[x, y] == CellState.Alive)
            {
                // 0, 1 = lonely = die
                // > 3 = overcrowded = die
                // 2, 3 = stay living
                if (aliveNeighbours < 2 || aliveNeighbours > 3)
                    buffer[x, y] = CellState.Dead;
            }
            else
            {
                // 3 = new life
                // < 3 or > 3 = no change
                if (aliveNeighbours == 3)
                    buffer[x, y] = CellState.Alive;
            }
        }

        public void TogglePause()
        {
            bRun = !bRun;
        }

        public void Reset()
        {
            bRun = false;
            Initialise();
        }

        public void FillRandom(double fillRate)
        {
            Random random = new Random();
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    cells[x, y] = random.NextDouble() < fillRate ? CellState.Alive : CellState.Dead;
                }
            }
        }
        public void SetCell(Vector2i hit, CellState newState)
        {
            Vector2i hitCell = HitToCell(hit);
            cells[hitCell.X, hitCell.Y] = newState;
        }

        protected Vector2i HitToCell(Vector2i hit)
        {
            return new Vector2i(
                (int)(RoundDownToNearest(hit.X, (int)cellShape.Shape.Size.X) / cellShape.Shape.Size.X),
                (int)(RoundDownToNearest(hit.Y, (int)cellShape.Shape.Size.Y) / cellShape.Shape.Size.Y)
            );
        }

        protected int RoundDownToNearest(int value, int multiple)
        {
            int rounded = value - value % multiple;
            return rounded >= 0 ? rounded : 0;
        }
    }
}
