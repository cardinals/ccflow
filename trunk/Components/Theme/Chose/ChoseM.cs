using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ����ѡ����
	/// </summary>
	public class ChoseMAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ����ѡ��������
		/// </summary>
		public const string ChoseMType="ChoseMType";
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// ����ѡ����
	/// </summary>
	public class ChoseM :ChoseBase
	{
		public string NameHtml
		{
			get
			{
				return "<b><font color=blue>"+this.Name+"</font></b>";
			}
		}
		#region attrs
		public ChoseItems HisChoseItems
		{
			get
			{
				return  new ChoseItems(this.No);
			}
		}
		public ChoseItems HisChoseItemsOfRight
		{
			get
			{
				ChoseItems cts=  new ChoseItems();
				cts.RetrieveRightItems(this.No);
				return cts;

			}
		}
		/// <summary>
		/// ����ѡ��������
		/// </summary>
		public int ChoseMType
		{
			get
			{
				return this.GetValIntByKey(ChoseMAttr.ChoseMType);
			}
			set
			{
				this.SetValByKey(ChoseMAttr.ChoseMType,value);
			}
		}
		/// <summary>
		/// FK_ThemeSort
		/// </summary>
		public string FK_ThemeSort
		{
			get
			{
				return this.GetValStringByKey( ChoseMAttr.FK_ThemeSort);
			}
			set
			{
				this.SetValByKey(ChoseMAttr.FK_ThemeSort,value);
			}
		}
		 
		 
		#endregion
	 
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// power
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uc = new UAC();
				uc.OpenForSysAdmin();
				return uc;
				 
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

				Map map = new Map("V_GTS_ChoseM");
				map.EnDesc="����ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(ChoseMAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(ChoseMAttr.Name,null,"����",true,false,0,100,100);
				map.AddDDLEntities(ChoseMAttr.FK_ThemeSort,"0001","����ѡ��������",new ThemeSorts(),true);

				//map.AddDtl(new ChoseMItems(),ChoseMItemAttr.FK_Chose);
				map.AddSearchAttr( ChoseMAttr.FK_ThemeSort);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����ѡ����
		/// </summary> 
		public ChoseM()
		{
		}
		/// <summary>
		/// ����ѡ����
		/// </summary>
		/// <param name="_No">����ѡ������</param> 
		public ChoseM(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		protected override bool beforeUpdate()
		{
			int i = DBAccess.RunSQLReturnValInt("select count(*) from GTS_ChoseMItem where FK_Chose='"+this.No+"' and isOk=1 ");
			if (i>1)
			{
				this.ChoseMType=1;
				/*�������ѡ���� */
			}
			else if (i==1) 
			{
				this.ChoseMType = 0;
			}

			return base.beforeUpdate ();
		}
		 

		 
		#endregion

	 
	}
	/// <summary>
	/// ����ѡ����
	/// </summary>
	public class ChoseMs :ChoseBases
	{
		public ChoseMs(string themesort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(ChoseOneAttr.FK_ThemeSort,themesort);
			qo.DoQuery();
		}
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public ChoseMs(){}
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ChoseM();
			}
		}
	}
}
