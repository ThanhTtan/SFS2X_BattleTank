

using SFS2XProject_BattleTank.Items;
using System.Collections.Generic;
namespace SFS2XProject_BattleTank.GameManagers
{
    class ItemManager
    {
        private List<Item> _items;

        public ItemManager()
        {
            _items = new List<Item>();
        }

        public void Update(float deltaTime)
        {
            foreach (Item i in _items)
                i.Update(deltaTime);
            foreach(Item i in _items)
            {
                if (i.TOTAL_LEFT_TIME >= i.LEFT_TIME)
                    Remove(i);
            }
        }
        public void Add(Item i)
        {
            if (i != null)
                _items.Add(i);
        }
        public void Remove(Item i)
        {
            if (i != null)
            {
                _items.Remove(i);
                GameManager.RemoveObject(i);
            }
        }
    }
}
