namespace Appi2.Domain.Entities;

public class Mensaje
{
    public int Id { get; set; }
    public string Contenido { get; set; } = string.Empty;
    public string Encriptado { get; set; } = string.Empty;
}
