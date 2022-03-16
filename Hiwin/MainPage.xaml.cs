using HiwinSDKTest.Robot;
using SDKHrobot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using MUXC = Microsoft.UI.Xaml.Controls;
using static HiwinSDKTest.ErrorDialogHelper;


namespace HiwinSDKTest
{
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel = new MainPageViewModel();
        //a mapping from a nav item to a page type
        private Dictionary<object, Type> pageTypeOfItem = new Dictionary<object, Type>();
        public MainPage()
        {
            this.InitializeComponent();

            pageTypeOfItem.Add(InfoNavItem, typeof(ContentPages.InfoPage));
            pageTypeOfItem.Add(ReadNavItem, typeof(ContentPages.ReadRobotPage));
            pageTypeOfItem.Add(OperateNavItem, typeof(ContentPages.OperateRobotPage));
            Navigation.SelectedItem = InfoNavItem;
        }

        private void robotCallback(ushort cmd, ushort rlt, ref ushort msg, int len)
        {

        }

        //single singleton 
        public static Robot.Robot Robot { get; private set; } = new RealRobot();

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (Robot.ConnectionState == HiwinSDKTest.Robot.Robot.ConnectState.Disconnected)
            {
                try
                {
                    await Robot.OpenConnection(IPTextBox.Text, 1, robotCallback);
                }
                catch (RobotException err)
                {
                    await ErrorDialog(err.ErrorId).ShowAsync();
                }
            } 
            else
            {
                Navigation.SelectedItem = InfoNavItem;
                await Robot.Disconnect();
            }
        }

        private void NavigationView_SelectionChanged(MUXC.NavigationView sender, MUXC.NavigationViewSelectionChangedEventArgs args)
        {
            ContentFrame.Navigate(pageTypeOfItem[args.SelectedItem]);
        }
    }
}
