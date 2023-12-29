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
        public GameScreen(bool drawLower, bool updateLower, CameraManager cameraManager, ContentManager content) : base(drawLower, updateLower, content)
        {
            mCameraManager = cameraManager;
        }

        public override void LoadContent()
        {
            List<string> texturePathList = new() { "Map/MapDebugging" };
            List<string> soundEffectPathList = new() { };
            List<string> songPathList = new() { };

            mResourceManager.LoadContent(texturePathList, soundEffectPathList, songPathList);
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: mCameraManager.TransformationMatrix);
            spriteBatch.Draw(mResourceManager.GetTexture("Map/MapDebugging"), new Rectangle(0, 0, 800, 480), Color.White);
            spriteBatch.End();
        }

    }
}
