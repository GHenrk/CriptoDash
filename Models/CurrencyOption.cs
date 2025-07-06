namespace CriptoDashTemplate.Models
{
    public class CurrencyOption
    {
        public string Code { get; set; }            // CoinGecko: "usd"
        public string DisplayCode { get; set; }     // Visual: "USD"
        public string Name { get; set; }            // Ex: "Dólar Americano"
        public string CssFlagClass { get; set; }// Ex: "fi-us"

        public CurrencyOption(string code, string displayCode, string name, string cssFlagClass)
        {
            Code = code;
            DisplayCode = displayCode;
            Name = name;
            CssFlagClass = cssFlagClass;
        }




    }
}
