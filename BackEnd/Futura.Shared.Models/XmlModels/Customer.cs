using System;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "Customer")]
    public class Customer
    {
        [XmlElement(ElementName = "CompanyName")]
        public string CompanyName { get; set; }

        [XmlElement(ElementName = "ContactName")]
        public string ContactName { get; set; }

        [XmlElement(ElementName = "ContactTitle")]
        public string ContactTitle { get; set; }

        [XmlElement(ElementName = "FullAddress")]
        public FullAddress FullAddress { get; set; }

        [XmlAttribute(AttributeName = "CustomerID")]
        public string CustomerID { get; set; }

        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "Fax")]
        public string Fax { get; set; }
    }
}
