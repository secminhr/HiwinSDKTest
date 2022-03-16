using SDKHrobot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiwinSDKTest.Robot
{
    //Robot provides a layer above the hiwin SDK to simplify the development of the actual application (especially when we couldn't access the robotic arm).
    //It also warps the functions in hiwin SDK in a async-await style, since we've already seen that some functions are time-consuming and will block the main thread if called direcly.
    public abstract class Robot
    {
        public enum ConnectState
        {
            Disconnected, Connecting, Connected
        }

        public delegate void OnRobotStateChange();
        public event OnRobotStateChange OnRobotStateChangeEvent;

        public string SDKVersion
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                HRobot.get_hrsdk_version(builder);
                return builder.ToString();
            }
        }

        private string? ip = null;
        public string? IP 
        {
            get => ip;
            protected set
            {
                ip = value;
                OnRobotStateChangeEvent?.Invoke();
            }
        }

        private ConnectState connectionState = ConnectState.Disconnected;
        public ConnectState ConnectionState
        {
            get => connectionState;
            protected set
            {
                connectionState = value;
                OnRobotStateChangeEvent?.Invoke();
            }
        }

        private int? connectionLevel = null;
        public int? ConnectionLevel 
        { 
            get => connectionLevel;
            protected set
            {
                connectionLevel = value;
                OnRobotStateChangeEvent?.Invoke();
            } 
        }
        public abstract Task OpenConnection(string ip, int mode, HRobot.CallBackFun callback);
        public abstract Task Disconnect();

        public abstract Task<int> GetAccDecRatio();
        public abstract Task<int> GetPtpSpeed();
        public abstract Task<double> GetlinSpeed();
        public abstract Task<int> GetOverrideRatio();

        //space_type is fixed, we'll use 1 (Joint Coordinate) for this testeing app
        public abstract Task Jog(int index, int dir);
        public abstract Task StopJog();

        public abstract Task P2pPos(int mode, double[] p);
    }

    public class RobotException: Exception
    {
        public int ErrorId;
        public RobotException(int errorId): base()
        {
            ErrorId = errorId;
        }
    }
}
