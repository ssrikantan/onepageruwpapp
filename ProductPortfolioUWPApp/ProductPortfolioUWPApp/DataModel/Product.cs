using Newtonsoft.Json;
using ProductPortfolioUWPApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioUWPApp.DataModel
{
    public class Product
    {
        public string Id { get; set; }

       [JsonProperty(PropertyName = "ProductDescription")]
        public string ProductDescription { get; set; }

        [JsonProperty(PropertyName = "ProductName")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "CurrentVersion")]
        public string CurrentVersion { get; set; }

        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }

        //Validation attributes
        [JsonProperty(PropertyName = "LifecycleRelevant")]
        public bool LifecycleRelevant { get; set; }

        [JsonProperty(PropertyName = "HasPreviewVersion")]
        public bool HasPreviewVersion { get; set; }


        //All URLs that are associated with it
        [JsonProperty(PropertyName = "BlogFeedUrl")]
        public string BlogFeedUrl { get; set; }

        [JsonProperty(PropertyName = "ProductHomePageUrl")]
        public string ProductHomePageUrl { get; set; }

        [JsonProperty(PropertyName = "MsdnUrl")]
        public string MsdnUrl { get; set; }

        [JsonProperty(PropertyName = "TechnetUrl")]
        public string TechnetUrl { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

      

        public string ImageUrl_Full
        {
            get
            {
                string _productName = ProductName;
          //      _productName = _productName.Replace(".", "");
                _productName = _productName.Replace(" ", "%20");
                return Constants.IMAGE_PRODUCTS_URL_PREFIX + _productName + "Wide.png";
            }
        }
    }
}
