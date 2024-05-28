using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
using DungeonBuilder.UI;
using Microsoft.Xna.Framework;

namespace DungeonBuilder.Screens
{
    internal class MenuMainScreen : MenuScreen
    {
        private const string mBackgroundPath = "Map/MapDebugging";
        private const bool mDrawLower = false;
        private const bool mUpdateLower = false;

        private const string mSpriteFontPath = "UI/Fonts/Vulnus";
        private const string mTexturePath = "Map/Tiles/0/0";

        public MenuMainScreen(ResourceManager resourceManager, KeyBindingManager keyBindingManager, ScreenManager screenManager, CameraManager cameraManager, Game1 game) : base(mDrawLower, mUpdateLower, mBackgroundPath, resourceManager, keyBindingManager)
        {
            Button startButton = new(new Point(0, 0), "Start", resourceManager);
            Button settingsButton = new(new Point(0, 50), "Settings", resourceManager);
            Button leaveButton = new(new Point(0, 100), "Leave", resourceManager);

            string startReturn = "start";
            string settingsReturn = "settings";
            string leaveReturn = "leave";

            keyBindingManager.AddButton(startButton, startReturn, InputManager.ClickableButtonState.Clicked);
            keyBindingManager.AddButton(settingsButton, settingsReturn, InputManager.ClickableButtonState.Clicked);
            keyBindingManager.AddButton(leaveButton, leaveReturn, InputManager.ClickableButtonState.Clicked);

            mMenuButtons = new()
            {
                startButton,
                settingsButton,
                leaveButton
            };

            #region Button Functions

            void startButtonFunc()
            {
                Screen gameScreen = new GameScreen(cameraManager, mKeyBindingManager, mResourceManager);
                screenManager.Pop(gameScreen);
            }
            mReturnValueToMethod.Add(startReturn, startButtonFunc);

            void settingsButtonFunc()
            {
                Trace.WriteLine("Settings");
            }
            mReturnValueToMethod.Add(settingsReturn, settingsButtonFunc);

            void leaveButtonFunc()
            {
                game.Exit();
            }
            mReturnValueToMethod.Add(leaveReturn, leaveButtonFunc);

            #endregion
        }

        public override void LoadContent()
        {
            foreach (Button button in mMenuButtons)
            {
                button.LoadContent(mSpriteFontPath, mTexturePath);
            }
        }
    }
}
