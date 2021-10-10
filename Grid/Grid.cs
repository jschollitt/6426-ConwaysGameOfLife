using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;

namespace CellularAutomata
{
    public class CellShape
    {
        public RectangleShape Shape;
        public Color StateOneColour = Color.White;
        public Color StateTwoColour = Color.Black;

        public CellShape(float Width, float Height)
        {
            Shape = new RectangleShape(new Vector2f(Width, Height));
            //Shape.OutlineColor = Color.White;
            //Shape.OutlineThickness = 1;
        }
        public void Draw(RenderTarget target, float X, float Y, Color colour)
        {
            Shape.Position = new Vector2f(X * Shape.Size.X, Y * Shape.Size.Y);
            Shape.FillColor = colour;
            target.Draw(Shape);
        }
    }
    public class Grid
    {
        int[,] cells, buffer;
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
            cells = new int[_cellsPerRow, _cellsPerRow];
            buffer = new int[_cellsPerRow, _cellsPerRow];
            cellShape = new CellShape(_width / _cellsPerRow, _height / _cellsPerRow);


            for (int y = 0; y < _cellsPerRow; y++)
            {
                for (int x = 0; x < _cellsPerRow; x++)
                {
                    cells[x, y] = 0;
                }
            }
        }

        public void Update()
        {
            if (!bRun) return;

            // copy grid state to buffer
            buffer = (int[,])cells.Clone();

            // conway rules
            for (int y = 0; y < _cellsPerRow; y++)
            {
                for (int x = 0; x < _cellsPerRow; x++)
                {
                    buffer[x, y] = Rule.UpdateState(cells, x, y);
                }
                //Console.WriteLine("");
            }
            //Console.WriteLine("");

            // write buffer back to grid state
            cells = (int[,])buffer.Clone();
        }

        public void Draw(RenderTarget target)
        {
            for (int y = 0; y < cells.GetLength(0); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    cellShape.Draw(target, x, y, Rule.GetStateColour(cells[x, y]));
                }
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
                    cells[x, y] = random.NextDouble() < fillRate ? 1 : 0;
                }
            }
        }
        public void SetCell(Vector2i hit, int newState)
        {
            Vector2i hitCell = HitToCell(hit);
            if (hitCell.X >= cells.GetLength(0) || hitCell.Y >= cells.GetLength(1))
                return;
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
