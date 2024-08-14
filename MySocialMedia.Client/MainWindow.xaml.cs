using MySocialMedia.Common.ResponseLogin;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySocialMedia.Common.ModelReq;
using System.Net;
using static System.Net.WebRequestMethods;
using MySocialMedia.Common.DTOs;
using System.Net.Http.Json;

namespace MySocialMedia.Client
{
 
    public partial class LoginWindow : Window
    {
        private const string url = "http://localhost:5018/";
        public LoginWindow()
        {
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameBox1.Text;
            string password = PasswordBox1.Password;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("please enter username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error); return;
            }
            var login = new LoginReq
            {
                UserName = userName,
                Password = password,
            };
            UserSessionDTO session = await LoginReqClient(login);
            if(session != null)
            {
                MessageBox.Show("Login Sucsse", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Login failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task<UserSessionDTO> LoginReqClient(LoginReq p_li)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.PostAsJsonAsync("Users/Login", p_li);
                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserSessionDTO>(json);
                }
                else
                {
                    return null;
                }
            }
        }
        private async void RegisterButton_Click(Object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;
            string userName = UserNameBox2.Text;
            string password = PasswordBox2.Password;
            var valid = Common.Validation.Validation.ValidationAll(firstName, lastName, userName, password);
            if (valid.Count > 0)
            {
                while (valid.Count > 0)
                {
                    string mess = valid.First();
                    valid.Remove(mess);
                    MessageBox.Show(mess, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                var user = new SigninReq
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = userName,
                    Password = password,
                };
                bool response = await AddUser(user);
                if(response)
                {
                    BackToLogIn(sender, e);
                    MessageBox.Show("Your account is create", "Succsee", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("Signin failed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
              
            }
        }
        private async Task<bool> AddUser(SigninReq p_user)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.PostAsJsonAsync("Users/Signin", p_user);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;   
                }
            }
        }
        private void SignInButton_Click(Object sender , RoutedEventArgs e)
        {
            signIn.Visibility = Visibility.Visible;
            logIn.Visibility = Visibility.Hidden;
        }
        private void BackToLogIn(Object sender, RoutedEventArgs e)
        {
            logIn.Visibility = Visibility.Visible;
            signIn.Visibility = Visibility.Hidden;
        
        }
    }
}