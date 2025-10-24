using Newtonsoft.Json.Linq;
using Services;
using System.IO;
using System.Windows;

namespace HotelManagement.Views;

public partial class LoginWindow : Window
{
    private readonly CustomerService _service = new CustomerService();
    private string adminEmail = "admin@FUMiniHotelSystem.com";
    private string adminPass = "@@abc123@@";

    public LoginWindow()
    {
        InitializeComponent();
        LoadAdminFromConfig();
    }

    private void LoadAdminFromConfig()
    {
        try
        {
            var text = File.ReadAllText("appsettings.json");
            var j = JObject.Parse(text);
            adminEmail = j["Admin"]?["Email"]?.ToString() ?? adminEmail;
            adminPass = j["Admin"]?["Password"]?.ToString() ?? adminPass;
        }
        catch { }
    }

    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        var email = txtEmail.Text.Trim();
        var pass = txtPass.Password.Trim();

        if (email == adminEmail && pass == adminPass)
        {
            new MainWindow("Admin").Show();
            Close();
            return;
        }

        var user = _service.Login(email, pass);
        if (user != null)
        {
            new MainWindow("Customer", user.CustomerId).Show();
            Close();
            return;
        }

        MessageBox.Show("Invalid credentials!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
