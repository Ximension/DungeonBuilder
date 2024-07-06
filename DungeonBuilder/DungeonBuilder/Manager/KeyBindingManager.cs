using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonBuilder.UI;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// Manages the binding of keys to actions
    /// </summary>
    public class KeyBindingManager
    {

        private InputManager mInputManager;
        public enum Actions
        {
            MoveCameraUp,
            MoveCameraDown,
            MoveCameraLeft,
            MoveCameraRight,
            ZoomCameraOut,
            ZoomCameraIn,
            DebugMapExtraRow,
            DebugMapExtraCol
        }

        private Dictionary<Actions, (Keys, InputManager.KeyState)> mKeyBindingDict = new()
        {
            { Actions.MoveCameraUp, (Keys.W, InputManager.KeyState.Held) },
            { Actions.MoveCameraDown, (Keys.S, InputManager.KeyState.Held)},
            { Actions.MoveCameraLeft, (Keys.A, InputManager.KeyState.Held)},
            { Actions.MoveCameraRight, (Keys.D, InputManager.KeyState.Held) },
            { Actions.DebugMapExtraRow, (Keys.D1, InputManager.KeyState.Pressed) },
            { Actions.DebugMapExtraCol, (Keys.D2, InputManager.KeyState.Pressed) },
        };

        private Dictionary<Actions, InputManager.ScrollWheelState> mScrollWheelBindingDict = new()
        {
            { Actions.ZoomCameraOut, InputManager.ScrollWheelState.Down },
            { Actions.ZoomCameraIn, InputManager.ScrollWheelState.Up }
        };

        private Dictionary<string, (Button, InputManager.ClickableButtonState)> mButtonBindingDict = new();

        /// <summary>
        /// Creates a new KeyBindingManager
        /// </summary>
        public KeyBindingManager()
        {
            // Creates a List for the InputManager
            mInputManager = new(GetAllKeys());
        }

        public void Update()
        {
            mInputManager.Update();
        }

        /// <summary>
        /// Checks, whether a specific action is currently carried out
        /// </summary>
        /// <param name="action"></param>
        /// <param name="consume"></param>
        /// <returns></returns>
        public bool CheckAction(Actions action, bool consume = true)
        {
            // If the action is bound to the scroll wheel, check the scroll wheel
            if (mScrollWheelBindingDict.Keys.Contains(action))
            {
                return mInputManager.CheckScrollWheelState(mScrollWheelBindingDict[action], consume);
            }
            // check the key otherwise
            (Keys, InputManager.KeyState) keyBinding = mKeyBindingDict[action];
            return mInputManager.CheckKeyState(keyBinding.Item1, keyBinding.Item2, consume);
        }

        /// <summary>
        /// Checks, whether a button has been pressed that corresponds to a given action
        /// </summary>
        /// <param name="returnValue">String that corresponds to a button</param>
        /// <param name="consume">Determines, whether the input press should be deleted</param>
        /// <returns></returns>
        public bool CheckAction(string returnValue, bool consume = true)
        {
            // Check the corresponding button to the return value
            (Button, InputManager.ClickableButtonState) buttonBinding = mButtonBindingDict[returnValue];
            return mInputManager.CheckButton(buttonBinding.Item1, buttonBinding.Item2, consume);
        }

        /// <summary>
        /// Changes the Keybindings for an action. The KeyState should not be changed.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public void ChangeKeyBinding(Actions action, Keys key)
        {
            mKeyBindingDict[action] = (key, mKeyBindingDict[action].Item2);
            mInputManager.UpdateBoundKeys(GetAllKeys());
        }

        /// <summary>
        /// Adds a button and a corresponding action
        /// </summary>
        /// <param name="newButton">Button to be added</param>
        /// <param name="returnValue">String that gets returned when the action is carried out</param>
        /// <param name="clickableButtonState">State the button must be in to return the value</param>
        public void AddButton(Button newButton, string returnValue, InputManager.ClickableButtonState clickableButtonState)
        {
            mButtonBindingDict.Add(returnValue, (newButton, clickableButtonState));
            mInputManager.AddButton(newButton);
        }

        /// <summary>
        /// Removes the binding to a button
        /// </summary>
        /// <param name="buttonToRemove"></param>
        public void RemoveButton(Button buttonToRemove)
        {
            foreach (string returnValue in mButtonBindingDict.Keys)
            {
                (Button, InputManager.ClickableButtonState) button = mButtonBindingDict[returnValue];
                if (button.Item1 == buttonToRemove)
                {
                    mButtonBindingDict.Remove(returnValue);
                }
            }
            mInputManager.RemoveButton(buttonToRemove);
        }

        /// <summary>
        /// Returns a list of all keys that are bound to an action
        /// </summary>
        /// <returns></returns>
        private List<Keys> GetAllKeys()
        {
            List<Keys> allKeys = new();
            foreach ((Keys, InputManager.KeyState) keyTuple in mKeyBindingDict.Values)
            {
                Keys key = keyTuple.Item1;
                allKeys.Add(key);
            }
            return allKeys;
        }
    }
}
