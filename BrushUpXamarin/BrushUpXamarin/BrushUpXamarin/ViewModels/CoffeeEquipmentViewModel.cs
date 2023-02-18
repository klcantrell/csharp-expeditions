using System;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace BrushUpXamarin.ViewModels
{
    public class CoffeeEquipmentViewModel : ViewModelBase
    {
        public CoffeeEquipmentViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
            Title = "Coffee Equipment";
        }

        public ICommand IncreaseCount { get; }

        int count = 0;

        string countDisplay = "Click the button below!";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
        }

        void OnIncrease()
        {
            count += 1;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}
