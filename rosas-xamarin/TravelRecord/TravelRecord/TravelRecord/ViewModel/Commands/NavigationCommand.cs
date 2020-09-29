using System;
using System.Windows.Input;

namespace TravelRecord.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public INavigableViewModel viewModel { get; set; }

        public NavigationCommand(INavigableViewModel homeVM)
        {
            viewModel = homeVM;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            viewModel.Navigate();
        }
    }
}
