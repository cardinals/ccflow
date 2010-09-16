using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;


namespace BP.GTS
{
	/// <summary>
	/// ����ѡ��������
	/// </summary>
	public class ChoseOneAttr:EntityNoNameAttr
	{
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public const string FK_ThemeSort="FK_ThemeSort";
		/// <summary>
		/// ����ѡ��������
		/// </summary>
		public const string ChoseOneType="ChoseOneType";
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public const string ItemNum="ItemNum";
	}
	/// <summary>
	/// ����ѡ����
	/// </summary>
	public class ChoseOne :ChoseBase
	{
		#region ��չ����
		public string NameHtml
		{
			get
			{
				return "<b><font color=blue  >"+this.Name+"</font> </b>";
			}
		}
		
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
		public int ChoseOneType
		{
			get
			{
				return this.GetValIntByKey(ChoseOneAttr.ChoseOneType);
			}
			set
			{
				this.SetValByKey(ChoseOneAttr.ChoseOneType,value);
			}
		}
		 
		 
		#endregion
	 
		#region ʵ�ֻ����ķ���
		/// <summary>
		/// Ȩ�޿���
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

				Map map = new Map("V_GTS_ChoseOne");
				map.EnDesc="����ѡ����";
				map.CodeStruct="5";
				map.EnType= EnType.Admin;
				map.AddTBStringPK(ChoseOneAttr.No,null,"���",true,true,0,50,20);
				map.AddTBString(ChoseOneAttr.Name,null,"����",true,false,0,100,100);


				map.AddDDLEntities(ChoseOneAttr.FK_ThemeSort,"0001","����ѡ��������",new ThemeSorts(),true);


				//map.AddDtl(new ChoseOneItems(),ChoseOneItemAttr.FK_Chose);
				map.AddSearchAttr( ChoseOneAttr.FK_ThemeSort);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����ѡ����
		/// </summary> 
		public ChoseOne()
		{
		}
		/// <summary>
		/// ����ѡ����
		/// </summary>
		/// <param name="_No">����ѡ������</param> 
		public ChoseOne(string _No ):base(_No)
		{
		}
		#endregion 

		#region �߼�����
		protected override bool beforeUpdate()
		{
			int i = DBAccess.RunSQLReturnValInt("select count(*) from GTS_ChoseOneItem where FK_Chose='"+this.No+"' and isOk=1 ");
			if (i>1)
			{
				this.ChoseOneType=1;
				/*�����ѡ���� */
			}
			else if (i==1) 
			{
				this.ChoseOneType = 0;
			}

			return base.beforeUpdate ();
		}
		/// <summary>
		/// �Զ�����4��ѡ����Ŀ��
		/// </summary> 
		protected override void afterInsert()
		{
			// �Զ�����4��ѡ����Ŀ��
			this.AutoGenerItems();
		
			base.afterInsert ();
		}

		/// <summary>
		/// �Զ�����Item
		/// </summary>
		/// <param name="themeNo"></param>
		public void AutoGenerItems()
		{
 
		}
		#endregion

	}
	/// <summary>
	/// ����ѡ���� ����
	/// </summary>
	public class ChoseOnes :ChoseBases
	{
		/// <summary>
		/// 
		/// </summary>
		public ChoseOnes(string themesort)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(ChoseOneAttr.FK_ThemeSort,themesort);
			qo.DoQuery();
		}
		
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public ChoseOnes(){}
		/// <summary>
		/// ����ѡ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ChoseOne();
			}
		}
	}
}
