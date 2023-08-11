 using System;
using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Cliente.Services
{
	//EL SERVICE HEREDA LOS METODOS DE LA INTERFAZ Y LOS IMPLEMENTA
	public class DepartamentoService : IDepartamentosService
	{
		//instanciamos el http
		private readonly HttpClient _http;

		public DepartamentoService(HttpClient http)
		{

			_http = http;
		}



	 	public async Task<List<DepartamentoDto>> Lista()
		{
			//SI NO AGREGO EL ASYNC AL MÉTOODO Y EL AWWAIT NO ME DEJA ENTRAR AL DETALLE DEL RESULT
            var result2 = await _http.GetFromJsonAsync<ResponseApi<List<ProductoDto>>>("/products");
            var result = await _http.GetFromJsonAsync<ResponseApi<List<DepartamentoDto>>>("api/Departamento/Lista");

            if (result!.EsCorrecto)
			{
				return result.Valor!;
				 
			}
			else
			{
				throw new Exception(result.Mensaje);

			}
			


        }
    }
}

