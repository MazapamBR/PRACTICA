using Appi2.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Appi2.Services;

public class MensajeService
{
    private readonly List<Mensaje> _mensajes = new();
    private int _idCounter = 1;

    public List<Mensaje> GetMensajes() => _mensajes;

    public void EnviarMensaje(string contenido)
    {
        var encriptado = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(contenido));
        _mensajes.Add(new Mensaje { Id = _idCounter++, Contenido = contenido, Encriptado = encriptado });
    }

    public string? DesencriptarMensaje(int id)
    {
        var mensaje = _mensajes.FirstOrDefault(m => m.Id == id);
        return mensaje == null ? null : System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(mensaje.Encriptado));
    }

    public bool ActualizarMensaje(int id, string nuevoContenido)
    {
        var mensaje = _mensajes.FirstOrDefault(m => m.Id == id);
        if (mensaje == null) return false;
        mensaje.Contenido = nuevoContenido;
        mensaje.Encriptado = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(nuevoContenido));
        return true;
    }
}
