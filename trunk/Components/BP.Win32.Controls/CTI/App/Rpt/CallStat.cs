using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.Rpt;

namespace BP.CTI.App
{
	/// <summary>
	/// ����ͳ������
	/// </summary>
	public class CallStatAttr:EntityOIDAttr
	{
		#region ����
		/// <summary>
		/// ��������
		/// </summary>
		public const string CallDate="CallDate";
		/// <summary>
		/// ��������
		/// </summary>
		public const string Num="Num";
		/// <summary>
		/// �绰����
		/// </summary>
		public const string FK_TelType="FK_TelType";
		/// <summary>
		/// ����״̬
		/// </summary>
		public const string CallingState="CallingState";
		#endregion
	}
	/// <summary>
	/// ����ͳ��
	/// </summary> 
	public class CallStat :Entity
	{
		#region ��������
		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public CallStat(){}
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
				Map map = new Map("V_CTI_CallStat");	
				map.DepositaryOfMap=Depositary.Application;
				map.EnType=EnType.View;
				//				map.CodeStruct="3";
				map.EnDesc="����ͳ��";                
 

				//map.AddTBStringPK(CallStatAttr.CallDate,null,"����",true,false,5,100,4);
				map.AddDDLEntitiesNoNamePK(CallListAttr.CallDate,null,"����",new BP.Pub.Days(),false);


				map.AddDDLEntities(CallListAttr.FK_TelType,0,DataType.AppInt,"�绰����",new TelTypes(),TelTypeAttr.OID,TelTypeAttr.Name,false);
				map.AddDDLSysEnum(CallStatAttr.CallingState,0,"����״̬",true,false);
				map.AddDDLEntitiesNoName(CallListAttr.CallingState,null,"����״̬",new CallStates(),false);

				map.AddTBInt(CallStatAttr.Num,0,"����",true,false);

				map.AddSearchAttr(CallListAttr.FK_TelType);
				//map.AddSearchAttr(CallListAttr.CallDate);
				map.AddSearchAttr(CallListAttr.CallingState);

				 


				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class CallStats :EntitiesOID
	{
		public static Rpt3DEntity GetRpt3D
		{
			get
			{
				DataTable dt =DBAccess.RunSQLReturnTable("select * from V_CTI_CallStat");

				BP.Pub.Days days = new BP.Pub.Days();
				days.RetrieveAll();

				TelTypes tels = new TelTypes();
				tels.RetrieveAll();

				CallStates stas = new CallStates();
				stas.RetrieveAll();



				Rpt3DEntity rpt = new Rpt3DEntity(days,tels,stas,dt) ;
				rpt.CutNotRefD1();
				rpt.CutNotRefD2();
				rpt.CutNotRefD3();			 
				rpt.Title="����ͳ�Ʊ���";

				return rpt;


			}
		}

		#region ���췽������
		/// <summary>
		/// CallStats
		/// </summary>
		public CallStats(){}
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallStat();
			}
		}
		#endregion
	}
}
