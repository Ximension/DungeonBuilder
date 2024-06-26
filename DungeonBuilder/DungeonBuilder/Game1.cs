﻿using System.Diagnostics;
using DungeonBuilder.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DungeonBuilder.Screens;
using System;

namespace DungeonBuilder
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager mGraphics;
        private SpriteBatch mSpriteBatch;

        private ScreenManager mScreenManager;
        private CameraManager mCameraManager;
        private KeyBindingManager mKeyBindingManager;
        private ResourceManager mResourceManager;

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            mKeyBindingManager = new();
            mCameraManager = new CameraManager(new Vector2(-200, -120), 2f, mKeyBindingManager);
            mResourceManager = new ResourceManager(Content);
            mScreenManager = new ScreenManager(mCameraManager);

            Screen menuMainScreen = new MenuMainScreen(mResourceManager, mKeyBindingManager, mScreenManager, mCameraManager, this);
            mScreenManager.Push(menuMainScreen);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            Viewport viewport = GraphicsDevice.Viewport;
            Trace.WriteLine($"Viewport Width: {viewport.Width}");
            Trace.WriteLine($"Viewport Height: {viewport.Height}");
            Trace.WriteLine($"Top Left Corner: {viewport.Bounds.Left}");
            Trace.WriteLine($"Bottom Right Corner: {viewport.Bounds.Right}");
            Trace.WriteLine($"Top-Left Coordinate: ({viewport.Bounds.Left}, {viewport.Bounds.Top})");
            Trace.WriteLine($"Bottom-Right Coordinate: ({viewport.Bounds.Right}, {viewport.Bounds.Bottom})");
        }

        protected override void Update(GameTime gameTime)
        {
            mScreenManager.Update();
            mCameraManager.Update();
            mKeyBindingManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            mScreenManager.Draw(mSpriteBatch);

            base.Draw(gameTime);
        }
    }
}