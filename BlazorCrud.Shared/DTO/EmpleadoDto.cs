using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//ESTA SE USA PARA LOS REQUIRED
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class EmpleadoDto
    {

        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo es requerido")]
        public int IdDepartamento { get; set; }

        public int Sueldo { get; set; }

        public DateTime FechaContrato { get; set; }

        public virtual DepartamentoDto? Departamento { get; set; }

    }
}