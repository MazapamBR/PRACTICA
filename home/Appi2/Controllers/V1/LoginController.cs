using Appi2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appi2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public LoginController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public IActionResult Login([FromQuery] string nombre)
    {
        if (_usuarioService.Login(nombre))
        {
            return Ok(new { mensaje = $"Bienvenido {nombre}", rol = _usuarioService.RolActual });
        }

        return Unauthorized("Usuario no válido.");
    }
}
