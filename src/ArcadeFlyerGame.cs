﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // The player
        private Player player;

        private Enemy enemy;

        private List<Projectile> projectiles;

        private Texture2D playerProjectileSprite;

        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }
        
        private int screenHeight = 900;
        public int ScreenHeight 
        {
            get { return screenHeight; }
            private set { screenHeight = value; }
        }
        
        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            // Initialize the player to be in the top left
            player = new Player(this, new Vector2(0.0f, 0.0f));

            enemy = new Enemy(this, new Vector2(screenWidth, 0));

            projectiles = new List<Projectile>();
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);

            player.Update(gameTime);
            enemy.Update(gameTime);

            foreach (Projectile p in projectiles)
            {
                p.Update();
            }
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.White);

            // Start batch draw
            spriteBatch.Begin();

            // Draw the player
            player.Draw(gameTime, spriteBatch);
            enemy.Draw(gameTime, spriteBatch);

            // End batch draw
            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, Vector2 velocity)
        {
            Projectile firedProjectile = new Projectile(position, velocity, playerProjectileSprite);
            projectiles.Add(firedProjectile);
        }
    }
}
