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

        private CameraManager mCameraManager;

        /// <summary>
        /// Creates a new ScreenManager
        /// </summary>
        /// <param name="firstScreen">Lowest Screen to be drawn/updated</param>
        /// <param name="cameraManager"></param>
        public ScreenManager(Screen firstScreen, CameraManager cameraManager)
        {
            mScreenStack = new();
            mScreenStack.Push(firstScreen);
            mCameraManager = cameraManager;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Screen screen in mScreenStack)
            {
                screen.Draw(spriteBatch);
                if (!screen.DrawLower)
                {
                    break;
                }
            }
        }
    }
}
