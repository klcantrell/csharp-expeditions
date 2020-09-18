using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TravelRecord.Model
{
    public class Users : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

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

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static async Task Insert(Users user)
        {
            await App.MobileService
                .GetTable<Users>()
                .InsertAsync(user);
        }

        public static async Task<Users> FindByEmail(string email)
        {
            return (await App.MobileService
                .GetTable<Users>()
                .Where(u => u.Email == email)
                .ToListAsync())
                .FirstOrDefault();
        }

        public static async Task<LoginResult> Login(string email, string password)
        {
            var isEmailEmpty = string.IsNullOrEmpty(email);
            var isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isEmailEmpty || isPasswordEmpty)
            {
                return LoginResult.MissingField;
            }
            else
            {
                var user = await Users.FindByEmail(email);
                if (user != null)
                {
                    App.user = user;
                    if (user.Password == password)
                    {
                        return LoginResult.Success;
                    }
                    else
                    {
                        return LoginResult.WrongPassword;
                    }
                }
                else
                {
                    return LoginResult.Failure;
                }
            }
        }

        public static async Task<RegisterResult> Register(string email, string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                Users user = new Users()
                {
                    Email = email,
                    Password = password,
                };

                await Users.Insert(user);
                return RegisterResult.Success;
            }
            else
            {
                return RegisterResult.Failure;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
