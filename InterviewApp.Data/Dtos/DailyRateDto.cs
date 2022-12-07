using System.Collections.Generic;
using System.Xml.Serialization;

namespace InterviewApp.Data.Dtos;

[XmlRoot(ElementName = "DailyExRates")]
public class DailyRateDto
{
    [XmlElement(ElementName = "Currency")] public List<CurrencyDto> Currency { get; set; }
    [XmlAttribute(AttributeName = "Date")] public string Date { get; set; }
}