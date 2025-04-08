using System.Linq;
using System.Windows;
using System.Windows.Input;
using TopikApp.Commands;
using TopikApp.Data;
using TopikApp.Models;
using TopikApp.Views;

namespace TopikApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand SignUpCommand { get; }
        public ICommand BackToLoginCommand { get; }

        public SignUpViewModel()
        {
            _context = new AppDbContext();
            SignUpCommand = new RelayCommand(SignUp, CanSignUp);
            BackToLoginCommand = new RelayCommand(BackToLogin);
        }

        private bool CanSignUp(object parameter) =>
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword);

        private void SignUp(object parameter)
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Passwords do not match";
                    return;
                }

                if (_context.Users.Any(u => u.Username == Username))
                {
                    ErrorMessage = "Username already exists";
                    return;
                }

                var newUser = new User
                {
                    Username = Username,
                    Password = Password, // Note: Should hash in production
                    Role = "User"
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                MessageBox.Show("Registration successful! Please login.",
                              "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                BackToLogin(null);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Registration failed: {ex.Message}";
            }
        }

        private void BackToLogin(object parameter)
        {
            new LoginWindow().Show();
            Application.Current.Windows.OfType<SignUpWindow>().FirstOrDefault()?.Close();
        }
    }
}