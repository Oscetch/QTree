using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace QTree.MonoGame.TestTool.Input
{
    public static class MouseManager
    {
        private static int _screenWidth = 0;
        private static int _screenHeight = 0;
        private static MouseState _mouseState = new MouseState();
        private static MouseState _previousMouseState = _mouseState;
        private static int _previousMouseWheelValue = 0;
        public static Point Position { get; private set; }
        public static Point RawPosition => _mouseState.Position;
        public static bool OverrideMouseState { get; set; }
        public static bool LeftClickHandled { get; set; }
        public static bool RightClickHandled { get; set; }
        public static MouseState CurrentState => _mouseState;
        public static bool IsMouseOverWindow => IsOverArea(new Rectangle(0, 0, _screenWidth, _screenHeight), true);
        public static bool IsRightButtonPressed => IsMouseOverWindow
            && _mouseState.RightButton == ButtonState.Pressed;

        public static bool IsLeftButtonPressed => IsMouseOverWindow
            && _mouseState.LeftButton == ButtonState.Pressed;

        public static bool IsRightButtonClicked => IsMouseOverWindow
            && !RightClickHandled
            && _previousMouseState.RightButton == ButtonState.Pressed
            && _mouseState.RightButton == ButtonState.Released;

        public static bool IsLeftButtonClicked => IsMouseOverWindow
            && !LeftClickHandled
            && _previousMouseState.LeftButton == ButtonState.Pressed
            && _mouseState.LeftButton == ButtonState.Released;

        public static bool IsOverArea(Rectangle area, bool translate = false) => translate ?
            area.Intersects(new Rectangle(RawPosition, new Point(1))) :
            area.Intersects(new Rectangle(Position, new Point(1, 1)));

        public static int MouseWheelValue => _mouseState.ScrollWheelValue;
        public static MouseWheelStatus CurrentMouseWheelStatus { get; private set; } = MouseWheelStatus.Unchanged;
        public static void SetScreenSize(int width, int height)
        {
            _screenWidth = width;
            _screenHeight = height;
        }

        public static void Update(Matrix cameraTranslation)
        {
            if (OverrideMouseState)
            {
                return;
            }

            LeftClickHandled = false;
            RightClickHandled = false;

            _previousMouseState = _mouseState;
            _mouseState = Mouse.GetState();
            Position = Vector2.Transform(RawPosition.ToVector2(), Matrix.Invert(cameraTranslation)).ToPoint();
            if (MouseWheelValue > _previousMouseWheelValue)
            {
                CurrentMouseWheelStatus = MouseWheelStatus.Up;
            }
            else if (MouseWheelValue < _previousMouseWheelValue)
            {
                CurrentMouseWheelStatus = MouseWheelStatus.Down;
            }
            else
            {
                CurrentMouseWheelStatus = MouseWheelStatus.Unchanged;
            }
            _previousMouseWheelValue = MouseWheelValue;
        }
    }
}
