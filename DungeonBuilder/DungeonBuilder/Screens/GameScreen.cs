using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
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
        public GameScreen(bool drawLower, bool updateLower, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice, CameraManager cameraManager) : base(drawLower, updateLower, graphics, graphicsDevice)
        {
            mCameraManager = cameraManager;
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: mCameraManager.TransformationMatrix);
            spriteBatch.End();
            mGraphicsDevice.Clear(Color.CornflowerBlue);
        }

    }
}
