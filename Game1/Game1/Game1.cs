using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 90;  //Скорость анимации

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame++;
                if (currentFrame == totalFrames) currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }


    public class Game1 : Game
    {
        public bool[] direction;
        private Texture2D background;
        private SpriteFont font;
        private AnimatedSprite horse_left;
        private AnimatedSprite horse_right;
        private AnimatedSprite horse_jump;

        private Texture2D currentHorseFrame;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            background = Content.Load<Texture2D>("Img/sky_menu");
            font = Content.Load<SpriteFont>("Fonts/Main");

            Texture2D horseRun_left = Content.Load<Texture2D>("Img/Player/horseRUN_left");
            Texture2D horseRun_Right = Content.Load<Texture2D>("Img/Player/horseRUN_right");
            Texture2D horseStand = Content.Load<Texture2D>("Img/Player/HorseJump");
            horse_left = new AnimatedSprite(horseRun_left, 1, 4);
            horse_right = new AnimatedSprite(horseRun_Right, 1, 4);
            horse_jump = new AnimatedSprite(horseStand, 1, 1);

            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.A)) horse_left.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) horse_right.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.DrawString(font, "Horse Game", new Vector2(100, 100), Color.Black);
            spriteBatch.End();

            if (Keyboard.GetState().IsKeyDown(Keys.D)) horse_right.Draw(spriteBatch, new Vector2(200, 200));
            else if (Keyboard.GetState().IsKeyDown(Keys.A)) horse_left.Draw(spriteBatch, new Vector2(200, 200));
            else horse_jump.Draw(spriteBatch, new Vector2(200, 200));
            base.Draw(gameTime);
        }


        

    }
}
