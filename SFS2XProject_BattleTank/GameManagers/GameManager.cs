
using SFS2XProject_BattleTank.Bases;
using System.Collections.Generic;

namespace SFS2XProject_BattleTank.GameManagers
{
    public static class GameManager
    {
        static private ulong _objId = 0;
        static public ulong GetId()
        {
            _objId++;
            return _objId;
        }

        static private ObjectManager _objects = new ObjectManager();
        // object manager
        static public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }
        static public void RemoveObject(GameObject obj)
        {
            _objects.Remove(obj);
        }
        static public List<GameObject> GetAllObject()
        {
            return _objects.GetAllObject();
        }
        static public string EssentialObject(GameObject obj)
        {
            return _objects.Essental(obj);
        }
    }
}
