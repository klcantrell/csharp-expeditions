using System;
using System.Windows.Input;
using TravelRecord.Model;

namespace TravelRecord.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterVM ViewModel { get; set; }

        public RegisterCommand(RegisterVM viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) =>
            ViewModel.Password == ViewModel.ConfirmPassword;

        public async void Execute(object parameter)
        {
            Users user = new Users
            {
                Email = ViewModel.Email,
                Password = ViewModel.Password,
            };

            await ViewModel.Register(user);
        }
    }
}
