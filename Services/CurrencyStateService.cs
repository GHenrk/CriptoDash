using System.Globalization;

namespace CriptoDashTemplate.Services
{
    public class CurrencyStateService
    {
        private string _selectedCurrency = "usd";
        public string SelectedCurrency => _selectedCurrency;
        private CultureInfo _currenctCulture = new CultureInfo("en-US");
        public CultureInfo CurrentCulture
        {
            get => _currenctCulture;
            set
            {
                if (_currenctCulture != value)
                {
                    _currenctCulture = value;
                }
            }
        }

        public Action<string>? OnCurrencyStateChanged { get; set; }
        public void SetCurrency(string currency)
        {
            if (_selectedCurrency != currency.ToLower())
            {
                _selectedCurrency = currency.ToLower();
                _currenctCulture = CurrencyService.GetCultureFor(_selectedCurrency);
                OnCurrencyStateChanged?.Invoke(this.SelectedCurrency);
            }
        }
    }
}
