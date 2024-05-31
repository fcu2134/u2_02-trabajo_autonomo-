using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using projecto_net.Models;

namespace projecto_net.Controllers
{
    public class LoginController : Controller
    {
        private readonly List<Usuario> _usuarios; // Simulación de una lista de usuarios en memoria

        public LoginController()
        {
            // Inicializar la lista de usuarios
            _usuarios =
            [
                new Usuario { Correo = "usuario1@example.com", Contraseña = "contraseña1" },
                new Usuario { Correo = "usuario2@example.com", Contraseña = "contraseña2" }
            ];
        }

        // GET: /Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            // Verificar si las credenciales son válidas
            var usuario = _usuarios.FirstOrDefault(u =>
            {
                return u.Correo == login.Correo && u.Contraseña == login.Contraseña;
            });

            if (usuario != null)
            {
                // Autenticación exitosa, redirigir al usuario a la página de inicio
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Credenciales inválidas, mostrar mensaje de error
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(login);
            }
        }
    }
}
