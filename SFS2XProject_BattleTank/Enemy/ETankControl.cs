

using SFS2XProject_BattleTank.ParticleSys;

namespace SFS2XProject_BattleTank.Enemy
{
    class ETankControl
    {
        private float _totalTimeChangeDir;
        private float _delayTimeChageDir;

        private float _totalShootTime;
        private float _delayShootTime;
        private RandomMaxMin _rd;
        public ETankControl()
        {
            _totalTimeChangeDir = 00.0f;
            _delayTimeChageDir = 5.0f + _rd.RandomInt(1000, 3000) % 1000.0f;
        }

        public void GetDirection(ref int xDir, ref int yDir,float deltaTime)
        {
            if(_totalTimeChangeDir >= _delayTimeChageDir)
            {
                RandomDir(ref xDir,ref yDir, 0, 0);
                _totalTimeChangeDir = 0;
            }
            else
            {
                _totalTimeChangeDir += deltaTime;
            }
        }
        public void RandomDir(ref int xDir,ref int yDir,int xExcept,int yExcept)
        {
            switch (_rd.RandomInt(0, 1))
            {
                case 0:
                    {
                        xDir = 0;
                        switch (_rd.RandomInt(0, 1))
                        {
                            case 0:
                                {
                                    if (yExcept != 1) yDir = 1;
                                    else yDir = -1;
                                    break;
                                }
                            default:
                                {
                                    if (yExcept != -1) yDir = -1;
                                    else yDir = 1;
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        yDir = 0;
                        switch (_rd.RandomInt(0, 1))
                        {
                            case 0:
                                {
                                    if (xExcept != 1) xDir = 1;
                                    else xDir = -1;
                                    break;
                                }
                            default:
                                {
                                    if (xExcept != -1) xDir = -1;
                                    else xDir = 1;
                                    break;
                                }
                        }
                        break;
                    }
            }

        }
        public bool Shoot(float deltaTime)
        {
            if(_totalShootTime >= _delayShootTime)
            {
                _totalShootTime = 0;
                return true;
            }
            else
            {
                _totalShootTime += deltaTime;
            }
            return false;
        }
    }
}
