using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
using BP.Web;

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
		public const string OID="OID";
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
	public class Book :BookBase
	{
	 
		#region ��������
		/// <summary>
		///  �Ƿ��ʹ�
		/// </summary>
		public bool IsSend
		{
			get
			{
				return GetValBooleanByKey(BookAttr.IsSend);
			}
			set
			{
				SetValByKey(BookAttr.IsSend,value);
			}
		}
		/// <summary>
		///  BookSendState�����ʹ�״̬��
		/// </summary>
		public BookSendState HisBookSendState
		{
			get
			{
				return (BookSendState)GetValIntByKey(BookAttr.BookSendState);
			}
			set
			{
				SetValByKey(BookAttr.BookSendState,(int)value);
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// ��Ŀ
		/// </summary>
		public Book(){}
		/// <summary>
		/// mid
		/// </summary>
		/// <param name="mid">mid</param>
		public Book(int mid):base(mid){}
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
				Map map = new Map( BookBase.PTable );
				map.DepositaryOfMap=Depositary.None;
				map.EnDesc="����";
			 

				map.AddTBMID();
				map.AddTBIntPK(BookAttr.WorkID,0,"����ID",false,true);
				map.AddDDLEntities(BookAttr.FK_Node,0, DataType.AppInt,"�ڵ�",new Nodes(),NodeAttr.OID,NodeAttr.Name,false);
				//map.AddTBString(BookAttr.BookNo,null,"���",true,true,0,100,5);
				map.AddDDLEntitiesPK(BookAttr.FK_NodeRefFunc,0, DataType.AppInt,"����",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				map.AddTBString(BookAttr.BookNo,null,"���",true,true,0,100,5);
				map.AddDDLSysEnum(WorkAttr.BookSendState,0,"�����ʹ�״̬",true,false);

				map.AddBoolean(BookAttr.IsSend,false,"�ʹ��",true,true);
				map.AddTBDateTime(BookAttr.SendDate,"�ʹ�ʱ��",true,true);

				map.AddBoolean(BookAttr.IsReturn,false,"�黹��",true,true);
				map.AddTBDate(BookAttr.ReturnTime,"�黹ʱ��",true,true);
				map.AddTBString(BookAttr.FK_Taxpayer,null,"��˰�˱��",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"��˰������",true,true,0,100,5);

				map.AddTBDate(BookAttr.RecordDateTime,"��¼ʱ��",true,true);
				map.AddDDLEntities(BookAttr.Recorder,Web.WebUser.OID, DataType.AppInt, "��¼��",new Emps(),EmpAttr.OID,EmpAttr.Name,false);

				//map.AddTBString(BookAttr.ReturnerNote,null,"��ע",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerDept,WebUser.FK_Dept ,"����",true,true,0,100,5);
				map.AddTBString(BookAttr.ReturnerZSJG,TaxUser.FK_ZSJG,"���ջ���",true,true,0,100,5);

				map.AddSearchAttr(BookAttr.IsReturn);
				map.AddSearchAttr(BookAttr.BookSendState);

				map.AttrsOfSearch.AddFromTo("����",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,12);
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		public void DoSendWork( int bookstate )
		{
			try
			{
				BP.WF.Node nd =  new BP.WF.Node(this.FK_Node);
				Work wk = nd.HisWork;
				wk.SetValByKey("OID",this.WorkID);
				wk.Retrieve(); // ��ѯ����������

				if (wk.EnMap.Attrs.Contains(WorkAttr.SendDateTime) && wk.EnMap.Attrs.Contains(WorkAttr.BookSendState))
				{
					wk.Update(WorkAttr.BookSendState, bookstate ,WorkAttr.SendDateTime,DataType.CurrentDataTime);
				}

				/*
				wk.IsSend=true;
				wk.SendDateTime=DataType.CurrentDataTime;
				wk.DirectUpdate();
				*/
			}
			catch(Exception ex)
			{
				throw new Exception("�����ʹ�ʧ��:"+ex.Message);
			}

		}
		public void DoSend( int bookstate )
		{
			this.DoSendWork(bookstate);
			//this.IsSend=true;
			//this.SendDate=DataType.CurrentData;
			this.Update(BookAttr.BookSendState, bookstate ,BookAttr.SendDate,DataType.CurrentDataTime);
		}

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			return base.beforeUpdateInsertAction ();
  
			if (this.IsReturn)
			{
				/*��������Ѿ��黹�ˣ�˵�����Ѿ�������*/
				if (this.MyBookSendState==BookSendState.UnSend)
				{
					//this.DoSendWork( (int)BookSendState.Send );
					this.MyBookSendState=BookSendState.Send;
					this.SendDate=DataType.CurrentData;
				}
			}
			 
			return base.beforeUpdateInsertAction ();
		}
		/// <summary>
		/// ����֮ǰ����Ĺ�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeUpdate()
		{
			
			return base.beforeUpdate ();
		}
		#endregion
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class Books :BookBases
	{
		 

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
