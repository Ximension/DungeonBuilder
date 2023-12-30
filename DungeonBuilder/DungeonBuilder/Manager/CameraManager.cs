using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// This class manages the camera. This means zooming and moving on the map.
    /// </summary>
    public class CameraManager
    {
        private int mPrevMouseWheel = 0; // TODO: put in inputmanager
        public Matrix TransformationMatrix { get; private set; } 

        /// <summary>
        /// Creates a new CameraManager
        /// </summary>
        /// <param name="initialPosition">The center of the camera should be on this coordinate</param>
        /// <param name="initialZoom">The zoom of the camera</param>
        public CameraManager(Vector2 initialPosition, float initialZoom)
        {
            Matrix zoomMatrix = Matrix.CreateScale(initialZoom);
            Matrix translationMatrix = Matrix.CreateTranslation(new Vector3(initialPosition, 0));

            TransformationMatrix = translationMatrix * zoomMatrix;
        }

        private void Zoom(float zoomFactor)
        {
            Matrix zoomMatrix = Matrix.CreateScale(zoomFactor);
            TransformationMatrix *= zoomMatrix;
        }

        private void Move(Vector2 moveVector)
        {
            Matrix moveMatrix = Matrix.CreateTranslation(new Vector3(moveVector, 0));
            TransformationMatrix *= moveMatrix;
        }

        public void Update()
        {
            // TODO: put in inputmanager
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Move(new Vector2(0, 5));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Move(new Vector2(5, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Move(new Vector2(0, -5));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Move(new Vector2( -5, 0));
            }
            int wheel = Mouse.GetState().ScrollWheelValue;
            if (wheel > mPrevMouseWheel)
            {
                Zoom(1.2f);
                mPrevMouseWheel = wheel;
            }
            if (wheel < mPrevMouseWheel)
            {
                Zoom(0.8f);
                mPrevMouseWheel = wheel;
            }
        }
    }
}
