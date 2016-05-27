using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using ProductPortfolioUWPApp.Util;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductPortfolioUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebPage : Page
    {
        private string WebUrl;
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
     
            public WebPage()
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

                WebViewMain.Height = Window.Current.Bounds.Height;
                WebViewMain.Width = Window.Current.Bounds.Width;
                WebUrl = e.Parameter.ToString();
                RefreshData();

            }

            protected override void OnNavigatedFrom(NavigationEventArgs e)
            {
                base.OnNavigatedFrom(e);
                App.NetworkStatusChanged -= new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);

            }

            private async Task RefreshData()
            {
                this.ProgressRingCtrl.IsActive = true;
                this.MainRelativePanel.Visibility = Visibility.Collapsed;
                if (!string.IsNullOrEmpty(WebUrl))
                {
                    try
                    {
                        this.WebViewMain.Navigate(new Uri(WebUrl));
                    }
                    catch (Exception)
                    {
                        this.ProgressRingCtrl.IsActive = false;
                        this.MainRelativePanel.Visibility = Visibility.Visible;
                    }
                }
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

            private void Catalog_Tapped(object sender, TappedRoutedEventArgs e)
            {
                this.Frame.Navigate(typeof(CatalogView));

            }

            private void AppHome_Tapped(object sender, TappedRoutedEventArgs e)
            {
                Frame.Navigate(typeof(MainPage));
            }


            private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
            {
                this.WebViewGrid.Width = Window.Current.Bounds.Width;
                this.WebViewGrid.Height = Window.Current.Bounds.Height;
                this.WebViewMain.Width = Window.Current.Bounds.Width;
                this.WebViewMain.Height = Window.Current.Bounds.Height;


            }

            private void WebViewMain_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
            {
                this.ProgressRingCtrl.IsActive = false;
                this.MainRelativePanel.Visibility = Visibility.Visible;
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
