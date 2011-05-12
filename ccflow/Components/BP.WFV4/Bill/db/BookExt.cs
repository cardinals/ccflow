using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.WF
{
	 
	/// <summary>
	/// ��������
	/// </summary>
	public class BookExtAttr:BookAttr
	{
		 
	}
	/// <summary>
	/// ����
	/// </summary> 
	public class BookExt :BookBase
	{
		#region ��չ����
		 
		#endregion

		#region ��������
		/// <summary>
		///  �Ƿ�黹
		/// </summary>
		public bool IsReturn
		{
			get
			{
				return GetValBooleanByKey(BookExtAttr.IsReturn);
			}
			set
			{
				SetValByKey(BookExtAttr.IsReturn,value);
			}
		}
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookExtAttr.WorkID);
			}
			set
			{
				SetValByKey(BookExtAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookExtAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookExtAttr.FK_Node,value);
			}
		}
		/// <summary>
		///  ����
		/// </summary>
		public int FK_NodeRefFunc
		{
			get
			{
				return GetValIntByKey(BookExtAttr.FK_NodeRefFunc);
			}
			set
			{
				SetValByKey(BookExtAttr.FK_NodeRefFunc,value);
			}
		}
		/// <summary>
		///  ����
		/// </summary>
		public int Admin
		{
			get
			{
				return GetValIntByKey(BookExtAttr.Admin);
			}
			set
			{
				SetValByKey(BookExtAttr.Admin,value);
			}
		}
		 
		/// <summary>
		/// ����ʱ�䡣
		/// </summary>
		public string  RecordDateTime
		{
			get
			{
				return GetValStringByKey(BookExtAttr.RecordDateTime);
			}
			set
			{
				SetValByKey(BookExtAttr.RecordDateTime,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Recorder
		{
			get
			{
				return GetValIntByKey(BookExtAttr.Recorder);
			}
			set
			{
				SetValByKey(BookExtAttr.Recorder,value);
			}
		}
		 
		/// <summary>
		/// �黹�˱�ע
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookExtAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookExtAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// �滮����
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookExtAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookExtAttr.ReturnTime,value);
			}
		}
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public BookExt(){}
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
				Map map = new Map("WF_BookExt");	
				map.DepositaryOfMap=Depositary.Application;
				//				map.CodeStruct="3";
				map.EnDesc="����";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBIntPK(BookExtAttr.WorkID,0,"����ID",true,true);

				map.AddBoolean(BookExtAttr.IsReturn,false,"�黹��",true,false);
				map.AddTBString(BookExtAttr.FlowTitle,null,"���̱���",true,true,0,100,5);
				//map.AddDDLEntities(BookExtAttr.FK_Node,null, DataType.AppInt,"�ڵ�",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookExtAttr.Admin,1, DataType.AppInt,"����Ա",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBDateTime(BookExtAttr.RecordDateTime,"���齨��ʱ��",true,true);
				map.AddDDLEntities(BookExtAttr.Recorder,1, DataType.AppInt, "�黹��",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				//map.AddTBDateTime(BookExtAttr.ReturnTime,"�黹ʱ��",true,true);
				map.AddTBString(BookExtAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddDDLEntitiesPK(BookExtAttr.FK_NodeRefFunc,null, DataType.AppInt,"��������",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);


				//map.AddSearchAttr(BookExtAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("����",BookExtAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("����",BookExtAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);
				//map.AddSearchAttr(BookExtAttr.FK_Node);
				//map.AddSearchAttr(BookExtAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookExtAttr.Returner);
				//map.AttrsOfSearch.AddFromTo("��ǰ",);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class BookExts :BookBases
	{
		#region ����
		/// <summary>
		/// ����һ��������Ա��ID , �õ����ܹ����˵���Ŀ.
		/// </summary>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static BookExts GetBookExtsByEmpId(int empId)
		{
			string sql=" SELECT * FROM CH_BookExt WHERE No IN ( SELECT FK_BookExt From CH_BookExtVSStation WHERE FK_Station IN  (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp="+empId+"))" ; 
			BookExts ens = new BookExts();
			ens.InitCollectionByTable(DBAccess.RunSQLReturnTable(sql)) ; 
			return ens;
		}
		#endregion 

		#region ���췽������
		/// <summary>
		/// BookExts
		/// </summary>
		public BookExts(){}
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookExt();
			}
		}
		#endregion
	}
}
