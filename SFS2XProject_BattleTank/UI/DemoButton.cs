using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFS2XProject_BattleTank.UI
{
    public class DemoButton : Button
    {
        protected float _timeInput;
        protected KeyboardState _currentKey;
        protected KeyboardState _lastKey;
        protected Vector2 _positionCursorText;
        protected int Numbertest = 8;

        public DemoButton(Vector2 position, Rectangle boundingbox)
            : base(position, boundingbox)
        {


        }

        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(_sprite, new Rectangle((int)_position.X, (int)_position.Y, _boundingBox.Width, _boundingBox.Height), _color);
            base.Draw(sp);
        }
        public override void TestDraw(SpriteBatch sp)
        {

            sp.Draw(_sprite, new Rectangle((int)_position.X, (int)_position.Y, _boundingBox.Width, _boundingBox.Height), _color);
            base.TestDraw(sp);
        }
        public override void InvilPass(SpriteBatch sp)
        {
            sp.Draw(_sprite, new Rectangle((int)_position.X, (int)_position.Y, _boundingBox.Width, _boundingBox.Height), _color);
            base.InvilPass(sp);
        }
        public void ButtonDraw(SpriteBatch sp)
        {
            sp.Draw(_sprite, new Rectangle((int)_position.X, (int)_position.Y, _boundingBox.Width, _boundingBox.Height), _color);
        }
        public void UpdateTest(float deltaTime, MouseState mouse)
        {

            _lastKey = _currentKey;
            _currentKey = Keyboard.GetState();


            this.InputUserName(deltaTime, ref _label, Numbertest);
            this.CursorMovement(_label);



        }


        public void InputUserName(float gameTime, ref string str, int maxLength)
        {
            Keys[] KEY;
            string temp = "";
            string tep = "";
            _timeInput += gameTime;
            //lay mang
            KEY = _currentKey.GetPressedKeys();
            foreach (Keys item in KEY)
            {
                if (_timeInput > 0.1f &&
                    (
                    (item >= Keys.A && item <= Keys.Z) ||

                    (item >= Keys.D0 && item <= Keys.D9) ||
                    (item >= Keys.NumPad0 && item <= Keys.NumPad9)
                    ) || item == Keys.Space || item == Keys.Enter || item == Keys.Back)
                {
                    if (_lastKey == _currentKey)
                    {
                        if (_timeInput > 0.6f)
                        {
                            if (item == Keys.Space)
                            {
                                temp += "";
                                tep += "*";
                            }
                            else
                            {
                                if (item == Keys.Back)
                                {
                                    if (str.Length > 0)
                                    {
                                        str = str.Remove(str.Length - 1);
                                        _Point = _Point.Remove(_Point.Length - 1);
                                    }
                                }
                                else
                                {
                                    temp += item.ToString();
                                    tep += "*";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (item == Keys.Space)
                        {
                            temp += "";
                            tep += "*";
                        }
                        else
                        {
                            if (item == Keys.Back)
                            {
                                if (str.Length > 0)
                                {
                                    str = str.Remove(str.Length - 1);
                                    _Point = _Point.Remove(_Point.Length - 1);
                                }
                            }
                            else
                            {
                                temp += item.ToString();
                                tep += "*";
                            }
                        }
                        _timeInput = 0;
                    }
                }
            }
            if (str == null)
            {
                str = "";
                _Point = "";
            }
            if (str.Length <= maxLength)
            {
                str = str + temp;
                _Point = _Point + tep;
            }
        }
        public bool isClick()
        {
            return _active;
        }

        public string Return()
        {
            return _label;
        }
        public bool NonActive()
        {
            _active = false;
            return _NonActive;
        }
        private void CursorMovement(string str)
        {
            // Q, W, F, M , N, L , I , J, H
            _positionCursorText = _position;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == 'Q')
                    _positionCursorText.X += 13;
                else if (str[i] == 'W')
                    _positionCursorText.X += 16;
                else if (str[i] == 'M')
                    _positionCursorText.X += 15;
                else if (str[i] == 'F')
                    _positionCursorText.X += 11;
                else if (str[i] == 'N')
                    _positionCursorText.X += 13;
                else if (str[i] == 'I')
                    _positionCursorText.X += 6;
                else if (str[i] == 'J')
                    _positionCursorText.X += 11;
                else if (str[i] == 'H')
                    _positionCursorText.X += 13;
                else if (str[i] == ' ')
                    _positionCursorText.X += 5;
                else if (str[i] == 'G')
                    _positionCursorText.X += 13;
                else
                    _positionCursorText.X += 12;

            }

        }
        public int Num()
        {
            return _NumOfClick;
        }
        protected override void Behavior()
        {
            //_color = Color.Blue;
            _active = true;
            _NonActive = false;
            base.Behavior();
        }
        protected override void Hover()
        {
            _color = Color.Purple;
            base.Hover();
        }
        protected override void Default()
        {
            _color = Color.White;

            base.Default();
        }

        public bool _NonActive { get; set; }
    }
}
