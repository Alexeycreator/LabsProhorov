using Shared.Calculate;
using Shared.Models;

namespace Shared.Helpers
{
    public sealed class Generation
    {
        private readonly string filePath = $"WinFormsApp/Files/Labs.json";
        private readonly JsonWriter jsonWriter = new JsonWriter();
        private readonly Calculation calculation = new Calculation();
        
        public void GenerateData(double lambda, double p0, double alpha, double l, double waveguideLength)
        {
            var root = new Root
            {
                Labs_1 = new LabData
                {
                },
                Labs_2 = new LabData
                {
                    WaveLength = calculation.GetValueLambda(lambda),        
                    OutputPower = calculation.GetValueP_t(p0, alpha, l),       
                    WaveguideLength = waveguideLength
                }
            };
            
            jsonWriter.Write(filePath, root);
        }
    }
}