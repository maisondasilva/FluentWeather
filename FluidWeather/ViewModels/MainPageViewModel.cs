﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace FluidWeather.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        private bool _isLoadingData;

        public bool IsLoadingData
        {
            get { return _isLoadingData; }
            set { SetProperty(ref _isLoadingData, value); }
        }


        public MainPageViewModel() { }
    }
}
