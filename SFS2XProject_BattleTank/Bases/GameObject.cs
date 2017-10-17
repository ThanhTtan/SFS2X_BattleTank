
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.GameManagers;

namespace SFS2XProject_BattleTank.Bases
{
    public abstract class GameObject
    {
        // only one id with one object
        protected ulong _id;
        // box used to collision detection
        protected Box2D _box;
        // essental's obj
        protected string _essental;
        public GameObject()
        {
            _id = GameManager.GetId();
            _box = new Box2D();
            _essental = "";
            GameManager.AddObject(this);
        }

        public virtual bool Init() { return true; }
        public virtual void LoadContents(ContentManager contents) { }
        public virtual void Draw(SpriteBatch sp) { }
        public virtual void Update(float deltaTime) { }
        public virtual void Behavior(string cmd) { }
        public virtual string Respose(string cmd) { return ""; }
        // properties
        public ulong ID
        {
            get { return _id; }
            private set { _id = value; }
        }
        public Box2D BOX2D
        {
            get { return _box; }
            protected set { _box = value; }
        }
        public string Essental()
        {
            return _essental;
        }
    }
}
