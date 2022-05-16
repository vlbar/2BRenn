using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using TwoBRenn.Engine.Interfaces;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace TwoBRenn.Engine.Common.Managers
{
    class MouseButtonState
    {
        public bool IsPressed;
        public bool IsDown;
        public bool IsUp;
    }

    class Input : IUpdatableEnginePart
    {
        private static Input _instance;
        public static Input Instance => _instance ?? (_instance = new Input());

        // forms
        private Form form;
        private GLControl glControl;

        // mouse
        private static MouseState _mouseState = Mouse.GetState();
        private Dictionary<MouseButtons, MouseButtonState> mouseStateDictionary;
        private readonly Dictionary<MouseButtons, MouseButtonState> mouseStateDictionaryBuffer =
            new Dictionary<MouseButtons, MouseButtonState>();

        public void Setup(GLControl control, Form mainForm)
        {
            glControl = control;
            form = mainForm;

            mouseStateDictionary = new Dictionary<MouseButtons, MouseButtonState>
            {
                { MouseButtons.Left, new MouseButtonState()},
                { MouseButtons.Middle, new MouseButtonState()},
                { MouseButtons.Right, new MouseButtonState()}
            };

            foreach (var key in mouseStateDictionary.Keys)
            {
                mouseStateDictionaryBuffer.Add(key, new MouseButtonState());
            }

            glControl.MouseDown += OnMouseButtonDown;
        }

        public void OnUpdate()
        {
            UpdateMouseState();
        }

        private void UpdateMouseState()
        {
            foreach (var button in mouseStateDictionary.Keys)
            {
                mouseStateDictionary[button].IsDown = false;
                mouseStateDictionary[button].IsUp = false;

                if (mouseStateDictionaryBuffer[button].IsDown)
                {
                    mouseStateDictionaryBuffer[button].IsDown = false;
                    mouseStateDictionary[button].IsDown = true;
                    mouseStateDictionary[button].IsPressed = true;
                }

                if (mouseStateDictionaryBuffer[button].IsUp)
                {
                    mouseStateDictionaryBuffer[button].IsUp = false;
                    mouseStateDictionary[button].IsUp = true;
                    mouseStateDictionary[button].IsPressed = false;
                }

                if (!Control.MouseButtons.HasFlag(button) && mouseStateDictionary[button].IsPressed)
                {
                    mouseStateDictionary[button].IsUp = true;
                    mouseStateDictionary[button].IsPressed = false;
                }
            }
        }

        // static methods
        public static Vector2 MouseAbsolutePosition => new Vector2(_mouseState.X, _mouseState.Y);

        public static Vector2 MouseRelativePosition
        {
            get
            {
                Point point = Instance.glControl.PointToClient(Control.MousePosition);
                return new Vector2(point.X, point.Y);
            }
        }

        public static bool IsMouseButtonPressed(MouseButtons button)
        {
            if (Instance.mouseStateDictionary.TryGetValue(button, out var value))
            {
                return value.IsPressed;
            }
            return false;
        }

        public static bool IsMouseButtonDown(MouseButtons button)
        {
            if (Instance.mouseStateDictionary.TryGetValue(button, out var value))
            {
                return value.IsDown;
            }
            return false;
        }

        public static bool IsMouseButtonUp(MouseButtons button)
        {
            if (Instance.mouseStateDictionary.TryGetValue(button, out var value))
            {
                return value.IsUp;
            }
            return false;
        }

        // event handlers
        private void OnMouseButtonDown(object sender, MouseEventArgs e)
        {
            if (Instance.mouseStateDictionaryBuffer.TryGetValue(e.Button, out var value))
            {
                value.IsDown = true;
            }
        }
    }
}
