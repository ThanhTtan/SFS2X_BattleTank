

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SFS2XProject_BattleTank.UI
{
    public class Number
    {
        protected Texture2D _NumText;
        protected Rectangle _NumPoi;
        protected Rectangle _NumPoi2;
        protected Rectangle _NumPoi3;
        protected Rectangle _NumDeru;
        protected int number;
        protected int DvNum;
        protected int Cnum;
        protected int Tnum;
        protected ContentManager _content;
        public Number(ContentManager content, Rectangle Numpoi, Rectangle Numderu)
        {
            _content = content;
            _NumPoi = Numpoi;
            _NumPoi2 = _NumPoi;
            _NumPoi3 = _NumPoi2;
            _NumDeru = Numderu;

        }
        public Number()
        {

        }
        public void load()
        {
        }
        public void UPdata(int _Number)
        {
            number = _Number;
            if (number > 9 && number <= 99)
            {
                _NumPoi2.X = _NumPoi.X + 20;
                Cnum = number / 10;
                DvNum = number - (Cnum * 10);

            }
            if (number > 99 && number <= 999)
            {
                _NumPoi3.X = _NumPoi2.X + 20;
                Tnum = number / 100;
                Cnum = (number - (Tnum * 100)) / 10;
                DvNum = (number - (Tnum * 100)) - (Cnum * 10);
            }

        }
        public void Draw(SpriteBatch sp)
        {
            if (number > 9 && number <= 99)
            {
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + DvNum), _NumPoi2, _NumDeru, Color.Wheat);
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + Cnum), _NumPoi, _NumDeru, Color.Wheat);
            }
            if (number > 99 && number <= 999)
            {
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + DvNum), _NumPoi3, _NumDeru, Color.Wheat);
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + Cnum), _NumPoi2, _NumDeru, Color.Wheat);
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + Tnum), _NumPoi, _NumDeru, Color.Wheat);
            }
            if (number <= 9)
                sp.Draw(_NumText = _content.Load<Texture2D>("Num" + number), _NumPoi, _NumDeru, Color.Wheat);

        }
    }
}
