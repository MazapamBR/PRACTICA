using Appi2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Appi2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MensajeController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly MensajeService _mensajeService;

    public MensajeController(UsuarioService usuarioService, MensajeService mensajeService)
    {
        _usuarioService = usuarioService;
        _mensajeService = mensajeService;
    }

    [HttpPost("enviar")]
    public IActionResult EnviarMensaje([FromQuery] string contenido)
    {
        if (_usuarioService.RolActual is not ("Encriptador" or "Administrador"))
            return Forbid("No tienes permiso para enviar mensajes.");
        
        _mensajeService.EnviarMensaje(contenido);
        return Ok("Mensaje encriptado y guardado.");
    }

    [HttpGet("ver")]
    public IActionResult VerMensajes()
    {
        return Ok(_mensajeService.GetMensajes());
    }

    [HttpGet("desencriptar/{id}")]
    public IActionResult Desencriptar(int id)
    {
        if (_usuarioService.RolActual is not ("Desencriptador" or "Administrador"))
            return Forbid("No tienes permiso para desencriptar.");
        
        var contenido = _mensajeService.DesencriptarMensaje(id);
        return contenido == null ? NotFound("Mensaje no encontrado.") : Ok(contenido);
    }

    [HttpPut("actualizar/{id}")]
    public IActionResult Actualizar(int id, [FromQuery] string nuevo)
    {
        if (_usuarioService.RolActual != "Administrador")
            return Forbid("No tienes permiso para actualizar.");
        
        return _mensajeService.ActualizarMensaje(id, nuevo) ? Ok("Actualizado.") : NotFound("Mensaje no encontrado.");
    }
}
