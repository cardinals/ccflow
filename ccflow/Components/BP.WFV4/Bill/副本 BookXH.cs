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
	public class BookAttr
	{
		#region ���� 
		/// <summary>
		/// MID
		/// </summary>
		public const string MID="MID";
		/// <summary>
		/// ����ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// ��ع���
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// �Ƿ�黹
		/// </summary>
		public const string IsReturn="IsReturn";
		/// <summary>
		/// FlowTitle
		/// </summary>
		public const string FlowTitle="FlowTitle";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string Admin="Admin";
		/// <summary>
		/// ����ʱ�䣮
		/// </summary>
		public const string CreateTime="CreateTime";
		/// <summary>
		/// �黹�ˣ�
		/// </summary>
		public const string Returner="Returner";
		/// <summary>
		/// ����
		/// </summary>
		public const string ReturnerDept="ReturnerDept";
		/// <summary>
		/// ���ջ���
		/// </summary>
		public const string ReturnerZSJG="ReturnerZSJG";
		/// <summary>
		/// �黹ʱ��
		/// </summary>
		public const string ReturnTime="ReturnTime";
		/// <summary>
		/// �黹�˱�ע
		/// </summary>
		public const string ReturnerNote="ReturnerNote";
		#endregion		
	}
	/// <summary>
	/// ����
	/// </summary> 
	public class Book :Entity
	{
		#region ��������
		/// <summary>
		///  MID
		/// </summary>
		public int MID
		{
			get
			{
				return GetValIntByKey(BookAttr.MID);
			}
			set
			{
				SetValByKey(BookAttr.MID,value);
			}
		}
		/// <summary>
		///  �Ƿ�黹
		/// </summary>
		public bool IsReturn
		{
			get
			{
				return GetValBooleanByKey(BookAttr.IsReturn);
			}
			set
			{
				SetValByKey(BookAttr.IsReturn,value);
			}
		}
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookAttr.WorkID);
			}
			set
			{
				SetValByKey(BookAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookAttr.FK_Node,value);
			}
		}
		/// <summary>
		///  ����
		/// </summary>
		public int FK_NodeRefFunc
		{
			get
			{
				return GetValIntByKey(BookAttr.FK_NodeRefFunc);
			}
			set
			{
				SetValByKey(BookAttr.FK_NodeRefFunc,value);
			}
		}
		/// <summary>
		///  ����
		/// </summary>
		public int Admin
		{
			get
			{
				return GetValIntByKey(BookAttr.Admin);
			}
			set
			{
				SetValByKey(BookAttr.Admin,value);
			}
		}
		/// <summary>
		/// ���̱���
		/// </summary>
		public string  FlowTitle
		{
			get
			{
				return GetValStringByKey(BookAttr.FlowTitle);
			}
			set
			{
				SetValByKey(BookAttr.FlowTitle,value);
			}
		}
		/// <summary>
		/// ����ʱ�䡣
		/// </summary>
		public string  CreateTime
		{
			get
			{
				return GetValStringByKey(BookAttr.CreateTime);
			}
			set
			{
				SetValByKey(BookAttr.CreateTime,value);
			}
		}
		/// <summary>
		/// ���š�
		/// </summary>
		public string  ReturnerDept
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerDept);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerDept,value);
			}
		}
		/// <summary>
		/// ���ջ��ء�
		/// </summary>
		public string  ReturnerZSJG
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerZSJG);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerZSJG,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Returner
		{
			get
			{
				return GetValIntByKey(BookAttr.Returner);
			}
			set
			{
				SetValByKey(BookAttr.Returner,value);
			}
		}
		 
		/// <summary>
		/// �黹�˱�ע
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// �滮����
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookAttr.ReturnTime,value);
			}
		}
		#endregion 

		public void GenerNewBook(string table)
		{
			Book.PTable=table;
			this._enMap=null;
		}
		/// <summary>
		/// 
		/// </summary>
		private static string PTable="WF_Book";

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Book(){}
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
				Map map = new Map( Book.PTable );
				map.DepositaryOfMap=Depositary.None;

				map.EnDesc="����";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBMID();
				map.AddTBIntPK(BookAttr.WorkID,0,"����ID",true,true);
				map.AddBoolean(BookAttr.IsReturn,false,"�黹��",true,true);
				map.AddTBString(BookAttr.FlowTitle,null,"���̱���",true,true,0,100,5);
				//map.AddDDLEntities(BookAttr.FK_Node,null, DataType.AppInt,"�ڵ�",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookAttr.Admin,1, DataType.AppInt,"����Ա",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBInt(BookAttr.Admin,1,"����Ա",false,false);

				map.AddTBDateTime(BookAttr.CreateTime,"����ʱ��",true,true);
				map.AddDDLEntities(BookAttr.Returner,1, DataType.AppInt, "�黹��",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBDateTime(BookAttr.ReturnTime,"�黹ʱ��",false,true);
				map.AddTBString(BookAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBIntPK(BookAttr.FK_NodeRefFunc,0,"����",false,true);
				map.AddTBString(BookAttr.ReturnerDept,null,"����",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,null,"���ջ���",true,true,0,100,5);

				//map.AddDDLEntitiesPK(BookAttr.FK_NodeRefFunc,null, DataType.AppInt,"��������",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				
				map.AddSearchAttr(BookAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("����",BookAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("����",BookAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);

				//map.AddSearchAttr(BookAttr.FK_Node);
				//map.AddSearchAttr(BookAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookAttr.Returner);
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
	public class Books :Entities
	{
		#region ����
		/// <summary>
		/// ����һ��������Ա��ID , �õ����ܹ����˵���Ŀ.
		/// </summary>
		/// <param name="empId">������ԱID</param>
		/// <returns></returns>
		public static Books GetBooksByEmpId(int empId)
		{
			string sql=" SELECT * FROM CH_Book WHERE No IN ( SELECT FK_Book From CH_BookVSStation WHERE FK_Station IN  (SELECT FK_Station FROM Port_EmpStation WHERE FK_Emp="+empId+"))" ; 
			Books ens = new Books();
			ens.InitCollectionByTable(DBAccess.RunSQLReturnTable(sql)) ; 
			return ens;
		}
		#endregion 

		#region ���췽������
		/// <summary>
		/// Books
		/// </summary>
		public Books(){}
		 
		
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Book();
			}
		}
		#endregion
	}
}
