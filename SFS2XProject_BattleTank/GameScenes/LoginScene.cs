

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SFS2XProject_BattleTank.Bases;
using SFS2XProject_BattleTank.GameManagers;
using SFS2XProject_BattleTank.OffSets;
using SFS2XProject_BattleTank.UI;
namespace SFS2XProject_BattleTank.GameScenes
{
    public class LoginScene : Scene
    {
        protected Texture2D BG;
        protected Texture2D LoginBG;
        protected Rectangle rec1;
        protected Rectangle rec2;

        protected DemoButton ID;
        protected DemoButton PASS;
        protected DemoButton LOGIN;



        public LoginScene(ContentManager contents) :
            base(Constants.SCENE_LOGIN, contents)
        {
            _contents = contents;
            rec1 = new Rectangle(0, 0, 800, 600);
            rec2 = new Rectangle(0, 0, 1280, 720);

            ID = new DemoButton(new Vector2(150, 250), new Rectangle(0, 0, 150, 50));
            PASS = new DemoButton(new Vector2(300, 250), new Rectangle(0, 0, 150, 50));
            LOGIN = new DemoButton(new Vector2(450, 250), new Rectangle(0, 0, 150, 50));
        }

        public override bool Init()
        {
            _isInit = LoadContents();
            return _isInit;
        }
        public override void Shutdown()
        {
            _contents.Unload();
            _isInit = false;
        }
        public override void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(BG = _contents.Load<Texture2D>("BG"), rec1, sourceRectangle: rec2, color: Color.Wheat);
            //sp.Draw(LoginBG = _contents.Load<Texture2D>("Login"), rec3, sourceRectangle: rec4, color: Color.Wheat);
            LOGIN.ButtonDraw(sp);
            if (ID.isClick() || ID.Return() != null)
                ID.TestDraw(sp);
            else
                ID.ButtonDraw(sp);
            if (PASS.isClick() || PASS.Return() != null)
                PASS.InvilPass(sp);
            else
                PASS.ButtonDraw(sp);
            sp.End();
        }
        public override void Update(float deltaTime)
        {
            MouseState mouse;
            mouse = Mouse.GetState();

            bool a = ID.isClick();
            bool b = PASS.isClick();
            if (a == true)
            {
                PASS.NonActive();
                LOGIN.NonActive();
                ID.UpdateTest(deltaTime, mouse);

            }
            if (b == true)
            {
                ID.NonActive();
                LOGIN.NonActive();
                PASS.UpdateTest(deltaTime, mouse);

            }

            ID.Update(deltaTime, mouse);
            LOGIN.Update(deltaTime, mouse);
            PASS.Update(deltaTime, mouse);
            if (LOGIN.isClick())
                if (ID.Return().Length > 0 && PASS.Return().Length > 0 && ID.Return() != "|" && PASS.Return() != "|")
                {
                    Game1.sceneManager.GotoScene(Constants.SCENE_MENU);
                }
        }
        public override bool LoadContents()
        {
            ID.LoadContents(_contents, "font", "ID");
            PASS.LoadContents(_contents, "font", "PASS");
            LOGIN.LoadContents(_contents, "font", "Login");
            return true;
        }
    }
}
