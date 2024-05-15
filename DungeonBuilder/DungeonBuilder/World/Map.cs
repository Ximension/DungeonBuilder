using System.Collections.Generic;
using System.Diagnostics;
using DungeonBuilder.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonBuilder.World
{
    public class Map
    {
        private CameraManager mCameraManager;
        private KeyBindingManager mKeyBindingManager;
        private ResourceManager mResourceManager;

        private List<List<Texture2D>> mMapGrid;
        private Point mMapSize;
        private int mTileSize = 128;

        private int mGroupCount = 3;
        private Dictionary<int, int> mGroupToTileCount = new()
        {
            { 0, 8 },
            { 1, 8 },
            { 2, 8 }
        };

        private string mTilePath = "Map/Tiles";

        /// <summary>
        /// Creates a new Map
        /// </summary>
        /// <param name="cameraManager"></param>
        /// <param name="keyBindingManager"></param>
        /// <param name="initialSize">Point(rows, columns)</param>
        /// <param name="resourceManager"></param>
        public Map(Point initialSize, CameraManager cameraManager, KeyBindingManager keyBindingManager, ResourceManager resourceManager)
        {
            mCameraManager = cameraManager;
            mKeyBindingManager = keyBindingManager;
            mResourceManager = resourceManager;

            mMapSize = initialSize;
            // Create an empty Grid
            mMapGrid = new List<List<Texture2D>>(mMapSize.X);
            for (int row = 0; row < mMapSize.X; row++)
            {
                mMapGrid.Add(new List<Texture2D>(mMapSize.Y));
            }
        }

        public void LoadContent()
        {
            List<string> texturePathList = new() { mTilePath + "/empty" };

            // Fill list for textures
            for (int group = 0; group < mGroupCount; group++)
            {
                int tileCount = mGroupToTileCount[group];
                for (int tile = 0; tile < tileCount; tile++)
                {
                    texturePathList.Add(mTilePath + "/" + group + "/" + tile);
                }
            }

            mResourceManager.LoadTextures(texturePathList);

            // Fill the grid with the "empty" texture
            Texture2D emptyTile = mResourceManager.GetTexture(mTilePath + "/empty");
            for (int row = 0; row < mMapSize.X; row++)
            {
                for (int col = 0; col < mMapSize.Y; col++)
                {
                    mMapGrid[row].Add(emptyTile);
                }
            }
        }

        public void Update()
        {
            if (mKeyBindingManager.CheckAction(KeyBindingManager.Actions.DebugMapExtraRow))
            {
                ExtendRows(1);
            }
            if (mKeyBindingManager.CheckAction(KeyBindingManager.Actions.DebugMapExtraCol))
            {
                ExtendCols(1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int row = 0; row < mMapSize.X; row++)
            {
                for (int col = 0; col < mMapSize.Y; col++)
                {
                    spriteBatch.Begin(transformMatrix: mCameraManager.TransformationMatrix);
                    spriteBatch.Draw(mMapGrid[row][col], new Rectangle(row * mTileSize, col * mTileSize, mTileSize, mTileSize), Color.White);
                    spriteBatch.End();
                }
            }
        }

        /// <summary>
        /// Extends the map by extra rows
        /// </summary>
        /// <param name="extraRowCount">Amount of rows to be added</param>
        private void ExtendRows(int extraRowCount)
        {
            Texture2D emptyTile = mResourceManager.GetTexture(mTilePath + "/empty");
            for (int row = 0; row < extraRowCount; row++)
            {
                List<Texture2D> extraRow = new List<Texture2D>(mMapSize.Y);
                for (int col = 0; col < mMapSize.Y; col++)
                {
                    extraRow.Add(emptyTile);
                }
                mMapGrid.Add(extraRow);
                mMapSize.X++;
            }
        }

        /// <summary>
        /// Extends the map by extra columns
        /// </summary>
        /// <param name="extraColCount">Amount of columns to be added</param>
        private void ExtendCols(int extraColCount)
        {
            Texture2D emptyTile = mResourceManager.GetTexture(mTilePath + "/empty");
            foreach (List<Texture2D> row in mMapGrid)
            {
                row.Add(emptyTile);
            }
            mMapSize.Y++;
        }
    }
}
