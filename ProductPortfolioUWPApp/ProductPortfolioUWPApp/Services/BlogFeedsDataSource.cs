using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductPortfolioUWPApp.Common;
using System.Collections.ObjectModel;
using ProductPortfolioUWPApp.DataModel;
using Windows.Web.Syndication;
using ProductPortfolioUWPApp.Util;

namespace ProductPortfolioUWPApp.Services
{
    public class FeedDataSource
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        private ObservableCollection<FeedItem> _Feeds = new ObservableCollection<FeedItem>();
        public ObservableCollection<FeedItem> Feeds
        {
            get
            {
                return this._Feeds;
            }
        }

        public async Task GetFeedsAsync(string feedUrl)
        {
            await GetFeedAsync(feedUrl);
        }

        private async Task GetFeedAsync(string feedUriString)
        {
            Windows.Web.Syndication.SyndicationClient client = new SyndicationClient();

            try
            {
                Uri feedUri = new Uri(feedUriString);

                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // This code is executed after RetrieveFeedAsync returns the SyndicationFeed.
                // Process the feed and copy the data we want into our FeedData and FeedItem classes.
                FeedData feedData = new FeedData();

                feedData.Title = feed.Title.Text;
                if (feed.Subtitle != null && feed.Subtitle.Text != null)
                {
                    feedData.Description = feed.Subtitle.Text;
                }
                // Use the date of the latest post as the last updated date.
                feedData.PubDate = feed.Items[0].PublishedDate.DateTime;

                foreach (SyndicationItem item in feed.Items)
                {
                    FeedItem feedItem = new FeedItem();
                    feedItem.Title = item.Title.Text.Trim();
                    feedItem.PubDate = item.PublishedDate.DateTime;
                    string _authorName = item.Authors[0].Name;
                    if (string.IsNullOrEmpty(item.Authors[0].Name))
                    {
                        _authorName = item.Authors[0].NodeValue;
                    }

                    feedItem.Author = _authorName;
                    // Handle the differences between RSS and Atom feeds.
                    if (feed.SourceFormat == SyndicationFormat.Atom10)
                    {
                        feedItem.Content = item.Content.Text;
                    }
                    else if (feed.SourceFormat == SyndicationFormat.Rss20)
                    {
                        feedItem.Content = item.Summary.Text.Trim();
                    }
                    feedItem.Link = item.Links[0].Uri;
                    this.Feeds.Add(feedItem);
                    //feedData.Items.Add(feedItem);
                }
            }
            catch (Exception ex)
            {
                if (!ApplicationStateParams.isInternetAvailable)
                {
                    _errorMessage = "Error retrieving blog feeds, check your internet connection";
                }
                else
                {
                    _errorMessage = "Error retrieving blog feeds";
                }
            }
        }

        // Returns the feed that has the specified title.
        public FeedItem GetFeed(string title)
        {
            // Simple linear search is acceptable for small data sets
            var matches = this.Feeds.Where((feed) => feed.Title.Equals(title));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        // Returns the post that has the specified title.
        //public FeedItem GetItem(string uniqueId)
        //{
        //    // Simple linear search is acceptable for small data sets
        //    var matches = this.Feeds.Where((feed) => feed..Equals(title));
        //    if (matches.Count() == 1) return matches.First();
        //    return null;
        //}


    }
}
