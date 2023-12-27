using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace DungeonBuilder.Screens
{
    /// <summary>
    /// This screen implements the game-loop
    /// </summary>
    public class GameScreen : Screen
    {

        public GameScreen(bool drawLower, bool updateLower, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice) : base(drawLower, updateLower, graphics, graphicsDevice)
        {

        }

        public override void Update()
        {

        }

        public override void Draw()
        {

            mGraphicsDevice.Clear(Color.CornflowerBlue);
        }

    }
}
