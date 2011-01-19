using System;
using System.Text;

namespace BP.DA
{
	 
	/// <summary>
	/// �ַ������ʽ����ʵ��,���ؼ������ַ�����
	/// ���ڣ�2005-05-17
	/// </summary>
	public class Calculate
	{
		public bool IsNumber(string s)
		{
			try
			{
				decimal i= decimal.Parse(s);
				return true;
			}
			catch
			{
				return false;
			}
		}
		private clsStack S=new clsStack();
		public Calculate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
  
  
		/// <summary>
		/// �������ֱ��ʽ�ַ������飬���ؼ������ַ�����
		/// </summary>
		/// <param name="strSoure">strSour ��׺���ʽ�ַ�����ͷ��û�С�#����β����Ҫ���ϡ�#��</param>
		/// <returns>������</returns>
		public string[] Run(string[] strSoure)
		{
			if(strSoure==null)
			{
				return null;
			}
			string[] dRtn=new string[strSoure.Length];
			for(int k=0;k<strSoure.Length;k++)
			{
				string[] ATemp;
				string strRPN;
				strRPN=GetRPN(strSoure[k]);
				try
				{
					ATemp=strRPN.Trim().Split(' ');
					for(int i=0;i<ATemp.Length;i++)
					{
						if(this.IsNumber(ATemp[i]))
							S.Push(ATemp[i]);
						else
							DoOperate(ATemp[i]);
					}
   
					dRtn[k]=S.Pop();
				}
				catch{}
			}
			return dRtn;
		}
		/// <summary>
		///  Run ���غ�׺���ʽ
		///  strSour ��׺���ʽ�ַ�����ͷ��û�С�#����β����Ҫ���ϡ�#��
		///  String  ��׺���ʽ�ַ�����ͷβ��û�С�#��
		/// </summary>
		/// <param name="strSource"></param>
		/// <returns></returns>
		private string GetRPN(string strSource)
		{
			string[] ATemp;
			string strRPN="",Y;
			ATemp=strSource.Trim().Split(' ');
			S.Initialize(ATemp.Length);
			S.MakeEmptly();
			S.Push("#");
			try
			{
				for(int k=0;k<ATemp.Length;k++)
				{
					//����
					if(this.IsNumber(ATemp[k]))
					{
						strRPN += " "+ATemp[k];
					}
						//�ַ�
					else 
					{
						if(ATemp[k]==")")
						{
							do
							{
								Y=S.Pop();
								if(Y!="(")
									strRPN += " "+Y;
							}
							while(Y.Trim()!="(");
						}
						else
						{
							do
							{
								Y = S.Pop();
								if (GetISP(Y) > GetICP(ATemp[k])) 
									strRPN += " "+Y;
							}
							while(GetISP(Y) > GetICP(ATemp[k]));
							S.Push(Y);
							S.Push(ATemp[k]);
						}
					}
				}
				do
				{
					Y=S.Pop();
					if(Y!="#")
						strRPN+=" "+Y;
				}
				while(Y!="#");
			}
			catch{}
			return strRPN;
		}

		#region ��������ȼ�����
		private enum isp
		{
			s35 = 0,
			s40 = 1,
			s94 = 7,
			s42 = 5,
			s47 = 5,
			s37 = 5,
			s43 = 3,
			s45 = 3,
			s41 = 8
		}
		private enum icp
		{
			s35 = 0,
			s40 = 8,
			s94 = 6,
			s42 = 4,
			s47 = 4,
			s37 = 4,
			s43 = 2,
			s45 = 2,
			s41 = 1
		}
		private int GetISP(string a1)
		{
			Encoding ascii =Encoding.ASCII;
			byte[] a=ascii.GetBytes(a1);
			switch(Convert.ToInt32(a[0]))
			{
				case 35:
					return (int)isp.s35;

				case 40:
					return (int)isp.s40;
     
				case 94:
					return (int)isp.s94;
     
				case 42:
					return (int)isp.s42;
     
				case 47:
					return (int)isp.s47;
     
				case 37:
					return (int)isp.s37;
     
				case 43:
					return (int)isp.s43;
     
				case 45:
					return (int)isp.s45;
     
				case 41:
					return (int)isp.s41;
				default:
					return (int)isp.s35;
			}
   
		}
		private int GetICP(string a1)
		{
			Encoding ascii =Encoding.ASCII;
			byte[] a=ascii.GetBytes(a1);
			switch(Convert.ToInt32(a[0]))
			{
				case 35:
					return (int)icp.s35;
     
				case 40:
					return (int)icp.s40;
     
				case 94:
					return (int)isp.s94;
     
				case 42:
					return (int)icp.s42;
     
				case 47:
					return (int)icp.s47;
     
				case 37:
					return (int)icp.s37;
     
				case 43:
					return (int)icp.s43;
     
				case 45:
					return (int)icp.s45;
     
				case 41:
					return (int)icp.s41;
				default:
					return (int)icp.s35;
    
			}
   
		}
		#endregion

		/// <summary>
		/// �ж��Ƿ�����������֣����Ҹ���
		/// </summary>
		/// <param name="dLeft">����ֵ</param>
		/// <param name="dRight">�ҵ���ֵ</param>
		/// <returns>�Ƿ�ɹ�</returns>
		private bool GetTwoItem(ref decimal dLeft,ref decimal dRight)
		{
			bool bRtn=true;
			try
			{
				if(S.IsEmptly())
					return false;
				else
					dRight = Convert.ToDecimal(S.Pop());
				if(S.IsEmptly())
					return false;
				else
					dLeft = Convert.ToDecimal(S.Pop());
			}
			catch
			{

			}
			return bRtn;
		}
		/// <summary>
		/// ����������ż��㣬���ҰѼ��������ַ���ʽ�����ջ
		/// </summary>
		/// <param name="op"></param>
		private void DoOperate(string op)
		{
			decimal NumLeft=0,NumRight=0;
			bool r;
			r=GetTwoItem(ref NumLeft,ref NumRight);
			if(r)
			{
				switch(op.Trim())
				{
					case "+":
						S.Push((NumLeft+NumRight).ToString());
						break;
					case "-":
						S.Push((NumLeft-NumRight).ToString());
						break;
					case "*":
						S.Push((NumLeft*NumRight).ToString());
						break;
					case "/":
						if(NumRight==0)
							S.Push("0");
						else
							S.Push((NumLeft/NumRight).ToString());
						break;

				}
			}
		}

	}

}

