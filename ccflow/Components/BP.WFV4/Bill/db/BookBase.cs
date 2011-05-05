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
	public class BookBaseAttr
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
		/// 
		/// </summary>
		public const string IsSend="IsSend";

		/// <summary>
		/// ��ع���
		/// </summary>
		public const string FK_NodeRefFunc="FK_NodeRefFunc";
		/// <summary>
		/// �Ƿ�黹
		/// </summary>
		public const string IsReturn="IsReturn";
		/// <summary>
		/// �ʹ��
		/// </summary>
		public const string BookSendState="BookSendState";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string SendDate="SendDate";
		/// <summary>
		/// ��˰�˱��
		/// </summary>
		public const string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// ����
		/// </summary>
		public const string TaxpayerName="TaxpayerName";

		
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string Admin="Admin";
		/// <summary>
		/// ��¼ʱ�䣮
		/// </summary>
		public const string RecordDateTime="RecordDateTime";
		/// <summary>
		/// ��¼�ˣ�
		/// </summary>
		public const string Recorder="Recorder";
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
	abstract public class BookBase :EntityMID
	{
		#region ��������
		 
		/// <summary>
		///  �Ƿ�黹
		/// </summary>
		public BookSendState MyBookSendState
		{
			get
			{
				return (BookSendState)GetValIntByKey(BookAttr.BookSendState);
			}
			set
			{
				SetValByKey(BookAttr.BookSendState,value);
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
		public string  BookNo
		{
			get
			{
				return GetValStringByKey(BookAttr.BookNo);
			}
			set
			{
				SetValByKey(BookAttr.BookNo,value);
			}
		}
		/// <summary>
		/// ���̱���
		/// </summary>
		public string  FK_Taxpayer
		{
			get
			{
				return GetValStringByKey(BookAttr.FK_Taxpayer);
			}
			set
			{
				SetValByKey(BookAttr.FK_Taxpayer,value);
			}
		}
		public string  TaxpayerName
		{
			get
			{
				return GetValStringByKey(BookAttr.TaxpayerName);
			}
			set
			{
				SetValByKey(BookAttr.TaxpayerName,value);
			}
		}

		/// <summary>
		/// RecordDateTime��
		/// </summary>
		public string  RecordDateTime
		{
			get
			{
				return GetValStringByKey(BookAttr.RecordDateTime);
			}
			set
			{
				SetValByKey(BookAttr.RecordDateTime,value);
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
		public int Recorder
		{
			get
			{
				return GetValIntByKey(BookAttr.Recorder);
			}
			set
			{
				SetValByKey(BookAttr.Recorder,value);
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
		/// �ʹ�����
		/// </summary>
		public string SendDate
		{
			get
			{
				return GetValStringByKey(BookAttr.SendDate);
			}
			set
			{
				SetValByKey(BookAttr.SendDate,value);
			}
		}
		/// <summary>
		/// �黹����
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
		public static string PTable="WF_Book";

		#region ���췽��
		public BookBase() {}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="mid"></param>
		public BookBase(int mid):base(mid){}		 
		#endregion 

		 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	abstract public class BookBases :Entities
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
		public BookBases(){}
		 
		
		#endregion 

		 
	}
}
