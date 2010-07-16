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
	public class TelTypeAttr:EntityOIDNameAttr
	{
		#region ���� 
		/// <summary>
		/// ����ʱ���
		/// </summary>
		public const string FromTime0="FromTime0";
		/// <summary>
		/// ����ʱ�䵽
		/// </summary>
		public const string ToTime0="ToTime0";
		/// <summary>
		/// ����ʱ���
		/// </summary>
		public const string FromTime1="FromTime1";
		/// <summary>
		/// ����ʱ�䵽
		/// </summary>
		public const string ToTime1="ToTime1";
		/// <summary>
		/// ��ߺ��д���
		/// </summary>
		public const string MaxCallTime="MaxCallTime";
		/// <summary>
		/// ���峤��
		/// </summary>
		public const string RingLong="RingLong";
		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note="Note";
		#endregion		
	}
	/// <summary>
	/// �绰����
	/// </summary> 
	public class TelType :EntityOIDName
	{
		#region ��������
		/// <summary>
		/// ����ʱ�䡣
		/// </summary>
		public string  FromTime0
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.FromTime0);
			}
			set
			{
				SetValByKey(TelTypeAttr.FromTime0,value);
			}
		} 
		public string  FromTime1
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.FromTime1);
			}
			set
			{
				SetValByKey(TelTypeAttr.FromTime1,value);
			}
		} 
		/// <summary>
		/// ���峤��
		/// </summary>
		public int  RingLong
		{
			get
			{
				return GetValIntByKey(TelTypeAttr.RingLong);
			}
			set
			{
				SetValByKey(TelTypeAttr.RingLong,value);
			}
		} 
		public int  MaxCallTime
		{
			get
			{
				return GetValIntByKey(TelTypeAttr.MaxCallTime);
			}
			set
			{
				SetValByKey(TelTypeAttr.MaxCallTime,value);
			}
		} 
		public string  Note
		{
			get
			{
				return GetValStringByKey(TelTypeAttr.Note);
			}
			set
			{
				SetValByKey(TelTypeAttr.Note,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// �绰����
		/// </summary>
		public TelType(){}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="no"></param>
		public TelType(int no):base(no){}

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
				Map map = new Map("CTI_TelType");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;

				map.CodeStruct="1";
				map.EnDesc="�û���������";
			 
				map.AddTBIntPK("OID",0,"ID",true,false);
				map.AddTBString(TelTypeAttr.Name,null,"����",true,false,1,100,4);
				map.AddTBString(TelTypeAttr.FromTime0,"08:00","ʱ��1��",true,false,5,5,5);
				map.AddTBString(TelTypeAttr.ToTime0,"12:00","��",true,false,5,5,5);

				map.AddTBString(TelTypeAttr.FromTime1,"14:00","ʱ��2��",true,false,5,5,5);
				map.AddTBString(TelTypeAttr.ToTime1,"20:00","��",true,false,5,5,5);

				map.AddTBInt(TelTypeAttr.MaxCallTime,10,"��ߺ��д���",true,false);
				map.AddTBInt(TelTypeAttr.RingLong,20,"���峤����",true,false);
				map.AddTBStringDoc(TelTypeAttr.Note,null,"��ע",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �绰����
	/// </summary>
	public class TelTypes :EntitiesOIDName
	{
		#region ����
		 
		#endregion 

		#region ���췽������
		/// <summary>
		/// TelTypes
		/// </summary>
		public TelTypes(){}
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TelType();
			}
		}
		#endregion
	}
}
