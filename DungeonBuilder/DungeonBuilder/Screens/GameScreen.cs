using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
using DungeonBuilder.UI;
using DungeonBuilder.World;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DungeonBuilder.Screens
{
    /// <summary>
    /// This screen implements the game-loop
    /// </summary>
    public class GameScreen : Screen
    {
        private CameraManager mCameraManager;
        private KeyBindingManager mKeyBindingManager;
        private ResourceManager mResourceManager;

        private Map mMap;

        public GameScreen(CameraManager cameraManager, KeyBindingManager keyBindingManager, ResourceManager resourceManager) : base(false, false, resourceManager)
        {
            mCameraManager = cameraManager;
            mKeyBindingManager = keyBindingManager;
            mResourceManager = resourceManager;

            mMap = new Map(new Point(10, 10), mCameraManager, mKeyBindingManager, mResourceManager);
        }

        public override void LoadContent()
        {
            mMap.LoadContent();
        }

        public override void Update()
        {
            mMap.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mMap.Draw(spriteBatch);
        }
    }
}
