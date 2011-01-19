using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace Tax666.Common
{
    public class CommonUtil
    {
        #region ����0-9�����
        /// <summary>
        /// ����0-9�����
        /// </summary>
        /// <param name="VcodeNum">���ɳ���</param>
        /// <returns></returns>
        public static string RndNum(int VcodeNum)
        {
            StringBuilder sb = new StringBuilder(VcodeNum);
            Random rand = new Random();
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                int t = rand.Next(9);
                sb.AppendFormat("{0}", t);
            }
            return sb.ToString();

        }
        #endregion

        #region �����־�ļ����Ŀ¼
        /// <summary>
        /// �����־�ļ����Ŀ¼
        /// </summary>
        public static string LogDir
        {
            get
            {
                string LogFilePath = WebRequests.GetPhysicsPath(ConfigurationManager.AppSettings["LogDir"]);
                if (!Directory.Exists(LogFilePath))
                    Directory.CreateDirectory(LogFilePath);
                return LogFilePath;
            }
        }
        #endregion

        #region ��û���������(�����ռ�)
        /// <summary>
        /// ��û���������(�����ռ�)
        /// </summary>
        public static string GetCachenamespace
        {
            get
            {
                return ConfigurationManager.AppSettings["Cachenamespace"];
            }
        }
        #endregion

        #region ��û���������(����)
        /// <summary>
        /// ��û���������(����)
        /// </summary>
        public static string GetCacheclassName
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheclassName"];
            }
        }
        #endregion 

    }
}
