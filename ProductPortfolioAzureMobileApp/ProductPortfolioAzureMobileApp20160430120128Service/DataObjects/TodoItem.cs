using Microsoft.Azure.Mobile.Server;

namespace ProductPortfolioAzureMobileApp20160430120128Service.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}