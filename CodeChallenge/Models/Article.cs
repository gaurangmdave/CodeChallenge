using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public partial class SearchResult<T>
    {
        [JsonProperty("content")]
        public IEnumerable<T> Content { get; set; }

        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
    }

    public partial class Article
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("modelId")]
        public string ModelId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("season")]
        public string Season { get; set; }

        [JsonProperty("seasonYear")]
        public string SeasonYear { get; set; }

        [JsonProperty("activationDate")]
        public System.DateTime ActivationDate { get; set; }

        [JsonProperty("additionalInfos")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<string> AdditionalInfos { get; set; } = new ObservableCollection<string>();

        [JsonProperty("genders")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<string> Genders { get; set; } = new ObservableCollection<string>();

        [JsonProperty("ageGroups ")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<string> AgeGroups { get; set; } = new ObservableCollection<string>();

        [JsonProperty("brand")]
        [System.ComponentModel.DataAnnotations.Required]
        public Brand Brand { get; set; } = new Brand();

        [JsonProperty("categoryKeys")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<string> CategoryKeys { get; set; } = new ObservableCollection<string>();

        [JsonProperty("attributes")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<ArticleAttribute> Attributes { get; set; } = new ObservableCollection<ArticleAttribute>();

        [JsonProperty("units")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<ArticleUnit> Units { get; set; } = new ObservableCollection<ArticleUnit>();

        [JsonProperty("tags", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public ObservableCollection<string> Tags { get; set; }

        [JsonProperty("media")]
        [System.ComponentModel.DataAnnotations.Required]
        public ArticleMedia Media { get; set; } = new ArticleMedia();
       
    }

    public partial class Brand
    {
        /// <summary>The unique key for a brand</summary>
        [JsonProperty("key")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Key { get; set; }

        /// <summary>Name of the brand</summary>
        [JsonProperty("name")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        /// <summary>The url of the brand within the Zalando web shop</summary>
        [JsonProperty("shopUrl")]
        [System.ComponentModel.DataAnnotations.Required]
        public string ShopUrl { get; set; }

        /// <summary>The url of the brand logo within the Zalando web shop</summary>
        [JsonProperty("logoUrl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LogoUrl { get; set; }

        /// <summary>The url of the large brand logo within the Zalando web shop</summary>
        [JsonProperty("logoLargeUrl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LogoLargeUrl { get; set; }

        [JsonProperty("brandFamily", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public BrandFamily BrandFamily { get; set; }
    }

    public partial class ArticleAttribute
    {
        [JsonProperty("name")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [JsonProperty("values")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<string> Values { get; set; } = new ObservableCollection<string>();
    }

    public partial class ArticleImage
    {
        [JsonProperty("orderNumber")]
        public int OrderNumber { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("thumbnailHdUrl")]
        public string ThumbnailHdUrl { get; set; }

        [JsonProperty("smallUrl")]
        public string SmallUrl { get; set; }

        [JsonProperty("smallHdUrl")]
        public string SmallHdUrl { get; set; }

        [JsonProperty("mediumUrl")]
        public string MediumUrl { get; set; }

        [JsonProperty("mediumHdUrl")]
        public string MediumHdUrl { get; set; }

        [JsonProperty("largeUrl")]
        public string LargeUrl { get; set; }

        [JsonProperty("largeHdUrl")]
        public string LargeHdUrl { get; set; }
    }

    public partial class ArticleMedia
    {
        [JsonProperty("images")]
        [System.ComponentModel.DataAnnotations.Required]
        public ObservableCollection<ArticleImage> Images { get; set; } = new ObservableCollection<ArticleImage>();
    }

    public partial class ArticlePrice
    {
        [JsonProperty("currency")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Currency { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("formatted")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Formatted { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ArticlePrice FromJson(string data)
        {
            return JsonConvert.DeserializeObject<ArticlePrice>(data);
        }
    }

    public partial class ArticleUnit
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("price")]
        public ArticlePrice Price { get; set; } = new ArticlePrice();

        [JsonProperty("originalPrice")]
        public ArticlePrice OriginalPrice { get; set; } = new ArticlePrice();

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("stock")]
        public int Stock { get; set; }

        public string PartnerId { get; set; }

    }

    public partial class BrandFamily
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shopUrl")]
        public string ShopUrl { get; set; }
    }
}
