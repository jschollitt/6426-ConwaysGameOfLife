using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Camera
{
    public class GameObject
    {
        protected Vector2f position;
        protected Vector2f velocity;
        protected float radius;
        protected CircleShape shape;
        public GameObject(float x, float y, float radius)
        {
            position = new Vector2f(x, y);
            velocity = new Vector2f(1, 0);
            this.radius = radius;
            shape = new CircleShape(radius);
            shape.Position = position;
        }
        public void Update()
        {
            position += velocity;
            if (position.X < 0 || position.X > 1000) velocity *= -1;
            
            shape.Position = position;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(shape);
        }

        public void SetColour(Color colour)
        {
            shape.FillColor = colour;
        }
    }
}
