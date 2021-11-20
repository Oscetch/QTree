using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QTree.Interfaces;

namespace QTree.MonoGame.TestTool
{
    public class GameObject : IQuadTreeObject<GameObject>
    {
        public Texture2D Sprite { get; }
        public Rectangle Bounds { get; set; }

        public QuadId Id { get; } = new QuadId();

        public GameObject Object => this;

        public GameObject(Texture2D sprite, Rectangle bounds)
        {
            Sprite = sprite;
            Bounds = bounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Bounds, Color.Red);
        }
    }
}
