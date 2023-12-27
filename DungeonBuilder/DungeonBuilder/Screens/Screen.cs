using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonBuilder.Screens
{
    /// <summary>
    /// This class implements a simple screen
    /// </summary>
    public abstract class Screen
    {
        public bool DrawLower { get; private set; }
        public bool UpdateLower { get; private set; }


        protected GraphicsDeviceManager mGraphics;
        protected GraphicsDevice mGraphicsDevice;
        protected SpriteBatch mSpriteBatch;

        /// <summary>
        /// Create a new screen
        /// </summary>
        /// <param name="drawLower">Determines whether Draw() should be called for lower screen</param>
        /// <param name="updateLower">Determines whether Update() should be called for lower screen</param>
        public Screen(bool drawLower, bool updateLower, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
        {
            DrawLower = drawLower;
            UpdateLower = updateLower;

            mGraphics = graphics;
            mGraphicsDevice = graphicsDevice;
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            mSpriteBatch = spriteBatch;
        }
        public abstract void Update();
        public abstract void Draw();
    }
}
