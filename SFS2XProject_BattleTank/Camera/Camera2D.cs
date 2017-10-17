

using Microsoft.Xna.Framework;
using SFS2XProject_BattleTank.OffSets;

namespace SFS2XProject_BattleTank.Camera
{
    public class Camera
    {
        private int _viewportWidth;
        private int _viewportHeight;
        private bool _follow;
        private Vector2 _followPos;
        private float _zoom;
        private float _rotation;


        public Camera()
        {
            _viewportWidth = Constants.VIEWPORT_WIDTH;
            _viewportHeight = Constants.VIEWPORT_HEIGHT;
            _follow = false;
            _zoom = 1.0f;
            _rotation = 0.0f;
            _followPos = Vector2.Zero;
        }

        public void Update(float deltaTime, Vector2 follow)
        {
            if (_follow)
                _followPos = follow;
        }

        public Matrix GetTransfromMatrix()
        {
            Matrix result = Matrix.Identity;
            result = Matrix.CreateTranslation(new Vector3(-_followPos, 0))
                * Matrix.CreateScale(_zoom)
                * Matrix.CreateRotationZ(_rotation)
                * Matrix.CreateTranslation(new Vector3(_viewportWidth / 2, _viewportHeight / 2, 0));
            return result;
        }
        public void ZoomOut()
        {
            _zoom -= 0.2f;
            if (_zoom <= 0.1f)
            {
                _zoom = 0.2f;
            }
        }
        public void ZoomIn()
        {
            _zoom += 0.2f;
        }
        public void Follow(bool value)
        {
            _follow = value;
        }
    }
}
