﻿using Xamarin.Essentials;

namespace Samples.ViewModel
{
    public class DeviceInfoViewModel : BaseViewModel
    {
        ScreenMetrics screenMetrics;

        public string Model => DeviceInfo.Model;

        public string Manufacturer => DeviceInfo.Manufacturer;

        public string Name => DeviceInfo.Name;

        public string VersionString => DeviceInfo.VersionString;

        public string Version => DeviceInfo.Version.ToString();

        public string Platform => DeviceInfo.Platform;

        public string Idiom => DeviceInfo.Idiom;

        public string StorageInformation { get; private set; }

        public DeviceType DeviceType => DeviceInfo.DeviceType;

        public ScreenMetrics ScreenMetrics
        {
            get => screenMetrics;
            set => SetProperty(ref screenMetrics, value);
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();

            DeviceDisplay.ScreenMetricsChanged += OnScreenMetricsChanged;
            ScreenMetrics = DeviceDisplay.ScreenMetrics;
            StorageInformation = string.Join("\n", await DeviceInfo.GetStorageInformationAsync());
        }

        public override void OnDisappearing()
        {
            DeviceDisplay.ScreenMetricsChanged -= OnScreenMetricsChanged;

            base.OnDisappearing();
        }

        void OnScreenMetricsChanged(object sender, ScreenMetricsChangedEventArgs e)
        {
            ScreenMetrics = e.Metrics;
        }
    }
}
