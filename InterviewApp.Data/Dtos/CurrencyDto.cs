using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace InterviewApp.Data.Dtos
{
    [XmlRoot(ElementName = "Currency")]
    public class CurrencyDto
    {
        [XmlElement(ElementName = "NumCode")] public int NumCode { get; set; }
        [XmlElement(ElementName = "CharCode")] public string CharCode { get; set; }
        [XmlElement(ElementName = "Scale")] public int Scale { get; set; }
        [XmlElement(ElementName = "Name")] public string Name { get; set; }
        [XmlElement(ElementName = "Rate")] public decimal Rate { get; set; }
    }
}