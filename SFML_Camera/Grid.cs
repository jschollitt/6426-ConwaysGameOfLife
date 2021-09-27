using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Camera
{
    public class Grid
    {
        public FloatRect bounds;
        public GridCell<Shape>[,] cells;
        public RectangleShape shape;
        public Grid(FloatRect bounds, int cols, int rows)
        {
            this.bounds = bounds;
            Initialise(cols, rows);
            shape = new RectangleShape();
            shape.FillColor = new Color(100, 100, 100, 100);
            shape.OutlineColor = Color.White;
            shape.OutlineThickness = 1f;
        }

        public Grid(float left, float top, float right, float bottom, int cols, int rows) 
            : this(new FloatRect(left, top, right, bottom), cols, rows)
        {
        }

        public void Initialise(int cols, int rows)
        {
            float cellWidth = bounds.Width / cols;
            float cellHeight = bounds.Height / rows;
            cells = new GridCell<Shape>[cols, rows];

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    float x = bounds.Left + (cellWidth * i) - (cellWidth / 2);
                    float y = bounds.Top + (cellHeight * j) - (cellHeight / 2);

                    cells[i, j] = new GridCell<Shape>
                    {
                        Position = new Vector2f(x, y),
                        Size = new Vector2f(cellWidth, cellHeight),
                        objects = new List<Shape>()
                    };
                }
            }
        }
        public void Update()
        {

        }

        public void Draw(RenderTarget target)
        {
            foreach (GridCell<Shape> cell in cells)
            {
                shape.Position = cell.Position;
                shape.Size = cell.Size;
                target.Draw(shape);
            }
        }
    }
}
