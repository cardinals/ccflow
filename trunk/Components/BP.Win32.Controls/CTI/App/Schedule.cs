using System;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// ʱ�������
	/// </summary>
	public class ScheduleAttr
	{
		#region ����
		/// <summary>
		/// �·�
		/// </summary>
		public const string FK_YF="FK_YF";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Context="FK_Context";

		/// <summary>
		/// ��ʼ����
		/// </summary>
		public const string DateFrom="DateFrom";
		/// <summary>
		/// ƽ������
		/// </summary>
		public const string DateTo="DateTo";	 
		/// <summary>
		/// Сʱ
		/// </summary>
		public const string RunHH="RunHH";
		/// <summary>
		/// ����
		/// </summary>
		public const string Runmm="Runmm";

		/// <summary>
		/// ��ע
		/// </summary>
		public const string Note="Note";

		#endregion
	}
	/// <summary>
	/// ʱ���
	/// </summary> 
	public class Schedule :EntityOIDName
	{
		#region ��������
		public BP.CTI.App.CallContext HisCallContext
		{
			get
			{
				return new CallContext(this.FK_Context);
			}
		}
		public string  FK_Context
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.FK_Context);
			}
			set
			{
				SetValByKey(ScheduleAttr.FK_Context,value);
			}
		} 
		public string  FK_YF
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.FK_YF);
			}
			set
			{
				SetValByKey(ScheduleAttr.FK_YF,value);
			}
		} 
		 
		public string  DateTo
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.DateTo);
			}
			set
			{
				SetValByKey(ScheduleAttr.DateTo,value);
			}
		}
		public string  DateFrom
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.DateFrom);
			}
			set
			{
				SetValByKey(ScheduleAttr.DateFrom,value);
			}
		}

		public string  RunHH
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.RunHH);
			}
			set
			{
				SetValByKey(ScheduleAttr.RunHH,value);
			}
		}
		public string  Runmm
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.Runmm);
			}
			set
			{
				SetValByKey(ScheduleAttr.Runmm,value);
			}
		}
		public string  Note
		{
			get
			{
				return GetValStringByKey(ScheduleAttr.Note);
			}
			set
			{
				SetValByKey(ScheduleAttr.Note,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ʱ���
		/// </summary>
		public Schedule(){}
		public Schedule(string day){}

		 
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
				Map map = new Map("CTI_Schedule");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;
				//				map.CodeStruct="3";
				map.EnDesc="ʱ���";                
				 

				map.AddTBIntPKOID();
				map.AddDDLEntitiesNoName(ScheduleAttr.FK_YF, DataType.CurrentMonth,"�·�", new Pub.YFs(),true);
				map.AddDDLEntitiesNoName(ScheduleAttr.FK_Context,"00","��������", new CallContexts(),true);
				map.AddTBString(ScheduleAttr.DateFrom,"10","���ڴ�",true,false,2,2,2);
				map.AddTBString(ScheduleAttr.DateTo,"20","��",true,false,2,2,2);				 
				map.AddTBString(CallContextAttr.Name,null,"��ע",true,false,0,50,20);

				//map.AddTBStringDoc(ScheduleAttr.Note,null,"��ע",true,false);

				map.AddSearchAttr(CallContextAttr.FK_YF);				

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class Schedules :EntitiesOIDName
	{
		public static Schedule CurrentSchedule
		{
			get
			{
				Schedules ens = new Schedules();
				QueryObject qo = new QueryObject(ens);
				qo.AddWhere(ScheduleAttr.DateFrom, "<=" ,DateTime.Now.Day);
				qo.addAnd();
				qo.AddWhere(ScheduleAttr.DateTo, ">=" ,DateTime.Now.Day);
				qo.addAnd();
				qo.AddWhere(CallContextAttr.FK_YF, "=" ,DataType.CurrentMonth);
				int i= qo.DoQuery();

				if (i==0)
					return null;
				else
					return (Schedule)ens[0];
			}
		}

		#region ���췽������
		/// <summary>
		/// Schedules
		/// </summary>
		public Schedules(){}
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Schedule();
			}
		}
		#endregion
	}
}
