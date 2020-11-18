using Microsoft.Xna.Framework;
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
        private List<Enemy> enemies;

        private Timer enemyCreationTimer;

        // List of all projectiles on the screen
        private List<Projectile> projectiles;

        // Projectile image for player
        private Texture2D playerProjectileSprite;

        // Projectile image for enemy
        private Texture2D enemyProjectileSprite;

        // Screen width
        private int screenWidth = 1200;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Screen height
        private int screenHeight = 500;
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

            enemies = new List<Enemy>();
            
            enemies.Add(new Enemy(this, new Vector2(screenWidth, 0)));

            enemyCreationTimer = new Timer(3.0f);
            enemyCreationTimer.StartTimer();

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
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            // Update base game
            base.Update(gameTime);

            // Update the components
            player.Update(gameTime);
            
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            // Update all projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                p.Update();

                bool isPlayerProjectile = p.ProjectileType == ProjectileType.Player;
                
                if (!isPlayerProjectile && player.Overlaps(p))
                {
                    projectiles.Remove(p);
                } 
                else if(isPlayerProjectile)
                {
                    for (int x = enemies.Count - 1; x >= 0; x--)
                    {
                        Enemy e = enemies[x];

                        if (e.Overlaps(p))
                        {
                            projectiles.Remove(p);
                            enemies.Remove(e);
                        }
                    }
                }
            }

            if (!enemyCreationTimer.Active)
            {
                enemies.Add(new Enemy(this, new Vector2(screenWidth, 0.0f)));
                enemyCreationTimer.StartTimer();
            }

            enemyCreationTimer.Update(gameTime);
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
            
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            // Draw all projectiles
            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            // End batch draw
            spriteBatch.End();
        }

        // Fires a projectile with the given position and velocity
        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            // Create the image for the projectile
            Texture2D projectileImage;
            
            if (projectileType == ProjectileType.Player)
            {
                // This is a projectile sent from the player, set it to the proper sprite
                projectileImage = playerProjectileSprite;
            }
            else
            {
                // This is a projectile sent from the enemy, set it to the proper sprite
                projectileImage = enemyProjectileSprite;
            }

            // Create the new projectile
            Projectile firedProjectile = new Projectile(position, velocity, projectileImage);

            // Add the projectile to the list
            projectiles.Add(firedProjectile);
        }
    }
}
