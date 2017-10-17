

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SFS2XProject_BattleTank.ParticleSys
{
    public class ParticleSystem
    {
        protected RandomMaxMin _rd;
        protected List<Particle> _particles;
        protected List<Texture2D> _textures;
        protected bool _active;
        protected int _minSize;
        public ParticleSystem()
        {
            _rd = new RandomMaxMin();
            _particles = new List<Particle>();
            _textures = new List<Texture2D>();
            _minSize = 1;
            _active = true;
        }

        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update(deltaTime);
            }
            RemoveOutTimeParticles();
        }
        public virtual void Draw(SpriteBatch sp)
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Draw(sp);
            }
        }
        public virtual void LoadContents(ContentManager contents) { }
        public virtual void AddPar() { }
        protected void RemoveOutTimeParticles()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].TOTALTIMELIFE >= _particles[i].LIFETIME)
                {
                    _particles.Remove(_particles[i]);
                    i--;
                }
            }
        }
        public void SetActive(bool value)
        {
            _active = value;
        }
    }
}
