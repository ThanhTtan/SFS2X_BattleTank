using Microsoft.Xna.Framework;
using SFS2XProject_BattleTank.Bases;
using System.Collections.Generic;


namespace SFS2XProject_BattleTank.CollisionDetection
{
    public class Quadtree
    {

        private int MAX_OBJECTS = 6;
        private int MAX_LEVELS = 10;

        private int _level;
        private List<GameObject> _objects;
        private Rectangle _bounds;
        private Quadtree[] _nodes;

        /*
         * Constructor
         */
        public Quadtree(int level, Rectangle bounds)
        {
            _level = level;
            _objects = new List<GameObject>();
            _bounds = bounds;
            _nodes = new Quadtree[4];
        }
        /*
 * Clears the quadtree
 */
        public void Clear()
        {
            _objects.Clear();
            for (int i = 0; i < _nodes.Length; i++)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i].Clear();
                    _nodes[i] = null;
                }
            }
        }
        /*
 * Splits the node into 4 subnodes
 */
        private void Split()
        {
            int subWidth = (int)(_bounds.Width / 2);
            int subHeight = (int)(_bounds.Height / 2);
            int x = (int)_bounds.X;
            int y = (int)_bounds.Y;

            _nodes[0] = new Quadtree(_level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            _nodes[1] = new Quadtree(_level + 1, new Rectangle(x, y, subWidth, subHeight));
            _nodes[2] = new Quadtree(_level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            _nodes[3] = new Quadtree(_level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }
        /*
 * Determine which node the object belongs to. -1 means
 * object cannot completely fit within a child node and is part
 * of the parent node
 */
        private int GetIndex(GameObject obj)
        {
            int index = -1;
            double verticalMidpoint = _bounds.X + (_bounds.Width / 2);
            double horizontalMidpoint = _bounds.Y + (_bounds.Height / 2);

            // obj can completely fit within the top quadrants
            bool topQuadrant = (obj.BOX2D.y < horizontalMidpoint && obj.BOX2D.y + obj.BOX2D.height < horizontalMidpoint);
            // obj can completely fit within the bottom quadrants
            bool bottomQuadrant = (obj.BOX2D.y > horizontalMidpoint);

            // obj can completely fit within the left quadrants
            if (obj.BOX2D.x < verticalMidpoint && obj.BOX2D.x + obj.BOX2D.width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            // obj can completely fit within the right quadrants
            else if (obj.BOX2D.x > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }
        /*
 * Insert the object into the quadtree. If the node
 * exceeds the capacity, it will Split and add all
 * _objects to their corresponding _nodes.
 */
        public void Insert(GameObject Object)
        {
            if (_nodes[0] != null)
            {
                int index = GetIndex(Object);

                if (index != -1)
                {
                    _nodes[index].Insert(Object);

                    return;
                }
            }

            _objects.Add(Object);

            if (_objects.Count > MAX_OBJECTS && _level < MAX_LEVELS)
            {
                if (_nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < _objects.Count)
                {
                    int index = GetIndex(_objects[i]);
                    if (index != -1)
                    {
                        _nodes[index].Insert(_objects[i]);
                        _objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        /*
 * Return all _objects that could collide with the given object
 */
        public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject pRect)
        {
            int index = GetIndex(pRect);
            if (index != -1 && _nodes[0] != null)
            {
                _nodes[index].Retrieve(returnObjects, pRect);
            }

            returnObjects.AddRange(_objects);

            return returnObjects;
        }
    }
}
