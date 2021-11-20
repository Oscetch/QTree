using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using QTree.MonoGame.TestTool.Input;

namespace QTree.MonoGame.TestTool.Camera
{
    public class CameraHandler
    {
        private float _scale = 1.0f;

        private readonly Vector3 _origin;
        private Rectangle _view;

        public float ScaleUpperLimit { get; set; } = 4f;
        public float ScaleLowerLimit { get; set; } = .5f;

        public int ScreenHeight { get; }
        public int ScreenWidth { get; }
        public float ScreenMoveSpeed { get; set; } = 2;
        public float CameraPositionX { get; set; }
        public float CameraPositionY { get; set; }
        public float Rotation { get; set; } = 0;
        public float Scale { get => _scale; set => _scale = MathHelper.Clamp(value, ScaleLowerLimit, ScaleUpperLimit); }
        public bool IsLocked { get; set; }
        public Vector3 CameraPosition
        {
            get => new Vector3(CameraPositionX, CameraPositionY, 0);
            set
            {
                CameraPositionX = value.X;
                CameraPositionY = value.Y;
            }
        }

        public Vector2 Camera2DPosition
        {
            get => new Vector2(CameraPositionX, CameraPositionY);
            set
            {
                CameraPositionX = value.X;
                CameraPositionY = value.Y;
            }
        }

        public Vector2 Center
        {
            get => ScreenToWorld(new Vector2(ScreenWidth, ScreenHeight) / 2);
        }

        public Rectangle View => _view;

        public Matrix ViewMatrix => Matrix.CreateTranslation(-_origin)
            * Matrix.CreateTranslation(CameraPosition)
            * Matrix.CreateRotationZ(Rotation)
            * Matrix.CreateScale(new Vector3(Scale, Scale, 1))
            * Matrix.CreateTranslation(_origin);

        public Matrix NonScaledViewMatrix => Matrix.CreateTranslation(-_origin)
            * Matrix.CreateTranslation(CameraPosition)
            * Matrix.CreateRotationZ(Rotation)
            * Matrix.CreateTranslation(_origin);

        public CameraHandler(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            _origin = new Vector3(ScreenWidth / 2f, ScreenHeight / 2f, 0);
            UpdateCamera();
        }

        public void CenterCameraToTarget(Rectangle target)
        {
            CameraPositionX = -target.Location.X + -target.Size.X / 2 - (-ScreenWidth / 2);
            CameraPositionY = -target.Location.Y + -target.Size.Y / 2 - (-ScreenHeight / 2);
        }

        public void CenterCameraToTarget(Vector2 target)
        {
            CameraPositionX = -target.X - (-ScreenWidth / 2);
            CameraPositionY = -target.Y - (-ScreenHeight / 2);
        }

        public bool IsObjectOnCamera(Rectangle objectArea)
        {
            return _view.Intersects(objectArea);
        }

        public bool IsPointOnCamera(Vector2 point)
        {
            return _view.Contains(point);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition, Matrix.Invert(ViewMatrix));
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, ViewMatrix);
        }

        public Rectangle WorldBoundsToScreenBounds(Rectangle worldBounds)
        {
            var position = WorldToScreen(worldBounds.Location.ToVector2());
            var size = worldBounds.Size.ToVector2() * Scale;
            return new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public Rectangle ScreenBoundsToWorldBounds(Rectangle screenBounds)
        {
            var position = ScreenToWorld(screenBounds.Location.ToVector2());
            var size = screenBounds.Size.ToVector2() / Scale;
            return new Rectangle(position.ToPoint(), size.ToPoint());
        }

        public void UpdateCamera()
        {
            Point cameraWorldPosition;
            Point screenSize;

            if (IsLocked)
            {
                cameraWorldPosition = ScreenToWorld(Vector2.Zero).ToPoint();
                screenSize = (new Vector2(ScreenWidth, ScreenHeight) / Scale).ToPoint();
                _view = new Rectangle(cameraWorldPosition, screenSize);
                return;
            }

            if (KeyboardManager.IsKeyDown(Keys.W))
            {
                CameraPositionY += ScreenMoveSpeed;
            }
            if (KeyboardManager.IsKeyDown(Keys.S))
            {
                CameraPositionY -= ScreenMoveSpeed;
            }
            if (KeyboardManager.IsKeyDown(Keys.D))
            {
                CameraPositionX -= ScreenMoveSpeed;
            }
            if (KeyboardManager.IsKeyDown(Keys.A))
            {
                CameraPositionX += ScreenMoveSpeed;
            }

            cameraWorldPosition = ScreenToWorld(Vector2.Zero).ToPoint();
            screenSize = (new Vector2(ScreenWidth, ScreenHeight) / Scale).ToPoint();
            _view = new Rectangle(cameraWorldPosition, screenSize);
        }

    }
}
