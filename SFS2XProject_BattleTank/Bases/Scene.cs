
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SFS2XProject_BattleTank.Bases
{
    public abstract class Scene
    {
        protected string _name;
        protected bool _isInit;
        protected ContentManager _contents;

        public Scene(string name, ContentManager contents)
        {
            _name = name;
            _contents = contents;
            _isInit = false;
        }

        public virtual bool Init()
        {
            _isInit = LoadContents();
            return _isInit;
        }
        public virtual void Shutdown()
        {
            _contents.Unload();
            _isInit = false;
        }
        public virtual void Draw(SpriteBatch sp) { }
        public virtual void Update(float deltaTime)
        {
        }
        public virtual bool LoadContents() { return true; }

        // properties
        public bool INIT
        {
            get { return _isInit; }
            protected set { _isInit = value; }
        }
        public string NAME
        {
            get { return _name; }
            set { _name = value; }
        }
        protected ContentManager CONTENTS
        {
            get { return _contents; }
            set { _contents = value; }
        }
    }
}
