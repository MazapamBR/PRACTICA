using Appi2.Domain.Entities;

namespace Appi2.Services;

public class UsuarioService
{
    public Usuario? UsuarioActual { get; private set; }

    public bool Login(string nombre)
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { Nombre = "Litzy", Rol = "Encriptador" },
            new Usuario { Nombre = "Rusell", Rol = "Desencriptador" },
            new Usuario { Nombre = "Gabriel", Rol = "Administrador" }
        };

        UsuarioActual = usuarios.FirstOrDefault(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        return UsuarioActual != null;
    }

    public string? RolActual => UsuarioActual?.Rol;
}
