using Newtonsoft.Json;
using ProductPortfolioUWPApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioUWPApp.DataModel
{
    public class Feature
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "FeatureName")]
        public string FeatureName { get; set; }

        [JsonProperty(PropertyName = "ProductId")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "ProductName")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "FeatureDescription")]
        public string FeatureDescription { get; set; }

        [JsonProperty(PropertyName = "ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "FeatureUpdateDate")]
        public DateTime FeatureUpdateDate { get; set; }

        [JsonProperty(PropertyName = "featureReferenceUrl")]
        public string featureReferenceUrl { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        public string ImageUrl_Full
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                    return null;
                else
                    return Constants.IMAGE_FEATURES_URL_PREFIX + ImageUrl + ".png"; 
            }
        }
    }
}
