using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public partial class Facet
    {
        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("facets")]
        public ObservableCollection<FacetDetail> Facets { get; set; } = new ObservableCollection<FacetDetail>();
    }

    public partial class FacetDetail
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
