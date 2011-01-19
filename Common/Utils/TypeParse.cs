using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Tax666.Common
{
    public class TypeParse
    {
        /// <summary>
        /// �ж϶����Ƿ�ΪInt32���͵�����
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;

        }


        public static bool IsDouble(object Expression)
        {
            if (Expression != null)
            {
                return Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// string��ת��Ϊbool��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����bool���ͽ��</returns>
        public static bool StrToBool(object Expression, bool defValue)
        {
            if (Expression != null)
            {
                if (string.Compare(Expression.ToString(), "true", true) == 0)
                {
                    return true;
                }
                else if (string.Compare(Expression.ToString(), "false", true) == 0)
                {
                    return false;
                }
            }
            return defValue;
        }

        /// <summary>
        /// ������ת��ΪInt32����
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static int StrToInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        /// string��ת��Ϊfloat��
        /// </summary>
        /// <param name="strValue">Ҫת�����ַ���</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>ת�����int���ͽ��</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                {
                    intValue = Convert.ToSingle(strValue);
                }
            }
            return intValue;
        }


        /// <summary>
        /// �жϸ������ַ�������(strNumber)�е������ǲ��Ƕ�Ϊ��ֵ��
        /// </summary>
        /// <param name="strNumber">Ҫȷ�ϵ��ַ�������</param>
        /// <returns>���򷵼�true �����򷵻� false</returns>
        public static bool IsNumericArray(string[] strNumber)
        {
            if (strNumber == null)
            {
                return false;
            }
            if (strNumber.Length < 1)
            {
                return false;
            }
            foreach (string id in strNumber)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;
        }

        ///   <summary> 
        ///   �ж�һ���ַ����Ƿ��������͵��ַ��� 
        ///   </summary> 
        ///   <param name= "strValue "> �ַ��� </param> 
        ///   <returns> ���򷵻�true�����򷵻�false </returns> 
        public static bool IsUnsign(string strValue)
        {
            int ret;
            if (strValue.Substring(0, 1) == "-")
            {
                return false;
            }
            else
            {
                return int.TryParse(strValue, out ret);
            }
        }

        ///   <summary> 
        ///   �ж�һ���ַ����Ƿ��������͵��ַ��� 
        ///   </summary> 
        ///   <param name= "strValue "> �ַ��� </param> 
        ///   <returns> ���򷵻�true�����򷵻�false </returns> 
        public static bool IsUnsignDouble(string strValue)
        {
            double ret;
            if (strValue.Substring(0, 1) == "-")
            {
                return false;
            }
            else
            {
                return double.TryParse(strValue, out ret);
            }
        }

        /// <summary>
        /// ���ַ�������ת���ַ���
        /// </summary>
        /// <param name="strArray">�ַ�������</param>        
        /// <param name="Separator">�ָ���</param>
        /// <returns>�ַ���</returns>
        public static string StringArrayToString(string[] strArray, char Separator)
        {
            string str = string.Empty;
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + strArray[i] + Separator;
            }
            return str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// �ж��ַ����Ƿ����ַ��������д���
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="strArray">�ַ�������</param>
        /// <returns>���򷵼�true �����򷵻� false</returns>
        public static bool IsStringArray(string str, string[] strArray)
        {
            bool s = false;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == str)
                {
                    s = true;
                }
            }
            return s;
        }

        /// <summary>
        /// �ж��ַ������ַ��������е�λ��
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <param name="strArray">�ַ�������</param>
        /// <returns>����λ��</returns>
        public static int ReturnStringSeat(string str, string[] strArray)
        {
            int ret = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i] == str)
                {
                    ret = i;
                }
            }
            return ret;
        }

        /// <summary>
        /// ȡ���������ֵ
        /// </summary>
        /// <param name="n">����</param>
        /// <returns></returns>
        public static int MaxInt(int[] n)
        {
            int ret = 0;
            if (n[0] != 0)
            {
                ret = n[0];
            }
            for (int i = 1; i < n.Length; i++)
            {
                if (n[i] > ret)
                {
                    ret = n[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// ȡ���������ֵ
        /// </summary>
        /// <param name="n">����</param>
        /// <returns></returns>
        public static string MaxStr(string[] str)
        {
            string ret = "0";
            if (str[0] != null)
            {
                ret = str[0];
            }
            for (int i = 1; i < str.Length; i++)
            {
                if (int.Parse(str[i]) > int.Parse(ret))
                {
                    ret = str[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// ȡ��������Сֵ
        /// </summary>
        /// <param name="n">����</param>
        /// <returns></returns>
        public static int MinInt(int[] n)
        {
            int ret = 0;
            if (n[0] != 0)
            {
                ret = n[0];
            }
            for (int i = 1; i < n.Length; i++)
            {
                if (n[i] < ret)
                {
                    ret = n[i];
                }
            }
            return ret;
        }

        /// <summary>
        /// ȡ��������Сֵ
        /// </summary>
        /// <param name="n">����</param>
        /// <returns></returns>
        public static string MinStr(string[] str)
        {
            string ret = "0";
            if (str[0] != null)
            {
                ret = str[0];
            }
            for (int i = 1; i < str.Length; i++)
            {
                if (int.Parse(str[i]) < int.Parse(ret))
                {
                    ret = str[i];
                }
            }
            return ret;
        }

    }
}
