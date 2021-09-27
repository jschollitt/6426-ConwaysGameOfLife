using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Camera
{
    public static class MathLib
    {
        public static int RandomInt(int min, int max)
        {
            return PRandom.Instance.RandomInt(min, max);
        }

        public static float RandomFloat(float min, float max)
        {
            return PRandom.Instance.RandomFloat(min, max);
        }

        public static void Normalise(Vector2f vector)
        {
            float mag = (float)Magnitude(vector);
            vector = vector * mag;
        }

        public static double Magnitude(Vector2f vector)
        {
            return Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
        }

        public static void ClampVector(Vector2f vector, float min, float max)
        {
            float x = ClampFloat(vector.X, min, max);
            float y = ClampFloat(vector.Y, min, max);
            vector = new Vector2f(x, y);
        }

        public static float ClampFloat(float value, float min, float max)
        {
            if (value > max) value = max;
            if (value < min) value = min;
            return value;
        }

        public static bool RectIntersect(float x, float y, float w, float h, float x1, float y1, float w1, float h1)
        {
            return x1 > x && x1 < x + w && y1 > y && y1 < y + h;
        }

        public static bool RectIntersect(Vector2f pos1, Vector2f size1, Vector2f pos2, Vector2f size2)
        {
            return RectIntersect(pos1.X, pos1.Y, size1.X, size1.Y, pos2.X, pos2.Y, size2.X, size2.Y);
        }

        public static bool IsPointInRect(float x, float y, float w, float h, float pointX, float pointY)
        {
            return RectIntersect(x, y, w, h, pointX, pointY, 0, 0);
        }

        public static bool IsPointInRect(Vector2f pos, Vector2f size, Vector2f point)
        {
            return IsPointInRect(pos.X, pos.Y, size.X, size.Y, point.X, point.Y);
        }

        public static float Lerp(float start, float end, float value)
        {
            return start + (end - start) * value;
        }

        public static Vector2f FindCenterOfRect(FloatRect rect)
        {
            float centreX = rect.Left + (rect.Width * 0.5f);
            float centreY = rect.Top + (rect.Height * 0.5f);
            return new Vector2f(centreX, centreY);
        }
    }
}
