using AplicativoMaui.Enums;
using AplicativoMaui.Services;
using SQLite;

namespace AplicativoMaui.Models;

public class Tarefa
{
    public Tarefa()
    {
        this.DataCriacao = DateTime.Now;
        this.DataAtualizacao = DateTime.Now;

    }
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public int UsuarioId { get; set; }
    
    public Usuario Usuario
    {
        get
        {
            if (this.UsuarioId == 0) return null;
            return UsuariosService.Instancia().Todos().Find(u => u.Id == this.UsuarioId);
        }
    }

    [Ignore]
    public string NomeUsuario
    {
        get
        {
            if (this.Usuario == null) return "Não possui usuário";
            return Usuario?.Nome;
        }
    }

    public EnumStatus? Status { get; set; }

}