using HiwinSDKTest.Robot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using static HiwinSDKTest.ErrorDialogHelper;

namespace HiwinSDKTest.ContentPages
{
    public sealed partial class ReadRobotPage : Page
    {
        public ObservableCollection<string> RobotProperty = new ObservableCollection<string>();
       
        public ReadRobotPage()
        {
            this.InitializeComponent();
            UpdateRobotProperty();
            MainPage.Robot.OnRobotStateChangeEvent += UpdateRobotProperty;
        }

        private async void UpdateRobotProperty()
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                RobotProperty.Clear(); //clear here for visual effect, the items disappear immediately after update is requested
                if (MainPage.Robot.ConnectionState == Robot.Robot.ConnectState.Connected)
                {
                    Task<int> accDecRatio = MainPage.Robot.GetAccDecRatio();
                    Task<int> ptpSpeed = MainPage.Robot.GetPtpSpeed();
                    Task<double> linSpeed = MainPage.Robot.GetlinSpeed();
                    Task<int> overrideRatio = MainPage.Robot.GetOverrideRatio();

                    try
                    {
                        await Task.WhenAll(accDecRatio, ptpSpeed, linSpeed, overrideRatio);
                        /*
                          Second clear is needed. Consider if UpdateRobotProperty is invoke multiple times in a short period, 
                          then Clear() after first invocation won't have any effect, and multiple items will be added to RobotProperty after tasks are finished
                        */
                        RobotProperty.Clear();
                        RobotProperty.Add(accDecRatio.Result.ToString());
                        RobotProperty.Add(ptpSpeed.Result.ToString());
                        RobotProperty.Add(linSpeed.Result.ToString());
                        RobotProperty.Add(overrideRatio.Result.ToString());
                    } catch (RobotException e)
                    {
                        await ErrorDialog(e.ErrorId).ShowAsync();
                    }
                }
            });
        }
    }
}
