using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Security.Cryptography;


namespace ModernUI.Common.Utility.Security
{
    public class MachineCodeUtil
    {

        public static string GetMachineCode()
        {
            MachineCodeUtil machineCodeUtil = new MachineCodeUtil();
            string hardID = machineCodeUtil.GetHardDiskID();
            string cpuID = machineCodeUtil.GetCPUID();
            string secondHardID = machineCodeUtil.GetDiskSerialNumber();//硬盘序列号辅助
            //string boardID = machineCodeUtil.GetBoardID();//主板
            return machineCodeUtil.GetSHA1MachineCode(hardID + cpuID + secondHardID);
        }

        ///获取磁盘序列号 
        /// <summary>
        /// 获取磁盘序列号
        /// </summary>
        private string GetHardDiskID()
        {
            string hardDiskId = string.Empty;
            try
            {
                ManagementObjectSearcher cmicWmi = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                uint tmpUint32 = 0;
                foreach (ManagementObject cmicWmiObj in cmicWmi.Get())
                {
                    tmpUint32 = Convert.ToUInt32(cmicWmiObj["signature"].ToString());
                }
                hardDiskId = tmpUint32.ToString();
            }
            catch { }
            return hardDiskId;
        }

        ///获取cpu序列号
        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        private string GetCPUID()
        {
            string cpuId = string.Empty;
            try
            {
                ManagementObjectSearcher Wmi = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject WmiObj in Wmi.Get())
                {
                    cpuId = WmiObj["ProcessorId"].ToString();
                }
            }
            catch { }
            return cpuId;
        }

        private string GetBoardID()
        {
            string boardID = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                boardID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            moc = null;
            mc = null;
            return boardID.Trim();
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        private string GetDiskSerialNumber()
        {
            string HDid = string.Empty;
            try
            {
                HDid = HardwareInfoHelper.GetHDInfo(0).SerialNumber;//辅助的硬盘ID 用来再次标识是否可用
            }
            catch
            { }
            return HDid;
        }

        ///获取经过SHA1哈希之后的机器码
        /// <summary>
        /// 获取经过SHA1哈希之后的机器码
        /// </summary>
        public string GetSHA1MachineCode(string code)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(code);
            SHA1 sha1 = SHA1.Create();
            return BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "");
        }
    }
}
