using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QTree.MonoGame.TestTool.Input;
using System;

namespace QTree.MonoGame.TestTool
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly CameraHandler _camera;
        private readonly DynamicQuadTree<GameObject> _quadTree = new DynamicQuadTree<GameObject>();
        private SpriteBatch _spriteBatch;
        private Texture2D _sprite;
        private Texture2D _outline;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this) 
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600,
            };
            IsMouseVisible = true;

            _camera = new CameraHandler(800, 600);
            MouseManager.SetScreenSize(800, 600);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var spriteColors = new Color[10 * 10];
            for(var i = 0; i < spriteColors.Length; i++)
            {
                spriteColors[i] = Color.White;
            }
            var outlineColors = new Color[10 * 10];
            var outlineBounds = new Rectangle(0, 0, 10, 10);
            for(var i = 0; i < outlineColors.Length; i++)
            {
                var x = i % 10;
                var y = (int)Math.Floor(i / 10d);
                if(outlineBounds.X == x 
                    || outlineBounds.Y == y 
                    || outlineBounds.Right == x + 1
                    || outlineBounds.Bottom == y + 1)
                {
                    outlineColors[i] = Color.White;
                }
                else
                {
                    outlineColors[i] = Color.Transparent;
                }
            }

            _sprite = new Texture2D(GraphicsDevice, 10, 10);
            _sprite.SetData(spriteColors);
            _outline = new Texture2D(GraphicsDevice, 10, 10);
            _outline.SetData(outlineColors);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardManager.Update();
            _camera.UpdateCamera();
            MouseManager.Update(_camera.ViewMatrix);

            if (!MouseManager.IsLeftButtonClicked)
            {
                return;
            }

            var bounds = new Rectangle(MouseManager.Position.X - 5, MouseManager.Position.Y - 5, 10, 10);
            _quadTree.Add(new GameObject(_sprite, bounds));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(transformMatrix: _camera.ViewMatrix);

            foreach(var obj in _quadTree.FindObject(_camera.View))
            {
                obj.Draw(_spriteBatch);
            }

            foreach(var obj in _quadTree.FindObject(MouseManager.Position))
            {
                _spriteBatch.Draw(_outline, obj.Bounds, Color.Yellow);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
