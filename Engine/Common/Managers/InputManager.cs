using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace TwoBRenn.Engine.Common.Managers
{
    public enum MouseButton
    {
        Left = 0,
        Middle = 1,
        Right = 2
    }

    class MouseButtonState
    {
        public bool IsPressed;
    }

    static class InputManager
    {
        public static Form Form { set; get; }

        private static GLControl _glControl;
        private static MouseState _mouseState = Mouse.GetState();

        private static readonly MouseButtonState[] MouseButtonStates = { new MouseButtonState(), new MouseButtonState(), new MouseButtonState() };

        public static GLControl GlControl
        {
            set
            {
                _glControl = value;
                _glControl.MouseDown += OnMouseButtonDown;
                _glControl.MouseUp += OnMouseButtonUp;
            }
            get => _glControl;
        }

        public static Vector2 MouseAbsolutePosition => new Vector2(_mouseState.X, _mouseState.Y);

        public static Vector2 MouseRelativePosition
        {
            get
            {
                Point point = GlControl.PointToClient(Control.MousePosition);
                return new Vector2(point.X, point.Y);
            }
        }

        public static bool IsMouseButtonDown(MouseButton button) => MouseButtonStates[(int)button].IsPressed;
        public static bool IsMouseButtonUp(MouseButton button) => !MouseButtonStates[(int)button].IsPressed;

        private static void OnMouseButtonUp(object sender, MouseEventArgs e)
        {
            MouseButtons mouseButton = e.Button;
            MouseButtonState buttonState;
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    buttonState = MouseButtonStates[(int)MouseButton.Left];
                    break;
                case MouseButtons.Middle:
                    buttonState = MouseButtonStates[(int)MouseButton.Middle];
                    break;
                case MouseButtons.Right:
                    buttonState = MouseButtonStates[(int)MouseButton.Right];
                    break;
                default:
                    return;
            }
            
            buttonState.IsPressed = false;
        }

        private static void OnMouseButtonDown(object sender, MouseEventArgs e)
        {
            MouseButtons mouseButton = e.Button;
            MouseButtonState buttonState = null;
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    buttonState = MouseButtonStates[(int)MouseButton.Left];
                    break;
                case MouseButtons.Middle:
                    buttonState = MouseButtonStates[(int)MouseButton.Middle];
                    break;
            }

            if (buttonState == null) return;
            buttonState.IsPressed = true;
        }
    }
}
