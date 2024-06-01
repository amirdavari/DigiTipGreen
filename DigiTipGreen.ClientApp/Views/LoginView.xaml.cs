using DigiTipGreen.ClientApp.Services;

namespace DigiTipGreen.ClientApp.Views;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		var serviceApi = new ApiServices();
		var result = await serviceApi.LoginUser(this.Username.Text, this.Password.Text);

        await DisplayAlert("Loggedin", "Login was successful", "Cancel");
    }
}