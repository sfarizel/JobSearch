using System.Collections.Generic;

namespace JobSearch.Web.Models
{
    //Encapsulamento do retorno HTTP
    public class ResponseService<T>
    {
        public bool IsSucess { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public bool IsPagination { get; set; }
        public int TotalItems { get; set; }
    }
}
