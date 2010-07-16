using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// ����б�����
	/// </summary>
	public class CTIReleaseAttr:EntityOIDAttr
	{
		#region ����
		/// <summary>
		/// tel
		/// </summary>
		public const string Tel="Tel";
		/// <summary>
		/// ����ʱ���
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// ����ʱ�䵽
		/// </summary>
		public const string FK_Context="FK_Context";
		/// <summary>
		/// ��ߺ��д���
		/// </summary>
		public const string CTITime="CTITime";
		/// <summary>
		/// ��ע1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// ����б�
	/// </summary> 
	public class CallRelease :Entity
	{
		#region ��������
		 
		public string  Note
		{
			get
			{
				return GetValStringByKey(CTIReleaseAttr.Note);
			}
			set
			{
				SetValByKey(CTIReleaseAttr.Note,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public CallRelease(){}
		#endregion 

		#region Map
		/// <summary>
		/// EnMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("CTI_Release");	
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;

				//				map.CodeStruct="3";
				map.EnDesc="����б�";                
			 
				map.AddTBStringPK(CTIReleaseAttr.Tel,null,"�绰",true,false,5,50,20);
				map.AddTBStringDoc(CTIReleaseAttr.Note,null,"��ע",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

		protected override void afterInsert()
		{
			base.afterInsert ();
			DBAccess.RunSQL("delete CTI_CallList where tel in (select tel from CTI_Release)");
		}

	}
	/// <summary>
	/// ����б�s
	/// </summary>
	public class CallReleases :EntitiesOID
	{
		#region ����
		 
		#endregion 

		#region ���췽������
		/// <summary>
		/// CTIReleases
		/// </summary>
		public CallReleases(){}
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallRelease();
			}
		}
		#endregion
	}
}
