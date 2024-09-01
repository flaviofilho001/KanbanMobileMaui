using AplicativoMaui.Models;

namespace AplicativoMaui.Paginas;

public partial class TarefasDetalhePage : ContentPage
{
    public Tarefa Tarefa { get; set; }
    public TarefasDetalhePage(Tarefa tarefa)
    {
        InitializeComponent();
        Tarefa = tarefa;
        BindingContext = this;
    }

    private async void VoltarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}