using System;
using Xamarin.Forms;

namespace BibHomeAutomationNavigation
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new LoginPage());
		}
		void OnCancelButtonClicked(object sender, EventArgs e)
		{
			usernameEntry.Text = string.Empty;
			passwordEntry.Text = string.Empty;
		}
		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var user = new User
			{
				Username = usernameEntry.Text,
				Password = passwordEntry.Text
			};

			var isValid = AreCredentialsCorrect(user);
			if (isValid)
			{
				App.IsUserLoggedIn = true;
				Navigation.InsertPageBefore(new MainPage(), this);
				await Navigation.PopAsync();
			}
			else
			{
				await DisplayAlert("Login Failed", "Please try again", "Cancel");
				passwordEntry.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect(User user)
		{
			return user.Username == AppConstants.AdminUser && user.Password == AppConstants.AdminPassword;
		}
	}
}