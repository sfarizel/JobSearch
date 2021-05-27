using System.Net.Http;

namespace JobSearch.Web.Services
{
    public abstract class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "http://localhost/jobsearchapi";
        //protected string BaseApiUrl = "http://sfarizel.infrananuvem.com.br/jobsearchapi";

        public Service()
        {
            //Instanceamento 
            _client = new HttpClient();
        }
    }

}
