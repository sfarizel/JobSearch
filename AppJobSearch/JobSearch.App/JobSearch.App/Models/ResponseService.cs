﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobSearch.App.Models
{
    //Encapsulamento do retorno HTTP
    public class ResponseService<T>
    {
        public bool IsSucess { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public Dictionary<string,List<string>> Erros  { get; set; }
    }
}
