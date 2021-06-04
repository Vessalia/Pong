using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace Pong
{
    public class Game1 : Game, GameStateSwitcher
    {
        //hello
        GameState gameState;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.Width;
            _graphics.PreferredBackBufferHeight = Constants.Height;
            _graphics.ApplyChanges();

            gameState = new MainMenuState(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            font1 = Content.Load<SpriteFont>("Arial32");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            int steps = (int)MathF.Pow(2, 10);
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds / steps;

            gameState.HandleInput();

            if (gameState == null)
            {
                Exit();
                return;
            }
           
            for (int i = 0; i < steps; i++)
            {
                gameState.Update(dt);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            gameState.DrawToScreen(_spriteBatch, font1);

            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void SetNextState(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}
