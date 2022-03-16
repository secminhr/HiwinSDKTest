using SDKHrobot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiwinSDKTest.Robot
{
    class RealRobot : Robot
    {
        private int? id = null;
        public override async Task OpenConnection(string ip, int mode, HRobot.CallBackFun callback)
        {
            ConnectionState = ConnectState.Connecting;
            int id = await Task.Run(() => HRobot.open_connection(ip, 1, callback));
            if (id >= 0)
            {
                ConnectionState = ConnectState.Connected;
                this.IP = ip;
                ConnectionLevel = await Task.Run(() => HRobot.get_connection_level(id));
                this.id = id;
            }
            else
            {
                ConnectionState = ConnectState.Disconnected;
                throw new RobotException(id);
            }
        }

        public override Task Disconnect()
        {
            return Task.Run(() =>
            {
                if (id != null)
                {
                    HRobot.disconnect(id.Value);
                }
                id = null;
                IP = null;
                ConnectionState = ConnectState.Disconnected;
                ConnectionLevel = null;
            });
        }

        //misleading function name
        public override async Task<int> GetAccDecRatio()
        {
            double[] mills = new double[6];
            int success = await Task.Run(() => HRobot.get_mileage(id.Value, mills));
            /*if (0 <= ratio && ratio <= 100)
            {
                return ratio;
            }
            */
            if (success == 0)
            {
                return (int)mills[0];
            }
            throw new RobotException(success);
        }

        public override async Task<int> GetPtpSpeed()
        {
            int speed = await Task.Run(() => HRobot.get_ptp_speed(id.Value));
            if (0 <= speed && speed <= 100)
            {
                return speed;
            }
            throw new RobotException(speed);
        }

        public override async Task<double> GetlinSpeed()
        {
            double speed = await Task.Run(() => HRobot.get_lin_speed(id.Value));
            if (speed >= 0.0)
            {
                return speed;
            }
            throw new RobotException((int) speed);
        }

        public override async Task<int> GetOverrideRatio()
        {
            int ratio = await Task.Run(() => HRobot.get_override_ratio(id.Value));
            if (0 <= ratio && ratio <= 100)
            {
                return ratio;
            }
            throw new RobotException(ratio);
        }

        public override async Task Jog(int index, int dir)
        {
            int code = await Task.Run(() => HRobot.jog(id.Value, 1, index, dir));
            if (code != 0)
            {
                throw new RobotException(code);
            }
        }

        public override async Task StopJog()
        {
            int code = await Task.Run(() => HRobot.jog_stop(id.Value));
            if (code != 0)
            {
                throw new RobotException(code);
            }
        }

        public override async Task P2pPos(int mode, double[] p)
        {
            int code = await Task.Run(() => HRobot.ptp_pos(id.Value, mode, p));
            if (code != 0)
            {
                throw new RobotException(code);
            }
        }
    }
}
