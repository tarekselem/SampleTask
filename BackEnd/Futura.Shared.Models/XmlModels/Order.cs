using System;
using System.Xml.Serialization;

namespace XmlModels
{
    [Serializable()]
    [XmlRoot(ElementName = "Order")]
    public class Order
    {
        [XmlElement(ElementName = "CustomerID")]
        public string CustomerID { get; set; }

        [XmlElement(ElementName = "EmployeeID")]
        public int EmployeeID { get; set; }

        [XmlElement(ElementName = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [XmlElement(ElementName = "RequiredDate")]
        public DateTime RequiredDate { get; set; }

        [XmlElement(ElementName = "ShipInfo")]
        public ShipInfo ShipInfo { get; set; }

    }
}
