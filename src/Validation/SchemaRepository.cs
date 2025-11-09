namespace Fisk2.Validation;

public interface ISchemaRepository
{
    Stream OpenSchema(string name);
}

public sealed class EmbeddedSchemaRepository : ISchemaRepository
{
    private readonly System.Reflection.Assembly _asm;
    private readonly string _prefix;
    public EmbeddedSchemaRepository(System.Reflection.Assembly asm, string resourcePrefix)
    {
        _asm = asm;
        _prefix = resourcePrefix.EndsWith(".") ? resourcePrefix : resourcePrefix + ".";
    }
    public Stream OpenSchema(string name)
    {
        var full = _prefix + name;
        return _asm.GetManifestResourceStream(full) ?? throw new FileNotFoundException($"Embedded XSD not found: {full}");
    }
}

public sealed class FolderSchemaRepository : ISchemaRepository
{
    private readonly string _folder;
    public FolderSchemaRepository(string folder) => _folder = folder;
    public Stream OpenSchema(string name) => File.OpenRead(Path.Combine(_folder, name));
}
