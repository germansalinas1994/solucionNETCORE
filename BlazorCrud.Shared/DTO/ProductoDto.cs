using System;
namespace BlazorCrud.Shared
{
	public class ProductoDto
	{

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
        public int Id_Categoria { get; set; }


    }
}