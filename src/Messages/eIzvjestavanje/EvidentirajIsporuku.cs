namespace Fisk2.Messages.eIzvjestavanje;

using System.Xml.Serialization;

[XmlRoot("EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajIsporukuZaKojuNijeIzdanERacunZahtjev
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}

[XmlRoot("EvidentirajIsporukuZaKojuNijeIzdanERacunOdgovor", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eIzvjestavanje")]
public sealed class EvidentirajIsporukuZaKojuNijeIzdanERacunOdgovor
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}
