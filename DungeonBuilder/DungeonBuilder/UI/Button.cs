using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonBuilder.UI
{
    public class Button
    {
        private Texture2D mTexture;
        private SpriteFont mSpriteFont;
        private string mLabel;
        private Vector2 mLabelPos;
        private Rectangle mBounds;

        private ResourceManager mResourceManager;

        public Button(Point position, string label, ResourceManager resourceManager)
        {
            mResourceManager = resourceManager;
            mLabel = label;

            mBounds = new();
            mBounds.X = position.X;
            mBounds.Y = position.Y;
        }

        public void LoadContent(string spriteFontPath, string texturePath)
        {
            mSpriteFont = mResourceManager.GetSpriteFont(spriteFontPath, true);
            mTexture = mResourceManager.GetTexture(texturePath, true);

            mBounds.Width = mTexture.Bounds.Width;
            mBounds.Height = mTexture.Bounds.Height;

            mLabelPos = mBounds.Center.ToVector2() - mSpriteFont.MeasureString(mLabel) / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mTexture, mBounds, Color.White);
            spriteBatch.DrawString(mSpriteFont, mLabel, mLabelPos, Color.White);
            spriteBatch.End();
        }

        public Rectangle GetBounds()
        {
            return mBounds;
        }
    }
}
