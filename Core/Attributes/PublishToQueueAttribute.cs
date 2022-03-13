namespace Core.Attributes;
public class PublishToQueueAttribute : Attribute
{
    public string RoutingKey { get; set; }
    public string Exchange { get; set; }
    public Type Tipo { get; set; }

    public enum Type
    {
        Fanout,
        Direct,
        Topic
    }
}
