using System.Collections.Generic;
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
        private static readonly string image = "https://images.prismic.io/yesplz/75c1e42d-4bcc-40e1-abec-bf35816c088b_Group+2471.png";

        public ObservableRangeCollection<Coffee> Coffee { get; set; }
        public ObservableRangeCollection<CoffeeGroups> CoffeeGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public MvvmHelpers.Commands.Command LoadMoreCommand { get; }

        public MvvmHelpers.Commands.Command DelayLoadMoreCommand { get; }

        public MvvmHelpers.Commands.Command ClearCommand { get; }

        public AsyncCommand<Coffee> FavoriteCommand { get; }

        public AsyncCommand<Coffee> DeleteCommand { get; }

        public CoffeeEquipmentViewModel()
        {
            Title = "Coffee Equipment";

            Coffee = new ObservableRangeCollection<Coffee>();
            CoffeeGroups = new ObservableRangeCollection<CoffeeGroups>();


            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Ignacio Quintero", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Giant Steps 10 9-8-7-6-5-4-3-2-1", Image = image });

            CoffeeGroups.Add(new CoffeeGroups("Yes Plz", Coffee.Where((c) => c.Roaster == "Yes Plz")));
            CoffeeGroups.Add(new CoffeeGroups("Blue Bottle", Coffee.Where((c) => c.Roaster == "Blue Bottle")));

            RefreshCommand = new AsyncCommand(Refresh);
            LoadMoreCommand = new MvvmHelpers.Commands.Command(LoadMore);
            DelayLoadMoreCommand = new MvvmHelpers.Commands.Command(DelayLoadMore);
            ClearCommand = new MvvmHelpers.Commands.Command(Clear);
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

            Coffee.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            if (Coffee.Count >= 20)
            {
                return;
            }

            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Party Round", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Ignacio Quintero", Image = image });
            Coffee.Add(new Coffee { Roaster = "Yes Plz", Name = "Ignacio Quintero", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Giant Steps 10 9-8-7-6-5-4-3-2-1", Image = image });
            Coffee.Add(new Coffee { Roaster = "Blue Bottle", Name = "Giant Steps 10 9-8-7-6-5-4-3-2-1", Image = image });

            CoffeeGroups.Clear();

            // HACK: using Add() causes an internal consistency exception on iOS
            // see https://github.com/xamarin/Xamarin.Forms/issues/6011#issuecomment-559017278
            CoffeeGroups.AddRange(new List<CoffeeGroups>()
                {
                    new CoffeeGroups("Yes Plz", Coffee.Where((c) => c.Roaster == "Yes Plz")),
                    new CoffeeGroups("Blue Bottle", Coffee.Where((c) => c.Roaster == "Blue Bottle"))
                }
            );
        }

        void DelayLoadMore()
        {
            if (Coffee.Count <= 10)
            {
                return;
            }

            LoadMore();
        }

        void Clear()
        {
            Coffee.Clear();
            CoffeeGroups.Clear();
        }
    }
}
