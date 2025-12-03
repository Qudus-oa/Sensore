namespace Sensore.Models
{
    public class PressureMatrix
    {
        public DateTime Timestamp { get; set; }
        public int[,] Matrix { get; set; } = new int[32, 32];

        // Computed metrics — values are assigned in PressureDataService
        public double PeakPressureIndex { get; set; }
        public double ContactAreaPercent { get; set; }
        public double AveragePressure { get; set; }
        public double DistributionScore { get; set; }
    }
}