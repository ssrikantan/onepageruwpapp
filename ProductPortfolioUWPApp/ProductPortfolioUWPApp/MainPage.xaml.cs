using Microsoft.WindowsAzure.MobileServices;
using ProductPortfolioUWPApp.DataModel;
using ProductPortfolioUWPApp.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProductPortfolioUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<ProductInPreview, ProductInPreview> productsInPreview;
        private IMobileServiceTable<ProductInPreview> productsInPreviewTable = App.MobileService.GetTable<ProductInPreview>();

        private MobileServiceCollection<Feature, Feature> featuresUpdates;
        private IMobileServiceTable<Feature> featureUpdatesTable = App.MobileService.GetTable<Feature>();

        private MobileServiceCollection<Announcement, Announcement> announcements;
        private IMobileServiceTable<Announcement> announcementTable = App.MobileService.GetTable<Announcement>();
        //private IMobileServiceSyncTable<TodoItem> todoTable = App.MobileService.GetSyncTable<TodoItem>(); // offline sync

        public MainPage()
        {
            this.InitializeComponent();
            App.NetworkStatusChanged += new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);
            RefreshData();
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.NetworkStatusChanged -= new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
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

        private void HideControlsBasedOnVisualState()
        {
            this.MainRelativePanel.Opacity = 0;
        }

        private void UnHideControlsBasedOnVisualState()
        {
            this.MainRelativePanel.Opacity = 1;
        }

        private async Task RefreshData()
        {
            this.ProgressRingCtrl.IsActive = true;
            HideControlsBasedOnVisualState();
            MobileServiceInvalidOperationException exception = null;
            string ExceptionDetail = string.Empty;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems

                productsInPreview = await productsInPreviewTable
                    .Where(product => product.Id != null)
                    .ToCollectionAsync();

                featuresUpdates = await featureUpdatesTable
                    .Where(feature => feature.FeatureUpdateDate != null)
                    .OrderByDescending(feature => feature.FeatureUpdateDate)
                    .ToCollectionAsync();
                announcements = await announcementTable
                    .Where(announcement => announcement.Id != null)
                    .ToCollectionAsync();


                this.ProgressRingCtrl.IsActive = false;
                UnHideControlsBasedOnVisualState();

                Announcement1.DataContext = announcements[0];
                Announcement2.DataContext = announcements[1];
                Announcement3.DataContext = announcements[2];

                Announcement1_Phone.DataContext = announcements[0];
                Announcement2_Phone.DataContext = announcements[1];
                Announcement3_Phone.DataContext = announcements[2];

                SetDataAcrossVisualState(AdaptiveStates.CurrentState);


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
                UnHideControlsBasedOnVisualState();
                await new MessageDialog(ExceptionDetail, "Error loading items").ShowAsync();
            }
        }


        private void Catalog_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CatalogView));

        }

        private void AppHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }


        private void GridViewPreviewItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductInPreview p = e.ClickedItem as ProductInPreview;
            Frame.Navigate(typeof(ProductPage), p.Id + "|ProductsInPreview");
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            SetDataAcrossVisualState(e.NewState);
        }

        private void SetDataAcrossVisualState(VisualState e)
        {
            if (e == PhoneState)
            {
                if (this.ProductsInPreviewHub2 != null)
                {
                    GridView productsInPreviewContainer2 = UtilityFunctions.RecurseChildren<GridView>(this.ProductsInPreviewHub2).FirstOrDefault();
                    if (productsInPreviewContainer2 != null)
                        productsInPreviewContainer2.ItemsSource = productsInPreview;
                }
                if (this.FeatureUpdatesHubNarrow != null)
                {
                    GridView featureUpdatesContainer = UtilityFunctions.RecurseChildren<GridView>(this.FeatureUpdatesHubNarrow).FirstOrDefault();
                    if (featureUpdatesContainer != null)
                        featureUpdatesContainer.ItemsSource = featuresUpdates;
                }
            }
            if (e == DefaultState)
            {
                if (this.ProductsInPreviewHub != null)
                {
                    GridView productsInPreviewContainer = UtilityFunctions.RecurseChildren<GridView>(this.ProductsInPreviewHub).FirstOrDefault();
                    if (productsInPreviewContainer != null)
                        productsInPreviewContainer.ItemsSource = productsInPreview;
                }
                if (this.FeatureUpdatesHub != null)
                {
                    GridView featureUpdatesContainer = UtilityFunctions.RecurseChildren<GridView>(this.FeatureUpdatesHub).FirstOrDefault();
                    if (featureUpdatesContainer != null)
                        featureUpdatesContainer.ItemsSource = featuresUpdates;
                }
            }
        }

        private void GridViewFeatureUpdateItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            Feature f = e.ClickedItem as Feature;
            Frame.Navigate(typeof(ProductFeatures), f.ProductId + "|" + f.Id + "|FeatureUpdatesList");
        }

        private void Announcement1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage), announcements[0].BlogUrl);
        }

        private void Announcement2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage), announcements[1].BlogUrl);
        }

        private void Announcement3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage), announcements[2].BlogUrl);
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)// Download All Products poster
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.onpagerAllProductsPosterUrl));
        }

        private async void Image_Tapped_1(object sender, TappedRoutedEventArgs e)  // Download Azure poster
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(Constants.onpagerAllAzurePosterUrl));
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

}
