
using Microsoft.Xna.Framework.Input;
using SFS2XProject_BattleTank.InputControls;

namespace SFS2XProject_BattleTank.MainTank
{
    public class MTankControl
    {
        private short _left;
        private short _right;
        private short _up;
        private short _down;
        private Keys _preKey;

        private float _delayTimeShoot;
        private float _totalAfterShootTime;
        public MTankControl()
        {
            _left = 0;
            _right = 0;
            _up = 0;
            _down = 0;
            _preKey = Keys.Escape;

            _delayTimeShoot = 0.15f;
            _totalAfterShootTime = 0.0f;
        }

        public Keys GetDirectionKey()
        {
            Reset();
            Keys curKey = Keys.Escape;
            if (Input.IsKeyDown(Keys.W))
            {
                _up = 1;
                curKey = Keys.W;
            }
            if (Input.IsKeyDown(Keys.S))
            {
                _down = 1;
                curKey = Keys.S;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                _left = 1;
                curKey = Keys.A;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                _right = 1;
                curKey = Keys.D;
            }
            if (_up + _down + _left + _right > 1)
            {
                Keys result = Keys.Escape;
                if (_preKey == Keys.Escape)
                {
                    Keys[] keyArr = Input.GetKeyDowns();
                    for(int i = 0; i < keyArr.Length; i++)
                    {
                        if(keyArr[i] == Keys.W || keyArr[i] == Keys.D || keyArr[i] == Keys.A || keyArr[i] == Keys.S)
                        {
                            _preKey = keyArr[i];
                            return keyArr[i];
                        }
                    }
                }
                else
                {
                    if (_up == 1 && _preKey == Keys.W)
                        result = Keys.W;
                    if (_down == 1 && _preKey == Keys.S)
                        result = Keys.W;
                    if (_left == 1 && _preKey == Keys.A)
                        result = Keys.A;
                    if (_right == 1 && _preKey == Keys.D)
                        result = Keys.D;
                }
                _preKey = result;
                return result;
            }
            else if (_up + _down + _left + _right <= 0)
            {
                _preKey = Keys.Escape;
                return Keys.Escape;
            }
            else
            {
                _preKey = curKey;
                return curKey;
            }
        }
        public bool Shoot(float deltaTime)
        {
            if(Input.IsKeyDown(Keys.Space))
            {
                if(_totalAfterShootTime >= deltaTime)
                {
                    _totalAfterShootTime = 0.0f;
                    return true;
                }
            }
            _totalAfterShootTime += deltaTime;
            return false;
        }
        private void Reset()
{
    _left = 0;
    _right = 0;
    _up = 0;
    _down = 0;
}
    }
}
