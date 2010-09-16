using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ѡ�����������
	/// </summary>
	public class ChoseBaseAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ѡ�������
		/// </summary>
		public const string FK_ChoseBase="FK_ChoseBase";
		/// <summary>
		/// ѡ�����������
		/// </summary>
		public const string ChoseBaseType="ChoseBaseType";
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// ѡ�������
	/// </summary>
	abstract public class ChoseBase :EntityNoName
	{
		#region attrs
		public static string GenerStr(string name)
		{
			char[] chars = name.ToCharArray();
			int i=0;
			string str="";
			foreach(char c in chars)
			{
				i++;
				str+=c.ToString();
				if (i==50)
				{
					str+="\n";
					i=0;
				}
			}
			if (str.Length < 49)
				str=str.PadLeft( 50 - str.Length, ' ' ) ;
			return str;
		}
		public string NameExt
		{
			get
			{
				return ChoseBase.GenerStr(this.Name);
				
			}
		}
		#endregion
	 
		#region ʵ�ֻ����ķ���
		 
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ѡ�������
		/// </summary> 
		public ChoseBase()
		{
		}
		/// <summary>
		/// ѡ�������
		/// </summary>
		/// <param name="_No">ѡ���������</param> 
		public ChoseBase(string _No ):base(_No)
		{
		}
		#endregion 
	 
	}
	/// <summary>
	/// ѡ�������
	/// </summary>
	abstract public class ChoseBases :EntitiesNoName
	{
		/// <summary>
		/// ѡ�������
		/// </summary>
		public ChoseBases(){}

		public int Search(string sort, string fk_emp, int sxcd)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(FillBlankAttr.FK_ThemeSort,sort);
			if (sxcd==4)
			{
				/* Ҫ��ѯȫ����*/
				//qo.AddWhereNotInSQL(FillBlankAttr.No, "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.ChoseOne+"'"  );
			}
			else
			{
				qo.addAnd();
				qo.AddWhereInSQL(FillBlankAttr.No,    "SELECT FK_Theme FROM GTS_Study WHERE FK_Emp='"+fk_emp+"' AND FK_ThemeType='"+ThemeType.ChoseOne+"' AND SXCD="+sxcd );
			}
			return qo.DoQuery();
		}
		 
	}
}
