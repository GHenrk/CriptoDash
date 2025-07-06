using CriptoDashTemplate.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CriptoDashTemplate.Services
{
    public class MarketService
    {
        private readonly HttpClient _httpClient;
        private static string COINS_LIST = "coins/list";
        private IEnumerable<CriptoMoeda> CriptoMoedas;
        private string _apiKey = string.Empty;
        public MarketService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            ConfigureHttpClient();
            CriptoMoedas = new List<CriptoMoeda>();
        }

        public MarketService()
        {
            _httpClient = new HttpClient();
            ConfigureHttpClient();
            CriptoMoedas = new List<CriptoMoeda>();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", _apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CriptoData> GetMostValuatesCripto(string outCurrency = "usd", int qntdade = 50, int page = 1)
        {
            if (!CriptoMoedas.Any())
                await CarregaCriptos();
            var response = await _httpClient.GetAsync($"coins/markets?vs_currency={outCurrency}&order=market_cap_desc&per_page={qntdade}&page={page}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<IEnumerable<CriptoMoeda>>();
            if (data != null)
            {
                var criptoData = new CriptoData() { Criptos = data, Quantidade = CriptoMoedas.Count() };
                return criptoData;
            }
            throw new Exception("Failed to deserialize cripto data.");
        }

        public async Task<CriptoData> GetEspecificCriptos(IEnumerable<string> ids, string outCurrency = "usd")
        {
            if (!CriptoMoedas.Any())
                await CarregaCriptos();
            var idsQuery = string.Join(",", ids);
            var response = await _httpClient.GetAsync($"coins/markets?vs_currency={outCurrency}&ids={idsQuery}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<IEnumerable<CriptoMoeda>>();
            if (data != null)
            {
                var criptoData = new CriptoData() { Criptos = data, Quantidade = CriptoMoedas.Count() };
                return criptoData;
            }
            throw new Exception("Failed to deserialize cripto data.");
        }


        public async Task<CriptoMoeda> LoadChartData(string cripto, string outCurrency = "usd", int days = 1)
        {
            if (!CriptoMoedas.Any())
                await CarregaCriptos();
            if (string.IsNullOrEmpty(cripto) || !CriptoMoedas.Any(c => c.id.Equals(cripto, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException("not found!");
            var criptoMoeda = CriptoMoedas.FirstOrDefault(c => c.id.Equals(cripto, StringComparison.OrdinalIgnoreCase));
            if (criptoMoeda == null)
                throw new ArgumentException("Cripto moeda not found in the list.");
            return await LoadChartDataAsync(criptoMoeda, outCurrency, days);
        }

        public async Task<CriptoMoeda> LoadChartDataAsync(CriptoMoeda cripto, string outCurrency, int days, int quantidadeAlteracoes = 10)
        {
            var response = await _httpClient.GetAsync($"coins/{cripto.id}/market_chart?vs_currency={outCurrency}&days={days}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<CriptoHistorico>();
            if (data != null)
            {
               GenerateCriptoHistoricoCharts(cripto.SparkLinePrices, quantidadeAlteracoes, data.prices);
               GenerateCriptoHistoricoCharts(cripto.SparkLineVolume, quantidadeAlteracoes, data.total_volumes);
               GenerateCriptoHistoricoCharts(cripto.SparklineMarketCap, quantidadeAlteracoes, data.market_caps);
                return cripto;
            }
            throw new Exception("Failed to deserialize cripto data.");
        }

        private static void GenerateCriptoHistoricoCharts(List<double> chartData, int quantidadeAlteracoes, List<List<double>> apiData)
        {
            if (chartData == null)
                chartData = new List<double>();
            chartData.Clear();

            var totalPontos = apiData.Count;

            if (totalPontos == 0)
                return;

            // Quantidade de variacoes x quantidade que queremos manter
            var intervalo = Math.Max(1, totalPontos / quantidadeAlteracoes);

            // Faz o downsampling mantendo a ordem cronológica
            var sparkline = apiData
                .Where((ponto, index) => index % intervalo == 0)
                .Select(p => p[1]) // pega só o valor do preço
                .Take(quantidadeAlteracoes); // garante que não passe do limite

            chartData.AddRange(sparkline);
        }

        public async Task CarregaCriptos()
        {
            var response = await _httpClient.GetAsync(COINS_LIST);
            response.EnsureSuccessStatusCode();
            CriptoMoedas = await response.Content.ReadFromJsonAsync<IEnumerable<CriptoMoeda>>();
        }


    }
}
