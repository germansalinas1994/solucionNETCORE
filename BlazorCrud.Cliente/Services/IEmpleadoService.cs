using System;
using BlazorCrud.Shared;

namespace BlazorCrud.Cliente.Services
{
	public interface IEmpleadoService
	{
		//en task va lo que devuelve el back, en el metodo van los argumentos que hay que pasarle

		Task<List<EmpleadoDto>> Lista();
		Task<EmpleadoDto> Buscar(int id);
        Task<int> Editar(EmpleadoDto empleado);
        Task<int> Guardar(EmpleadoDto empleado);
        Task<string> Eliminar(int id);




	}
}

