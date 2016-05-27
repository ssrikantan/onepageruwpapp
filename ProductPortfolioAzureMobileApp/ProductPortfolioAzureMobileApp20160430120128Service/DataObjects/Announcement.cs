using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Mobile.Server;

namespace ProductPortfolioAzureMobileApp20160430120128Service.DataObjects
{
    public class Announcement : EntityData
    {
       
            public string Subject { get; set; }
            public string ImageUrl { get; set; }
            public DateTime AnnouncementDate { get; set; }
            public string BlogUrl { get; set; }
            public string Description { get; set; }
        
    }
}
