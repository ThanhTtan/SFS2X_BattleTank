
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SFS2XProject_BattleTank.InputControls;
using SFS2XProject_BattleTank.OffSets;
namespace SFS2XProject_BattleTank.UI
{
    public class Button
    {
        protected string _label;
        protected Texture2D _sprite;
        protected Rectangle _boundingBox;
        protected Vector2 _position;
        protected SpriteFont _font;
        protected bool _active;
        protected Color _color;
        protected string _invi;
        protected string _Point;
        protected bool _NonACtive;
        protected int _NumOfClick = 1;

        public Button(Vector2 position, Rectangle boundingbox)
        {

            _sprite = null;
            _boundingBox = boundingbox;
            _position = position;
            _active = false;
            _color = Color.White;
        }
        public Button()
        {
            _label = "";
            _sprite = null;
            _boundingBox = new Rectangle();

            _active = false;
            _color = Color.White;
        }

        public void Init(Vector2 position, Rectangle boundingBox)
        {
            _boundingBox = boundingBox;
            _position = position;
            _active = true;
        }
        public void LoadContents(ContentManager content, string fontPath = "", string spritePath = "")
        {
            if (fontPath != "")
            {
                _font = content.Load<SpriteFont>(fontPath);
                _font.DefaultCharacter = '?';
            }
            if (spritePath != "")
            {
                _sprite = content.Load<Texture2D>(spritePath);
            }

        }

        public virtual void Draw(SpriteBatch sp)
        {
            Vector2 center = new Vector2(_position.X + _boundingBox.Width / 2, _position.Y + _boundingBox.Height / 2);
            Vector2 size = Vector2.Zero;
            GetSizeText(_label, out size.X, out size.Y);
            Vector2 textPos = center - size / 2;
            sp.DrawString(_font, _label, textPos, _color);
        }
        public virtual void TestDraw(SpriteBatch sp)
        {
            Vector2 textPos = _position + new Vector2(35, 30);
            if (_active == true)
                sp.DrawString(_font, _label + "|", textPos, color: Color.Black);
            else
            {
                sp.DrawString(_font, _label, textPos, color: Color.Black);
            }
        }
        public virtual void InvilPass(SpriteBatch sp)
        {




            Vector2 textPos = _position + new Vector2(35, 30);
            if (_active == true)
                sp.DrawString(_font, _Point + "|", textPos, color: Color.Black);
            else
            {
                sp.DrawString(_font, _Point, textPos, color: Color.Black);
            }
        }
        public virtual void Update(float deltaTime, MouseState mouse)
        {
            //if (_active)
            if (IsInside(mouse))
            {
                Hover();
                if (Input.Clicked(Constants.MOUSEBUTTON_LEFT))
                {
                    Behavior();
                    _NumOfClick++;

                }
            }
            else
            {

                Default();
            }

        }

        protected virtual void Behavior()
        {
            // when click
        }
        protected virtual void Hover()
        {
            // effect when hover
        }
        protected virtual void Default()
        {
            // normal do nothing
        }

        protected bool IsInside(MouseState mouse)
        {

            Rectangle rec = new Rectangle();
            rec = _boundingBox;
            rec.X = (int)_position.X;
            rec.Y = (int)_position.Y;

            Rectangle mousrec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mousrec.Intersects(rec))
            {
                _color = Color.Red;
                return true;
            }
            return false;
        }
        protected void GetSizeText(string text, out float width, out float height)
        {
            Vector2 size = _font.MeasureString(text);
            width = size.X;
            height = size.Y;
        }
    }

}

