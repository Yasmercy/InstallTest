using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MyApp
{
    public class IniManager
    {
        private string filePath;
        private StringBuilder result;
        private int bufferSize;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        public IniManager(string iniPath)
        {
            filePath = iniPath;
            bufferSize = 512;
            result = new StringBuilder(bufferSize);
        }

        /// <summary>
        /// 讀取ini
        /// </summary>
        /// <param name="section"> ini 裡面的 [ ] 字串 </param>
        /// <param name="key"></param>
        /// <param name="defaultValue"> 如果沒找到 section 和 key 會回傳的 預設值</param>
        /// <returns></returns>
        public string ReadIniFile(string section, string key, string defaultValue)
        {
            result.Clear();
            GetPrivateProfileString(section, key, defaultValue, result, bufferSize, filePath);
            return result.ToString();
        }

        /// <summary>
        /// 寫入ini 
        /// </summary>
        /// <param name="section"> ini 裡面的 [ ] 字串</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteIniFile(string section, string key, Object value)
        {
            WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
    }
}
