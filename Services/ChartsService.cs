using CriptoDashTemplate.Models;
using MudBlazor;

namespace CriptoDashTemplate.Services
{
    public class ChartsService
    {

        public static ChartOptions CriaCharsetOptions()
        {
            return new ChartOptions()
            {
                ShowLegend = false,
                ShowLabels = false,
                ShowLegendLabels = false,
                ShowToolTips = false
                // ValueFormatString = "C2"
            };
        }

        public static List<ChartSeries> CriaChartSeries(CriptoMoeda cripto, List<double> data)
        {
            if (cripto == null)
                throw new ArgumentNullException(nameof(cripto), "CriptoMoeda cannot be null.");
            if (data == null || !data.Any())
                return new List<ChartSeries> { new() { Name = cripto.name, Data = Array.Empty<double>() } };
            return new List<ChartSeries> { new() { Name = cripto.name, Data = data.Select(d => Double.Round(d, 2)).ToArray() } };
        }
    }
}
