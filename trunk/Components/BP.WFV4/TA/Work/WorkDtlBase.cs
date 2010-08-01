using System;
using BP.En;

namespace BP.TA
{
	public class WorkDtlBaseAttr:EntityOIDAttr
	{
		/// <summary>
		/// ParentID
		/// </summary>
		public const string ParentID="ParentID";
		/// <summary>
		/// ��������
		/// </summary>
		public const string AdjunctNum="AdjunctNum";
		/// <summary>
		/// ������
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// ִ����
		/// </summary>
		public const string Executer="Executer";
		/// <summary>
		/// �ʱ��
		/// </summary>
		public const string DTOfActive="DTOfActive";
	}
	/// <summary>
	/// WorkBase ��ժҪ˵����
	/// </summary>
	abstract public class WorkDtlBase:EntityOID
	{
		#region ����
		/// <summary>
		/// ���ڵ�
		/// </summary>
		public int ParentID
		{
			get
			{
				return this.GetValIntByKey(ReWorkAttr.ParentID);
			}
			set
			{
				SetValByKey(ReWorkAttr.ParentID,value);
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public int AdjunctNum
		{
			get
			{
				return this.GetValIntByKey(ReWorkAttr.AdjunctNum);
			}
			set
			{
				SetValByKey(ReWorkAttr.AdjunctNum,value);
			}
		}
		/// <summary>
		/// �ظ���
		/// </summary>
		public string Executer 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.Executer);
			}
			set
			{
				SetValByKey(ReWorkAttr.Executer,value);
			}
		}
		public string ExecuterText
		{
			get
			{
				return this.GetValRefTextByKey(ReWorkAttr.Executer);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string SenderText
		{
			get
			{
				return this.GetValRefTextByKey(ReWorkAttr.Sender);
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Sender 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.Sender);
			}
			set
			{
				SetValByKey(ReWorkAttr.Sender,value);
			}
		}
		/// <summary>
		/// �ʱ��
		/// </summary>
		public string DTOfActive
		{
			get
			{
				return this.GetValAppDateByKey(WorkDtlAttr.DTOfActive); 
			}
			set
			{
				SetValByKey(WorkDtlAttr.DTOfActive,value);
			}
		}
		/// <summary>
		/// �ʱ��
		/// </summary>
		public DateTime DTOfActive_S
		{
			get
			{
				return DA.DataType.ParseSysDateTime2DateTime(this.DTOfActive);
			}
		}
		#endregion

		#region ��չ����
		private Work _hisWork=null;
		private ReWork _hisReWork=null;
		private ReturnWork _hisReturnWork=null;
		private WorkDtl _hisWorkDtl=null;
		/// <summary>
		/// ���Ĺ���
		/// </summary>
		public Work HisWork
		{
			get
			{
				if (this._hisWork==null)
					_hisWork=new Work(this.ParentID);
				return _hisWork;
			}
		}
		public ReWork HisReWork
		{
			get
			{
				if (this._hisReWork==null)
					_hisReWork=new ReWork(this.OID);
				return _hisReWork;
			}
		}
		public ReturnWork HisReturnWork
		{
			get
			{
				if (this._hisReturnWork==null)
					_hisReturnWork=new ReturnWork(this.OID);
				return _hisReturnWork;
			}
		}
		public WorkDtl HisWorkDtl
		{
			get
			{
				if (this._hisWorkDtl==null)
					_hisWorkDtl=new WorkDtl(this.OID);
				return _hisWorkDtl;
			}
		}
		#endregion ��չ����

		public WorkDtlBase()
		{
		}
		public WorkDtlBase(int oid):base( oid)
		{
		}
	}
	abstract public class WorkDtlBases:EntitiesOID
	{
		public WorkDtlBases()
		{
		}
	}
}
