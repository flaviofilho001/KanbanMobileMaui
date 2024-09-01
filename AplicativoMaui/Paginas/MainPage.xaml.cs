using System.Windows.Input;
using AplicativoMaui.Constantes;
using AplicativoMaui.Enums;
using AplicativoMaui.Models;
using AplicativoMaui.Services;

namespace AplicativoMaui.Paginas;

public partial class MainPage : ContentPage
{
    DatabaseService<Tarefa> _tarefaService;
    public ICommand NavigateToDetailCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }
    public ICommand NavigateToChangeCommand { get; private set; }

    


    public MainPage()
    {
        InitializeComponent();
        _tarefaService = new DatabaseService<Tarefa>(Db.DB_PATH);
        NavigateToDetailCommand = new Command<Tarefa>(async (tarefa) => await NavigateToDetail(tarefa));
        DeleteCommand = new Command<Tarefa>(ExecuteDeleteCommand);
        NavigateToChangeCommand = new Command<Tarefa>(async (tarefa) => await NavigateToChange(tarefa));

        TarefasCollectionViewTable.BindingContext = this;
        CarregarTarefas();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarTarefas();
    }

    private async void ExecuteDeleteCommand(Tarefa tarefa)
    {
        bool confirm = await DisplayAlert("Confirmação", "Deseja Excluir esta tarefa?", "Sim", "Não");
        if (confirm)
        {
            await _tarefaService.DeleteAsync(tarefa);
            CarregarTarefas();
        }
    }
    
    private async Task NavigateToChange(Tarefa tarefa)
    { 
        Navigation.PushAsync(new TarefasSalvarPage(tarefa));
    }

    private async Task NavigateToDetail(Tarefa tarefa)
    { 
        Navigation.PushAsync(new TarefasDetalhePage(tarefa));
    }

    private async void CarregarTarefas()
    {
        var tarefas = await _tarefaService.TodosAsync();
        TarefasCollectionViewTable.ItemsSource = tarefas;
    }
    
    private async void NovoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TarefasSalvarPage()); 
    }
    
}