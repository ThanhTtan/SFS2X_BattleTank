
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.Bases;
using SFS2XProject_BattleTank.GameManagers;
using SFS2XProject_BattleTank.OffSets;
using System.Collections.Generic;


namespace SFS2XProject_BattleTank.Enemy
{
    class ETank : GameObject
    {
        private float _speed;
        private Rectangle _sourceRect;
        private Texture2D _sprite;

        private float _totalElapsedTime;
        private int _curFrame;
        private float _delayTime;
        private float _rotation;
        private int _xDir;
        private int _yDir;

        private ETankControl _control;
        private ContentManager _contents;

        private List<EBullet> _bullets;
        public ETank()
            : base()
        {
            _box = new Box2D(0, 0, 0, 0, 0, 0);
            _essental = Constants.GAMEOBJECT_ENEMY;
            _speed = 50.0f;
            _control = new ETankControl();
        }

        public override bool Init()
        {
            _totalElapsedTime = 0.0f;
            _delayTime = 0.2f;
            _curFrame = 0;
            _rotation = 0.0f;
            _bullets = new List<EBullet>();
            return true;
        }
        public override void LoadContents(ContentManager contents)
        {
            _contents = contents;
            _sprite = contents.Load<Texture2D>("");
        }
        public override void Draw(SpriteBatch sp)
        {
            if (_sprite != null)
                sp.Draw(_sprite,
                    destinationRectangle: new Rectangle((int)_box.x, (int)_box.y, _box.width, _box.height),
                    sourceRectangle: _sourceRect,
                    rotation: _rotation,
                    color: Color.White);
        }
        public override void Update(float deltaTime)
        {
            // shoot
            if (_control.Shoot(deltaTime)) Shoot();
            _control.GetDirection(ref _xDir, ref _yDir, deltaTime);
            // moving
            if (_xDir == -1) _rotation = MathHelper.ToRadians(180);
            if (_xDir == 1) _rotation = MathHelper.ToRadians(0);
            if (_yDir == -1) _rotation = MathHelper.ToRadians(90);
            if (_yDir == 1) _rotation = MathHelper.ToRadians(-90);
            Move(_xDir, _yDir, deltaTime);
            UpdateBullet(deltaTime);
            // update box2d
            _box.vx = _xDir * _speed;
            _box.vy = _yDir * _speed;
        }
        public override void Behavior(string cmd)
        {
            base.Behavior(cmd);
        }

        private void Move(int xDir, int yDir, float deltaTime)
        {
            _box.x += xDir * _speed * deltaTime;
            _box.y += yDir * _speed * deltaTime;
            
            Animation(deltaTime);
            _sourceRect = new Rectangle(_curFrame * 32, _sourceRect.Y, _sourceRect.Width, _sourceRect.Height);
        }
        private void Shoot()
        {
            _bullets.Add(new EBullet(_xDir, _yDir));
        }
        private void Animation(float deltaTime)
        {
            if (_totalElapsedTime >= _delayTime)
            {
                _curFrame = (_curFrame + 1) % 8;
                _totalElapsedTime = 0;
            }
            else
            {
                _totalElapsedTime += deltaTime;
            }
        }
        private void UpdateBullet(float deltaTime)
        {
            foreach (EBullet b in _bullets)
            {
                b.Update(deltaTime);
            }
            foreach (EBullet b in _bullets)
            {
                if (BulletOutside(b))
                {
                    RemoveBullet(b);
                }
            }
        }
        private void RemoveBullet(EBullet b)
        {
            _bullets.Remove(b);
            GameManager.RemoveObject(b);
        }
        private bool BulletOutside(EBullet b)
        {
            return false;
        }
    }
}
