using System;
using System.Management;
using System.Threading.Tasks;

namespace RussianTasks.src.utils
{
    class Utils
    {
        public static string getApplicationVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        public static async Task getMachineUniqueIdAsync(Action<string> callback)
        {
            string result = await Task.Run(new Func<string>(getMachineUniqueId));
            callback?.Invoke(result);
        }

        static string machineId = null;
        public static string getMachineUniqueId()
        {
            if (machineId != null)
            {
                return machineId;
            }
            else
            {
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }

                string drive = System.IO.Path.GetPathRoot(Environment.SystemDirectory).Split(':')[0];
                ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                dsk.Get();
                string volumeSerial = dsk["VolumeSerialNumber"].ToString();
                machineId = string.Concat(cpuInfo, volumeSerial);
                return machineId;
            }
        }


        public static void encode(ref byte[] input, byte val = 0xA2)
        {
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = (byte)(input[i] ^ val);
            }
        }


        public static string createMD5ForString(string input)
        {
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return createMD5(inputBytes);
        }


        public static string createMD5(byte[] inputBytes)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp, bool toLocalTime = false)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            if (toLocalTime)
            {
                dateTime = dateTime.ToLocalTime();
            }
            return dateTime;
        }

        public static long getCurrentTimestamp()
        {
            return (long)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}
