using System;

namespace BP.Win32.Controls.CTI.App
{
	/// <summary>
	/// MakeMoney ��ժҪ˵����
	/// </summary>
	public class MakeMoney
	{
		 
		public MakeMoney()
		{
			 
		}

		public string[] GetMoney(double money)
		{
			double	fraction;
			int	RmbJiao, RmbFen, Rmb;
			double	RmbYuan;

			if (money >= 0)
				money = money + 0.005;          /* �������봦�� */
			else
				money = money - 0.005;

			fraction = Math.Abs( Math.mod modf(money, &RmbYuan));	/* ������Ϊ������С������ */

			Rmb = floor(fraction*100.0);    /* С������ȫ���'��' */
			RmbJiao = floor(Rmb/10);        /* �ǵ�ֵ */
			RmbFen = Rmb - RmbJiao*10;      /* �ֵ�ֵ */

			/* �����ַ��� MoneyString ����'Ԫ�Ƿ�'����������: */

			if (RmbYuan) 
			{
				TV_MakeSentence (RmbYuan, MoneyString);  /* ���������������� */
				//		StringLen = strlen (MoneyString);
				MoneyString[StringLen++] = UCN_YUAN;     /* ����'Ԫ' */
			}
			else 
			{
				StringLen = 0;
				if (money < 0.0 && fabs(money) >= 0.01)	 /* ��Ϊ��, ����'��' */
					MoneyString[StringLen++] = CN_NEGATIVE;
			}
			if (!RmbYuan && !RmbJiao && !RmbFen) 
			{  /* 0 Ԫ 0 �� 0 �� */
				MoneyString[StringLen++] = CN_DIGIT0;     /* ֻ�� '0 ��' */
				MoneyString[StringLen++] = UCN_FEN;
			}
			if (RmbYuan && !RmbJiao && RmbFen)      /* n Ԫ 0 �� m �� */
				MoneyString[StringLen++] = CN_DIGIT0;
			if (RmbJiao) 
			{                          /* n �� */
				MoneyString[StringLen++] = CN_DIGIT0 + RmbJiao;
				MoneyString[StringLen++] = UCN_JIAO;
			}
			if (RmbFen) 
			{                           /* n �� */
				MoneyString[StringLen++] = CN_DIGIT0 + RmbFen;
				MoneyString[StringLen++] = UCN_FEN;
			}
			MoneyString[StringLen++] = 0;
			return StringLen;
		}

	}
}
