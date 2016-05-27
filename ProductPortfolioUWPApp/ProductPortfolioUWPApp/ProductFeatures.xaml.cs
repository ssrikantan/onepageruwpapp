using Microsoft.WindowsAzure.MobileServices;
using ProductPortfolioUWPApp.DataModel;
using ProductPortfolioUWPApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductPortfolioUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductFeatures : Page
    {
        private MobileServiceCollection<Feature, Feature> features;
        private IMobileServiceTable<Feature> featureTable = App.MobileService.GetTable<Feature>();

        private MobileServiceCollection<Feature, Feature> featureUpdates;
        private IMobileServiceTable<Feature> featureUpdateTable = App.MobileService.GetTable<Feature>();

        private MobileServiceCollection<FeatureInPreview, FeatureInPreview> featuresInPreview;
        private IMobileServiceTable<FeatureInPreview> featuresInPreviewTable = App.MobileService.GetTable<FeatureInPreview>();

        private string CurrentProductId;
        private string CurrentFeatureId;

        int counter = 0;
        private FeatureInfoFlag flag;

        public ProductFeatures()
        {
            this.InitializeComponent();
            App.NetworkStatusChanged += new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Show UI back button - do it on each page navigation
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            string parameter = e.Parameter.ToString();
            string[] vals = parameter.Split(new char[] { '|' });
            CurrentProductId = vals[0];
            CurrentFeatureId = vals[1];
            if (vals.Count() <= 2)
            {
                flag = FeatureInfoFlag.FeaturesList;
            }
            else
            {
                if ("FeatureUpdatesList".Equals(vals[2]))
                {
                    flag = FeatureInfoFlag.FeatureUpdatesList;
                }
                else
                {
                    flag = FeatureInfoFlag.FeaturesInPreviewList;
                }
            }
            RefreshData();

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.NetworkStatusChanged -= new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);

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
                RelativePanelRoot.Opacity = 1;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Collapsed;
                this.ErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                RelativePanelRoot.Opacity = 0;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Visible;
                this.ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private async Task RefreshData()
        {
            this.ProgressRingCtrl.IsActive = true;
            this.RelativePanelRoot.Opacity = 0;

            MobileServiceInvalidOperationException exception = null;
            string ExceptionDetail = string.Empty;
            try
            {
                if (flag == FeatureInfoFlag.FeaturesList) // When its a listing of all Features in the same Product
                {
                    features = await featureTable
                       .Where(feature => feature.ProductId == CurrentProductId)
                       .ToCollectionAsync();

                    this.ProgressRingCtrl.IsActive = false;
                    this.RelativePanelRoot.Opacity = 1;


                    ProductFeaturesFlipView.ItemsSource = features;
                    //ToDo The following is not very efficient - need to come back to this later
                    List<Feature> allf = features.ToList<Feature>();
                    foreach (Feature f in allf)
                    {
                        if (f.Id.Equals(CurrentFeatureId))
                        {
                            break;
                        }
                        else
                            counter++;
                    }
                    if (counter != 1)   // This seems to throw an exception otherwise, when counter =1  !!!
                        ProductFeaturesFlipView.SelectedIndex = counter;
                }
                else if (flag == FeatureInfoFlag.FeatureUpdatesList)  // This same page is used to display the Feature Updates information
                {
                    featureUpdates = await featureUpdateTable
                        .Where(feature => feature.FeatureUpdateDate != null)
                        .OrderByDescending(feature => feature.FeatureUpdateDate)
                        .ToCollectionAsync();

                    this.ProgressRingCtrl.IsActive = false;
                    this.RelativePanelRoot.Opacity = 1;


                    ProductFeaturesFlipView.ItemsSource = featureUpdates;
                    //ToDo The following is not very efficient - need to come back to this later
                    List<Feature> allf = featureUpdates.ToList<Feature>();
                    foreach (Feature f in allf)
                    {
                        if (f.Id.Equals(CurrentFeatureId))
                        {
                            break;
                        }
                        else
                            counter++;
                    }
                    if (counter != 1)   // This seems to throw an exception otherwise, when counter =1  !!!
                        ProductFeaturesFlipView.SelectedIndex = counter;
                }
                else // This indicates Features In Preview
                {
                    featuresInPreview = await featuresInPreviewTable
                       .Where(feature => feature.ProductId == CurrentProductId)
                       .ToCollectionAsync();

                    this.ProgressRingCtrl.IsActive = false;
                    this.RelativePanelRoot.Opacity = 1;

                    ProductFeaturesFlipView.ItemsSource = featuresInPreview;
                    //ToDo The following is not very efficient - need to come back to this later
                    List<FeatureInPreview> allf = featuresInPreview.ToList<FeatureInPreview>();
                    foreach (FeatureInPreview f in allf)
                    {
                        if (f.Id.Equals(CurrentFeatureId))
                        {
                            break;
                        }
                        else
                            counter++;
                    }
                    if (counter != 1)   // This seems to throw an exception otherwise, when counter =1  !!!
                        ProductFeaturesFlipView.SelectedIndex = counter;
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                ExceptionDetail = e.Message;
            }
            catch (Exception ex)
            {
                ExceptionDetail = ex.Message;
            }

            if (!(string.IsNullOrEmpty(ExceptionDetail)))
            {
                this.ProgressRingCtrl.IsActive = false;
                this.RelativePanelRoot.Opacity = 1;
                await new MessageDialog(ExceptionDetail, "Error loading items").ShowAsync();
            }
        }

        private void AppHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Catalog_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CatalogView));
        }


        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {

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


    public enum FeatureInfoFlag
    {
        FeaturesList, FeatureUpdatesList, FeaturesInPreviewList
    }

}
