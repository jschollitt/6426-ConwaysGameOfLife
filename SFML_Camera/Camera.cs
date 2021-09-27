using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Camera
{
    public class Camera
    {
        protected RenderWindow window;
        public View cameraView;
        protected Vector2f originalSize;
        protected float zoomChange = 0.01f;

        Vector2f currentCentre;
        Vector2f targetCentre;
        float targetZoom = 0.0f;
        float currentZoom = 0.0f;
        float previousZoom = 0.0f;
        bool isZooming = false;

        bool isDragging = false;
        Vector2f dragStart;

        public Camera(RenderWindow window, FloatRect rect)
        {
            this.window = window;
            cameraView = new View(rect);
            originalSize = cameraView.Size;
        }

        public void SetBounds(FloatRect rect)
        {
            cameraView.Viewport = rect;
        }

        public void Update()
        {
            if (isZooming)
            {
                currentZoom = MathLib.Lerp(currentZoom, targetZoom, zoomChange);
                cameraView.Zoom(1.0f + (currentZoom - previousZoom));

                currentCentre.X = MathLib.Lerp(currentCentre.X, targetCentre.X, zoomChange);
                currentCentre.Y = MathLib.Lerp(currentCentre.Y, targetCentre.Y, zoomChange);
                cameraView.Center = currentCentre;

                previousZoom = currentZoom;

                if (MathF.Abs(targetZoom - currentZoom) < 0.01f)
                    isZooming = false;
            }
        }

        public void Zoom(float delta, bool fast)
        {
            
            //delta *= -1;
            //float adjust = fast ? 5 * zoomChange : zoomChange;
            //cameraView.Size *= (delta > 0) ? 1 + adjust : 1 - adjust;
        }

        public void ZoomToTarget(float delta, bool fast, int x, int y)
        {
            ZoomToTarget(delta, fast, new Vector2i(x, y));
        }
        public void ZoomToTarget(float delta, bool fast, Vector2i position)
        {
            isZooming = true;
            delta *= -1;

            if (delta > 0)
                targetZoom = 1.3f;
            else
                targetZoom = 0.7f;

            currentZoom = 1.0f;
            previousZoom = currentZoom;
            currentCentre = cameraView.Center;
            targetCentre = window.MapPixelToCoords(position, cameraView);
        }

        public void ResetZoom()
        {
            cameraView.Size = originalSize;
        }

        public void Move(float x, float y)
        {
            Move(new Vector2f(x, y));
        }

        public void Move(Vector2f movement)
        {
            //Console.WriteLine($"move {movement}");
            cameraView.Move(movement);
        }

        public void StartDrag(int x, int y)
        {
            StartDrag(new Vector2i(x, y));
        }
        public void StartDrag(Vector2i position)
        {
            isDragging = true;
            dragStart = window.MapPixelToCoords(position);
        }

        public void StopDrag()
        {
            isDragging = false;
        }

        public void Drag(int x, int y)
        {
            Drag(new Vector2i(x, y));
        }

        public void Drag(Vector2i position)
        {
            if (isDragging)
            {
                //TODO: Adjust delta by zoom level
                Vector2f mappedPosition = window.MapPixelToCoords(position);
                Vector2f delta = dragStart - mappedPosition;
                delta.X = (float)Math.Floor(delta.X);
                delta.Y = (float)Math.Floor(delta.Y);
                //Console.WriteLine($"pos: {position}, mapped: {mappedPosition}, dragStart: {dragStart}, delta: {delta}");
                Move(delta);
            }
        }
    }
}
