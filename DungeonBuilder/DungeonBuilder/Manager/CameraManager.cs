﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DungeonBuilder.Manager
{
    /// <summary>
    /// This class manages the camera. This means zooming and moving on the map.
    /// </summary>
    public class CameraManager
    {
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

            TransformationMatrix = zoomMatrix * translationMatrix;
        }

        private void Zoom(float zoomFactor)
        {
            Matrix zoomMatrix = Matrix.CreateScale(zoomFactor);
            TransformationMatrix = TransformationMatrix * zoomMatrix;
        }

        private void Move(Vector2 moveVector)
        {
            Matrix moveMatrix = Matrix.CreateTranslation(new Vector3(moveVector, 0));
            TransformationMatrix = TransformationMatrix * moveMatrix;
        }
    }
}