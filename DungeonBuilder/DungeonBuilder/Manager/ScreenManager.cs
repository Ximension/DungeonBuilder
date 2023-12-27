using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DungeonBuilder.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// This manages all screens and their drawing order as well as 
    /// drawing and updating only screens that should be drawn or updated.
    /// </summary>
    public class ScreenManager
    {
        private Stack<Screen> mScreenStack;

        /// <summary>
        /// Creates a new ScreenManager
        /// </summary>
        /// <param name="firstScreen">Lowest Screen to be drawn/updated</param>
        public ScreenManager(Screen firstScreen)
        {
            mScreenStack = new();
            mScreenStack.Push(firstScreen);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            foreach (Screen screen in mScreenStack)
            {
                screen.LoadContent(spriteBatch);
            }
        }

        public void Update()
        {
            foreach (Screen screen in mScreenStack)
            {
                screen.Update();
                if (!screen.UpdateLower)
                {
                    break;
                }
            }
        }

        public void Draw()
        {
            foreach (Screen screen in mScreenStack)
            {
                screen.Draw();
                if (!screen.DrawLower)
                {
                    break;
                }
            }
        }
    }
}
