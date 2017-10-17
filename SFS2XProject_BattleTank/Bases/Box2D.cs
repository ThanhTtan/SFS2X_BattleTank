namespace SFS2XProject_BattleTank.Bases
{
    public struct Box2D
    {
        public float x;
        public float y;
        public float vx;
        public float vy;
        public int width;
        public int height;

        public Box2D(float x, float y, float vx, float vy, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.width = width;
            this.height = height;
        }
    }
}
