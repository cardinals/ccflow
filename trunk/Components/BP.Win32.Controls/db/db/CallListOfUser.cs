using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	 
	/// <summary>
	/// �����б�����
	/// </summary>
	public class CallListOfUserAttr:EntityOIDAttr
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
		public const string CallTime="CallTime";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string CallingState="CallingState";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string CallDateTime="CallDateTime";
		/// <summary>
		/// ��ע1
		/// </summary>
		public const string Note="Note";
		#endregion
	}
	/// <summary>
	/// �����б�
	/// </summary> 
	public class CallListOfUser :EntityOID
	{
		#region ��������
		/// <summary>
		/// �绰
		/// </summary>
		public string  Tel
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.Tel);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.Tel,value);
			}
		}
		/// <summary>
		/// �绰����
		/// </summary>
		public string  FK_TelType
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.FK_Context);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.FK_Context,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string  FK_ContextText
		{
			get
			{
				return GetValRefTextByKey(CallListOfUserAttr.FK_Context);
			}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.Note);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.Note,value);
			}
		} 
		/// <summary>
		/// ������ʱ��
		/// </summary>
		public int CallTime
		{
			get
			{
				return GetValIntByKey(CallListOfUserAttr.CallTime);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallTime,value);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListOfUserAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallingState,value);
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListOfUserAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListOfUserAttr.CallDateTime,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary>
		public CallListOfUser()
		{
		}
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
				Map map = new Map("CTI_CallListOfUser");
				//map.DepositaryOfMap=Depositary.Application;
				//map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="�����б�";
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBStringPK(CallListOfUserAttr.Tel,null,"�绰",true,false,5,50,20);
				map.AddDDLEntitiesNoName(CallListOfUserAttr.FK_TelType,"1","�绰����",new TelTypes(),true);
				map.AddDDLEntitiesNoName(CallListOfUserAttr.FK_Context,"1","��������",new CallContexts(),true);
				//map.AddTBInt(CallListOfUserAttr.CallTime,0,"�Ѻ�������",true,true);
				//map.AddDDLSysEnum(CallListOfUserAttr.CallingState,0,"����״̬",true,true);
				//map.AddTBDateTime(CallListOfUserAttr.CallDateTime,"����ʱ��",true,true);
				//map.AddTBStringDoc(CallListOfUserAttr.Note,null,"��ע",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class CallListOfUsers :EntitiesOID
	{

		#region �õ�һ������
		/// <summary>
		/// �õ�һ������
		/// </summary>
		/// <returns></returns>
		public static CallListOfUser GetCall()
		{
			CallListOfUser cl = new CallListOfUser();
			QueryObject qo = new QueryObject(cl);
			qo.AddWhere(CallListOfUserAttr.CallingState,0);
			if (qo.DoQuery() >= 1)		
			{
				cl.CallDateTime=DataType.SysDataTimeFormat;
				return cl;
			}
			else
				return null;
		}
		#endregion

		#region ����
		#endregion 

		#region ���췽������
		/// <summary>
		/// CallListOfUsers
		/// </summary>
		public CallListOfUsers(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallListOfUser();
			}
		}
		#endregion
	}
}
