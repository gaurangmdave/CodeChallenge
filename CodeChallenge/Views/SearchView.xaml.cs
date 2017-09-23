using CodeChallenge.Helpers;
using CodeChallenge.Models;
using CodeChallenge.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CodeChallenge.Views
{
    public sealed partial class SearchView : Page
    {
        #region :: VARIABLES, PROPERTIES ::

        SearchViewModel searchViewModel { get; set; }

        #endregion

        #region :: CONSTRUCTOR ::

        public SearchView()
        {
            this.InitializeComponent();

            searchViewModel = new SearchViewModel();
        }

        #endregion

        #region :: OVERRIDES ::

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            changeButtonContent(Constants.Genders.Female);
            await searchViewModel.SetGenderToFemale();
        }

        #endregion

        #region :: EVENTS ::

        private async void GenderButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;
                string gender = btn.Tag.ToString();

                switch (gender)
                {
                    case "Male":
                        changeButtonContent(Constants.Genders.Male);
                        await searchViewModel.SetGenderToMale();
                        break;
                    case "Female":
                        changeButtonContent(Constants.Genders.Female);
                        await searchViewModel.SetGenderToFemale();
                        break;
                }

                txtSearchArticle.Text = string.Empty;
                txtSearchArticle.Focus(FocusState.Programmatic);
            }
        }

        private void txtSearchArticle_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestions = searchViewModel.GetSuggestions(sender.Text);
                if (suggestions.Count > 0)
                {
                    sender.ItemsSource = suggestions;
                }
                else
                {
                    sender.ItemsSource = new string[] { "No results found." };
                }
            }
        }

        private void txtSearchArticle_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null && args.ChosenSuggestion is FacetDetail)
            {
                //User selected an item, take an action
                var facetValue = args.ChosenSuggestion as FacetDetail;
                openToSearchResultView(facetValue.DisplayName);
            }
            else
            {
                openToSearchResultView(args.QueryText);
            }
        }

        private void txtSearchArticle_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is FacetDetail)
            {
                var facet = args.SelectedItem as FacetDetail;
                sender.Text = facet.DisplayName;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            openToSearchResultView(txtSearchArticle.Text);
        }

        #endregion

        #region :: FUNCTIONS ::

        private void openToSearchResultView(string query)
        {
            searchViewModel.SetQueryText(query);
            Frame.Navigate(typeof(ResultView));
        }

        private void changeButtonContent(Constants.Genders gender)
        {
            switch (gender)
            {
                case Constants.Genders.Male:
                    btnMen.Content = "\u2713" + " MEN";
                    btnWomen.Content = "WOMEN";
                    break;
                case Constants.Genders.Female:
                    btnWomen.Content = "\u2713" + " WOMEN";
                    btnMen.Content = "MEN";
                    break;
            }
        }

        #endregion
    }
}
