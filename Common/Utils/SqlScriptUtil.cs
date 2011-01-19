using System;
using System.Collections.Generic;
using System.Text;

namespace Tax666.Common
{
    public class SqlScriptUtil
    {
        #region ��ʽ���ַ���,����SQL���
        /// <summary>
        /// ��ʽ���ַ���,����SQL���
        /// </summary>
        /// <param name="formatStr">��Ҫ��ʽ�����ַ���</param>
        /// <returns>�ַ���</returns>
        public static string inSQL(string formatStr)
        {
            string rStr = formatStr;
            if (formatStr != null && formatStr != string.Empty)
            {
                rStr = rStr.Replace("'", "''");
                //rStr = rStr.Replace("\"", "\"\"");
            }
            return rStr;
        }
        /// <summary>
        /// ��ʽ���ַ���,��inSQL�ķ���
        /// </summary>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string outSQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null)
            {
                rStr = rStr.Replace("''", "'");
                rStr = rStr.Replace("\"\"", "\"");
            }
            return rStr;
        }

        /// <summary>
        /// ��ѯSQL���,ɾ��һЩSQLע������
        /// </summary>
        /// <param name="formatStr">��Ҫ��ʽ�����ַ���</param>
        /// <returns></returns>
        public static string querySQL(string formatStr)
        {
            string rStr = formatStr;
            if (rStr != null && rStr != "")
            {
                rStr = rStr.Replace("'", "");
            }
            return rStr;
        }
        #endregion
    }
}
