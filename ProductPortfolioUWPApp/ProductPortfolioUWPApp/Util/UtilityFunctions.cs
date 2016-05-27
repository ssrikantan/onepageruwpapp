using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;


namespace ProductPortfolioUWPApp.Util
{
    public class UtilityFunctions
    {
        public static IEnumerable<T> RecurseChildren<T>(DependencyObject root) where T : UIElement
        {
            if (root is T)
            {
                yield return root as T;
            }

            if (root != null)
            {
                var count = VisualTreeHelper.GetChildrenCount(root);


                for (var idx = 0; idx < count; idx++)
                {
                    foreach (var child in RecurseChildren<T>(VisualTreeHelper.GetChild(root, idx)))
                    {
                        yield return child;
                    }
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
