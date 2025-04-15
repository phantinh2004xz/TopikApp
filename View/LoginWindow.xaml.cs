using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace TopikApp.View
{
    public partial class LoginWindow : Window
    {
        private readonly string connectionString = "server=localhost;database=TopikAppDB;user=root;password=Thao2k4@.";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM users WHERE username = @username LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["password"]?.ToString() ?? "";
                                bool isAdmin = Convert.ToBoolean(reader["is_admin"]);
                                bool isLocked = Convert.ToBoolean(reader["is_locked"]);

                                if (isLocked)
                                {
                                    MessageBox.Show("Tài khoản đã bị khóa.");
                                    return;
                                }

                                // So sánh mật khẩu trực tiếp (không hash)
                                if (password == dbPassword)
                                {
                                    MessageBox.Show("Đăng nhập thành công!");

                                    if (isAdmin)
                                    {
                                        AdminWindow adminWindow = new AdminWindow();
                                        adminWindow.Show();
                                    }
                                    else
                                    {
                                        UserWindow userWindow = new UserWindow();
                                        userWindow.Show();
                                    }

                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Sai mật khẩu.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản không tồn tại.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}