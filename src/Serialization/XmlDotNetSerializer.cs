namespace Fisk2.Serialization;

using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Fisk2.Core.Abstractions;

public sealed class XmlDotNetSerializer : ISerializer
{
    private readonly XmlWriterSettings _writerSettings;
    private readonly XmlSerializerNamespaces _namespaces;

    public XmlDotNetSerializer(string? defaultNamespace = null, (string prefix, string ns)[]? extraNamespaces = null)
    {
        _writerSettings = new XmlWriterSettings
        {
            Indent = false,
            OmitXmlDeclaration = false,
            Encoding = new UTF8Encoding(false)
        };
        _namespaces = new XmlSerializerNamespaces();
        if (!string.IsNullOrWhiteSpace(defaultNamespace))
            _namespaces.Add(string.Empty, defaultNamespace);
        if (extraNamespaces is { Length: > 0 })
            foreach (var (p, ns) in extraNamespaces) _namespaces.Add(p, ns);
    }

    public string ContentType => "application/xml";

    public string Serialize<T>(T obj)
    {
        var s = new XmlSerializer(typeof(T));
        using var sw = new StringWriter();
        using var xw = XmlWriter.Create(sw, _writerSettings);
        s.Serialize(xw, obj, _namespaces);
        return sw.ToString();
    }

    public T Deserialize<T>(string payload)
    {
        var s = new XmlSerializer(typeof(T));
        using var sr = new StringReader(payload);
        return (T)s.Deserialize(sr)!;
    }
}
