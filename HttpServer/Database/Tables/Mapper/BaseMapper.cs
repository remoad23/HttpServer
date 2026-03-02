namespace HttpServer.Database.Tables.Mapper;

public abstract class BaseMapper<TEntity> : IMapper<TEntity>
{
    public List<TEntity> LoadEntitiesFromFile(string path)
    {
        var entities = new List<TEntity>();
        
        using var filestream = File.OpenRead(path);
        using var streamReader = new StreamReader(filestream);

        while (!streamReader.EndOfStream)
        {
            var entityPropertiesInFile = streamReader.ReadLine()!
                .Trim('{','}')
                .Split(',')
                .Select(p => p.Trim('"')).ToList();

            var deserializedEntity = DeserializeEntity(entityPropertiesInFile);
            entities.Add(deserializedEntity);
        }

        return entities;
    }
    
    public void SaveEntitiesToFile(string path,List<TEntity> entities)
    {
        using var filestream = File.Open(path, FileMode.Create);
        using var streamWriter = new StreamWriter(filestream);

        foreach (var entity in entities)
        {
            streamWriter.WriteLine(entity!.ToString());
        }
    }
    
    public abstract TEntity DeserializeEntity(List<string> fieldsOfEntity);
}
