using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static DungeonBuilder.Manager.InputManager;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// Computes all keyboard and mouse inputs
    /// </summary>
    public class InputManager
    {
        private int mPreviousScrollWheelValue;
        private MouseState mMouseState;
        private HashSet<Keys> mPressed;
        private HashSet<Keys> mReleased;
        private HashSet<Keys> mHeld;
        private HashSet<MouseButton> mPressedMouse;
        private HashSet<MouseButton> mReleasedMouse;
        private HashSet<MouseButton> mHeldMouse;
        private HashSet<Button> mClickedButtons;
        private HashSet<Button> mReleasedButtons;
        private HashSet<Button> mHeldButtons;
        private HashSet<Button> mHoveredButtons;

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

        public enum ClickableButtonState
        {
            Clicked,
            Released,
            Held,
            Hovered
        }

        public enum MouseButton
        {
            Left,
            Right,
            Middle
        }

        private ScrollWheelState mCurrentScrollWheelState;
        private List<Keys> mBoundKeyList;
        private List<Button> mBoundButtons;
        /// <summary>
        /// Creates a new InputManager
        /// </summary>
        public InputManager(List<Keys> boundKeyList)
        {
            mPressed = new();
            mReleased = new();
            mHeld = new();

            mPressedMouse = new();
            mReleasedMouse = new();
            mHeldMouse = new();

            mClickedButtons = new();
            mReleasedButtons = new();
            mHeldButtons = new();
            mHoveredButtons = new();

            mCurrentScrollWheelState = ScrollWheelState.Still;
            mBoundKeyList = boundKeyList;
            mBoundButtons = new();
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
                    if (!mPressed.Contains(key) && !mHeld.Contains(key))
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
            int currentScrollWheelValue = mMouseState.ScrollWheelValue;

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

            mPreviousScrollWheelValue = mMouseState.ScrollWheelValue;
        }

        /// <summary>
        /// Updates the MouseState and computes inputs over the mouse buttons
        /// </summary>
        private void UpdateMouse()
        {
            mMouseState = Mouse.GetState();
            // Create a list of mouse buttons that should be checked
            ButtonState[] buttonsToCheck = { mMouseState.LeftButton, mMouseState.RightButton, mMouseState.MiddleButton };
            MouseButton[] currentlyChecked = { MouseButton.Left, MouseButton.Right, MouseButton.Middle };

            // Go through the list of mouse buttons
            for (int buttonI = 0; buttonI < currentlyChecked.Length; buttonI++)
            {
                MouseButton currentButton = currentlyChecked[buttonI];
                // if the button is pressed, it is either pressed or held depending on if it was pressed before
                if (buttonsToCheck[buttonI] == ButtonState.Pressed)
                {
                    if (!mPressedMouse.Contains(currentButton) && !mHeldMouse.Contains(currentButton))
                    {
                        mPressedMouse.Add(currentButton);
                    }
                    else
                    {
                        mPressedMouse.Remove(currentButton);
                        mHeldMouse.Add(currentButton);
                    }
                }
                // if the button is not pressed and was released before, it is now no longer released
                else if (mReleasedMouse.Contains(currentButton))
                {
                    mReleasedMouse.Remove(currentButton);
                }
                // otherwise the button was just released
                else
                {
                    if (mPressedMouse.Contains(currentButton))
                    {
                        mPressedMouse.Remove(currentButton);
                    }
                    else if (mHeldMouse.Contains(currentButton))
                    {
                        mHeldMouse.Remove(currentButton);
                    }
                    mReleasedMouse.Add(currentButton);
                }
            }

        }

        /// <summary>
        /// Updates all buttons so that they are inserted in the corresponding list to determine their state.
        /// </summary>

        private void UpdateButtons()
        {
            foreach (Button button in mBoundButtons)
            {
                bool inBounds = button.GetBounds().Contains(mMouseState.Position);
                bool leftDown = (mMouseState.LeftButton == ButtonState.Pressed);

                if (inBounds && leftDown)
                {
                    // if a button is down and was pressed just before, it is held
                    // if a button is down and was not pressed just before, it is pressed
                    if (!mClickedButtons.Contains(button) && !mHeldButtons.Contains(button))
                    {
                        mClickedButtons.Add(button);
                    }
                    else
                    {
                        mClickedButtons.Remove(button);
                        mHeldButtons.Add(button);
                    }
                }
                else
                {
                    // if a button is not down but was released just before, it now isn't released anymore
                    if (mReleasedButtons.Contains(button))
                    {
                        mReleasedButtons.Remove(button);
                    }// if a button is not down but was down just before, it is now released
                    else
                    {
                        if (mClickedButtons.Contains(button))
                        {
                            mClickedButtons.Remove(button);
                            mReleasedButtons.Add(button);
                        }
                        else if (mHeldButtons.Contains(button))
                        {
                            mHeldButtons.Remove(button);
                            mReleasedButtons.Add(button);
                        }
                    }
                }

                if (inBounds)
                {
                    // if the mouse is over a button, it is hovered
                    mHoveredButtons.Add(button);
                }
                else
                {
                    // if the mouse is not over the button, it is not hovered anymore
                    mHoveredButtons.Remove(button);
                }
            }
        }

        /// <summary>
        /// Updates Mouse, Keys, scroll wheel and Buttons
        /// </summary>

        public void Update()
        {
            UpdateMouse();
            UpdateKeys();
            UpdateScrollWheel();
            UpdateButtons();
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
        /// Checks,whether a mouse button is currently pressed, held or released
        /// </summary>
        /// <param name="button">button to be checked</param>
        /// <param name="keyState">check for pressed, held or released</param>
        /// <param name="consume">if the click should be used multiple times, set false</param>
        /// <returns></returns>
        public bool CheckMouse(MouseButton button, KeyState keyState, bool consume = true)
        {
            switch (keyState)
            {
                case KeyState.Pressed:
                    if (consume)
                    {
                        return mPressedMouse.Remove(button);
                    }
                    return mPressedMouse.Contains(button);
                case KeyState.Held:
                    if (consume)
                    {
                        return mHeldMouse.Remove(button);
                    }
                    return mHeldMouse.Contains(button);
                case KeyState.Released:
                    if (consume)
                    {
                        return mReleasedMouse.Remove(button);
                    }
                    return mReleasedMouse.Contains(button);
                default:
                    return false;
            }
        }


        /// <summary>
        /// Checks for one state for one button
        /// </summary>
        /// <param name="button">Button to be checked</param>
        /// <param name="buttonState">State to check for (pressed, held, released or hovered)</param>
        /// <param name="consume">Determines, whether the state should be deleted to not be used later</param>
        /// <returns></returns>
        public bool CheckButton(Button button, ClickableButtonState buttonState, bool consume = true)
        {
            switch (buttonState)
            {
                case ClickableButtonState.Clicked:
                    if (consume)
                    {
                        return mClickedButtons.Remove(button);
                    }
                    return mClickedButtons.Contains(button);
                case ClickableButtonState.Held:
                    if (consume)
                    {
                        return mHeldButtons.Remove(button);
                    }
                    return mHeldButtons.Contains(button);
                case ClickableButtonState.Released:
                    if (consume)
                    {
                        return mReleasedButtons.Remove(button);
                    }
                    return mReleasedButtons.Contains(button);
                case ClickableButtonState.Hovered:
                    if (consume)
                    {
                        return mHoveredButtons.Remove(button);
                    }
                    return mHoveredButtons.Contains(button);
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

        /// <summary>
        /// Add a button that should be checked constantly by the manager
        /// </summary>
        /// <param name="button">Button to be checked</param>
        public void AddButton(Button button)
        {
            mBoundButtons.Add(button);
        }

        /// <summary>
        /// Removes a button from being checked by the manager
        /// </summary>
        /// <param name="button"></param>
        public void RemoveButton(Button button)
        {
            mBoundButtons.Remove(button);
        }
    }
}
