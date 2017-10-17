using SFS2XProject_BattleTank.Bases;

namespace SFS2XProject_BattleTank.CollisionDetection
{
    public class Collision
    {
        public Collision() { }


        // public
        public bool Intersect(Box2D obj_1, Box2D obj_2)
        {
            return AABB(obj_1, obj_2);
        }
        public float SweptAABB(Box2D obj_1, Box2D obj_2, ref float normalx, ref float normaly)
        {
            float xInvEntry, yInvEntry;
            float xInvExit, yInvExit;

            // find the distance between the objects on the near and far sides for both x and y
            if (obj_1.vx > 0.0f)
            {
                xInvEntry = obj_2.x - (obj_1.x + obj_1.width);
                xInvExit = (obj_2.x + obj_2.width) - obj_1.x;
            }
            else
            {
                xInvEntry = (obj_2.x + obj_2.width) - obj_1.x;
                xInvExit = obj_2.x - (obj_1.x + obj_1.width);
            }

            if (obj_1.vy > 0.0f)
            {
                yInvEntry = obj_2.y - (obj_1.y + obj_1.height);
                yInvExit = (obj_2.y + obj_2.height) - obj_1.y;
            }
            else
            {
                yInvEntry = (obj_2.y + obj_2.height) - obj_1.y;
                yInvExit = obj_2.y - (obj_1.y + obj_1.height);
            }
            // find time of collision and time of leaving for each axis (if statement is to prevent divide by zero)
            float xEntry, yEntry;
            float xExit, yExit;

            if (obj_1.vx == 0.0f)
            {
                xEntry = -float.PositiveInfinity;
                xExit = float.PositiveInfinity;
            }
            else
            {
                xEntry = xInvEntry / obj_1.vx;
                xExit = xInvExit / obj_1.vx;
            }

            if (obj_1.vy == 0.0f)
            {
                yEntry = -float.PositiveInfinity;
                yExit = float.PositiveInfinity;
            }
            else
            {
                yEntry = yInvEntry / obj_1.vy;
                yExit = yInvExit / obj_1.vy;
            }
            // find the earliest/latest times of collision
            float entryTime = System.Math.Max(xEntry, yEntry);
            float exitTime = System.Math.Min(xExit, yExit);
            // if there was no collision
            if (entryTime > exitTime || xEntry < 0.0f && yEntry < 0.0f || xEntry > 1.0f || yEntry > 1.0f)
            {
                normalx = 0.0f;
                normaly = 0.0f;
                return 1.0f;
            }
            else // if there was a collision
            {
                // calculate normal of collided surface
                if (xEntry > yEntry)
                {
                    if (xInvEntry < 0.0f)
                    {
                        normalx = 1.0f;
                        normaly = 0.0f;
                    }
                    else
                    {
                        normalx = -1.0f;
                        normaly = 0.0f;
                    }
                }
                else
                {
                    if (yInvEntry < 0.0f)
                    {
                        normalx = 0.0f;
                        normaly = 1.0f;
                    }
                    else
                    {
                        normalx = 0.0f;
                        normaly = -1.0f;
                    }
                }

                // return the time of collision
                return entryTime;
            }
        }
        public Box2D GetBroadPhaseBox2D(Box2D box)
        {
            Box2D broadPhaseBox = new Box2D();


            if (box.vx > 0)
            {
                // x
                broadPhaseBox.x = box.x;
                // w
                broadPhaseBox.width = (int)((box.vx * 1.0f) + box.width);
            }
            else
            {
                // x
                broadPhaseBox.x = box.x + (box.vx * 1.0f);
                // w
                broadPhaseBox.width = (int)(box.width - (box.vx * 1000.0f));
            }
            if (box.vy > 0)
            {
                // y
                broadPhaseBox.y = box.y;
                // h
                broadPhaseBox.height = (int)((box.vy * 1.0f) + box.height);
            }
            else
            {
                // y
                broadPhaseBox.y = box.y + (box.vy * 1.0f);
                // h
                broadPhaseBox.height = (int)(box.height - (box.vy * 1.0f));
            }
            return broadPhaseBox;
        }
        // private	
        public bool AABB(Box2D obj_1, Box2D obj_2)
        {
            if (obj_1.x > obj_2.x + obj_2.width ||
                obj_1.y > obj_2.y + obj_2.height ||
                obj_1.y + obj_1.height < obj_2.y ||
                obj_1.x + obj_1.width < obj_2.x)
            {
                return false;
            }
            return true;
        }

    }
}
