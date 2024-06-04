using System.Linq;
using Microsoft.AspNetCore.Mvc;
using projecto_net.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace projecto_net.Controllers
{
    public class LoginController : Controller
    {// uso el contexto de mi base para poder enviar y recibir datos 
        private readonly MercyDeveloperContext _context;

        public bool AllowRefresh { get; private set; }

        public LoginController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: /Login pa leer los datos de la base ,uwu


        // GET: /recibimos los datos del registro (algo asi entendi ) xd
        public IActionResult Registro()
        {
            return View(new Registro());
        }

        // POST: /Login/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistroAsync(Registro registro)
        {
            // Validamos que tanto el correo como la contraseña no sean nulos
            if (registro.Correo == null || registro.Password == null)
            {
                ViewData["mensaje"] = "Correo y contraseña son obligatorios.";
                return View();
            }
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == registro.Correo);
            if (usuarioExistente != null)
            {
                ViewData["mensaje"] = "El correo ya está registrado.";
                return View();
            }

            // Creamos un nuevo usuario con los datos proporcionados
            Usuario usuario = new Usuario()
            {
                Nombre = registro.Nombre,
                Apellido = registro.Apellido,
                Correo = registro.Correo,
                Password = registro.Password
            };

            // Guardamos los datos del nuevo usuario en la base de datos
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Validamos que el ID del usuario sea válido y redirigimos según corresponda
            if (usuario.Id != 0)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // Si no se guardó correctamente, mostramos un mensaje de error
                ViewData["mensaje"] = "Hubo un error al registrar el usuario.";
                return View();
            }
        }


        public IActionResult Index()
        {
            return View(new Login());
        }

        // POST: /Login basicamente en donde ingresamos nuestras cosas pa despues subirlas 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login login)
        {
            Usuario?
//verifica si el usuario existe

 usuario_encontrado = await _context.Usuarios
                                 .Where(u => u.Correo == login.Correo && u.Password == login.Password  )
                                 .FirstOrDefaultAsync();


            if (usuario_encontrado == null)
            {
                ViewData["mensaje"] = "no se encontraron coincidencias ";
                return View();
            }

   //método para guardar la autenticación para que después me diga el usuario a ingresado (te muestra un mensaje en la página de inicio ),esto lo llamamos en el index con un 
//if(){                    @User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault()}


            List <Claim>claims= new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,usuario_encontrado.Nombre),
                    new Claim(ClaimTypes.Email,usuario_encontrado.Correo),
                   
                    new Claim(ClaimTypes.Anonymous,usuario_encontrado.Password)
                };
                ClaimsIdentity claimsIdentity =new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties();
            {
                AllowRefresh = true;
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
             );
            return RedirectToAction("Index", "Home");
        }
    }
}