using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ESTA SE USA PARA LOS REQUIRED
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class ResponseApi<T>
    {
        public bool EsCorrecto { get; set; }
        public T? Valor { get; set; }
        public string? Mensaje { get; set; }
       
    }
}