using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Shop.Extensions
{
    public static class CollectionsExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
        {
            var observableList = new ObservableCollection<T>();
            foreach (var item in list)
            {
                observableList.Add(item);
            }

            return observableList;
        }
    }
}
