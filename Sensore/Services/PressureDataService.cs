using Sensore.Models;
using System.Globalization;

namespace Sensore.Services
{
    public class PressureDataService
    {
        private readonly string _folderPath;

        public PressureDataService()
        {
            _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
        }

        public List<PressureMatrix> LoadAllFrames()
        {
            var frames = new List<PressureMatrix>();

            foreach (var file in Directory.GetFiles(_folderPath, "*.csv"))
            {
                var frame = ReadMatrixFromCsv(file);
                frames.Add(frame);
            }

            return frames.OrderBy(f => f.Timestamp).ToList();
        }

        private PressureMatrix ReadMatrixFromCsv(string path)
        {
            var matrix = new int[32, 32];
            var lines = File.ReadAllLines(path);

            for (int i = 0; i < 32 && i < lines.Length; i++)
            {
                var values = lines[i].Split(',').Select(int.Parse).ToArray();
                for (int j = 0; j < 32 && j < values.Length; j++)
                {
                    matrix[i, j] = values[j];
                }
            }

            // --- Compute metrics ---
            var flattened = matrix.Cast<int>().ToArray();
            int lowerThreshold = 10; // pixels under this are considered zero-contact
            int upperThreshold = 255;

            var validPixels = flattened.Where(p => p >= lowerThreshold).ToList();
            double totalPixels = 32 * 32;

            double peakPressure = validPixels.Any() ? validPixels.Max() : 0;
            double avgPressure = validPixels.Any() ? validPixels.Average() : 0;
            double contactAreaPercent = (validPixels.Count / totalPixels) * 100;

            // Simple "distribution score" – you can tune this formula later
            double distributionScore = Math.Max(0, 100 - (peakPressure / 2));

            return new PressureMatrix
            {
                Matrix = matrix,
                Timestamp = File.GetCreationTime(path),
                PeakPressureIndex = peakPressure,
                AveragePressure = avgPressure,
                ContactAreaPercent = contactAreaPercent,
                DistributionScore = distributionScore
            };
        }

        private DateTime ExtractTimestampFromFilename(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            if (DateTime.TryParse(fileName.Replace("frame_", ""), out var time))
                return time;

            return DateTime.Now;
        }
    }
}