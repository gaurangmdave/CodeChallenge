using CodeChallenge.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace CodeChallenge.Helpers
{
    /// <summary>
    /// INCREMENTAL ITEMS SOURCE FOR LOADING SEARCHED ITEMS WHEN USER SCROLLS DOWN THE PAGE
    /// </summary>
    public class IncrementalItemsSource : IList, ISupportIncrementalLoading, INotifyCollectionChanged
    {
        #region :: VARIBALES, PROPERTIES ::

        List<object> searchResults = new List<object>();
        
        uint totalPages = 1;
        uint currentPage = 0;

        #endregion

        #region :: CONSTRUCTOR ::

        public IncrementalItemsSource()
        {
        }

        #endregion

        #region :: IList IMPLEMENTATION ::

        public object this[int index]
        {
            get
            {
                return searchResults[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public int Count
        {
            get { return searchResults.Count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region :: ISupportIncrementalLoading IMPLEMENTATION ::

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(c => loadContentAsync());
        }

        private async Task<LoadMoreItemsResult> loadContentAsync()
        {
            var page = await App.WebApiAdapter.GetArticlesAsync(currentPage + 1);

            totalPages = (uint)page.TotalPages;
            currentPage = (uint)page.Page;

            var baseIndex = searchResults.Count;
            var itemsCount = page.Content.Count();

            searchResults.AddRange(page.Content.Select(a => new ArticleViewModel(a)));

            NotifyNewItemsAdded(baseIndex, itemsCount);

            return new LoadMoreItemsResult { Count = (uint)itemsCount };
        }

        public bool HasMoreItems
        {
            get { return currentPage < totalPages; }
        }

        #endregion 

        #region :: INotifyCollectionChanged IMPLEMENTATION ::

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion 

        #region :: FUNCTIONS ::

        void NotifyNewItemsAdded(int baseIndex, int count)
        {
            if (CollectionChanged == null)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                var args = new NotifyCollectionChangedEventArgs(
                  NotifyCollectionChangedAction.Add,
                  searchResults[i + baseIndex],
                  i + baseIndex
                );
                CollectionChanged(this, args);
            }
        }

        #endregion
    }
}
