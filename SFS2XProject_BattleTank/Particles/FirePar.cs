

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SFS2XProject_BattleTank.ParticleSys;

namespace SFS2XProject_BattleTank.Particles
{
    public class FirePar : ParticleSystem
    {
        public FirePar() : base()
        {
            _minSize = 10;
        }

        public override void Update(float deltaTime)
        {
            AddPar();
            base.Update(deltaTime);
        }
        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp);
        }
        public override void LoadContents(ContentManager contents)
        {
            for (int i = 0; i < 3; i++)
                _textures.Add(contents.Load<Texture2D>(@"Particles\Fire\" + i.ToString()));

            base.LoadContents(contents);
        }
        public override void AddPar()
        {
            if (_active)
                if (_particles.Count < 100)
                {
                    int count = 0;
                    while (count < 10)
                    {
                        _particles.Add(new Particle(_textures[_rd.RandomInt(0, _textures.Count - 1)],
                                                     new Microsoft.Xna.Framework.Vector2(_rd.RandomInt(92, 100), _rd.RandomInt(85, 100)),
                                                     _rd.RandomInt(10, 20),
                                                     (float)(1.0 * _rd.RandomDouble() + 1.50d),
                                                     _rd.RandomInt(70, 130),
                                                     (float)_rd.RandomDouble(),
                                                     _rd.RandomInt(5, 10),
                                                     _minSize,
                                                     1.0f,
                                                     255));
                        count++;
                    }
                }
            base.AddPar();
        }
    }
}
