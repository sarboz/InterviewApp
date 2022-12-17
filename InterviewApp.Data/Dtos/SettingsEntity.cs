using InterviewApp.Data.Entities;

namespace InterviewApp.Data.Dtos;

public class SettingsEntity:BaseEntity
{
    public string CurrencyCharCode { get; set; }
    public bool IsHide { get; set; }
}