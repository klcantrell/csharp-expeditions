using System;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public MainVM ViewModel { get; set; }

        public LoginCommand(MainVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var user = parameter as Users;

            if (user == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Login();
        }
    }
}
