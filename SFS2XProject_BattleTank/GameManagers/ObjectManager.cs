

using SFS2XProject_BattleTank.Bases;
using System.Collections.Generic;

namespace SFS2XProject_BattleTank.GameManagers
{
    public class ObjectManager
    {
        private List<GameObject> _allGameObject;

        public ObjectManager()
        {
            _allGameObject = new List<GameObject>();
        }

        public void Add(GameObject obj)
        {
            if (obj != null)
            {
                _allGameObject.Add(obj);
            }
        }
        public void Remove(GameObject obj)
        {
            if (obj != null)
            {
                _allGameObject.Remove(obj);
            }
        }
        public string Essental(GameObject obj)
        {
            return obj.Essental();
        }
        // properties
        public List<GameObject> GetAllObject()
        {
            return _allGameObject;
        }
    }
}
