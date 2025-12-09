using System;
using System.Collections.Generic;

namespace Sensore.Models
{
    public class SystemAlertViewModel
    {
        public int CriticalCount { get; set; }
        public int WarningCount { get; set; }
        public int NewCount { get; set; }
        public int AcknowledgedCount { get; set; }
        public List<AlertItem> Alerts { get; set; } = new();
    }

    public class AlertItem
    {
        public string Id { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public int PressureReading { get; set; }
        public DateTime DetectedAt { get; set; }
        public TimeSpan Duration { get; set; }
        public string RiskLevel { get; set; } = "HIGH RISK";
        public bool IsNew { get; set; }
        public string[] RecommendedActions { get; set; } = Array.Empty<string>();
    }
}
