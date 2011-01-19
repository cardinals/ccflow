using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;

namespace BP.Sys
{
	 
	public class UIEnsConditionAttr : EntityClassNameAttr
	{
		public const string PKey="PKey";

	}
	 
	 
	public class UIEnsCondition: EntityClassName
	{
		#region ��������
		public string PKey
		{
			get
			{
				return this.GetValStringByKey(UIEnsConditionAttr.PKey ) ; 
			}
			set
			{
				this.SetValByKey(UIEnsConditionAttr.PKey,value) ; 
			}
		}
		#endregion

		#region ���췽��
		public UIEnsCondition(){}		 
		public UIEnsCondition(string _No ): base(_No){}
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_UIEnsCondition");
				map.EnType=EnType.Sys;
				map.EnDesc="ʵ���ֵ��Ϣ";
				map.DepositaryOfEntity=Depositary.Application;
				map.AddTBStringPK(UIEnsConditionAttr.EnsClassName ,null,"ʵ��������",true,true,2,20,10);
				map.AddTBString(UIEnsConditionAttr.PKey,null,"����ֵ",true,false,2,10,10);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 


	}
	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class UIEnsConditions : Entities
	{		 
		public UIEnsConditions(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new UIEnsCondition();
			}
		}
		
	}
}
