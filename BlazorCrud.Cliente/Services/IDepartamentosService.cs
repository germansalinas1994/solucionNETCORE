using System;
using BlazorCrud.Shared;

namespace BlazorCrud.Cliente.Services
{
	public interface IDepartamentosService
    {

		Task<List<DepartamentoDto>>Lista();
	}
}

