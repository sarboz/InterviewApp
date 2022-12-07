using System;

namespace InterviewApp.Data.Entities
{
    public sealed class Currency : BaseEntity
    {
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public string Name { get; set; }
        public int Scale { get; set; }
        public decimal Rate { get; set; }
        public bool IsHide { get; set; }
        public Guid DailyRateId { get; set; }
        public DailyRate DailyRate { get; set; }
    }
}