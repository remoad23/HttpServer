namespace HttpServer.Database.Tables;

public class BaseEntity : IEntity
{
    public override string ToString()
    {
        var implementedAttributes = this.GetType().GetFields().ToList();

        var entityAsString = "";

        foreach (var field in implementedAttributes)
        {
            entityAsString += $"{field},";
        }
        
        entityAsString = "{" + entityAsString[0 .. ^2] + "}";

        return entityAsString;
    }

    public Guid Id { get; set; }
}