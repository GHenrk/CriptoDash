using CriptoDashTemplate.Models;
using System.Globalization;

namespace CriptoDashTemplate.Services
{
    public class CurrencyService
    {
          private static readonly Dictionary<string, string> _map = new()
        {
            { "brl", "pt-BR" },
            { "usd", "en-US" },
            { "eur", "fr-FR" },  // França para símbolo €
            { "rub", "ru-RU" },
            { "cny", "zh-CN" },
            { "vnd", "vi-VN" },
            { "gbp", "en-GB" },
            { "jpy", "ja-JP" },
            { "aud", "en-AU" },
            { "cad", "en-CA" },
            { "inr", "hi-IN" },
            { "ars", "es-AR" },
            { "chf", "de-CH" }
        };

        public static CultureInfo GetCultureFor(string currencyCode)
        {
            return new CultureInfo(_map.TryGetValue(currencyCode.ToLower(), out var culture)
                ? culture
                : "en-US");
        }
        public static List<CurrencyOption> GetAvaibleCurrencies()
        {
            return new()
                    {
                        // Populares
                        new("rub", "RUB", "Rublo Russo", "fi-ru"),
                        new("brl", "BRL", "Real Brasileiro", "fi-br"),
                        new("usd", "USD", "Dólar Americano", "fi-us"),
                        new("eur", "EUR", "Euro", "fi-eu"),
                        new("cny", "CNY", "Yuan Chinês", "fi-cn"),
                        new("vnd", "VND", "Dong Vietnamita", "fi-vn"),
                        new("gbp", "GBP", "Libra Esterlina", "fi-gb"),
                        new("jpy", "JPY", "Iene Japonês", "fi-jp"),
                        new("aud", "AUD", "Dólar Australiano", "fi-au"),
                        new("cad", "CAD", "Dólar Canadense", "fi-ca"),
                        new("inr", "INR", "Rúpia Indiana", "fi-in"),
                        new("ars", "ARS", "Peso Argentino", "fi-ar"),
                        new("chf", "CHF", "Franco Suíço", "fi-ch"),

                        //// Criptomoedas
                        //new("btc", "BTC", "Bitcoin", ""),
                        //new("eth", "ETH", "Ethereum", ""),
                        //new("bnb", "BNB", "Binance Coin", ""),
                        //new("xrp", "XRP", "Ripple", ""),
                        //new("ltc", "LTC", "Litecoin", ""),

                    };

        }
    }
}
