using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace TopikApp.View
{
    public partial class RegisterWindow : Window
    {
        private const string DatabaseConnectionString = "server=localhost;database=TopikAppDB;user=root;password=Thao2k4@.";
        private readonly string connectionString = DatabaseConnectionString;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string checkUser = "SELECT COUNT(*) FROM users WHERE username = @username OR email = @email";
                using var checkCmd = new MySqlCommand(checkUser, conn);
                checkCmd.Parameters.AddWithValue("@username", username);
                checkCmd.Parameters.AddWithValue("@email", email);

                var exists = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                if (exists)
                {
                    MessageBox.Show("Tên đăng nhập hoặc email đã tồn tại.");
                    return;
                }

                string insert = "INSERT INTO users (username, password, email) VALUES (@username, @password, @email)";
                using var insertCmd = new MySqlCommand(insert, conn);
                insertCmd.Parameters.AddWithValue("@username", username);
                insertCmd.Parameters.AddWithValue("@password", password); // nếu cần có thể mã hóa
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
