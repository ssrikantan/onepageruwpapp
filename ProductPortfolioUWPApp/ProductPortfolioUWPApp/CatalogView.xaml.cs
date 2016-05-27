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
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
//using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProductPortfolioUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CatalogView : Page
    {
        //private MobileServiceCollection<Product, Product> products;

        private MobileServiceIncrementalLoadingCollection<Product, Product> products;
        private IMobileServiceTable<Product> productTable = App.MobileService.GetTable<Product>();

        private MobileServiceCollection<Category, Category> categories;
        private IMobileServiceTable<Category> categoryTable = App.MobileService.GetTable<Category>();

        private string CategorySelected;

        public CatalogView()
        {
            this.InitializeComponent();
            App.NetworkStatusChanged += new PortfolioAppNetworkStatusChangedEventHandler(NetworkChangedAction);
            RefreshData();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //Show UI back button - do it on each page navigation
            if (Frame.CanGoBack)
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            else
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
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
                SplitViewRPanel.Opacity = 1;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Collapsed;
                this.ErrorTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                SplitViewRPanel.Opacity = 0;
                this.InternetConnectionStatusPanel.Visibility = Visibility.Visible;
                this.ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        private async Task RefreshData()
        {
            this.ProgressRingCtrl.IsActive = true;
            this.SplitViewRPanel.Opacity = 0;
            ClearSelectionFilter.Visibility = Visibility.Collapsed;
            ClearSelectionFilter.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            MobileServiceInvalidOperationException exception = null;
            string ExceptionDetail = string.Empty;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                products = productTable
                    .Where(product => product.Id != null)
                    .ToIncrementalLoadingCollection();

                categories = await categoryTable
                    .Where(category => category.Id != null)
                    .ToCollectionAsync();


                this.ProgressRingCtrl.IsActive = false;
                this.SplitViewRPanel.Opacity = 1;

                CategoryList.ItemsSource = categories;
                ProductListingGridView.ItemsSource = products;

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
                await new MessageDialog(ExceptionDetail, "Error loading items").ShowAsync();
            }
        }

        private async void CategoryList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category c = e.ClickedItem as Category;
            CategorySelected = c.Id;

            this.ProgressRingCtrl.IsActive = true;
            this.SplitViewRPanel.Opacity = 0;

            try
            {
                products = productTable
                      .Where(product => product.CategoryName == c.CategoryName)
                      .ToIncrementalLoadingCollection();
                ProductListingGridView.ItemsSource = products;

                categories = await categoryTable
                .Where(category => category.Id == CategorySelected)
                .ToCollectionAsync();

                this.ProgressRingCtrl.IsActive = false;
                this.SplitViewRPanel.Opacity = 1;

                CategoryList.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                this.ProgressRingCtrl.IsActive = false;
                this.SplitViewRPanel.Opacity = 1;
                await new MessageDialog(ex.Message, "Error loading items").ShowAsync();
                return;
            }
            ClearSelectionFilter.Visibility = Visibility.Visible;
            ClearSelectionFilter.Foreground = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue);
            SetDataTemplateFilter();

            if (AdaptiveStates.CurrentState == NarrowStateView2)
            {
                if (Window.Current.Bounds.Width >= 450)
                {
                    VisualStateManager.GoToState(this, "MediumState", false);
                }
                else
                {
                    VisualStateManager.GoToState(this, "NarrowState", false);
                }
            }
        }

        private void ProductListingGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(ProductPage), ((Product)e.ClickedItem).Id);
        }

        private async void ClearSelectionFilter_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await RefreshData();
            SetDataTemplateFilterDefault();
        }


        private void SetDataTemplateFilter()
        {
            string template = @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""><Grid Height = ""40""><StackPanel Grid.Column = ""1"" HorizontalAlignment = ""Left"" Margin = ""4,4,4,4""><TextBlock Text = ""{Binding CategoryName}"" Foreground = ""CornflowerBlue""  FontWeight=""Light"" TextWrapping = ""Wrap"" Style=""{ StaticResource SubheaderTextBlockStyle}"" FontSize=""20""></TextBlock></StackPanel></Grid></DataTemplate>";
            this.CategoryList.ItemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(template) as DataTemplate;
        }

        private void SetDataTemplateFilterDefault()
        {
            string template = @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""><Grid Height = ""40""><StackPanel Grid.Column = ""1"" HorizontalAlignment = ""Left"" Margin = ""4,4,4,4""><TextBlock Text = ""{Binding CategoryName}"" Foreground = ""Black""   FontWeight=""Light""  TextWrapping = ""Wrap"" Style=""{ StaticResource SubheaderTextBlockStyle}"" FontSize=""20""></TextBlock></StackPanel></Grid></DataTemplate>";
            this.CategoryList.ItemTemplate = Windows.UI.Xaml.Markup.XamlReader.Load(template) as DataTemplate;
        }

        private void AppHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "NarrowStateView2", true);
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            //  UpdateForVisualState(e.NewState, e.OldState);
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


