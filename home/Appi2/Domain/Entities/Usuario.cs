namespace Appi2.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Rol { get; set; } = ""; // Tipo1, Tipo2, Tipo3
}
