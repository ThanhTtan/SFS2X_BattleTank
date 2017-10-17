using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.Bases;
using SFS2XProject_BattleTank.OffSets;
using SFS2XProject_BattleTank.Sounds;
namespace SFS2XProject_BattleTank.Items
{
    public abstract class Item : GameObject
    {
        protected Texture2D _sprite;
        protected Color _color;
        protected float _totalLifeTime;
        protected SEffect _effectCollision;

        public float TOTAL_LEFT_TIME
        {
            get { return _totalLifeTime; }
            protected set { _totalLifeTime = value; }
        }
        private float _lifeTime;

        public float LEFT_TIME
        {
            get { return _lifeTime; }
            protected set { _lifeTime = value; }
        }

        protected bool _swapDir;
        public Item(float positionX, float positionY, float lifeTime)
            : base()
        {
            _essental = Constants.GAMEOBJECT_ITEM;
            _box = new Box2D(positionX, positionY, 0, 0, 0, 0);

            _lifeTime = lifeTime;
            _swapDir = false;
        }

        public override bool Init() { return true; }
        public override void LoadContents(ContentManager contents)
        {
            _sprite = contents.Load<Texture2D>("");
            _effectCollision.LoadContents(contents, "");
        }
        public override void Draw(SpriteBatch sp)
        {
            if (_sprite != null)
                sp.Draw(_sprite, new Rectangle((int)_box.x, (int)_box.y, _box.width, _box.height), _color);
        }
        public override void Update(float deltaTime)
        {
            _totalLifeTime += deltaTime;
            if (_totalLifeTime + 4 >= LEFT_TIME)
            {
                OutTimeEffect(deltaTime);
            }
        }
        public override void Behavior(string cmd) { }
        public override string Respose(string cmd) { return ""; }

        protected void OutTimeEffect(float deltaTime)
        {
            int r, g, b, a;
            r = _color.R;
            g = _color.G;
            b = _color.B;
            a = _color.A;
            if (_swapDir)
            {
                r += (int)(100 * deltaTime);
                g += (int)(100 * deltaTime);
                b += (int)(100 * deltaTime);
                a += (int)(100 * deltaTime);
                if (r >= 255 || g >= 255 || b >= 255 || a >= 255)
                    _swapDir = false;
            }
            else
            {
                r -= (int)(100 * deltaTime);
                g -= (int)(100 * deltaTime);
                b -= (int)(100 * deltaTime);
                a -= (int)(100 * deltaTime);
                if (r <= 100 || g <= 100 || b <= 100 || a <= 100)
                    _swapDir = false;
            }
            _color = new Color(r, g, b, a);
        }
    }
}
