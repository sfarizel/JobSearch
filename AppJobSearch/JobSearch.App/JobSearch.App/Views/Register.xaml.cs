using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Load;
using JobSearch.APP.Domain.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private UserService _service;

        public Register()
        {
            InitializeComponent();
            _service = new UserService();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveAction(object sender, EventArgs e)
        {

            LblMsg.Text = string.Empty;

            string name = TxtName.Text;
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;

            //Prepara o novo usuário
            User user = new User() { Name = name, Email = email, Password = password };

            await Navigation.PushPopupAsync(new Loading());

            ResponseService<User> responseService = await _service.AddUser(user);

            if (responseService.IsSucess)
            {
                //Armazenda internamente no aplicativo (persiste quando sair e voltar ao aplicativo)
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));
                await App.Current.SavePropertiesAsync();

                //Troca a navigation page pela start
                App.Current.MainPage = new NavigationPage(new Start());
            }
            else
            {
                if (responseService.StatusCode == 400)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var dicKey in responseService.Errors)
                    {
                        foreach (var dicMessage in dicKey.Value)
                        {
                            sb.AppendLine(dicMessage);
                        }
                    }
                    LblMsg.Text = sb.ToString();
                }
                else
                {
                    await DisplayAlert("Erro!", "Ocorreu um erro inesperado, tente novamente mais tarde.", "OK");
                }


            }
            await Navigation.PopAllPopupAsync();
        }
    }
}