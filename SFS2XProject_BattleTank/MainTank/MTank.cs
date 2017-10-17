
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.Bases;
using SFS2XProject_BattleTank.GameManagers;
using SFS2XProject_BattleTank.OffSets;
using System.Collections.Generic;

namespace SFS2XProject_BattleTank.MainTank
{
    public class MTank : GameObject
    {
        private int _gameId;
        private float _speed;
        private Rectangle _sourceRect;
        private Texture2D _sprite;

        private float _totalElapsedTime;
        private int _curFrame;
        private float _delayTime;
        private float _rotation;

        private MTankControl _control;
        private ContentManager _contents;

        private List<MBullet> _bullets;
        public MTank(int gameId)
            : base()
        {
            _box = new Box2D(0, 0, 0, 0, 0, 0);
            _essental = Constants.GAMEOBJECT_MAINTANK;
            _speed = 50.0f;
            _control = new MTankControl();
            _gameId = gameId;
        }

        public override bool Init()
        {
            _totalElapsedTime = 0.0f;
            _delayTime = 0.2f;
            _curFrame = 0;
            _rotation = 0.0f;
            _bullets = new List<MBullet>();
            return true;
        }
        public override void LoadContents(ContentManager contents)
        {
            _contents = contents;
            if (_gameId >= 0 && _gameId <= 6)
            {
                _sourceRect = new Rectangle(0, _gameId * 32, 32, 32);
            }
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
            #region moving character
            switch (_control.GetDirectionKey())
            {
                case Microsoft.Xna.Framework.Input.Keys.A:
                    {
                        Move(-1, 0, deltaTime);
                        _rotation = MathHelper.ToRadians(180);
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.D:
                    {
                        Move(1, 0, deltaTime);
                        _rotation = MathHelper.ToRadians(0);
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.W:
                    {
                        Move(0, 1, deltaTime);
                        _rotation = MathHelper.ToRadians(-90);
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.S:
                    {
                        Move(0, -1, deltaTime);
                        _rotation = MathHelper.ToRadians(90);
                        break;
                    }
                default:
                    {
                        _curFrame = 0;
                        _totalElapsedTime = 0;
                        break;
                    }
            }
            #endregion
            if (_control.Shoot(deltaTime)) Shoot();
            UpdateBullet(deltaTime);
        }
        public override void Behavior(string cmd)
        {
            base.Behavior(cmd);
        }

        private void Move(int xDir, int yDir, float deltaTime)
        {
            _box.x += xDir * _speed * deltaTime;
            _box.y += yDir * _speed * deltaTime;
            SetBoxVelocity(ref _box, xDir * _speed * deltaTime, yDir * _speed * deltaTime);
            Animation(deltaTime);
            _sourceRect = new Rectangle(_curFrame * 32, _sourceRect.Y, _sourceRect.Width, _sourceRect.Height);
        }
        private void Shoot()
        {
            switch (_control.GetDirectionKey())
            {
                case Microsoft.Xna.Framework.Input.Keys.A:
                    {
                        _bullets.Add(new MBullet(-1, 0));
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.D:
                    {
                        _bullets.Add(new MBullet(1, 0));
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.W:
                    {
                        _bullets.Add(new MBullet(0, 1));
                        break;
                    }
                case Microsoft.Xna.Framework.Input.Keys.S:
                    {
                        _bullets.Add(new MBullet(0, -1));
                        break;
                    }
            }
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
            foreach (MBullet b in _bullets)
            {
                b.Update(deltaTime);
            }
            foreach (MBullet b in _bullets)
            {
                if (BulletOutside(b))
                {
                    RemoveBullet(b);
                }
            }
        }
        private void RemoveBullet(MBullet b)
        {
            _bullets.Remove(b);
            GameManager.RemoveObject(b);
        }
        private bool BulletOutside(MBullet b)
        {
            return false;
        }
        private void SetBoxVelocity(ref Box2D box,float x,float y)
        {
            box.vx = x;
            box.vy = y;
        }
    }
}
