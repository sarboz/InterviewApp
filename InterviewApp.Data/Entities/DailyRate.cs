using System;
using System.Collections.Generic;

namespace InterviewApp.Data.Entities;

public class DailyRate : BaseEntity
{
    public List<Currency> Currency { get; set; }
    public DateTime Date { get; set; }
}