using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
namespace HiwinSDKTest.ContentPages
{
    public sealed partial class InfoPage : Page
    {
        public ObservableCollection<string> Info = new ObservableCollection<string>();

        public InfoPage()
        {
            this.InitializeComponent();
            UpdateInfo();
            MainPage.Robot.OnRobotStateChangeEvent += UpdateInfo;
        }

        private void UpdateInfo()
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Info.Clear();
                Info.Add(MainPage.Robot.SDKVersion);
                Info.Add(MainPage.Robot.IP ?? "");
                Info.Add(MainPage.Robot.ConnectionLevel?.ToString() ?? "");
            });
        }

    }
}
