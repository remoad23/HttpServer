using System.Reflection;
using HttpServer.Database.Tables;

namespace HttpServer.Database;

public partial class Database
{
    public List<User> _users;
    public List<string> test;

    public Database()
    {
        _users = [];
        _readerWriterLocks = new ();
        LoadEntitiesFromFile();
    }

    private void LoadEntitiesFromFile()
    {
        var tablesOfDatabase = GetAllTables();
        foreach (var table in tablesOfDatabase)
        {
            _readerWriterLocks[table.FieldType] = (new ReaderWriterLockSlim(),GetTableByType(table.FieldType));
            var (mapper,seederPath) = GetMapperAndPathOfTable(table);
            var entityTypeOfTable = table.GetValue(this);
            var tableInFile = mapper.GetType().GetMethod("LoadEntitiesFromFile")!.Invoke(mapper, [seederPath]);
            entityTypeOfTable = tableInFile;
        }
    }

    private void SaveDatabase()
    {
        var tablesOfDatabase = GetAllTables();
        
        foreach (var table in tablesOfDatabase)
        {
            var (mapper,seederPath) = GetMapperAndPathOfTable(table);
            var entityTypeOfTable = table.GetValue(this);
            mapper.GetType().GetMethod("SaveEntitiesToFile")!.Invoke(mapper, [seederPath, entityTypeOfTable]);
        }
    }
    

    private List<FieldInfo> GetAllTables()
    {
        var tablesOfDatabase = this.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .Where((field) => field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            .ToList();

        return tablesOfDatabase;
    }

    private (object mapper,string path) GetMapperAndPathOfTable(FieldInfo table)
    {
        var genericTypeOfList = table.FieldType.GetGenericArguments()[0] ??
                                throw new NullReferenceException("Generic der List nicht gefunden");

        var mapperPath = $"HttpServer.Database.Tables.Mapper.{genericTypeOfList.Name}Mapper";
        var mapperForGeneric = Type.GetType(mapperPath) ??
                               throw new NullReferenceException("Mapper fuer Entity nicht finden");

        var mapper = Activator.CreateInstance(mapperForGeneric)!;
        var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        var seederFilePath = Path.Combine(projectDirectory, "Database", "Seeder", $"{genericTypeOfList.Name}seeder.txt");
        

        return (mapper,seederFilePath);
    }
}