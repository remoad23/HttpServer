using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using HttpServer.Database.Tables;
using HttpServer.Database.Tables.Mapper;

namespace HttpServer.Database;

public class Database
{
    private List<User> _users = new();
    public Database()
    {
        SaveDatabase();
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

    // Todo: refactor GetAllTables hier hin
    // evt mit der loaddatabase zusammen refactorieren?
    private void SaveDatabase()
    {
        var databases = GetAllTables();
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

    private List<FieldInfo> GetAllTables()
    {
        //vielleicht:
        // wont compare 
        var tables = this.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where((field) => field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                .ToList();
        
        foreach(var table in tables)
        {
            var genericTypeOfList = table.FieldType.GetGenericArguments()[0] ??
                                    throw new NullReferenceException("Generic der List nicht gefunden");
            
            var mapperPath = $"HttpServer.Database.Tables.Mapper.{genericTypeOfList.Name}Mapper";
            var mapperForGeneric = Type.GetType(mapperPath) ??
                                   throw new NullReferenceException("Mapper fuer Entity nicht finden");
            
            var mapper = Activator.CreateInstance(mapperForGeneric)!;
   
            var entity = table.GetValue(this);
            
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            
            var path = Path.Combine(projectDirectory, "Database","Seeder",$"{genericTypeOfList.Name}seeder.txt");

            mapper.GetType().GetMethod("SaveEntitiesToFile")!.Invoke(mapper,[path,entity]);
        }
        
        return tables;
    }
    
    internal string GetPathOfTable(Assembly assembly)
    {
        var name = $"{assembly.GetName()}seeder.txt";
        var path = Path.Combine(Directory.GetCurrentDirectory(),"Database","Seeder", name);
        return path;
    }
}