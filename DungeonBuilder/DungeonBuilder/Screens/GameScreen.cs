using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
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
        private InputManager mInputManager;
        private KeyBindingManager mKeyBindingManager;

        private Map mMap;

        public GameScreen(bool drawLower, bool updateLower, CameraManager cameraManager, KeyBindingManager keyBindingManager, ResourceManager resourceManager) : base(drawLower, updateLower, resourceManager)
        {
            mCameraManager = cameraManager;
            mKeyBindingManager = keyBindingManager;

            mMap = new Map(new Point(10, 10), mCameraManager, mKeyBindingManager, resourceManager);
        }

        public override void LoadContent()
        {
            mMap.LoadContent();
        }

        public override void Update()
        {
            mCameraManager.Update();
            mMap.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            mMap.Draw(spriteBatch);
        }

    }
}
