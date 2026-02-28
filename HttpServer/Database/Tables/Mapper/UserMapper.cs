namespace HttpServer.Database.Tables.Mapper;

public class UserMapper : BaseMapper<User>,IMapper<User>
{
    public override User DeserializeEntity(List<string> fieldsOfEntity)
    {
        var user = new User();
        
        return user;
    }
}