using JobSearch.Domain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class JobService : Service
    {
        public async Task<IEnumerable<Job>> GetJobss(string word, string cityState, int numberOfPage = 1)
        {
            List<Job> list = null;

          HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs?word={word}&cityState={cityState}&numberOfPage={numberOfPage}");
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<Job>>();
            }
            return list;
        }

        public async Task<Job> GetJob(int id)
        {
            Job job  = null;

            HttpResponseMessage response = await _client.GetAsync($"{BaseApiUrl}/api/Jobs/{id}");
            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<Job>();
            }
            return job;
        }

        public async Task<Job> AddJob(Job job)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Jobs",job);
            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<Job>();
            }
            else
            {
                job = null;
            }
            return job;
        }
    }
}
