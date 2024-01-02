using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// Computes all keyboard and mouse inputs
    /// </summary>
    public class InputManager
    {
        private int mPreviousScrollWheelValue;
        private HashSet<Keys> mPressed;
        private HashSet<Keys> mReleased;
        private HashSet<Keys> mHeld;


        public enum KeyState
        {
            Pressed,
            Released,
            Held
        }

        public enum ScrollWheelState
        {
            Up,
            Down,
            Still
        }

        private ScrollWheelState mCurrentScrollWheelState;
        private List<Keys> mBoundKeyList;
        /// <summary>
        /// Creates a new InputManager
        /// </summary>
        public InputManager(List<Keys> boundKeyList)
        {
            mPressed = new();
            mReleased = new();
            mHeld = new();
            mCurrentScrollWheelState = ScrollWheelState.Still;
            mBoundKeyList = boundKeyList;
        }

        /// <summary>
        /// Computes all current KeyStates and whether a key is pressed, held or released
        /// </summary>
        private void UpdateKeys()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (Keys key in mBoundKeyList)
            {
                // if a key is down and was pressed just before, it is held
                // if a key is down and was not pressed just before, it is pressed
                if (keyboardState.IsKeyDown(key))
                {
                    if (!mPressed.Contains(key))
                    {
                        mPressed.Add(key);
                    }
                    else
                    {
                        mPressed.Remove(key);
                        mHeld.Add(key);
                    }
                }
                // if a key is not down but was released just before, it now isn't released anymore
                else if (mReleased.Contains(key))
                {
                    mReleased.Remove(key);
                }
                // if a key is not down but was down just before, it is now released
                else
                {
                    if (mPressed.Contains(key))
                    {
                        mPressed.Remove(key);
                        mReleased.Add(key);
                    }
                    else if (mHeld.Contains(key))
                    {
                        mHeld.Remove(key);
                        mReleased.Add(key);
                    }
                }
            }
        }

        /// <summary>
        /// Computes the MouseWheel
        /// </summary>
        private void UpdateScrollWheel()
        {
            // Compares the last and the current value of the scroll wheel
            // and decides whether the wheel is scrolling up/down or whether it is still
            // it then updates the previous value of the scroll wheel to prevent infinite scrolling
            MouseState mouseState = Mouse.GetState();
            int currentScrollWheelValue = mouseState.ScrollWheelValue;

            if (currentScrollWheelValue < mPreviousScrollWheelValue)
            {
                mCurrentScrollWheelState = ScrollWheelState.Down;
            }
            else if (currentScrollWheelValue > mPreviousScrollWheelValue)
            {
                mCurrentScrollWheelState = ScrollWheelState.Up;
            }
            else
            {
                mCurrentScrollWheelState = ScrollWheelState.Still;
            }
           
            mPreviousScrollWheelValue = mouseState.ScrollWheelValue;
        }

        public void Update()
        {
            UpdateKeys();
            UpdateScrollWheel();
        }

        /// <summary>
        /// Checks whether a key is currently pressed, released or held
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyState"></param>
        /// <param name="consume">If true, the key won't be marked as pressed/released/held anymore</param>
        /// <returns></returns>
        public bool CheckKeyState(Keys key, KeyState keyState, bool consume = true)
        {
            // Only tests for the specified KeyState
            // Removes the input from the corresponding list if it should be consumed
            switch (keyState)
            {
                case KeyState.Pressed:
                    if (consume)
                    {
                        return mPressed.Remove(key);
                    }
                    return mPressed.Contains(key);
                case KeyState.Held:
                    if (consume)
                    {
                        return mHeld.Remove(key);
                    }
                    return mHeld.Contains(key);
                case KeyState.Released:
                    if (consume)
                    {
                        return mReleased.Remove(key);
                    }
                    return mReleased.Contains(key);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks, whether the mouse is currently scrolling up/down
        /// </summary>
        /// <param name="scrollWheelState"></param>
        /// <param name="consume"></param>
        /// <returns></returns>
        public bool CheckScrollWheelState(ScrollWheelState scrollWheelState, bool consume = true)
        {
            return mCurrentScrollWheelState == scrollWheelState;
        }

        /// <summary>
        /// Updates the list of keys that should be checked
        /// </summary>
        /// <param name="boundKeyList"></param>
        public void UpdateBoundKeys(List<Keys> boundKeyList)
        {
            mBoundKeyList = boundKeyList;
        }
    }
}
