using System.Windows;

namespace TopikApp.View
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void AddExamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thêm đề thi");
        }

        private void EditExamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sửa đề thi");
        }

        private void DeleteExamButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xóa đề thi");
        }

        private void LockAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Khóa tài khoản");
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xóa tài khoản");
        }

        private void ViewResultsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Xem kết quả thi");
        }
    }
}
