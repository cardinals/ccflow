using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.CTI.App
{
	/// <summary>
	/// ������������
	/// </summary>
	public class CallContextAttr:EntityNoNameAttr
	{
		#region ����
		public const string FK_YF="FK_YF";
		public const string FK_DTS="FK_DTS";
		/// <summary>
		/// �ʺ��û�����
		/// </summary>
		public const string FK_TelTypeOfFit="FK_TelTypeOfFit";		 
		/// <summary>
		///  Context
		/// </summary>
		public const string Context="Context";
		/// <summary>
		/// DTSDay
		/// </summary>
		public const string DTSDay="DTSDay";
		/// <summary>
		/// ����ʱ
		/// </summary>
		public const string DTSHH="DTSHH";
		/// <summary>
		/// ���ȷ�
		/// </summary>
		public const string DTSmm="DTSmm";

		/// <summary>
		/// Note
		/// </summary>
		public const string Note="Note";



		#endregion
	}
	/// <summary>
	/// ��������
	/// </summary> 
	public class CallContext :EntityNoName
	{
		 
		public BP.DTS.SysDTS HisDTS
		{
			get
			{
				return new BP.DTS.SysDTS(this.Context);
			}
		}
		 

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			//if (this.No=="1" || this.No=="2")
			///	throw new Exception("���=1 ,����=2���Ǳ������������г�����Զ���ɸ��¡�");
			return base.beforeUpdateInsertAction ();
		}

		#endregion

		#region ��������
		 
		public string  Context
		{
			get
			{
				return GetValStringByKey(CallContextAttr.Context);
			}
			set
			{
				SetValByKey(CallContextAttr.Context,value);
			}
		} 
		public string  FK_YF
		{
			get
			{
				return GetValStringByKey(CallContextAttr.FK_YF);
			}
			set
			{
				SetValByKey(CallContextAttr.FK_YF,value);
			}
		} 
		public string  FK_DTS
		{
			get
			{
				return GetValStringByKey(CallContextAttr.FK_DTS);
			}
			set
			{
				SetValByKey(CallContextAttr.FK_DTS,value);
			}
		} 
		public string  DTSDay
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSDay);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSDay,value);
			}
		} 

		public string  DTSHH
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSHH);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSHH,value);
			}
		} 
		public string  DTSmm
		{
			get
			{
				return GetValStringByKey(CallContextAttr.DTSmm);
			}
			set
			{
				SetValByKey(CallContextAttr.DTSmm,value);
			}
		} 
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��������
		/// </summary>
		public CallContext()
		{
		}
		public CallContext(string no):base(no)
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
				Map map = new Map("CTI_Context");	
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.None;
				map.CodeStruct="1";
				map.EnDesc="������������";
			 

				map.AddTBStringPK(CallContextAttr.No,null,"���",true,false,0,50,20);				 
				map.AddTBString(CallContextAttr.Name,null,"����",true,false,0,50,20);			 
				map.AddTBString(CallContextAttr.Context,null,"��������",true,false,0,50,20);
				map.AddTBStringDoc(CallContextAttr.Note,null,"��������",true,false);
				map.AddDDLEntities(CallContextAttr.FK_TelTypeOfFit, "0" ,DataType.AppInt,"�ʺ��û�����", new BP.CTI.App.TelTypeOfFits(),TelTypeOfFitAttr.OID, TelTypeOfFitAttr.Name,true);
				map.AddDDLEntitiesNoName(CallContextAttr.FK_DTS, "000" ,"ִ�еĵ���", new BP.DTS.SysDTSs(),true);
				map.AddTBString(CallContextAttr.DTSDay,"00","����ÿ��",true,false,0,50,20);
				map.AddTBString(CallContextAttr.DTSHH,"00","����ÿʱ",true,false,0,50,20);
				map.AddTBString(CallContextAttr.DTSmm,"00","����ÿ��",true,false,0,50,20);

				//map.AddSearchAttr(CallContextAttr.FK_YF);				
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		protected override void afterInsertUpdateAction()
		{
			
			base.afterInsertUpdateAction ();
		}

	}
	/// <summary>
	/// ��������s
	/// </summary>
	public class CallContexts :EntitiesNoName
	{
		

		#region ���췽������
		/// <summary>
		/// CallContexts
		/// </summary>
		public CallContexts()
		{
		}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CallContext();
			}
		}
		 
		public static void SetDTS(CallContext cc)
		{
			BP.DTS.SysDTS dts =new BP.DTS.SysDTS(cc.FK_DTS);
			dts.EveryDay=cc.DTSDay;
			dts.EveryHour=cc.DTSHH;
			dts.EveryMinute=cc.DTSmm;
			dts.Update();
		}
		#endregion
	}
}
