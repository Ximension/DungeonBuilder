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
        }

        /// <summary>
        /// Loads textures into the manager
        /// </summary>
        /// <param name="texturePathList"></param>
        public void LoadTextures(List<string> texturePathList)
        {
            foreach (string texturePath in texturePathList)
            {
                Texture2D texture = mContent.Load<Texture2D>(texturePath);
                mTextures.Add(texturePath, texture);
            }
        }

        /// <summary>
        /// Loads soundeffects into the manager
        /// </summary>
        /// <param name="soundEffectPathList"></param>
        public void LoadSoundEffects(List<string> soundEffectPathList)
        {
            foreach (string soundEffectPath in soundEffectPathList)
            {
                SoundEffect soundEffect = mContent.Load<SoundEffect>(soundEffectPath);
                mSoundEffects.Add(soundEffectPath, soundEffect);
            }
        }

        /// <summary>
        /// Loads songs into the manager
        /// </summary>
        /// <param name="songPathList"></param>
        public void LoadSongs(List<string> songPathList)
        {
            foreach (string songPath in songPathList)
            {
                Song song = mContent.Load<Song>(songPath);
                mSongs.Add(songPath, song);
            }
        }

        /// <summary>
        /// Returns a Texture, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the Texture</param>
        /// <returns></returns>
        public Texture2D GetTexture(string path)
        {
            if (!mTextures.ContainsKey(path))
            {
                return null;
            }
            return mTextures[path];
        }

        /// <summary>
        /// Returns a SoundEffect, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the SoundEffect</param>
        /// <returns></returns>
        public SoundEffect GetSoundEffect(string path)
        {
            if (!mSoundEffects.ContainsKey(path))
            {
                return null;
            }
            return mSoundEffects[path];
        }

        /// <summary>
        /// Returns a Song, stored by the ResourceManager. If it does not exist, null is returned.
        /// </summary>
        /// <param name="path">path to the Song</param>
        /// <returns></returns>
        public Song GetSong(string path)
        {
            if (!mSongs.ContainsKey(path))
            {
                return null;
            }
            return mSongs[path];
        }
    }
}
