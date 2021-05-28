using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.APP.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Start : ContentPage
    {
        private JobService _service;
        public Start()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Visualizer());
        }

        private void GoRegisterJob(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterJob());
        }

        private void FocusPesquisa(object sender, EventArgs e)
        {
            TextoPesquisa.Focus();
        }

        private void FocusCidade(object sender, EventArgs e)
        {
            TextoCidade.Focus();
        }

        private void Logout(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("User");
            App.Current.SavePropertiesAsync();
            App.Current.MainPage = new Login();
        }

        private async void Search(object sender, EventArgs e)
        {
            string word = TextoPesquisa.Text;
            string cityState = TextoCidade.Text;

            ResponseService<List<Job>> responseService = await _service.GetJobs(word, cityState);

            if (responseService.IsSucess)
            {
                //Colocar dentro da collection 
                ListOfJobs.ItemsSource = responseService.Data;
            }
            else
            {
                await DisplayAlert("Erro!", "Ocorreu um erro inesperado, tente novamente mais tarde.", "OK");
            }
         }
    }
}