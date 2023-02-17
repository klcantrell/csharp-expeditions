using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BrushUpXamarin.ViewModels
{
    public class CoffeeEquipmentViewModel : BindableObject
    {
        public CoffeeEquipmentViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
        }

        public ICommand IncreaseCount { get; }

        int count = 0;

        string countDisplay = "Click the button below!";
        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                {
                    return;
                }
                else
                {
                    countDisplay = value;
                    OnPropertyChanged();
                }
            }
        }

        void OnIncrease()
        {
            count += 1;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}
