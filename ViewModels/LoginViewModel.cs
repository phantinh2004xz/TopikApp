using System.Linq;
using System.Windows;
using System.Windows.Input;
using TopikApp.Commands;
using TopikApp.Data;
using TopikApp.Models;
using TopikApp.Views;

namespace TopikApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand LoginCommand { get; }
        public ICommand ShowSignUpCommand { get; }

        public LoginViewModel()
        {
            _context = new AppDbContext();
            LoginCommand = new RelayCommand(Login, CanLogin);
            ShowSignUpCommand = new RelayCommand(ShowSignUp);
        }

        private bool CanLogin(object parameter) =>
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Password);

        private void Login(object parameter)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u =>
                    u.Username == Username &&
                    u.Password == Password);

                if (user != null)
                {
                    ErrorMessage = "";
                    OpenMainWindow(user.Role);
                    CloseCurrentWindow();
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
        }

        private void ShowSignUp(object parameter)
        {
            new SignUpWindow().Show();
            CloseCurrentWindow();
        }

        private void OpenMainWindow(string role)
        {
            Window window = role == "Admin" ? new AdminWindow() : new UserWindow();
            window.Show();
        }

        private void CloseCurrentWindow()
        {
            Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
        }
    }
}