/// <summary>
/// Transport abstraction (HTTP POST or SOAP 1.1).
/// </summary>
namespace Fisk2.Core.Abstractions;

using Fisk2.Core.Common;

public interface ITransport
{
    Task<(int StatusCode, string Body, string? ContentType)> SendAsync(
        MessageKind kind,
        string content,
        string contentType,
        CancellationToken ct = default);
}
