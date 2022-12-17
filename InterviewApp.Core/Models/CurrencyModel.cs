namespace InterviewApp.Core.Models;

public record CurrencyModel
{
    public string CharCode { get; set; }
    public string Name { get; set; }
    public int Scale { get; set; }
    public decimal CurrentRate { get; set; }
    public decimal OlderRate { get; set; }
}