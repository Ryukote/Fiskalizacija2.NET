namespace Fisk2.Messages.eFiskalizacija;

using System.Xml.Serialization;

[XmlRoot("EvidentirajERacunZahtjev", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eFiskalizacija")]
public sealed class EvidentirajERacunZahtjev
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}

[XmlRoot("EvidentirajERacunOdgovor", Namespace="http://www.porezna-uprava.gov.hr/fin/2024/types/eFiskalizacija")]
public sealed class EvidentirajERacunOdgovor
{
    [XmlAnyElement] public System.Xml.XmlElement[]? Any { get; set; }
}
