using SDKHrobot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiwinSDKTest.Robot
{
    class MockRobot : Robot
    {
        public override Task OpenConnection(string ip, int mode, HRobot.CallBackFun callback)
        {
            return Task.Run(() =>
            {
                ConnectionState = ConnectState.Connecting;
                Thread.Sleep(1000);
                ConnectionState = ConnectState.Connected;
                this.IP = ip;
                ConnectionLevel = mode;
            });
        }

        public override Task Disconnect()
        {
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                IP = null;
                ConnectionState = ConnectState.Disconnected;
                ConnectionLevel = null;
            });
        }

        private bool firstAccess = true;
        public override Task<int> GetAccDecRatio()
        {
            Debug.Assert(IP != null);
            firstAccess = false;
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                return firstAccess ? 10 : 15;
            });
        }

        public override Task<int> GetPtpSpeed()
        {
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 20;
            });
        }

        public override Task<double> GetlinSpeed()
        {
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 30.5;
            });
        }

        public override Task<int> GetOverrideRatio()
        {
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 40;
            });
        }

        public override Task Jog(int index, int dir)
        {
            Debug.Assert(IP != null);
            Debug.Assert(0 <= index && index <= 5);
            Debug.Assert(dir == 1 || dir == -1);
            return Task.Run(() => 
            {
                Thread.Sleep(2000);
            });
        }

        public override Task StopJog()
        {
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
        }

        public override Task P2pPos(int mode, double[] p)
        {
            //left unimplemented, only for testing
            Debug.Assert(IP != null);
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
        }
    }
}
