using JobSearch.App.Models;
using JobSearch.APP.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class JobService : Service
    {
        public async Task<ResponseService<List<Job>>> GetJobs(string word, string cityState, int numberOfPage = 1)
        {

            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs?word={word}&cityState={cityState}&numberOfPage={numberOfPage}");
            ResponseService<List<Job>> responseService = new ResponseService<List<Job>>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; //se usar ToString retorna a mensagem

            if (response.IsSuccessStatusCode)
            {
                //Ler a string do conteúdo retornado e deserializar 
                responseService.Data = await response.Content.ReadAsAsync<List<Job>>();
            }
            else
            {
                string erroResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<List<User>>>(erroResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Job>> GetJob(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs/{id}");
            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; //se usar ToString retorna a mensagem

            if (response.IsSuccessStatusCode)
            {
                //Ler a string do conteúdo retornado e deserializar 
                responseService.Data = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                string erroResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<User>>(erroResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<Job>> AddJob(Job job)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs", job);

            ResponseService<Job> responseService = new ResponseService<Job>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; //se usar ToString retorna a mensagem
            if (response.IsSuccessStatusCode)
            {
                //Ler a string do conteúdo retornado e deserializar 
                responseService.Data = await response.Content.ReadAsAsync<Job>();
            }
            else
            {
                string erroResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<Job>>(erroResponse);
                responseService.Errors = erros.Errors;
            }
            return responseService;
        }
    }
}
