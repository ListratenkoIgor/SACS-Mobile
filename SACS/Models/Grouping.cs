using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SACS.Models
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }
        public Grouping(K name, IEnumerable<T> items)
        {
            Key = name;
            foreach (T item in items)
                Items.Add(item);
        }
    }
}
