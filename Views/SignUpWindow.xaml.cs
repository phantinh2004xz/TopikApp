using System.Windows;
using System.Windows.Controls;
using TopikApp.ViewModels;

namespace TopikApp.Views
{
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignUpViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }

        private void txtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SignUpViewModel vm)
                vm.ConfirmPassword = ((PasswordBox)sender).Password;
        }
    }
}