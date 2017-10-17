using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SFS2XProject_BattleTank.Sounds
{
    public class SBackground
    {
        protected Song _song;

        public SBackground()
        {
            _song = null;
        }

        public void Play(float volume = 1.0f)
        {
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(_song);
        }
        public void LoadContents(ContentManager content, string path)
        {
            _song = content.Load<Song>(path);
        }

        public void Dispose()
        {
            _song.Dispose();
        }
        public void Stop()
        {
            MediaPlayer.Stop();
        }
        public void Pause()
        {
            MediaPlayer.Pause();
        }
        public void Resume()
        {
            MediaPlayer.Resume();
        }
        public void Mute(bool value = true)
        {
            MediaPlayer.IsMuted = value;
        }
        public void Repeat(bool value = false)
        {
            MediaPlayer.IsRepeating = value;
        }
    }
}
