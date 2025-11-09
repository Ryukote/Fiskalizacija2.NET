namespace Fisk2.Core.Common;

/// <summary>
/// Standard envelope for all calls.
/// </summary>
public sealed class FiskResponse<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? RawPayload { get; init; }
    public string? ErrorCode { get; init; }
    public string? ErrorMessage { get; init; }

    public static FiskResponse<T> Ok(T data, string? raw = null)
        => new() { Success = true, Data = data, RawPayload = raw };

    public static FiskResponse<T> Fail(string? code, string? message, string? raw = null)
        => new() { Success = false, ErrorCode = code, ErrorMessage = message, RawPayload = raw };

    public string AsXml(Fisk2.Core.Abstractions.ISerializer? xml = null)
    {
        if (Data is null) throw new InvalidOperationException("No data to serialize.");
        var x = xml ?? new Fisk2.Serialization.XmlDotNetSerializer(defaultNamespace: "urn:fisk:2.0");
        return x.Serialize(Data);
    }
}
