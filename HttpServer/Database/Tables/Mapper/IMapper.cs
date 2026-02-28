namespace HttpServer.Database.Tables.Mapper;

public interface IMapper<TEntity>
{
    TEntity DeserializeEntity(List<string> fieldsOfEntity);
    List<TEntity> LoadEntitiesFromFile(string path);
    void SaveEntitiesToFile(string path,List<TEntity> entities);
}