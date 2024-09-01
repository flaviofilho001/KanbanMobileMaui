namespace AplicativoMaui.Services;
using AplicativoMaui.Models;

public class UsuariosService
{
    private static List<Usuario> _usuarios = new List<Usuario>();
    private static UsuariosService _usuariosService = new UsuariosService();

    private UsuariosService()
    {
        _usuarios.Add(new Usuario{Id = 1, Nome = "Flavio"});
        _usuarios.Add(new Usuario{Id = 2, Nome = "Gabriel"});
        _usuarios.Add(new Usuario{Id = 3, Nome = "Bia"});
        _usuarios.Add(new Usuario{Id = 4, Nome = "Jose"});
    }

    public static UsuariosService Instancia()
    {
        return _usuariosService;
    }

    public List<Usuario> Todos()
    {
        return _usuarios;
    }
}