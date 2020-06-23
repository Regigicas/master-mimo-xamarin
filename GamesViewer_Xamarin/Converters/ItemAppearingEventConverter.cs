using System;
using System.Globalization;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Converters
{
    public class ItemAppearingEventConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as ItemVisibilityEventArgs;
            if (eventArgs == null)
                return null;

            return eventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
