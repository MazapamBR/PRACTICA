using Appi2.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace Appi2.Services;

public class MensajeService
{
    private readonly string _archivoJson = "mensajes.json"; // Archivo JSON en carpeta raíz de la app
    private List<Mensaje> _mensajes = new();
    private int _idCounter = 1;

    public MensajeService()
    {
        CargarMensajes();
    }

    private void CargarMensajes()
    {
        if (File.Exists(_archivoJson))
        {
            var json = File.ReadAllText(_archivoJson);
            _mensajes = JsonSerializer.Deserialize<List<Mensaje>>(json) ?? new List<Mensaje>();
            if (_mensajes.Any())
            {
                _idCounter = _mensajes.Max(m => m.Id) + 1;
            }
        }
    }

    private void GuardarMensajes()
    {
        var json = JsonSerializer.Serialize(_mensajes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_archivoJson, json);
    }

    public List<Mensaje> GetMensajes() => _mensajes;

    public void EnviarMensaje(string contenido)
    {
        var encriptado = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(contenido));
        _mensajes.Add(new Mensaje { Id = _idCounter++, Contenido = contenido, Encriptado = encriptado });
        GuardarMensajes(); // Guarda después de añadir
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
        GuardarMensajes(); // Guarda después de actualizar
        return true;
    }
}
