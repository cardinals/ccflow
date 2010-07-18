using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GTS
{
	
	/// <summary>
	/// ѧϰ
	/// </summary>
	public class ThemeSortAttr  
	{
		#region ��������
		/// <summary>
		/// ѧ��
		/// </summary>
		public const  string No="No";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Name="Name";
		 
		#endregion	
	}
	/// <summary>
	/// ѧϰ ��ժҪ˵��
	/// </summary>
	public class ThemeSort :EntityNoName
	{
		#region ���캯��
		public ThemeSort()
		{

		}
		public ThemeSort(string no ):base(no)
		{

		}
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
				//return base.HisUAC;
			}
		}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("GTS_ThemeSort");
				map.EnDesc="ѧϰ";	
				map.CodeStruct ="2" ;
				map.IsAllowRepeatNo=false;
				map.IsAutoGenerNo=true;
				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;
				map.EnType=EnType.App;
				
				map.AddTBStringPK(SimpleNoNameFixAttr.No,null,"���",true,true,1,20,4);
				map.AddTBString(SimpleNoNameFixAttr.Name,null,"����",true,false,2,60,200);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		#region ���ػ��෽��
		#endregion 
	
	}
	/// <summary>
	/// ѧϰ 
	/// </summary>
	public class ThemeSorts : EntitiesNoName
	{
		#region ����
		/// <summary>
		/// ѧϰ
		/// </summary>
		public ThemeSorts(){}

		 
		#endregion

		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ThemeSort();
			}
		}	
		#endregion 

		#region ��ѯ����
		 
		#endregion
	}
	
}
