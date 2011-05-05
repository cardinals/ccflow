using System;

namespace BP.DA
{
	/// <summary>
	/// ջ���趨��
	/// ���ڣ�2005-05-17
	/// </summary>
    public class clsStack
    {
        private long Top;    //ջ������
        private int MaxSize; // MaxSize ջ������
        private string[] Element;
        public clsStack()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
            Top = -1;
        }
        /// <summary>
        /// �趨ջ���������
        /// </summary>
        /// <param name="Size"></param>
        public void Initialize(int Size)
        {
            MaxSize = Size;
            Element = new string[Size];
        }
        /// <summary>
        /// ��ջ
        /// </summary>
        /// <param name="strItem"></param>
        public void Push(string strItem)
        {
            if (!IsFull())
            {
                Top = Top + 1;
                Element[Top] = strItem;
            }
        }
        /// <summary>
        /// ��ջ
        /// </summary>
        /// <returns></returns>
        public string Pop()
        {
            string strRtn = " ";
            if (!IsEmptly())
            {
                strRtn = Element[Top];
                Top = Top - 1;
            }
            return strRtn;
        }
        public string GetTop()
        {
            string strRtn = " ";
            if (!IsEmptly())
            {
                strRtn = Element[Top];
            }
            return strRtn;
        }
        public bool IsFull()
        {
            bool IsFull = Top == (MaxSize - 1) ? true : false;
            return IsFull;
        }
        public void MakeEmptly()
        {
            Top = -1;
        }
        public bool IsEmptly()
        {
            bool IsEmptly = Top == -1 ? true : false;
            return IsEmptly;
        }
    }
}

