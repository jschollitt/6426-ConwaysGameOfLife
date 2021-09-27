using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Camera
{
    public class Game
    {
        public bool isRunning = true;
        public List<Keyboard.Key> keyDown;
        public RenderWindow window;
        public FloatRect windowBounds;
        public Camera camera;

        public GameObject gameObject;
        public GameObject gameObject2;
        public Grid grid;
        public Walker walker;
        public Game()
        {
            
            VideoMode mode = new VideoMode(1920, 1080);
            windowBounds = new FloatRect(0, 0, 1920, 1080);
            window = new RenderWindow(mode, "SFML Application", Styles.Default);
            camera = new Camera(window, windowBounds);
            
            window.SetView(camera.cameraView);
            
            window.KeyPressed += AppWindow_KeyPressed;
            window.KeyReleased += AppWindow_KeyReleased;
            window.MouseButtonPressed += AppWindow_MouseButtonPressed;
            window.MouseButtonReleased += AppWindow_MouseButtonReleased;
            window.MouseWheelScrolled += AppWindow_MouseWheelScrolled;
            window.MouseMoved += Window_MouseMoved;
            window.Closed += AppWindow_Closed;

            InitialiseComponents();
        }

        private void InitialiseComponents()
        {
            grid = new Grid(windowBounds, 20, 20);
            gameObject = new GameObject(0, 0, 100);
            gameObject.SetColour(Color.Red);
            gameObject2 = new GameObject(1000, 0, 100);
            gameObject2.SetColour(Color.Blue);
            walker = new Walker(MathLib.FindCenterOfRect(windowBounds), new Color(255, 255, 255, 100));

            keyDown = new List<Keyboard.Key>();
        }
        /// <summary>
        /// Method to begin the application loop.
        /// While the window is open, call the Update and Draw
        /// </summary>
        public void Run()
        {
            while (isRunning)
            {
                Update();
                Draw();
            }
        }
        
        public void Update()
        {
            window.DispatchEvents();
            UpdateKeyDown();
            //gameObject.Update();
            //gameObject2.Update();
            walker.Walk();
            camera.Update();
            window.SetView(camera.cameraView);
        }

        public void Draw()
        {
            window.Clear();
            //grid.Draw(window);
            //gameObject.Draw(window);
           // gameObject2.Draw(window);
            walker.Draw(window);
            window.Display();
        }

        private void UpdateKeyDown()
        {
            keyDown.ForEach(key =>
            {
                switch (key)
                {
                    case Keyboard.Key.W:
                        camera.Move(0, -1);
                        break;
                    case Keyboard.Key.A:
                        camera.Move(-1, 0);
                        break;
                    case Keyboard.Key.S:
                        camera.Move(0, 1);
                        break;
                    case Keyboard.Key.D:
                        camera.Move(1, 0);
                        break;
                }
            });
        }

        public void SetWindow(RenderWindow window)
        {
            this.window = window;
            this.window.SetView(camera.cameraView);
        }

        private void ChangeWindowSize(int index)
        {
            VideoMode[] resolutions = VideoMode.FullscreenModes;
            if (index < resolutions.Length)
            {
                window.Size = new Vector2u(resolutions[index].Width, resolutions[index].Height);
                windowBounds = new FloatRect(0, 0, window.Size.X, window.Size.Y);
                camera.SetBounds(windowBounds);
                //window.SetView(camera.cameraView);
                window.Position = new Vector2i(0, 0);
                Console.WriteLine($"camera centre: {window.MapPixelToCoords(new Vector2i((int)(windowBounds.Width / 2f), (int)(windowBounds.Height / 2f)))}");
            }
        }

        public void DrawToImage()
        {
            walker.SaveAsSVG("save.svg");
            //FloatRect rect = walker.GetBounds();
            //RenderTexture texture = new RenderTexture((uint)rect.Left + (uint)rect.Width, (uint)rect.Top + (uint)rect.Height);
            //texture.Clear(Color.Black);
            //texture.Draw(walker.vertexArray);
            ////texture.Display();
            //texture.Texture.CopyToImage().SaveToFile("save.png");
        }

        private void AppWindow_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    camera.StopDrag();
                    break;
                case Mouse.Button.Right:
                    break;
                case Mouse.Button.Middle:
                    break;
                default:
                    break;
            }
        }

        private void AppWindow_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    camera.StartDrag(e.X, e.Y);
                    break;
                case Mouse.Button.Right:
                    DrawToImage();
                    break;
                case Mouse.Button.Middle:
                    break;
                default:
                    break;
            }
        }

        private void AppWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            if (keyDown.Contains(e.Code) == true) keyDown.Remove(e.Code);
            switch (e.Code)
            {
                default:
                    break;
            }
        }

        private void AppWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            if (keyDown.Contains(e.Code) == false) keyDown.Add(e.Code);
            
            switch (e.Code)
            {
                case Keyboard.Key.Escape:
                    isRunning = false;
                    break;
                case Keyboard.Key.Num1:
                    ChangeWindowSize(0);
                    break;
                case Keyboard.Key.Num2:
                    ChangeWindowSize(1);
                    break;
                case Keyboard.Key.Num3:
                    ChangeWindowSize(2);
                    break;
                case Keyboard.Key.Num4:
                    ChangeWindowSize(3);
                    break;
                case Keyboard.Key.Num5:
                    ChangeWindowSize(4);
                    break;
                case Keyboard.Key.Num6:
                    ChangeWindowSize(5);
                    break;
                case Keyboard.Key.Num7:
                    ChangeWindowSize(6);
                    break;
                case Keyboard.Key.Num8:
                    ChangeWindowSize(7);
                    break;
                case Keyboard.Key.Num9:
                    ChangeWindowSize(8);
                    break;
                case Keyboard.Key.Num0:
                    //;
                    break;
                default:
                    break;
            }
        }

        private void AppWindow_MouseWheelScrolled(object sender, MouseWheelScrollEventArgs e)
        {
            Console.WriteLine($"Zoom: {e.Delta}");
            camera.ZoomToTarget(e.Delta, Keyboard.IsKeyPressed(Keyboard.Key.LControl), e.X, e.Y);
        }

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                camera.Drag(e.X, e.Y);
            }
        }

        private void AppWindow_Closed(object sender, EventArgs e)
        {
            window.Dispose();
        }
    }
}
