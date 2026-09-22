using System.IO;
using Shared.Calculate;
using Shared.Models;

namespace Shared.Helpers
{
    public sealed class Generation
    {
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Labs.json");
        private readonly JsonWriter jsonWriter = new();

        public void GenerateData(double p0)
        {
            EnsureFileExists();
            var root = new Root
            {
                Labs_1 = new LabData
                {
                },
                Labs_2 = new LabData
                {
                    WaveRange = [1, 2],
                    WaveguideLengthRange = [100, 1000],
                    InputPower = p0
                },
                Labs_3 = new LabData
                {
                },
                Labs_4 = new LabData
                {
                },
                Labs_5 = new LabData
                {
                },
                Labs_6 = new LabData
                {
                }
            };

            jsonWriter.Write(filePath, root);
        }

        private void EnsureFileExists()
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(filePath))
                using (File.Create(filePath))
                    ;
        }
    }
}