using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using HttpServer.Database.Tables;

namespace HttpServer.Database;

public class Database
{
    public Database()
    {
        LoadDatabase();
    }
    
    private void LoadDatabase()
    {
        var files = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList();
        List<Task<string[]>> fileContents = [];
        
        foreach (var file in files)
        {
            // hier neue methode fuer einzelne entities differenzierne und wie sie eingefuegt werden
            // design pattern anwenden
            //File.AppendAllLinesAsync(file,[]);
            fileContents.Add(File.ReadAllLinesAsync(file));
        }
        
        Task.WaitAll(fileContents.ToArray());
    }

    private void SaveDatabase()
    {
        var databases = GetAllDatabasesAsAttribute();
        List<Task> fileContents = [];
        
        // @Todo einzelene entities mittels design pattern (ka welche) einfuegen
        foreach (var table in databases)
        { 
            // hier neue methode fuer einzelne entities differenzierne und wie sie eingefuegt werden
            // design pattern anwenden
            //File.AppendAllLinesAsync(file,[]);
        }
        
        Task.WaitAll(fileContents.ToArray());
    }

    private List<Attribute> GetAllDatabasesAsAttribute()
    {
        //vielleicht:
        //if (type is typeof(List<>) && attribute.GetType().GenericTypeArguments[0] is IEntity)
        var tables = this.GetType()
            .GetCustomAttributes()
            .Where((attribute) => attribute.GetType() != typeof(List<IEntity>)).ToList();

        return tables;
    }
    
    internal string GetPathOfTable(Assembly assembly)
    {
        var name = $"{assembly.GetName()}seeder.txt";
        var path = Path.Combine(Directory.GetCurrentDirectory(),"Database","Seeder", name);
        return path;
    }
}