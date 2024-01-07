using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            { Actions.MoveCameraUp, (Keys.W, InputManager.KeyState.Pressed) },
            { Actions.MoveCameraDown, (Keys.S, InputManager.KeyState.Pressed)},
            { Actions.MoveCameraLeft, (Keys.A, InputManager.KeyState.Pressed)},
            { Actions.MoveCameraRight, (Keys.D, InputManager.KeyState.Pressed) },
            { Actions.DebugMapExtraRow, (Keys.D1, InputManager.KeyState.Pressed) },
            { Actions.DebugMapExtraCol, (Keys.D2, InputManager.KeyState.Pressed) },
        };

        private Dictionary<Actions, InputManager.ScrollWheelState> mScrollWheelBindingDict = new()
        {
            { Actions.ZoomCameraOut, InputManager.ScrollWheelState.Down },
            { Actions.ZoomCameraIn, InputManager.ScrollWheelState.Up }
        };

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

        // Returns a list of all keys that are bound to an action
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
