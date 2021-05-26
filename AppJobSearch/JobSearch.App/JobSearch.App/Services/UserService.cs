using JobSearch.App.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class UserService : Service
    {
        public async Task<ResponseService<User>> GetUser(string email, string password)
        {
            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Users/?email={email}&password={password}");

            List<User> list = null;

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSucess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; //se usar ToString retorna a mensagem

            if (response.IsSuccessStatusCode)
            {
                //Ler a string do conteúdo retornado e deserializar 
                list = await response.Content.ReadAsAsync<List<User>>();
                responseService.Data = list[0];
            }
            else
            {
                string erroResponse = await response.Content.ReadAsStringAsync();
                var erros = JsonConvert.DeserializeObject<ResponseService<User>>(erroResponse);
                responseService.Erros = erros.Erros ;
            }
            return responseService;
        }
        public async Task<User> AddUser(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs", user);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                user = null;
            }

            return user;
        }
    }
}
