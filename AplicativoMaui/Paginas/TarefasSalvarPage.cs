using AndroidX.AppCompat.App;
using AplicativoMaui.Constantes;
using AplicativoMaui.Models;
using AplicativoMaui.Services;
using AplicativoMaui.Enums;
using AplicativoMaui.Services;

namespace AplicativoMaui.Paginas;

public partial class TarefasSalvarPage : ContentPage
{
    DatabaseService<Tarefa> _tarefaService;
    public Tarefa Tarefa { get; set; }
    public TarefasSalvarPage(Tarefa tarefa = null)
    {
        InitializeComponent();
        _tarefaService = new DatabaseService<Tarefa>(Db.DB_PATH);

        Tarefa = tarefa ?? new Tarefa();
        BindingContext = tarefa;
        
        StatusPicker.ItemsSource = Enum.GetValues(typeof(EnumStatus)).Cast<EnumStatus>().ToList();
        UsuarioPicker.ItemsSource = UsuariosService.Instancia().Todos();
        
        StatusPicker.SelectedItem = Tarefa.Status ?? EnumStatus.Backlog;
        UsuarioPicker.SelectedItem = Tarefa.Usuario;
    }
    
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TituloEntry.Text))
        {
            await DisplayAlert("Erro", "Título é obrigatório","Fechar");
            TituloEntry.Focus();
            return;
        }
        
        Tarefa.Titulo = TituloEntry.Text;
        Tarefa.Descricao = DescricaoEditor.Text;
        if (StatusPicker.SelectedItem != null)
            Tarefa.Status = (EnumStatus)StatusPicker.SelectedItem;
        else
            Tarefa.Status = EnumStatus.Backlog;
        if(UsuarioPicker.SelectedItem != null)
            Tarefa.UsuarioId = ((Usuario)UsuarioPicker.SelectedItem).Id;

        
        if(Tarefa.Id == 0)
            await _tarefaService.IncluirAsync(Tarefa);
        else
        {
            Tarefa.DataAtualizacao = DateTime.Now;
            await _tarefaService.AlterarAsync(Tarefa);
        }
        
        await Navigation.PopAsync();
    }
    private async void VoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}