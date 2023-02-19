using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BrushUpXamarin.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace BrushUpXamarin.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }
        public AsyncCommand RefreshCommand { get; }

        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";

            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();

            var image = "https://images.prismic.io/yesplz/75c1e42d-4bcc-40e1-abec-bf35816c088b_Group+2471.png";

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Ignacio Quintero", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Giant Steps", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Take(2)));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where((c) => c.Roaster == "Blue Bottle")));
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }
    }
}
