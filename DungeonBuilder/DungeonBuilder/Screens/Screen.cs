using DungeonBuilder.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonBuilder.Screens
{
    /// <summary>
    /// This class implements a simple screen
    /// </summary>
    public abstract class Screen
    {
        public bool DrawLower { get; private set; }
        public bool UpdateLower { get; private set; }

        protected ResourceManager mResourceManager;

        /// <summary>
        /// Create a new screen
        /// </summary>
        /// <param name="drawLower">Determines whether Draw() should be called for lower screen</param>
        /// <param name="updateLower">Determines whether Update() should be called for lower screen</param>
        public Screen(bool drawLower, bool updateLower, ContentManager content)
        {
            DrawLower = drawLower;
            UpdateLower = updateLower;

            mResourceManager = new ResourceManager(content);
        }

        public abstract void LoadContent();

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
