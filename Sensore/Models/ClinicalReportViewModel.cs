namespace Sensore.Models
{
    public class ClinicalReportViewModel
    {
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public int Age { get; set; }
        public string Room { get; set; }
        public string RiskLevel { get; set; }
        public int ActiveAlerts { get; set; }
        public string LastReading { get; set; }
        public string ReportPeriod { get; set; }
        public double PeakPressure { get; set; }
        public double ContactArea { get; set; }
        public double PPI { get; set; }
        public int PostureChanges { get; set; }
    }
}
