using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Helps keeping a reference to the main Camera.
    /// </summary>
    public static class CameraHelper
    {
        private static Camera _camera;

        public static Camera Camera
        {
            get
            {
                if (_camera == null) _camera = Camera.main;
                return _camera;
            }
        }
    }
}