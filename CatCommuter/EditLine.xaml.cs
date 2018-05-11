﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditLine : Page
    {
        BusLine busLineSelected;
        BusStop sampleStop;
        BusStopManager bsManager = BusStopManager.getInstance();
        public static BusStop toEditBusStop { get; set; }

        public EditLine()
        {
            this.InitializeComponent();

            //Add a back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                Debug.WriteLine("BackRequested");
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };

            busLineSelected = Preferences.toEditBusLine;
            DataContext = busLineSelected;
            //System.Diagnostics.Debug.WriteLine("Size:" + busLineSelected.busStops.Count);
            stop_ListView.ItemsSource = busLineSelected.busStops;

            DataContext = busLineSelected.busStops;


            _parentItems = busLineSelected.busStops;
        }

        private IList<BusStop> _parentItems;

        public IList<BusStop> ParentItems
        {
            get { return _parentItems; }
            set { _parentItems = value; }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            
            BasicGeoposition position = new BasicGeoposition
            {
                Latitude = 37.363930,
                Longitude = -120.430695

            };
            //BusStop sampleStop = new BusStop("Emigrant Pass at Scholars Lane ", bsManager.busLines, position);
            //bsManager.busStops.Add(sampleStop);
            //busLineSelected.addStop(sampleStop);
            bsManager.addBusStop("Em Pass", position, busLineSelected);
            stop_ListView.ItemsSource = null;
            stop_ListView.ItemsSource = busLineSelected.busStops;
            DataContext = busLineSelected.busStops;
            _parentItems = busLineSelected.busStops;

            Button _button = (Button)sender;
            toEditBusStop = _button.DataContext as BusStop;
            Frame.Navigate(typeof(EditStop));
        }

        private void CreateBusStop_Click(object sender, RoutedEventArgs e)
        {
            toEditBusStop = null;
            Frame.Navigate(typeof(EditStop));
        }

        // Handles the Click event on the Button on the page and opens the Popup. 
        private void ShowPopupOffsetClicked(object sender, RoutedEventArgs e)
        {
            // open the Popup if it isn't open already 
            //if (toEditBusStop == null)
            //    toEditBusStop = new BusStop()
            Button _button = (Button)sender;
            toEditBusStop = _button.DataContext as BusStop;
            if (!StandardPopup.IsOpen) { StandardPopup.IsOpen = true; }
        }

        // Handles the Click event on the Button inside the Popup control and 
        // closes the Popup. 
        private void ClosePopupClicked(object sender, RoutedEventArgs e)
        {
            // if the Popup is open, then close it 
            if (StandardPopup.IsOpen) { StandardPopup.IsOpen = false; }
            toEditBusStop = null;
        }

    }
}
