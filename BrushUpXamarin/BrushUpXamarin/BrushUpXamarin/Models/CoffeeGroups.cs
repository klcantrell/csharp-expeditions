using System.Collections.Generic;
using MvvmHelpers;

namespace BrushUpXamarin.Models
{
    public class CoffeeGroups : Grouping<string, Coffee>
    {
        public CoffeeGroups(string key, IEnumerable<Coffee> items) : base(key, items)
        {
        }
    }
}
