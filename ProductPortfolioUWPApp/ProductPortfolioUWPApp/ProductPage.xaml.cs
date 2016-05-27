using Microsoft.WindowsAzure.MobileServices;
using ProductPortfolioUWPApp.DataModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using ProductPortfolioUWPApp.Services;
using ProductPortfolioUWPApp.Util;
using Windows.Phone.UI.Input;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductPortfolioUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
   
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class ProductPage : Page
        {
            private MobileServiceCollection<Product, Product> products;
            private IMobileServiceTable<Product> productTable = App.MobileService.GetTable<Product>();

            private MobileServiceCollection<ProductInPreview, ProductInPreview> productsInPreview;
            private IMobileServiceTable<ProductInPreview> productsInPreviewTable = App.MobileService.GetTable<ProductInPreview>();

            private MobileServiceCollection<Feature, Feature> features;
            private IMobileServiceTable<Feature> featureTable = App.MobileService.GetTable<Feature>();

            private MobileServiceCollection<FeatureInPreview, FeatureInPreview> featuresInPreview;
            private IMobileServiceTable<FeatureInPreview> featuresInPreviewTable = App.MobileService.GetTable<FeatureInPreview>();

            private FeedDataSource blogsDs = new FeedDataSource();

            private string CurrentProductId = string.Empty;
            private Product CurrentProduct;

            private ProductTypeFlag flag;
            public ProductPage()
            {
                this.InitializeComponent();
                App.NetworkStatusChanged += new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);

            }

            protected async override void OnNavigatedTo(NavigationEventArgs e)
            {

                //Show UI back button - do it on each page navigation
                if (Frame.CanGoBack)
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                else
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

                string parameter = e.Parameter.ToString();
                string[] vals = parameter.Split(new char[] { '|' });
                CurrentProductId = vals[0];
                if (vals.Count() <= 1)
                {
                    flag = ProductTypeFlag.Product;
                }
                else
                {
                    flag = ProductTypeFlag.ProductInPreview;
                }
                await RefreshData();
            }

            private async void NetworkChangedAction(object sender, PortfolioAppNetworkStatusDetector e)
            {
                //When Service connectiviy had gone offline earlier and its back now ..
                if (ApplicationStateParams.isInternetAvailable)
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => { await RefreshData(); });
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => { SetPageConnectivityOptions(true); });
                }
                else
                {
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => { SetPageConnectivityOptions(false); });
                }
            }

            private void SetPageConnectivityOptions(bool flag)
            {
                if (flag)
                {
                    MainRelativePanel.Opacity = 1;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Collapsed;
                this.ErrorTextBlock.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainRelativePanel.Opacity = 0;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Visible;
                    this.ErrorTextBlock.Visibility = Visibility.Visible;
                }
            }

            private async Task RefreshData()
            {
                this.ProgressRingCtrl.IsActive = true;
                this.SplitViewRPanel.Opacity = 0;

                MobileServiceInvalidOperationException exception = null;
                string ExceptionDetail = string.Empty;
                try
                {
                    products = await productTable
                           .Where(product => product.Id == CurrentProductId)
                           .ToCollectionAsync();
                    if (flag == ProductTypeFlag.Product)
                    {

                        this.ProductGridMain.DataContext = products[0];
                        this.ProductGridMainNarrow.DataContext = products[0];
                        CurrentProduct = products[0];
                        features = await featureTable
                           .Where(feature => feature.ProductId == CurrentProductId)
                           .ToCollectionAsync();


                        this.ProgressRingCtrl.IsActive = false;
                        this.SplitViewRPanel.Opacity = 1;

                        FeatureListingGridView.ItemsSource = features;
                        SetDataAcrossVisualState(AdaptiveStates.CurrentState, false);

                        await blogsDs.GetFeedsAsync(CurrentProduct.BlogFeedUrl);

                    }
                    else  // Products in Preview Page
                    {
                        productsInPreview = await productsInPreviewTable
                           .Where(product => product.Id == CurrentProductId)
                           .ToCollectionAsync();
                        this.ProductGridMain.DataContext = productsInPreview[0];
                        this.ProductGridMainNarrow.DataContext = productsInPreview[0];
                        //CurrentProduct = products[0];
                        featuresInPreview = await featuresInPreviewTable
                           .Where(feature => feature.ProductId == CurrentProductId)
                           .ToCollectionAsync();


                        PreviewFlagTxtBlock.Visibility = Visibility.Visible;
                        PreviewFlagTxtBlockNarrow.Visibility = Visibility.Visible;

                        this.ProgressRingCtrl.IsActive = false;
                        this.SplitViewRPanel.Opacity = 1;

                        FeatureListingGridView.ItemsSource = featuresInPreview;
                        SetDataAcrossVisualState(AdaptiveStates.CurrentState, false);

                        await blogsDs.GetFeedsAsync(products[0].BlogFeedUrl);
                    }

                    SetBlogsPanelProgressRingVisibilityStatus(false);

                }
                catch (MobileServiceInvalidOperationException e)
                {
                    ExceptionDetail = e.Message;
                }
                catch (Exception e)
                {
                    ExceptionDetail = e.Message;
                }
                if (!(string.IsNullOrEmpty(ExceptionDetail)))
                {
                    this.ProgressRingCtrl.IsActive = false;
                    this.SplitViewRPanel.Opacity = 1;
                    ProgressRing ring = UtilityFunctions.RecurseChildren<ProgressRing>(this.BlogPostsHubNarrow).FirstOrDefault();
                    if (ring != null)
                    {
                        ring.IsActive = false;
                    }
                    ring = UtilityFunctions.RecurseChildren<ProgressRing>(this.BlogPostsHub).FirstOrDefault();
                    if (ring != null)
                    {
                        ring.IsActive = false;
                    }
                    await new MessageDialog(ExceptionDetail, "Error loading items").ShowAsync();
                }
            }

            protected override void OnNavigatedFrom(NavigationEventArgs e)
            {
                base.OnNavigatedFrom(e);
                App.NetworkStatusChanged -= new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);

            }


            private void SetBlogsPanelProgressRingVisibilityStatus(bool visible)
            {
                ProgressRing ring = UtilityFunctions.RecurseChildren<ProgressRing>(this.BlogPostsHubNarrow).FirstOrDefault();
                if (ring != null)
                {
                    ring.IsActive = visible;
                }
                ring = UtilityFunctions.RecurseChildren<ProgressRing>(this.BlogPostsHub).FirstOrDefault();
                if (ring != null)
                {
                    ring.IsActive = visible;
                }
            }
            private void SetDataAcrossVisualState(VisualState e, bool isPageLoad)
            {
                if (e == PhoneState || e == NarrowState)
                {
                    if (this.BlogPostsHubNarrow != null)
                    {
                        GridView blogItemsContainer = UtilityFunctions.RecurseChildren<GridView>(this.BlogPostsHubNarrow).FirstOrDefault();
                        if (blogItemsContainer != null)
                        {
                            blogItemsContainer.ItemsSource = blogsDs.Feeds;

                        }

                    }
                }
                if (e == DefaultState)
                {
                    if (this.BlogPostsHub != null)
                    {
                        GridView blogItemsContainer = UtilityFunctions.RecurseChildren<GridView>(this.BlogPostsHub).FirstOrDefault();
                        if (blogItemsContainer != null)
                        {
                            blogItemsContainer.ItemsSource = blogsDs.Feeds;

                        }


                    }
                }

                if (isPageLoad)
                {
                    SetBlogsPanelProgressRingVisibilityStatus(false);
                }
            }

            private async void HomePageImage_Tapped(object sender, TappedRoutedEventArgs e)
            {
            try
            {
                string productHomeUrl = CurrentProduct.ProductHomePageUrl;
                if (!(string.IsNullOrEmpty(productHomeUrl)))
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(productHomeUrl));
            }
            catch(Exception) { }
            }


            private async void TechnetImage_Tapped(object sender, TappedRoutedEventArgs e)
            {
            try
            {
                string technetUrl = CurrentProduct.TechnetUrl;
                if (!(string.IsNullOrEmpty(technetUrl)))
                    await Windows.System.Launcher.LaunchUriAsync(new Uri(technetUrl));
            }
            catch(Exception) { }
            }

            private void AppHome_Tapped(object sender, TappedRoutedEventArgs e)
            {
                Frame.Navigate(typeof(MainPage));
            }

            private void Catalog_Tapped(object sender, TappedRoutedEventArgs e)
            {
 //               this.Frame.Navigate(typeof(CatalogView));

            }

            private void FeatureListingGridView_ItemClick(object sender, ItemClickEventArgs e)
            {
                if (flag == ProductTypeFlag.Product)
                {
                    Feature f = e.ClickedItem as Feature;
                if (!(string.IsNullOrEmpty(CurrentProductId)))
                    Frame.Navigate(typeof(ProductFeatures), CurrentProductId + "|" + f.Id);
            }
                else // This is a list of Features in Preview
                {
                    FeatureInPreview f = e.ClickedItem as FeatureInPreview;
                if (!(string.IsNullOrEmpty(CurrentProductId)))
                    Frame.Navigate(typeof(ProductFeatures), CurrentProductId + "|" + f.Id + "|FeaturesInPreview");
            }
            }



            private async void GridViewBlogItems_ItemClick(object sender, ItemClickEventArgs e)
            {
                FeedItem feed = e.ClickedItem as FeedItem;
                if (feed != null && (feed.Link != null))
                {
                    try
                    {
                        if (Window.Current.Bounds.Width <= 1000)
                            await Windows.System.Launcher.LaunchUriAsync(feed.Link);
                        else
                            Frame.Navigate(typeof(WebPage), feed.Link);
                    }
                    catch (Exception)
                    {
                        // No need to catch the Exception
                    }
                }
            }

            private async void MsdnLink_Tapped(object sender, TappedRoutedEventArgs e)
            {
                string MsdnUrl = CurrentProduct.MsdnUrl;
                if (!(string.IsNullOrEmpty(MsdnUrl)))
                {
                    try
                    {
                        await Windows.System.Launcher.LaunchUriAsync(new Uri(MsdnUrl));
                    }
                    catch (Exception) { }
                }
            }

            private async void CaseStudiesLink_Tapped(object sender, TappedRoutedEventArgs e)
            {
                string caseStudies = Constants._caseStudyUrl + CurrentProduct.ProductName;
                if (!(string.IsNullOrEmpty(caseStudies)))
                {
                    try
                    {
                        await Windows.System.Launcher.LaunchUriAsync(new Uri(caseStudies));
                    }
                    catch (Exception) { }
                }

            }

            private async void SupportLink_Tapped(object sender, TappedRoutedEventArgs e)
            {
                if (CurrentProduct.LifecycleRelevant)
                {
                    string supportUrl = Constants.supportLifeCycleUrl + CurrentProduct.ProductName;
                    if (!(string.IsNullOrEmpty(supportUrl)))
                    {
                        try
                        {
                            await Windows.System.Launcher.LaunchUriAsync(new Uri(supportUrl));
                        }
                        catch (Exception) { }
                    }

                }
            }

            private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
            {
                SetDataAcrossVisualState(e.NewState, true);
            }

        private async void PrivacyPolicy_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.onpagerPrivacyPolicyUrl));
        }

        private async void Disclaimer_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.onpagerDisclaimerUrl));
        }
    }

        public enum ProductTypeFlag
        {
            Product, ProductInPreview
        }

    }


