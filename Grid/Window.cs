using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Grid
{
    public class Window
    {
        RenderWindow renderWindow;
        Grid grid;

        Clock clock;
        float delta;

        bool bMouseDragging;
        public Window()
        {
            VideoMode mode = new VideoMode(1000, 1000);
            renderWindow = new RenderWindow(mode, "SFML Application", Styles.Default);

            renderWindow.KeyPressed += renderwindow_KeyPressed;
            renderWindow.KeyReleased += renderwindow_KeyReleased;
            renderWindow.MouseButtonPressed += renderwindow_MouseButtonPressed;
            renderWindow.MouseButtonReleased += renderwindow_MouseButtonReleased;
            renderWindow.MouseWheelScrolled += renderwindow_MouseWheelScrolled;
            renderWindow.MouseMoved += renderwindow_MouseMoved;
            renderWindow.Closed += renderwindow_Closed;

            bMouseDragging = false;

            InitialiseComponents();
        }

        public void InitialiseComponents()
        {
            grid = new Grid(1000, 1000, 50);
            clock = new Clock();
            delta = 0f;
        }

        public void Run()
        {
            while(renderWindow.IsOpen)
            {
                Update(1 / 100f);
                Draw();
            }
        }

        public void Update(float tickTime)
        {
            delta = clock.ElapsedTime.AsSeconds();

            renderWindow.DispatchEvents();
            
            if (delta > tickTime)
            {
                grid.Update();
                delta = clock.Restart().AsSeconds();
            }
        }

        public void Draw()
        {
            renderWindow.Clear();
            grid.Draw(renderWindow);
            renderWindow.Display();
        }

        private void renderwindow_Closed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void renderwindow_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (bMouseDragging)
                grid.SetCell(new Vector2i(e.X, e.Y), CellState.Alive);
        }

        private void renderwindow_MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void renderwindow_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            bMouseDragging = false;
        }

        private void renderwindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            bMouseDragging = true;
            grid.SetCell(new Vector2i(e.X, e.Y), CellState.Alive);
        }

        private void renderwindow_KeyReleased(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void renderwindow_KeyPressed(object sender, KeyEventArgs e)
        {
            switch(e.Code)
            {
                case Keyboard.Key.Escape:
                    renderWindow.Close();
                    break;
                case Keyboard.Key.Space:
                    grid.TogglePause();
                    break;
                case Keyboard.Key.Enter:
                    grid.Reset();
                    break;
                case Keyboard.Key.Tab:
                    grid.Reset();
                    grid.FillRandom(0.3);
                    break;
            }
        }
    }
}
