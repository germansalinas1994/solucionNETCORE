using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoController : Controller
    {
        private readonly DbcrudBlazorContext _dbContext;
        //Instancio el automapper
        //private readonly IMapper _mapper;

        //Constructor para poder inyectar el servicio de base de datos en la clase controller
        public EmpleadoController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<EmpleadoDto>>();
            var listaEmpleado = new List<EmpleadoDto>();
            try
            {
                //ESTA INSTANCIA SE PUEDE HACER DIRECTA CON AUTOMAPPER AUNQUE DIFICULTA ENCONTRAR ERRORES
                //TAMBIEN SE INSTALA DEPENDENCY INJECTION PARA PERMITIR HACER LAS INYECCIONES DE DEPENDENCIA

                foreach (var item in await _dbContext.Empleados.Include(d => d.Departamento).ToListAsync())
                {
                    listaEmpleado.Add(new EmpleadoDto
                    {
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDto
                        {
                            IdDepartamento = item.Departamento.IdDepartamento,
                            Nombre = item.Departamento.Nombre
                        }


                    });

                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaEmpleado;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseApi<EmpleadoDto>();
            var empleado = new EmpleadoDto();
            try
            {
                //ESTA INSTANCIA SE PUEDE HACER DIRECTA CON AUTOMAPPER AUNQUE DIFICULTA ENCONTRAR ERRORES
                //TAMBIEN SE INSTALA DEPENDENCY INJECTION PARA PERMITIR HACER LAS INYECCIONES DE DEPENDENCIA

                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    empleado.IdEmpleado = dbEmpleado.IdEmpleado;
                    empleado.NombreCompleto = dbEmpleado.NombreCompleto;
                    empleado.Sueldo = dbEmpleado.Sueldo;
                    empleado.IdDepartamento = dbEmpleado.IdDepartamento;
                    empleado.FechaContrato = dbEmpleado.FechaContrato;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleado;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el empleado";
                }
                
                    

                
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody]EmpleadoDto empleado)
        {
            //aca seteo lo que devuelve la api, en este caso me devuelve el id del empleado creado
            var responseApi = new ResponseApi<int>();
            try
            {
                //Empleado nuevoEmpleado = new Empleado();

                //nuevoEmpleado.NombreCompleto = empleado.NombreCompleto;
                //nuevoEmpleado.IdDepartamento = empleado.IdDepartamento;
                //nuevoEmpleado.Sueldo = empleado.Sueldo;
                //nuevoEmpleado.FechaContrato = empleado.FechaContrato;


                // otra posibilidad es hacer asi

                Empleado nuevoEmpleado = new Empleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato
                };

                _dbContext.Add(nuevoEmpleado);
                await _dbContext.SaveChangesAsync();

                if (nuevoEmpleado.IdEmpleado != 0)
                {
                    //si el id es distinto a 0 entonces el empleado se creo
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = nuevoEmpleado.IdEmpleado;

                }





            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody]EmpleadoDto empleado,int id)
        {
            //la respuesta de la api va a ser el empleado editado
            var responseApi = new ResponseApi<EmpleadoDto>();
            try
            {

                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    dbEmpleado.NombreCompleto = empleado.NombreCompleto;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;

                    _dbContext.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = empleado;



                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el empleado";
                }





            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(EmpleadoDto empleado, int id)
        {
            //la respuesta de la api va a ser el empleado editado
            var responseApi = new ResponseApi<string>();
            try
            {

                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    _dbContext.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = "Se elimino el empleado";



                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se encontro el empleado";
                }





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

