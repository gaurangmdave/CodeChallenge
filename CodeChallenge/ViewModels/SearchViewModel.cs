using CodeChallenge.Helpers;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.ViewModels
{
    public class SearchViewModel : BaseNotifyableViewModel
    {
        #region :: VARIBALES, PROPERTIES ::

        private IEnumerable<Facet> facets;
        public IEnumerable<Facet> Facets
        {
            get
            {
                return facets ?? new List<Facet>();
            }
            set
            {
                facets = value;

                RaisePropertyChanged("Facets");
            }
        }

        private bool isMaleEnabled;
        public bool IsMaleEnabled
        {
            get
            {
                return isMaleEnabled;
            }
            set
            {
                SetProperty(ref isMaleEnabled, value);
            }
        }

        private bool isFemaleEnabled;
        public bool IsFemaleEnabled
        {
            get
            {
                return isFemaleEnabled;
            }
            set
            {
                SetProperty(ref isFemaleEnabled, value);
            }
        }
        #endregion 

        #region :: CONSTRUCTOR ::

        public SearchViewModel()
        {
        }

        #endregion

        #region :: FUNCTIONS ::

        public async Task LoadFacetsForSuggestion()
        {
            Facets = await App.WebApiAdapter.GetFacetsAsync();
        }

        public async Task SetGenderToMale()
        {
            IsMaleEnabled = false;
            IsFemaleEnabled = true;
            App.WebApiAdapter.SetGenderToSearchQuery(Constants.Genders.Male);
            await LoadFacetsForSuggestion();
        }

        public async Task SetGenderToFemale()
        {
            IsMaleEnabled = true;
            IsFemaleEnabled = false;
            App.WebApiAdapter.SetGenderToSearchQuery(Constants.Genders.Female);
            await LoadFacetsForSuggestion();
        }

        public void SetQueryText(string query)
        {
            App.WebApiAdapter.SetFullTextToSearchQuery(query);
        }

        public List<FacetDetail> GetSuggestions(string query)
        {
            var suggestions = new List<FacetDetail>();

            foreach (var facet in Facets)
            {
                var matchingItems = facet.Facets.Where(p => p.DisplayName.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) >= 0);
                foreach (var item in matchingItems)
                {
                    suggestions.Add(item);
                }
            }

            return suggestions
              .OrderByDescending(i => i.DisplayName.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
              .ThenBy(i => i.DisplayName)
              .ToList();
        }

        #endregion Methods
    }
}
