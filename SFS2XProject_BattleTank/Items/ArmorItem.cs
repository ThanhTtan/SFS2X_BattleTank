
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.OffSets;
namespace SFS2XProject_BattleTank.Items
{
    class ArmorItem : Item
    {
        public ArmorItem(float positionX, float positionY, float lifeTime)
            : base(positionX,positionY,lifeTime)
        {
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
            base.Update(deltaTime);
        }
        public override void Behavior(string cmd) { }
        public override string Respose(string cmd) { return Constants.RESPONSE_ITEM_ARMOR; }
    }
}
