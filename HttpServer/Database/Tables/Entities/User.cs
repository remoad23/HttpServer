namespace HttpServer.Database.Tables;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
}