using Shared.Calculate;
using Shared.Models;

namespace Shared.Helpers
{
    public sealed class Generation
    {
        private readonly string filePath = $"WinFormsApp/Files/Labs.json";
        private readonly JsonWriter jsonWriter = new JsonWriter();
        private readonly Calculation calculation = new Calculation();

        public void GenerateData(double lambda, double p0, double l, double c, double waveguideLength)
        {
            var root = new Root
            {
                Labs_1 = new LabData
                {
                },
                Labs_2 = new LabData
                {
                    WaveLength = calculation.GetValueLambda(lambda),
                    OutputPower = calculation.GetValueP_t(p0, l, lambda, c),
                    WaveguideLength = waveguideLength
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
    }
}