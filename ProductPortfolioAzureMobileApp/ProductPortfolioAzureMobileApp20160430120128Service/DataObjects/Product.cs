using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioAzureMobileApp20160430120128Service.DataObjects
{
    public class Product:EntityData
    {
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public string CurrentVersion { get; set; }
        public string CategoryName { get; set; }
        //Validation attributes
        public bool LifecycleRelevant { get; set; }
        public bool HasPreviewVersion { get; set; }
        //All URLs that are associated with it
        public string BlogFeedUrl { get; set; }
        public string ProductHomePageUrl { get; set; }
        public string MsdnUrl { get; set; }
        public string TechnetUrl { get; set; }
    }
}
