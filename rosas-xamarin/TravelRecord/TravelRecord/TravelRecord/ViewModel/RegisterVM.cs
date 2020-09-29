using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TravelRecord.Model;
using TravelRecord.ViewModel.Commands;

namespace TravelRecord.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterCommand RegisterCommand { get; set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string password;
        public string Password {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public RegisterVM()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        public async Task Register(Users user)
        {
            if (Password == ConfirmPassword)
            {
                await Users.Register(user);
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match", "Ok");
            }
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
