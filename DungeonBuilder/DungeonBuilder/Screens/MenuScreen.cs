using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
using DungeonBuilder.UI;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonBuilder.Screens
{
    public abstract class MenuScreen : Screen
    {
        protected List<Button> mMenuButtons;
        protected KeyBindingManager mKeyBindingManager;

        protected delegate void VoidDelegate();
        protected Dictionary<string, VoidDelegate> mReturnValueToMethod;

        private Texture2D mBackground;

        protected MenuScreen(bool drawLower, bool updateLower, string backgroundPath, ResourceManager resourceManager, KeyBindingManager keyBindingManager) : base(drawLower, updateLower, resourceManager)
        {
            mMenuButtons = new();
            mReturnValueToMethod = new();
            mKeyBindingManager = keyBindingManager;

            mBackground = resourceManager.GetTexture(backgroundPath, true);
        }

        public override void Update()
        {
            // Check for all buttons, whether they returned a value
            // and execute the corresponding function
            foreach (string returnValue in mReturnValueToMethod.Keys)
            {
                if (mKeyBindingManager.CheckAction(returnValue))
                {
                    mReturnValueToMethod[returnValue]();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // update Buttons
            foreach (Button button in mMenuButtons)
            {
                button.Draw(spriteBatch);
            }
        }
    }
}
