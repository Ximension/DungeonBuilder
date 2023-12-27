using System.Diagnostics;
using DungeonBuilder.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonBuilder.Screens;

namespace DungeonBuilder
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mSpriteBatch;

        private ScreenManager mScreenManager; // TODO: Move this to ScreenManager

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Screen gameScreen = new GameScreen(false, false, mGraphics, GraphicsDevice);
            mScreenManager = new ScreenManager(gameScreen);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            mScreenManager.LoadContent(mSpriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            mScreenManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            mScreenManager.Draw();

            base.Draw(gameTime);
        }
    }
}