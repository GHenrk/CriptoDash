using Blazored.LocalStorage;

namespace CriptoDashTemplate.Services
{
    public class ClientStorage
    {
        private List<string> _favoriteCurrencies = new List<string>();
        ILocalStorageService localStorageService;

        public ClientStorage(ILocalStorageService localStorage)
        {
            localStorageService = localStorage; 
            
        }



        public void LoadFavoritesFromLocalStorage()
        {
            var favorites = localStorageService.GetItemAsync<List<string>>("favoriteCurrencies").Result;
            if (favorites != null)
            {
                _favoriteCurrencies = favorites;
            }
        }


        public void AddFavoriteCurrency(string currencyId)
        {
            if (!_favoriteCurrencies.Contains(currencyId))
            {
                _favoriteCurrencies.Add(currencyId);
                localStorageService.SetItemAsync("favoriteCurrencies", _favoriteCurrencies);
            }
        }

        public void RemoveFavoriteCurrency(string currencyId)
        {
            if (_favoriteCurrencies.Contains(currencyId))
            {
                _favoriteCurrencies.Remove(currencyId);
                localStorageService.SetItemAsync("favoriteCurrencies", _favoriteCurrencies);
            }
        }
        public IEnumerable<string> GetFavoriteCurrencies()
        {
            return this._favoriteCurrencies;
        }


        
    }
}
