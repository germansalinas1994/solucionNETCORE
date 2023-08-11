using System;
using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Cliente.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _http;
        public EmpleadoService(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<EmpleadoDto>> Lista()
        {
            //SI NO AGREGO EL ASYNC AL MÉTOODO Y EL AWWAIT NO ME DEJA ENTRAR AL DETALLE DEL RESULT
            var result = await _http.GetFromJsonAsync<ResponseApi<List<EmpleadoDto>>>("api/Empleado/Lista");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);

            }
        }


        public async Task<EmpleadoDto> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<EmpleadoDto>>($"api/Empleado/Buscar/{id}");

            if (result!.EsCorrecto)
            {
                return result.Valor!;
            }
            else
            {
                throw new Exception(result.Mensaje);

            }
        }


        //ACA VIENEN TODOS LOS METODOS QUE INTERACTUAN CON LA BASE DE DATOS, ENTONCES EN ESTOS METODOS SE SETEA EL RESULT Y LA RESPONSE QUE VAMOS A DEVOLVER
        //POST PARA GUARDAR
        //PUT PARA EDITAR
        //DELETE PARA BORRAR

        public async Task<int> Guardar(EmpleadoDto empleado)
        {
            //al metodo le mandamos un objeto de tipo EmpleadoDto 
            //tengo que hacer un post y del resultado, que es un json me quedo con la respuesta 
            //var result = await _http.PostAsync($"api/Empleado/Guardar", empleado.NombreCompleto,empleado.Sueldo, empleado.IdDepartamento, empleado.FechaContrato);
            //var result = await _http.PostAsJsonAsync($"api/Empleado/Guardar", empleado);

            var result = await _http.PostAsJsonAsync($"api/Empleado/Guardar", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);

            }
        }


        public async Task<int> Editar(EmpleadoDto empleado)
        {
            //al metodo le mandamos un objeto de tipo EmpleadoDto 
            //tengo que hacer un put y del resultado, que es un json me quedo con la respuesta



            var result = await _http.PutAsJsonAsync($"api/Empleado/Editar/{empleado.IdEmpleado}", empleado);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<EmpleadoDto>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!.IdEmpleado;
            }
            else
            {
                throw new Exception(response.Mensaje);

            }
        }

        public async Task<string> Eliminar(int id)
        {
            //al metodo le mandamos un objeto de tipo EmpleadoDto 
            //tengo que hacer un delete y del resultado, que es un json me quedo con la respuesta 

            var result = await _http.DeleteAsync($"api/Empleado/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<string>>();

            if (response!.EsCorrecto)
            {
                return response.Valor!;
            }
            else
            {
                throw new Exception(response.Mensaje);

            }
        }


    }
}

