using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace SFS2XProject_BattleTank.Sounds
{
    public class SEffect
    {
        protected SoundEffect _sound;
        protected SoundEffectInstance _instance;

        public SEffect()
        {
            _sound = null;
            _instance = null;
        }
        public void Play(float volume = 1.0f, float pitch = 0.0f, float pan = 0.0f)
        {
            _instance.Volume = volume;
            _instance.Pitch = pitch;
            _instance.Pan = pan;

            _instance.Play();
        }
        public void LoadContents(ContentManager content, string path)
        {
            _sound = content.Load<SoundEffect>(path);
            _instance = _sound.CreateInstance();
        }

        public void Dispose()
        {
            _instance.Dispose();
            _sound.Dispose();
        }
        public void Stop(bool value = true)
        {
            _instance.Stop(value);
        }
        public void Pause()
        {
            _instance.Pause();
        }
        public void Resume()
        {
            _instance.Resume();
        }
        public void Mute(bool value = true)
        {
            _instance.Volume = 0.0f;
        }
        public void Repeat(bool value = false)
        {
            _instance.IsLooped = value;
        }
    }
}
