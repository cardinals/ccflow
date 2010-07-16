using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	 
	/// <summary>
	/// �绰��������
	/// </summary>
	public class TelTypeOfFitAttr:EntityOIDNameAttr
	{
		#region ���� 
		#endregion		
	}
	/// <summary>
	/// �绰����
	/// </summary> 
	public class TelTypeOfFit :EntityOIDName
	{
		#region ��������		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// �绰����
		/// </summary>
		public TelTypeOfFit(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="no"></param>
		public TelTypeOfFit(int no):base(no){}
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
				Map map = new Map("V_CTI_TelTypeOfFit");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
				map.EnType=EnType.View; 
				map.EnDesc="�ʺ��û�����";
				 
				map.AddTBIntPKOID();
				map.AddTBString(TelTypeOfFitAttr.Name,null,"����",true,false,1,100,4);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �绰����
	/// </summary>
	public class TelTypeOfFits :EntitiesOIDName
	{
		#region ����
		#endregion 

		#region ���췽������
		/// <summary>
		/// TelTypeOfFits
		/// </summary>
		public TelTypeOfFits(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TelTypeOfFit();
			}
		}
		#endregion
	}
}
