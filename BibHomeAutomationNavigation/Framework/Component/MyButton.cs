using System;
using Xamarin.Forms;

namespace BibHomeAutomationNavigation.Framework
{
    public class MyButton : Button
    {
        public MyButton ()
        {
            TextColor = Color.Black;
            BackgroundColor = Color.White;
			BorderRadius = 5;
            BorderColor = Color.Black;
			BorderWidth = 1;
			HeightRequest = 30;
			WidthRequest = 100;
        }
    }
}

