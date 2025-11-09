namespace Fisk2.Validation;

using System.Xml;
using System.Xml.Schema;

public sealed class XmlSchemaValidator
{
    private readonly ISchemaRepository _schemas;
    private readonly string[] _xsdFiles;

    public XmlSchemaValidator(ISchemaRepository schemas, params string[] xsdFiles)
    {
        _schemas = schemas;
        _xsdFiles = xsdFiles;
    }

    public void ValidateXmlRaw(string xml)
    {
        var set = new XmlSchemaSet();
        foreach (var file in _xsdFiles)
        {
            using var stream = _schemas.OpenSchema(file);
            set.Add(null, XmlReader.Create(stream));
        }
        var errors = new List<string>();
        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = set,
            ValidationFlags = XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings
        };
        settings.ValidationEventHandler += (s, e) => errors.Add($"[{e.Severity}] {e.Message}");
        using var sr = new StringReader(xml);
        using var xr = XmlReader.Create(sr, settings);
        while (xr.Read()) { }
        if (errors.Count > 0) throw new InvalidOperationException(string.Join("\n", errors));
    }
}
