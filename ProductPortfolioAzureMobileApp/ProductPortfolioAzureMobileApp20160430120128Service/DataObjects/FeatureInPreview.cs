using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioAzureMobileApp20160430120128Service.DataObjects
{
    public class FeatureInPreview:EntityData
    {
        public string FeatureName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string FeatureDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime FeatureUpdateDate { get; set; }
        public string FeatureReferenceUrl { get; set; }
    }
}
