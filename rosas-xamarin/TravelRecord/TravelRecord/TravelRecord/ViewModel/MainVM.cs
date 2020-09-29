using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class MainVM : INotifyPropertyChanged, INavigableViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginCommand LoginCommand { get; set; }
        public NavigationCommand NavigationCommand { get; set; }

        private Users user;
        public Users User {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                User = new Users
                {
                    Email = Email,
                    Password = Password,
                };
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            { 
                password = value;
                User = new Users
                {
                    Email = Email,
                    Password = Password,
                };
                OnPropertyChanged("Password");
            }
        }

        public MainVM()
        {
            User = new Users();
            LoginCommand = new LoginCommand(this);
            NavigationCommand = new NavigationCommand(this);
        }

        public async void Login()
        {
            switch (await Users.Login(User.Email, User.Password))
            {
                case LoginResult.Success:
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                    break;
                case LoginResult.WrongPassword:
                    await App.Current.MainPage.DisplayAlert("Error", "Password is incorrect", "Ok");
                    break;
                case LoginResult.Failure:
                    await App.Current.MainPage.DisplayAlert("Error", "There was an error logging you in", "Ok");
                    break;
            }
        }

        public async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (isDeserializing) return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool isDeserializing = false;

        [OnDeserializing]
        private void OnDeserializing(StreamingContext streamingContext)
        {
            isDeserializing = true;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext streamingContex)
        {
            isDeserializing = false;
        }
    }
}
