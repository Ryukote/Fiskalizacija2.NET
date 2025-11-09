namespace Fisk2.Core.Security;

using System.Security.Cryptography.X509Certificates;

public static class CertificateLoader
{
    public static X509Certificate2 LoadFromPfx(string path, string? password = null)
        => new(path, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
}
