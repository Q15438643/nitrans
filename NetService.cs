using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nitrans
{
    public static class NetService
    {
        private static ServiceStatus _serviceStatus = new ServiceStatus() { Status = 0, StatusName = "停止" };

        /// <summary>
        /// 服务状态
        /// </summary>
        public struct ServiceStatus
        {
            public int Status;
            public string StatusName;
        }

        /// <summary>
        /// 服务状态的获取或设置
        /// </summary>
        public static ServiceStatus Status
        {
            get { return _serviceStatus; }
            set { _serviceStatus = value; }
        }

        /// <summary>
        /// 正常情况的状态变更
        /// </summary>
        /// <param name="status"></param>
        public static void ChangeServiceStatus(int status)
        {
            switch (status)
            {
                case 0:
                    Status = new ServiceStatus() { Status = 0, StatusName = "停止" };
                    break;
                case 1:
                    Status = new ServiceStatus() { Status = 1, StatusName = "启动" };
                    break;
                default:
                    Status = new ServiceStatus() { Status = 0, StatusName = "停止" };
                    break;
            }
        }

        /// <summary>
        /// 异常情况，显示异常原因
        /// </summary>
        /// <param name="status"></param>
        /// <param name="errmsg"></param>
        public static void ChangeServiceStatus(int status, string errmsg)
        {
            Status = new ServiceStatus() { Status = status, StatusName = errmsg };
        }

    }
}
