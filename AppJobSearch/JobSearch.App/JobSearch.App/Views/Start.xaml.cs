using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.APP.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Job> _listOfJobs;
        private SearchParams _searchParams;
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
            Loading.IsVisible = true;
            Loading.IsRunning = true;
            LblResult.Text = "Aguarde...";

            string word = TextoPesquisa.Text;
            string cityState = TextoCidade.Text;

            _searchParams = new SearchParams { Word = word, CityState = cityState, PageNumber = 1 };

            ResponseService<List<Job>> responseService = await _service.GetJobs(_searchParams.Word, _searchParams.CityState, _searchParams.PageNumber);

            if (responseService.IsSucess)
            {
                //Colocar dentro da collection 
                _listOfJobs = new ObservableCollection<Job>(responseService.Data);
                ListOfJobs.ItemsSource = _listOfJobs;
                ListOfJobs.RemainingItemsThreshold = 1;
            }
            else
            {
                await DisplayAlert("Erro!", "Ocorreu um erro inesperado, tente novamente mais tarde.", "OK");
            }

            LblResult.Text = "Nenhum Resultado";
            Loading.IsVisible = false;
            Loading.IsRunning = false;
        }

        private async void InfinitySearch(object sender, EventArgs e)
        {
            _searchParams.PageNumber++;

            Loading.IsVisible = true;
            Loading.IsRunning = true;

            ResponseService<List<Job>> responseService = await _service.GetJobs(_searchParams.Word, _searchParams.CityState, _searchParams.PageNumber);

            if (responseService.IsSucess)
            {

                var listOfJobsFromService = new ObservableCollection<Job>(responseService.Data);

                foreach (var item in listOfJobsFromService)
                {
                    _listOfJobs.Add(item);
                }
            }
            else
            {
                await DisplayAlert("Erro!", "Ocorreu um erro inesperado, tente novamente mais tarde.", "OK");
            }

            Loading.IsVisible = false;
            Loading.IsRunning = false;
        }
    }
}