/// <summary>
/// XML serializer abstraction.
/// </summary>
namespace Fisk2.Core.Abstractions;

public interface ISerializer
{
    string ContentType { get; }
    string Serialize<T>(T obj);
    T Deserialize<T>(string payload);
}
