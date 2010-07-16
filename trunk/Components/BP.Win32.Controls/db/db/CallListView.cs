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
	public class CallListViewAttr:EntityOIDAttr
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
	public class CallListView :EntityOID
	{
		#region ��������
		/// <summary>
		/// �绰
		/// </summary>
		public string  Tel
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.Tel);
			}
			set
			{
				SetValByKey(CallListViewAttr.Tel,value);
			}
		}
		/// <summary>
		/// �绰����
		/// </summary>
		public string  FK_TelType
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.FK_TelType);
			}
			set
			{
				SetValByKey(CallListViewAttr.FK_TelType,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.FK_Context);
			}
			set
			{
				SetValByKey(CallListViewAttr.FK_Context,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string  FK_ContextText
		{
			get
			{
				return GetValRefTextByKey(CallListViewAttr.FK_Context);
			}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.Note);
			}
			set
			{
				SetValByKey(CallListViewAttr.Note,value);
			}
		} 
		/// <summary>
		/// ������ʱ��
		/// </summary>
		public int CallTime
		{
			get
			{
				return GetValIntByKey(CallListViewAttr.CallTime);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallTime,value);
			}
		}
		/// <summary>
		/// ����״̬
		/// </summary>
		public int CallingState
		{
			get
			{
				return GetValIntByKey(CallListViewAttr.CallingState);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallingState,value);
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public string  CallDateTime
		{
			get
			{
				return GetValStringByKey(CallListViewAttr.CallDateTime);
			}
			set
			{
				SetValByKey(CallListViewAttr.CallDateTime,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ����
		/// </summary>
		public CallListView()
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
				Map map = new Map("CTI_CallListView");
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="�����б�";
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBStringPK(CallListViewAttr.Tel,null,"�绰",true,false,5,50,20);
				map.AddDDLEntitiesNoName(CallListViewAttr.FK_TelType,"0","�绰����", new TelTypes(),true);
				//map.AddDDLEntitiesNoName(CallListViewAttr.FK_Context,"0","��������",new CallContexts(),true);
				map.AddTBInt(CallListViewAttr.CallTime,0,"�Ѻ�������",true,false);
				map.AddDDLSysEnum(CallListViewAttr.CallingState,0,"����״̬",true,false);
				map.AddTBDateTime(CallListViewAttr.CallDateTime,"����ʱ��",true,false);
				map.AddTBStringDoc(CallListViewAttr.Note,null,"��ע",true,false);

				map.AddSearchAttr(CallListViewAttr.FK_TelType);
				map.AddSearchAttr(CallListViewAttr.CallingState);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class CallListViews :EntitiesOID
	{

		#region �õ�һ������
		/// <summary>
		/// �õ�һ������
		/// </summary>
		/// <returns></returns>
		public static CallListView GetCall()
		{
			CallListView cl = new CallListView();
			QueryObject qo = new QueryObject(cl);
			qo.AddWhere(CallListViewAttr.CallingState,0);
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
		/// CallListViews
		/// </summary>
		public CallListViews(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallListView();
			}
		}
		#endregion

		 
	}
}
