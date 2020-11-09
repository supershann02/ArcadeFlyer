using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Enemy : Sprite
    {
        private ArcadeFlyerGame root;

        private Vector2 velocity;

        public Enemy(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            this.root = root;
            this.position = position;
            this.SpriteWidth = 128.0f;
            this.velocity = new Vector2(-1.0f, 5.0f);

            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Enemy");
        }

        public void Update(GameTime gameTime)
        {
            position += velocity;

            if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
            {
                velocity.Y *= -1;
            }
        }
    }
}