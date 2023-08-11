using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorCrud.Server.Controllers
{



    [Route("api/[controller]")]
    public class DepartamentoController : Controller
    {
        //Instancio el servicio de la base de datos como variables _dbContext
        private readonly DbcrudBlazorContext _dbContext;
        //Instancio el automapper
        //private readonly IMapper _mapper;

        //Constructor para poder inyectar el servicio de base de datos en la clase controller
        public DepartamentoController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<DepartamentoDto>>();
            var listaDepartamentoDto = new List<DepartamentoDto>();

            //con el automapper quizas sea

            //var listaDepartamentoAutomapper = await _dbContext.Departamentos.ToListAsync();
            //List<DepartamentoDto> departamento = _mapper.Map<List<DepartamentoDto>>(listaDepartamentoAutomapper).ToList();


            try
            {
                //ESTA INSTANCIA SE PUEDE HACER DIRECTA CON AUTOMAPPER AUNQUE DIFICULTA ENCONTRAR ERRORES
                //TAMBIEN SE INSTALA DEPENDENCY INJECTION PARA PERMITIR HACER LAS INYECCIONES DE DEPENDENCIA

                var departamentos = await _dbContext.Departamentos.ToListAsync();

                foreach (var item in await _dbContext.Departamentos.ToListAsync())
                {
                    listaDepartamentoDto.Add(new DepartamentoDto
                    {
                        IdDepartamento = item.IdDepartamento,
                        Nombre = item.Nombre,

                    });

                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaDepartamentoDto;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message; 
            }
            return Ok(responseApi);
        }

    }
}

