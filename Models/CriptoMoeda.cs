namespace CriptoDashTemplate.Models
{
    public class CriptoMoeda
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public decimal? current_price { get; set; }
        public decimal? market_cap { get; set; }
        public decimal? market_cap_rank { get; set; }
        public decimal? fully_diluted_valuation { get; set; }
        public decimal? total_volume { get; set; }
        public decimal?  high_24h { get; set; }
        public decimal? low_24h { get; set; }
        public decimal? price_change_24h { get; set; }
        public decimal? price_change_percentage_24h { get; set; }
        public decimal? market_cap_change_24h { get; set; }
        public decimal? market_cap_change_percentage_24h { get; set; }
        public decimal? circulating_supply { get; set; }
        public decimal? total_supply { get; set; }
        public decimal? max_supply { get; set; }
        public decimal? ath { get; set; }
        public decimal? ath_change_percentage { get; set; }
        public DateTime? ath_date { get; set; }
        public decimal? atl { get; set; }
        public decimal? atl_change_percentage { get; set; }
        public DateTime? atl_date { get; set; }
        public object? roi { get; set; }
        public DateTime? last_updated { get; set; }
        public bool? UserFavorite { get; set; } = false;

        public List<double> SparkLinePrices { get; set; } = new();
        public string[] SparkLinePriceLabels => Enumerable.Range(1, SparkLinePrices.Count).Select(i => i.ToString()).ToArray();
        public List<double> SparkLineVolume { get; internal set; } = new();
        public string[] SparkLineVolumeLabels => Enumerable.Range(1, SparkLineVolume.Count).Select(i => i.ToString()).ToArray();
        public List<double> SparklineMarketCap { get; internal set; } = new();
        public string[] SparkLineMarketCapLabels => Enumerable.Range(1, SparklineMarketCap.Count).Select(i => i.ToString()).ToArray();

    }

}
