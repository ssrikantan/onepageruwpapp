using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioAzureMobileApp20160430120128Service.DataObjects
{
    public class ProductInPreview:EntityData
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CurrentVersion { get; set; }
        public string ProductDescription { get; set; }
        public string ProductHomePageUrl { get; set; }
        public DateTime AnnouncementDate { get; set; }
    }
}
