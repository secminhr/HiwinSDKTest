using HiwinSDKTest.Robot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace HiwinSDKTest
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Robot.Robot.ConnectState ConnectionState => MainPage.Robot.ConnectionState;

        public bool Disconnected => ConnectionState == Robot.Robot.ConnectState.Disconnected;
        public bool Connected => ConnectionState == Robot.Robot.ConnectState.Connected;
        public bool Connecting => ConnectionState == Robot.Robot.ConnectState.Connecting;
        
        public string IPButtonText => Disconnected ? "連線" : "斷線";
        public bool NotConnecting => !Connecting;

        public MainPageViewModel()
        {
            MainPage.Robot.OnRobotStateChangeEvent += () =>
            {
                //this will update ui, must be executed on main thread
                //We don't await RunAsync since we don't care when it is finished, and awaiting it may freeze the main thread
                CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                });
            };
        }
    }
}
