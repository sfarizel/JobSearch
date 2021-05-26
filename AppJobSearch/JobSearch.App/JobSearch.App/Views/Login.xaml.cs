using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using JobSearch.App.Utility.Load;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private UserService _service;
        public Login()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void GoRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }

        private async void GoStart(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            string password = TxtPassword.Text;
            if (email == null || password == null)
            {
                await DisplayAlert("Erro!", "Verifique os campos informados", "OK");
                return;
            }

            await Navigation.PushPopupAsync(new Loading());

            ResponseService<User> responseService = await _service.GetUser(email, password);

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
                if (responseService.StatusCode == 404)
                {
                    await DisplayAlert("Erro!", "Nenhum usuário encontrado", "OK");
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
