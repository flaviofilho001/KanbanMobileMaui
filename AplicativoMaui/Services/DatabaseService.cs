namespace AplicativoMaui.Services;
using SQLite;

public class DatabaseService<T> where T : new()
{
    private SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<T>().Wait();
    }

    public Task<int> IncluirAsync(T item)
    {
        return _database.InsertAsync(item);
    }
    
    public Task<int> DeleteAsync(T item)
    {
        return _database.DeleteAsync(item);
    }
    
    //UpdateAsync
    public Task<int> AlterarAsync(T item)
    {
        return _database.UpdateAsync(item);
    }
    public Task<List<T>> TodosAsync()
    {
        return _database.Table<T>().ToListAsync();
    }
    

    public Task<int> QuantidadeAsync()
    {
        return _database.Table<T>().CountAsync();
    }
}