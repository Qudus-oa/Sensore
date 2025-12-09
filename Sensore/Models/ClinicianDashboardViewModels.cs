using System;
using System.Collections.Generic;

namespace Sensore.Models
{
    public class DashboardViewModel
    {
        public int CriticalAlertCount { get; set; }
        public int HighRiskPatientsCount { get; set; }
        public int NewAlertsCount { get; set; }

        public List<AlertViewModel> RecentCriticalAlerts { get; set; } = new();
        public List<PatientRiskViewModel> HighRiskPatients { get; set; } = new();
    }

    public class AlertViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Zone { get; set; } = string.Empty;
        public int HighestPressure { get; set; }
        public DateTime DetectedAt { get; set; }
        public TimeSpan Duration { get; set; }
        public string SeverityTag { get; set; } = "HIGH RISK";
    }

    public class PatientRiskViewModel
    {
        public string PatientId { get; set; } = string.Empty;
        public string Patient { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = string.Empty;
        public int HighestPressure { get; set; }
        public int ActiveAlerts { get; set; }
    }
}
