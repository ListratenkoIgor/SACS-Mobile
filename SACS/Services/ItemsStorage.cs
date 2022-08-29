using System;
using System.Collections.Generic;
using System.Text;

namespace SACS.Services
{
    public class ItemsStorage<T>
    {
        public ItemsStorage(List<T> items = null)
        {
            Items = items ?? (Items = new List<T>());
        }
        public List<T> Items;
        public List<T> GetItems()
        {
            return Items ?? (Items = new List<T>());
        }
        public void SetItems(List<T> items) {
            Items = items;
        }
        public void Clear()
        {
            Items = new List<T>();
        }
    }
}
