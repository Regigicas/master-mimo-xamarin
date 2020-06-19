using System;
using Xamarin.Forms;

namespace GamesViewer_Xamarin.Behaviors
{
    public class MinMaxLengthBehavior : Behavior<Entry>
    {
        public static readonly BindableProperty MinValueProperty = BindableProperty.CreateAttached(nameof(MinValue),
            typeof(int), typeof(MinMaxLengthBehavior), 0);
        public static readonly BindableProperty MaxValueProperty = BindableProperty.CreateAttached(nameof(MaxValue),
            typeof(int), typeof(MinMaxLengthBehavior), 0);

        public int MinValue
        {
            get
            {
                return (int)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public int MaxValue
        {
            get
            {
                return (int)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

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
            bool isValid = true;
            var textLength = args.NewTextValue.Length;
            if (textLength < MinValue || textLength > MaxValue)
                isValid = false;
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
