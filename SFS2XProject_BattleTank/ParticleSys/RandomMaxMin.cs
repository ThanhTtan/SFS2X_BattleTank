

using System;

namespace SFS2XProject_BattleTank.ParticleSys
{
    public class RandomMaxMin
    {
        private Random _rd;
        public RandomMaxMin()
        {
            _rd = new Random();
        }
        public int RandomInt(int min, int max)
        {
            return _rd.Next(min, max) + 1;
        }
        public double RandomDouble()
        {
            return _rd.NextDouble();
        }
    }
}
