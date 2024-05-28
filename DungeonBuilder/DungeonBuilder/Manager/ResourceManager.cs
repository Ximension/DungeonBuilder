using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// Manages textures, soundeffects and songs and offers methods to get resources if needed.
    /// </summary>
    public class ResourceManager
    {
        private Dictionary<string, Texture2D> mTextures;
        private Dictionary<string, SoundEffect> mSoundEffects;
        private Dictionary<string, Song> mSongs;
        private Dictionary<string, SpriteFont> mSpriteFonts;

        private ContentManager mContent;

        /// <summary>
        /// Create a new ResourceManager
        /// </summary>
        /// <param name="content">ContentManager which will be used to load in content</param>
        public ResourceManager(ContentManager content)
        {
            mContent = content;

            mTextures = new();
            mSoundEffects = new();
            mSongs = new();
            mSpriteFonts = new();
        }

        /// <summary>
        /// Load a texture into the manager
        /// </summary>
        /// <param name="texturePath"></param>
        public void LoadTexture(string texturePath)
        {
            if (mTextures.Keys.Contains(texturePath))
            {
                return;
            }
            Texture2D texture = mContent.Load<Texture2D>(texturePath);
            mTextures.Add(texturePath, texture);
        }

        /// <summary>
        /// Loads textures into the manager
        /// </summary>
        /// <param name="texturePathList"></param>
        public void LoadTextures(List<string> texturePathList)
        {
            foreach (string texturePath in texturePathList)
            {
                LoadTexture(texturePath);
            }
        }

        /// <summary>
        /// Loads a sound effect into the manager
        /// </summary>
        /// <param name="soundEffectPath"></param>
        public void LoadSoundEffect(string soundEffectPath)
        {
            SoundEffect soundEffect = mContent.Load<SoundEffect>(soundEffectPath);
            mSoundEffects.Add(soundEffectPath, soundEffect);
        }

        /// <summary>
        /// Loads sound effects into the manager
        /// </summary>
        /// <param name="soundEffectPathList"></param>
        public void LoadSoundEffects(List<string> soundEffectPathList)
        {
            foreach (string soundEffectPath in soundEffectPathList)
            {
                LoadSoundEffect(soundEffectPath);
            }
        }

        /// <summary>
        /// Loads a song into the manager
        /// </summary>
        /// <param name="songPath"></param>
        public void LoadSong(string songPath)
        {
            Song song = mContent.Load<Song>(songPath);
            mSongs.Add(songPath, song);
        }

        /// <summary>
        /// Loads songs into the manager
        /// </summary>
        /// <param name="songPathList"></param>
        public void LoadSongs(List<string> songPathList)
        {
            foreach (string songPath in songPathList)
            {
                LoadSong(songPath);
            }
        }

        /// <summary>
        /// Loads a sprite font into the manager
        /// </summary>
        /// <param name="spriteFontPath"></param>
        public void LoadSpriteFont(string spriteFontPath)
        {
            SpriteFont spriteFont = mContent.Load<SpriteFont>(spriteFontPath);
            mSpriteFonts.Add(spriteFontPath, spriteFont);
        }

        /// <summary>
        /// Loads sprite fonts into the manager
        /// </summary>
        /// <param name="spriteFontPathList"></param>
        public void LoadSpriteFonts(List<string> spriteFontPathList)
        {
            foreach (string spriteFontPath in spriteFontPathList)
            {
                LoadSpriteFont(spriteFontPath);
            }
        }

        /// <summary>
        /// Returns a Texture, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the Texture</param>
        /// <returns></returns>
        public Texture2D GetTexture(string path, bool add = false)
        {
            if (!mTextures.ContainsKey(path))
            {
                if (!add)
                {
                    return null;
                }
                LoadTexture(path);
            }
            return mTextures[path];
        }

        /// <summary>
        /// Returns a SoundEffect, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the SoundEffect</param>
        /// <returns></returns>
        public SoundEffect GetSoundEffect(string path, bool add=false)
        {
            if (!mSoundEffects.ContainsKey(path))
            {
                if (!add)
                {
                    return null;
                }
                LoadSoundEffect(path);
            }
            return mSoundEffects[path];
        }

        /// <summary>
        /// Returns a Song, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the Song</param>
        /// <returns></returns>
        public Song GetSong(string path, bool add=false)
        {
            if (!mSongs.ContainsKey(path))
            {
                if (!add)
                {
                    return null;
                }
                LoadSong(path);
            }
            return mSongs[path];
        }

        /// <summary>
        /// Returns a SpriteFont, stored by the ResourceManager.
        /// </summary>
        /// <param name="path">path to the SpriteFont</param>
        /// <param name="add">If true, texture will be added if it does not exist</param>
        /// <returns></returns>
        public SpriteFont GetSpriteFont(string path, bool add = false)
        {
            if (!mSpriteFonts.ContainsKey(path))
            {
                if (!add)
                {
                    return null;
                }
                this.LoadSpriteFont(path);
            }
            return mSpriteFonts[path];
        }
    }
}
