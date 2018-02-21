using System;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "ShipInfo")]
    public class ShipInfo
    {
        [XmlAttribute(AttributeName = "ShippedDate")]
        public DateTime ShippedDate { get; set; }

        [XmlElement(ElementName = "ShipVia")]
        public int ShipVia { get; set; }

        [XmlElement(ElementName = "Freight")]
        public decimal Freight { get; set; }

        [XmlElement(ElementName = "ShipName")]
        public string ShipName { get; set; }

        [XmlElement(ElementName = "ShipAddress")]
        public string ShipAddress { get; set; }

        [XmlElement(ElementName = "ShipCity")]
        public string ShipCity { get; set; }

        [XmlElement(ElementName = "ShipRegion")]
        public string ShipRegion { get; set; }

        [XmlElement(ElementName = "ShipPostalCode")]
        public string ShipPostalCode { get; set; }

        [XmlElement(ElementName = "ShipCountry")]
        public string ShipCountry { get; set; }

    }
}
