namespace Shared.Models
{
    public sealed class LabData
    {
        // длина волны в микрометрах
        public double WaveLength { get; set; }

        // выходная мощность 
        public double OutputPower { get; set; }

        // длина волновода в метрах
        public double WaveguideLength { get; set; }
    }
}