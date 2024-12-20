﻿using FluentWeather.Models;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FluentWeather.Helpers;
using FluentWeather.ViewModels;
using FluentWeather.Views;

namespace FluentWeather.Dialogs
{
    public sealed partial class FirstRunDialog : ContentDialog
    {
        private AppViewModel AppViewModel = AppViewModelHolder.GetViewModel();

        public FirstRunDialog()
        {
            RequestedTheme = ((FrameworkElement) Window.Current.Content).RequestedTheme;

            Closing += DialogClosingEvent;

            //make the primary button colored
            DefaultButton = ContentDialogButton.Primary;

            //disable primary button until user selects a location
            IsPrimaryButtonEnabled = false;

            //FullSizeDesired = true;

            InitializeComponent();

            /*MainGrid.Width = bounds.Width * scaleFactor;
            MainGrid.Height = bounds.Height*scaleFactor;*/
        }


        //prevent dialog dismiss by escape key
        private void DialogClosingEvent(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            // This mean user does click on Primary or Secondary button
            if (args.Result == ContentDialogResult.None)
            {
                args.Cancel = true;
            }
            else
            {
                AppViewModel.UpdateUi();
            }
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            MainPage.AutoSuggestBox_TextChanged(sender, args);
        }

        private async void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender,
            AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var selectedPlaceId = ((SearchedLocation) args.SelectedItem).placeId;

            //save location to settings
            await ApplicationData.Current.LocalSettings.SaveAsync("lastPlaceId", selectedPlaceId);

            IsPrimaryButtonEnabled = true;

            //set the focus back to the dialog, to prevent the focus to go behind the dialog
            //this.Focus(FocusState.Unfocused);
        }

        private void AutoSuggestBoxMain_OnGotFocus(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox autoSuggestBox = (AutoSuggestBox) sender;
            autoSuggestBox.IsSuggestionListOpen = true;
        }
    }
}
