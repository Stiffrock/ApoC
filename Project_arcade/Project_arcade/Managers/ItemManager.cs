using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_arcade
{
    static class ItemManager
    {

        static Dictionary<string, Item> items = new Dictionary<string, Item>();

        public static void AddItem(Item item, string key)
        {
            items.Add(key, item);
        }

        public static Item GetItem(string key)
        {
            return items[key];
        }
  
    
    }
}
