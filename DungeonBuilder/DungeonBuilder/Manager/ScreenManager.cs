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
        /// <param name="cameraManager"></param>
        public ScreenManager(CameraManager cameraManager)
        {
            mScreenStack = new();
            mCameraManager = cameraManager;
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

        /// <summary>
        /// Puts a new screen on the stack
        /// </summary>
        /// <param name="screen"></param>
        public void Push(Screen screen)
        {
            screen.LoadContent();
            mScreenStack.Push(screen);
        }

        /// <summary>
        /// Deletes a screen and optionally replaces it
        /// </summary>
        /// <param name="replaceScreen"></param>
        public void Pop(Screen replaceScreen=null)
        {
            mScreenStack.Pop();
            if (replaceScreen is not null)
            {
                Push(replaceScreen);
            }
        }
    }
}
