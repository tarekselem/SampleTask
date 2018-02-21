using System;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "FullAddress")]
    public class FullAddress
    {
        [XmlElement(ElementName = "Address")]
        public string Address { get; set; }

        [XmlElement(ElementName = "City")]
        public string City { get; set; }

        [XmlElement(ElementName = "Region")]
        public string Region { get; set; }

        [XmlElement(ElementName = "PostalCode")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "Country")]
        public string Country { get; set; }
    }
}
