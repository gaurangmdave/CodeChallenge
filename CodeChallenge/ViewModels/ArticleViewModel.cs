using CodeChallenge.Models;
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace CodeChallenge.ViewModels
{
    public class ArticleViewModel : BaseNotifyableViewModel<Article>
    {
        #region :: VARIBALES, PROPERTIES ::

        public string Name { get { return This.Name; } }

        public string Color { get { return This.Color; } }

        public string BrandName { get { return This.Brand.Name; } }

        public string Price { get { return This.Units[0].Price.Formatted; } }

        public string Size { get { return string.Format("Size: {0}", This.Units[0].Size); } }

        private BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                if (image == null)
                {

                    image = new BitmapImage(new Uri(This.Media.Images[0].LargeHdUrl));
                }
                return image;
            }
        }

        #endregion

        #region :: CONSTRUCTOR ::

        public ArticleViewModel(Article article) : base(article)
        {
        } 

        #endregion
    }
}
