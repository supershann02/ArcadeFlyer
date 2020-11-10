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

        // An enemy
        private Enemy enemy;

        // List of all projectiles on the screen
        private List<Projectile> projectiles;

        private Texture2D playerProjectileSprite;

        private Texture2D enemyProjectileSprite;

        private int screenWidth = 1200;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Screen height
        private int screenHeight = 600;
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
            
            // Initialize an enemy to be on the right side
            enemy = new Enemy(this, new Vector2(screenWidth, 0));

            // Initialize empty list of projectiles
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

            // Load in textures
            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyProjectile");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);

            // Update the components
            player.Update(gameTime);
            enemy.Update(gameTime);

            // Update all projectiles
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

            // Draw the components
            player.Draw(gameTime, spriteBatch);
            enemy.Draw(gameTime, spriteBatch);

            // Draw all projectiles
            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, Vector2 velocity, string projectileType)
        {
            Texture2D projectileTexture;

            if (projectileType == "enemy")
            {
                projectileTexture = enemyProjectileSprite;
            } else 
            {
                projectileTexture = playerProjectileSprite;
            }
            
            Projectile firedProjectile = new Projectile(position, velocity, projectileTexture);
            projectiles.Add(firedProjectile);
        }
    }
}
