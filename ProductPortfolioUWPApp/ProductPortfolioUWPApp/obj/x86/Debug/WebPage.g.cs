﻿#pragma checksum "C:\Solution Files\MyStore Apps\ProductPortfolioUWPApp\ProductPortfolioUWPApp\ProductPortfolioUWPApp\WebPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "854AB75B244DEB84574A5C9F449586B7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductPortfolioUWPApp
{
    partial class WebPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.AdaptiveStates = (global::Windows.UI.Xaml.VisualStateGroup)(target);
                    #line 12 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.VisualStateGroup)this.AdaptiveStates).CurrentStateChanged += this.AdaptiveStates_CurrentStateChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.DefaultState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 3:
                {
                    this.MediumState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 4:
                {
                    this.PhoneState = (global::Windows.UI.Xaml.VisualState)(target);
                }
                break;
            case 5:
                {
                    this.ProgressRingCtrl = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 6:
                {
                    this.InternetConnectionStatusPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 7:
                {
                    this.SettingsPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 8:
                {
                    this.MainRelativePanel = (global::Windows.UI.Xaml.Controls.RelativePanel)(target);
                }
                break;
            case 9:
                {
                    this.TabbedMenuPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 10:
                {
                    this.WebViewGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 11:
                {
                    this.WebViewMain = (global::Windows.UI.Xaml.Controls.WebView)(target);
                    #line 67 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.WebView)this.WebViewMain).NavigationCompleted += this.WebViewMain_NavigationCompleted;
                    #line default
                }
                break;
            case 12:
                {
                    this.HomeTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 59 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.HomeTextBlock).Tapped += this.AppHome_Tapped;
                    #line default
                }
                break;
            case 13:
                {
                    this.CatalogViewTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                    #line 61 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBlock)this.CatalogViewTextBlock).Tapped += this.Catalog_Tapped;
                    #line default
                }
                break;
            case 14:
                {
                    this.About = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 15:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element15 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 46 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element15).Click += this.PrivacyPolicy_Click;
                    #line default
                }
                break;
            case 16:
                {
                    global::Windows.UI.Xaml.Controls.MenuFlyoutItem element16 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)(target);
                    #line 48 "..\..\..\WebPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)element16).Click += this.Disclaimer_Click;
                    #line default
                }
                break;
            case 17:
                {
                    this.ErrorTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

