using System.Linq;
using System.Threading.Tasks;
using BrushUpXamarin.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace BrushUpXamarin.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<Grouping<string, Coffee>> CoffeeGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Coffee> FavoriteCommand { get; }

        public AsyncCommand<Coffee> DeleteCommand { get; }

        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";

            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<Grouping<string, Coffee>>();

            var image = "https://images.prismic.io/yesplz/75c1e42d-4bcc-40e1-abec-bf35816c088b_Group+2471.png";

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Ignacio Quintero", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Giant Steps 10 9-8-7-6-5-4-3-2-1", Image = image });

            CoffeeGroups.Add(new Grouping<string, Coffee>("Yes Plz", Coffee.Where((c) => c.Roaster == "Yes Plz")));
            CoffeeGroups.Add(new Grouping<string, Coffee>("Blue Bottle", Coffee.Where((c) => c.Roaster == "Blue Bottle")));

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Coffee>(Favorite);
            DeleteCommand = new AsyncCommand<Coffee>(Delete);
        }

        Coffee selectedCoffee;
        public Coffee SelectedCoffee
        {
            get => selectedCoffee;
            set
            {
                if (value != null)
                {
                    Application.Current.MainPage.DisplayAlert("Selected", value.Name, "OK");
                    value = null;
                }

                selectedCoffee = value;
                OnPropertyChanged();
            }
        }

        async Task Favorite(Coffee coffee)
        {
            if (coffee == null)
            {
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Favorite", coffee.Name, "OK");
        }

        async Task Delete(Coffee coffee)
        {
            if (coffee == null)
            {
                return;
            }
            await Application.Current.MainPage.DisplayAlert("Delete", coffee.Name, "OK");
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }
    }
}
