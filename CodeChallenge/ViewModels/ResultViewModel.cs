using CodeChallenge.Helpers;

namespace CodeChallenge.ViewModels
{
    public class ResultViewModel
    {
        #region :: VARIBALES, PROPERTIES ::

        public IncrementalItemsSource DataSource { get; set; }

        #endregion

        #region :: CONSTRUCTOR ::

        public ResultViewModel()
        {
            DataSource = new IncrementalItemsSource();
        }

        #endregion

    }
}
