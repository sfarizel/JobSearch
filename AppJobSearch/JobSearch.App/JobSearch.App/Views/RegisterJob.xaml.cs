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
    public partial class RegisterJob : ContentPage
    {
        private JobService _service;

        public RegisterJob()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Save(object sender, EventArgs e)
        {
            LblMsg.Text = string.Empty;

            //Coleta o usuário registrado no celular via properties do aplicatvo
            User user = JsonConvert.DeserializeObject<User>(App.Current.Properties["User"].ToString());

            string company = TxtCompany.Text;

            string jobTitle = TxtJob.Text;
            string cityState = TxtCityState.Text;
            double initialSalary = (TxtInitialSalary.Text == null) ? 0.0 : double.Parse(TxtInitialSalary.Text);
            double finalSalary = (TxtFinalSalary.Text == null) ? 0.0 : double.Parse(TxtFinalSalary.Text);
            string contractType = (RBCLT.IsChecked) ? "CLT" : "PJ";
            string tecnologyTools = TxtTecnologyTools.Text;
            string companyDescription = TxtCompanyDescription.Text;
            string jobDescription = TxtJobDescription.Text;
            string benefits = TxtBenefits.Text;
            string interestedSendEmailTo = TxtInterestedSendEmailTo.Text;

            //Prepara o novo usuário
            Job Job = new Job()
            {
                Company = TxtCompany.Text,
                JobTitle = jobTitle,
                CityState = cityState,
                InitialSalary = initialSalary,
                FinalSalary = finalSalary,
                ContractType = contractType,
                TecnologyTools = tecnologyTools,
                CompanyDescription = companyDescription,
                JobDescription = jobDescription,
                Benefits = benefits,
                InterestedSendEmailTo = interestedSendEmailTo,
                PublicationDate = DateTime.Now,
                UserId = user.Id
            };

            await Navigation.PushPopupAsync(new Loading());

            ResponseService<Job> responseService = await _service.AddJob(Job);

            if (responseService.IsSucess)
            {
                //Armazenda internamente no aplicativo (persiste quando sair e voltar ao aplicativo)
                App.Current.Properties.Add("Job", JsonConvert.SerializeObject(responseService.Data));
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