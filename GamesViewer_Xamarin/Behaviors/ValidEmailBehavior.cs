using System;
using GamesViewer_Xamarin.Misc;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Behaviors
{
    public class ValidEmailBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            base.OnAttachedTo(entry);
            entry.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            base.OnDetachingFrom(entry);
            entry.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = false;
            if (args.NewTextValue != null && Util.IsEmailValid(args.NewTextValue))
                isValid = true;
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
