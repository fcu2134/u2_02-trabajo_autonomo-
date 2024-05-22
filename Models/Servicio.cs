using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace projecto_net.Models;





public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Precio { get; set; }

    public string? Sku { get; set; }

    /// <summary>
    /// 0:Inactivo ; 1: Activo
    /// </summary>
    public int Estado { get; set; }

    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
