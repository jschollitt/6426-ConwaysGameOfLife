using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;

namespace SFML_Camera
{
    public class GridCell<T>
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public List<T> objects { get; set; }
    }
}
