using System;
using System.Collections.Generic;

namespace projecto_net.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    /// <summary>
    /// 0:Inactivo ; 1:Activo
    /// </summary>
    public int Estado { get; set; }
}
