using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using SFML.Graphics;
using SFML.System;

namespace SFML_Camera
{
    public class Walker
    {
        public VertexArray vertexArray;
        Color colour;
        public Walker(float x, float y, Color colour) : this(new Vector2f(x, y), colour) { }
        public Walker(Vector2f position, Color colour)
        {
            this.colour = colour;
            vertexArray = new VertexArray(PrimitiveType.LineStrip);
            vertexArray.Append(new Vertex(position, this.colour));
        }

        public void Walk()
        {
            Vector2f currentPosition = vertexArray[vertexArray.VertexCount - 1].Position;
            //int moveX = MathLib.RandomInt(-1, 1);
            //int moveY = MathLib.RandomInt(-1, 1);
            int move = MathLib.RandomInt(1, 8);
            //     1 2 3
            //     4   5
            //     6 7 8
            int x = 0, y = 0;
            if (move < 4) x = -1;
            else if (move > 5) x = 1;
            if (move == 1 || move == 4 || move == 6) y = -1;
            else if (move == 3 || move == 5 || move == 8) y = 1;

            Vector2f newPosition = new Vector2f(x, y);
            vertexArray.Append(new Vertex(newPosition + currentPosition, colour));
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(vertexArray);
        }

        public FloatRect GetBounds()
        {
            return vertexArray.Bounds;
        }

        public void SaveAsSVG(string path)
        {
            //// locals
            //FloatRect rect = vertexArray.Bounds;
            //int width = (int)rect.Width;
            //int height = (int)rect.Height;

            //// build vertex array as string
            //StringBuilder sb = new StringBuilder();
            //for (uint i = 0; i < vertexArray.VertexCount; i++)
            //{
            //    Vertex vertex = vertexArray[i];
            //    sb.Append($"{vertex.Position.X},{vertex.Position.Y} ");
            //    Console.WriteLine(i.ToString());
            //}
            //Console.WriteLine(vertexArray.VertexCount.ToString());

            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Indent = true;
            //settings.NewLineChars = "\n";
            //// open stream
            //FileStream fStream = new FileStream(path, FileMode.OpenOrCreate);
            //XmlWriter writer = XmlWriter.Create(fStream, settings);

            //// write XML
            //writer.WriteStartDocument();
            //writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
            //writer.WriteAttributeString("width", $"{vertexArray.Bounds.Width}cm");
            //writer.WriteAttributeString("height", $"{vertexArray.Bounds.Height}cm");
            //writer.WriteAttributeString("viewBox", $"{rect.Left - 10} {rect.Top - 10} {rect.Width + 10} {rect.Height + 10}");
            //writer.WriteAttributeString("style", "background-color:black");
            //writer.WriteAttributeString("xmlns", @"http://www.w3.org/2000/svg");
            //writer.WriteAttributeString("version", "1.1");

            //writer.WriteStartElement("polyline");
            //writer.WriteAttributeString("fill", "none");
            //writer.WriteAttributeString("stroke", $"#{BitConverter.ToString(new byte[] { colour.R, colour.G, colour.B }).Replace("-", "")}");
            //writer.WriteAttributeString("stroke-width", "0.02");
            //writer.WriteAttributeString("stroke-opacity", BitConverter.ToString(new byte[] { colour.A }).Replace("-", ""));
            //writer.WriteAttributeString("points", sb.ToString());
            //writer.WriteEndElement();

            //writer.WriteEndElement();
            //writer.WriteEndDocument();
            //writer.Close();
            //fStream.Close();
        }
    }
}
