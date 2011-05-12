using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;

namespace BP.WF
{
	 
	/// <summary>
	/// �����������
	/// </summary>
	public class BookAssessorAttr
	{
		#region ���� 
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Flow="FK_Flow";
		/// <summary>
		/// ����ID
		/// </summary>
		public const string WorkID="WorkID";
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string FK_Node="FK_Node";
		/// <summary>
		/// �����
		/// </summary>
		public const string Assessor="Assessor";
		/// <summary>
		/// ���״̬
		/// </summary>
		public const string AssessorState="AssessorState";
		/// <summary>
		/// ������
		/// </summary>
		public const string Note="Note";
		/// <summary>
		/// ��¼ʱ��
		/// </summary>
		public const string RecorderDateTime="RecorderDateTime";
		/// <summary>
		/// ��������
		/// </summary>
		public const string GenerDateTime="GenerDateTime";
		#endregion		
	}
	/// <summary>
	/// �������
	/// </summary> 
	public class BookAssessor :EntityOID
	{
		#region ��������		 
		/// <summary>
		///  WorkID
		/// </summary>
		public int WorkID
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.WorkID);
			}
			set
			{
				SetValByKey(BookAssessorAttr.WorkID,value);
			}
		}
		/// <summary>
		///  FK_Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.FK_Node);
			}
			set
			{
				SetValByKey(BookAssessorAttr.FK_Node,value);
			}
		}
		 
		/// <summary>
		///  ����
		/// </summary>
		public int Assessor
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.Assessor);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Assessor,value);
			}
		}
		/// <summary>
		/// ���̱���
		/// </summary>
		public string  Note
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.Note);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Note,value);
			}
		}
		/// <summary>
		/// ����ʱ�䡣
		/// </summary>
		public string  CreateTime
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.CreateTime);
			}
			set
			{
				SetValByKey(BookAssessorAttr.CreateTime,value);
			}
		}
		/// <summary>
		/// ���š�
		/// </summary>
		public string  ReturnerDept
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerDept);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerDept,value);
			}
		}
		/// <summary>
		/// ���ջ��ء�
		/// </summary>
		public string  ReturnerZSJG
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerZSJG);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerZSJG,value);
			}
		}
		/// <summary>
		///  Returner
		/// </summary>
		public int Returner
		{
			get
			{
				return GetValIntByKey(BookAssessorAttr.Returner);
			}
			set
			{
				SetValByKey(BookAssessorAttr.Returner,value);
			}
		}
		 
		/// <summary>
		/// �黹�˱�ע
		/// </summary>
		public string  ReturnerNote
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnerNote);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnerNote,value);
			}
		}
		/// <summary>
		/// �滮����
		/// </summary>
		public string  ReturnTime
		{
			get
			{
				return GetValStringByKey(BookAssessorAttr.ReturnTime);
			}
			set
			{
				SetValByKey(BookAssessorAttr.ReturnTime,value);
			}
		}
		#endregion 

	 

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public BookAssessor(){}


		 
		public BookAssessor(int nodeId,int workId)
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
				Map map = new Map( BookAssessor.PTable );
				map.DepositaryOfMap=Depositary.None;

				map.EnDesc="�������";                
				map.IsDelete=true;
				map.IsInsert=true;
				map.IsUpdate=true;
				map.IsView=true;
				map.AddTBIntPKOID();

				map.AddTBIntPK(BookAssessorAttr.WorkID,0,"����ID",true,true);
			
				map.AddBoolean(BookAssessorAttr.IsReturn,false,"�黹��",true,true);
				map.AddTBString(BookAssessorAttr.FlowTitle,null,"���̱���",true,true,0,100,5);
				//map.AddDDLEntities(BookAssessorAttr.FK_Node,null, DataType.AppInt,"�ڵ�",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddDDLEntities(BookAssessorAttr.Admin,1, DataType.AppInt,"����Ա",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBInt(BookAssessorAttr.Admin,1,"����Ա",false,false);

				map.AddTBDateTime(BookAssessorAttr.CreateTime,"����ʱ��",true,true);
				map.AddDDLEntities(BookAssessorAttr.Returner,1, DataType.AppInt, "�黹��",new Emps(),EmpAttr.OID,EmpAttr.Name,false);
				map.AddTBString(BookAssessorAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBDateTime(BookAssessorAttr.ReturnTime,"�黹ʱ��",false,true);
				map.AddTBString(BookAssessorAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBIntPK(BookAssessorAttr.FK_NodeRefFunc,0,"�������",false,true);
				map.AddTBString(BookAssessorAttr.ReturnerDept,null,"����",true,true,0,100,5);
				map.AddTBString(BookAssessorAttr.ReturnerZSJG,null,"���ջ���",true,true,0,100,5);

				//map.AddDDLEntitiesPK(BookAssessorAttr.FK_NodeRefFunc,null, DataType.AppInt,"�����������",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				
				map.AddSearchAttr(BookAssessorAttr.IsReturn);
				//map.AttrsOfSearch.AddFromTo("����",BookAssessorAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,7);
				map.AttrsOfSearch.AddFromTo("����",BookAssessorAttr.CreateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataFormat), DA.DataType.CurrentData,7);

				//map.AddSearchAttr(BookAssessorAttr.FK_Node);
				//map.AddSearchAttr(BookAssessorAttr.FK_NodeRefFunc);
				//map.AddSearchAttr(BookAssessorAttr.Returner);
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
	public class BookAssessors :EntitiesOID
	{
		 

		#region ���췽������
		/// <summary>
		/// BookAssessors
		/// </summary>
		public BookAssessors(){}
		#endregion

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookAssessor();
			}
		}
		#endregion
	}
}
