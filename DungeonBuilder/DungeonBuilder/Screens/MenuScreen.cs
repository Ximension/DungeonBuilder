using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
using DungeonBuilder.UI;
using Microsoft.Xna.Framework;
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
        private Rectangle mBounds;

        protected MenuScreen(bool drawLower, bool updateLower, string backgroundPath, Rectangle bounds, ResourceManager resourceManager, KeyBindingManager keyBindingManager) : base(drawLower, updateLower, resourceManager)
        {
            mMenuButtons = new();
            mReturnValueToMethod = new();
            mKeyBindingManager = keyBindingManager;

            mBackground = resourceManager.GetTexture(backgroundPath, true);
            mBounds = bounds;
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
            // draw background
            spriteBatch.Begin();
            spriteBatch.Draw(mBackground, new Rectangle(0, 0, 800, 400), Color.White);
            spriteBatch.End();
            // draw Buttons
            foreach (Button button in mMenuButtons)
            {
                button.Draw(spriteBatch);
            }
        }

        protected void RepositionButtons(Point distance)
        {
            int buttonCount = mMenuButtons.Count;

            Point currentPos = new(mBounds.X, mBounds.Y);

            int lastX = mBounds.X + mBounds.Width;
            int lastY = mBounds.Y + mBounds.Height;

            int buttonIndex = 0;

            // While there are buttons left and the next button would fit into the rectangle
            while (buttonIndex < buttonCount)
            {
                Button currentButton = mMenuButtons[buttonIndex];
                Rectangle buttonBounds = currentButton.GetBounds();

                // if the button is wider than the area, no further testing ist needed
                if (buttonBounds.Width > mBounds.Width)
                {
                    break;
                }

                // If the area is not wide enough for the button, try the next row
                bool fitsX = currentPos.X + buttonBounds.Width <= lastX;
                if (!fitsX)
                {
                    currentPos.X = mBounds.X;
                    currentPos.Y += buttonBounds.Height + distance.Y;
                }
                // If the area is not high enough for the button, then no further testing is necessary
                bool fitsY = currentPos.Y + buttonBounds.Height <= lastY;
                Trace.WriteLine(fitsY);
                if (!fitsY)
                {
                    break;
                }
                Trace.WriteLine(buttonBounds);
                // If everything fits, change the position of the button
                currentButton.ChangePosition(currentPos);
                // Move the current position
                currentPos.X += buttonBounds.Width + distance.X;
                buttonIndex++;
            }
        }
    }
}
