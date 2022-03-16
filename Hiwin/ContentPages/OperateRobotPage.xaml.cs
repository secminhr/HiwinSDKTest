using HiwinSDKTest.Robot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class OperateRobotPage : Page
    {
        public OperateRobotPage()
        {
            this.InitializeComponent();
        }

        public async void JogPos()
        {
            Debug.WriteLine("JosPos");
            await Jog(1);
        }

        public async void JogNeg()
        {
            Debug.WriteLine("JogNeg");
            await Jog(-1);
        }

        private async Task Jog(int direction)
        {
            try
            {
                await MainPage.Robot.Jog(JointPicker.SelectedIndex, direction);
            }
            catch (RobotException e)
            {
                await ErrorDialog(e.ErrorId).ShowAsync();
            }
        }

        public async void StopJog()
        {
            Debug.WriteLine("StopJog");
            try
            {
                await MainPage.Robot.StopJog();
            }
            catch (RobotException e)
            {
                await ErrorDialog(e.ErrorId).ShowAsync();
            }
        }

        double[] p2p_point = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

        private async void moveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (double d in p2p_point)
                {
                    Debug.WriteLine(d);
                }
                await MainPage.Robot.P2pPos(0, p2p_point);
            } 
            catch (RobotException err)
            {
                await ErrorDialog(err.ErrorId).ShowAsync();
            }

        }
    }
}
