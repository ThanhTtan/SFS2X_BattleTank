
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.Bases;
using SFS2XProject_BattleTank.OffSets;

namespace SFS2XProject_BattleTank.MainTank
{
    public class MBullet: GameObject
    {

        private float _speed;
        private int _xDir;
        private int _yDir;
        private Texture2D _sprite;

        public MBullet(int xDir,int yDir)
            :base()
        {
            _essental = Constants.GAMEOBJECT_MBULLET;
            _box = new Box2D();
            _xDir = xDir;
            _yDir = yDir;
            Init();
        }

        public override bool Init()
        {
            _speed = 50.0f;
            return true;
        }
        public override void LoadContents(ContentManager contents)
        {
            _sprite = contents.Load<Texture2D>("");
        }
        public override void Draw(SpriteBatch sp)
        {
            if (_sprite != null)
                sp.Draw(_sprite, new Rectangle((int)_box.x, (int)_box.y, _box.width, _box.height),Color.White);
        }
        public override void Update(float deltaTime)
        {
            Move(deltaTime);
        }
        public override void Behavior(string cmd) { }

        //
        private void Move(float deltaTime)
        {
            _box.x += _xDir * _speed * deltaTime;
            _box.y += _yDir * _speed * deltaTime;
        }
    }
}
