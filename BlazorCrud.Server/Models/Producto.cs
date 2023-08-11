﻿using System;
using System.Collections.Generic;

namespace BlazorCrud.Server.Models;

public partial class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Precio { get; set; }
    public int Stock { get; set; }
    public int Id_Categoria { get; set; }
}
