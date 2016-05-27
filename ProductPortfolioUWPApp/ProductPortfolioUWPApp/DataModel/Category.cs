using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPortfolioUWPApp.DataModel
{
    public class Category
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
        
    }
}
