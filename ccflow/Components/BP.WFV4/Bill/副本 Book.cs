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
	/// �����ʹ�״̬
	/// </summary>
	public enum BookState
	{
		/// <summary>
		/// δ�ʹ�
		/// </summary>
		UnSend,
		/// <summary>
		/// ����δ�ʹ�
		/// </summary>
		UnSendTimeout,
		/// <summary>
		/// ���ʹ�
		/// </summary>
		Send,
		/// <summary>
		/// δ�ҵ���
		/// </summary>
		Notfind,
		/// <summary>
		///�Ѿ��鵵
		/// </summary>
		Pigeonhole
	}
	/// <summary>
	/// ��������
	/// </summary>
	public class BookAttr
	{
		#region ����
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
		public const string BookState="BookState";
	
		/// <summary>
		/// �˻�ʱ��
		/// </summary>
		public const string ReturnDateTime="ReturnDateTime";
		/// <summary>
		/// ��˰�˱��
		/// </summary>
		public const string FK_Taxpayer="FK_Taxpayer";
		/// <summary>
		/// ����
		/// </summary>
		public const string TaxpayerName="TaxpayerName";
		/// <summary>
		/// TaxpayerAddr
		/// </summary>
		public const string TaxpayerAddr="TaxpayerAddr";
		/// <summary>
		/// TaxpayerTel
		/// </summary>
		public const string TaxpayerTel="TaxpayerTel";
		/// <summary>
		/// BookNo
		/// </summary>
		public const string BookNo="BookNo";
		/// <summary>
		/// ����Ա
		/// </summary>
		public const string BookAdmin="BookAdmin";
		/// <summary>
		/// ��¼ʱ�䣮
		/// </summary>
		public const string RDT="RDT";
		/// <summary>
		/// Ӧ�ʹ�ʱ��
		/// </summary>
		public const string ShouldSendDT="ShouldSendDT";
		/// <summary>
		/// ��¼�ˣ�
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// �������
		/// </summary>
		public const string FK_XJ="FK_XJ";
		/// <summary>
		/// �������
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		///<summary>
		///�ʹ���
		///</summary>
		public const string Sender="Sender";
		/// <summary>
		/// ���ʹ���
		/// </summary>
		public const string Accepter="Accepter";
		/// <summary>
		/// �ʹ�ص�
		/// </summary>
		public const string AccepterAddr="AccepterAddr";
		/// <summary>
		/// �ռ�����
		/// </summary>
		public const string AccepterDateTime="AccepterDateTime";

		/// <summary>
		/// �����˴�������
		/// </summary>
		public const string AccepterNote="AccepterNote";
		/// <summary>
		/// ���ʹ��˾������ɺ�����
		/// </summary>
		public const string AccepterDisNote="AccepterDisNote";
		/// <summary>
		/// ��֤��ǩ�������
		/// </summary>
		public const string JZR="JZR";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_NY="FK_NY";
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary> 
	public class Book :EntityOID
	{
		#region ͳ����Ϣ������
		/// <summary>
		/// δ�ʹ�
		/// </summary>
		public static int NumOfUnSend
		{
			get
			{
				if ( int.Parse( DateTime.Now.ToString("hh")) <9  )
				{
					string sq1l="UPDATE WF_Book SET BookState="+(int)BookState.UnSendTimeout +"  WHERE ShouldSendDT > '"+DataType.CurrentData+"' AND  BookState="+(int)BookState.UnSend ;
					DBAccess.RunSQL(sq1l);
				}
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.UnSend ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// ����δ�͵�
		/// </summary>
		public static int NumOfUnSendTimeout
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND  BookState="+(int)BookState.UnSendTimeout ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// �Ѿ��ʹ�
		/// </summary>
		public static int NumOfSend
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Send ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// û�з�����
		/// </summary>
		public static int NumOfNotfind
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Notfind ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		/// <summary>
		/// �Ѿ��鵵
		/// </summary>
		public static int NumOfPigeonhole
		{
			get
			{
				string sql="SELECT COUNT(*)  FROM WF_Book WHERE  Recorder='"+WebUser.No+"' AND BookState="+(int)BookState.Pigeonhole ;
				return DBAccess.RunSQLReturnValInt(sql);
			}
		}
		#endregion

		#region ��������
		/// <summary>
		///   �����ʹ�״̬��
		/// </summary>
		public BookState BookState
		{
			get
			{
				return (BookState)GetValIntByKey(BookAttr.BookState);
			}
			set
			{
				SetValByKey(BookAttr.BookState,(int)value);
			}
		}
		/// <summary>
		///  ������
		/// </summary>
		public string BookNo
		{
			get
			{
				return this.GetValStringByKey(BookAttr.BookNo);
			}
			set
			{
				this.SetValByKey(BookAttr.BookNo,value);
			}
		}
		public string FK_NodeRefFuncText
		{
			get
			{
				return this.GetValRefTextByKey(BookAttr.FK_NodeRefFunc);
			}			
		}
		public int FK_NodeRefFunc
		{
			get
			{
				return this.GetValIntByKey(BookAttr.FK_NodeRefFunc);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_NodeRefFunc,value);
			}
		}
		public int WorkID
		{
			get
			{
				return this.GetValIntByKey(BookAttr.WorkID);
			}
			set
			{
				this.SetValByKey(BookAttr.WorkID,value);
			}
		}
		/// <summary>
		/// Node
		/// </summary>
		public int FK_Node
		{
			get
			{
				return this.GetValIntByKey(BookAttr.FK_Node);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_Node,value);
			}
		}
		/// <summary>
		/// �ʹ�ʱ��
		/// </summary>
		public string AccepterDateTime
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterDateTime);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterDateTime,value);
			}
		}
		public string ShouldSendDT
		{
			get
			{
				return this.GetValStringByKey(BookAttr.ShouldSendDT);
			}
			set
			{
				this.SetValByKey(BookAttr.ShouldSendDT,value);
			}
		}
		/// <summary>
		/// �黹ʱ��
		/// </summary>
		public string ReturnDateTime
		{
			get
			{
				return this.GetValStringByKey(BookAttr.ReturnDateTime);
			}
			set
			{
				this.SetValByKey(BookAttr.ReturnDateTime,value);
			}
		}
		public string FK_Taxpayer
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_Taxpayer);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_Taxpayer,value);
			}
		}
		/// <summary>
		/// ��˰������
		/// </summary>
		public string TaxpayerName
		{
			get
			{
				return this.GetValStringByKey(BookAttr.TaxpayerName);
			}
			set
			{
				this.SetValByKey(BookAttr.TaxpayerName,value);
			}
		}
		/// <summary>
		/// �����ӡʱ��
		/// </summary>
		public string RDT
		{
			get
			{
				return this.GetValStringByKey(BookAttr.RDT);
			}
			set
			{
				this.SetValByKey(BookAttr.RDT,value);
			}
		}
		/// <summary>
		/// ��ӡ��
		/// </summary>
		public string Recorder
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Recorder);
			}
			set
			{
				this.SetValByKey(BookAttr.Recorder,value);
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string FK_ZSJG
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_ZSJG);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_ZSJG,value);
			}
		}
		/// <summary>
		/// �ؾ�
		/// </summary>
		public string FK_XJ
		{
			get
			{
				return this.GetValStringByKey(BookAttr.FK_XJ);
			}
			set
			{
				this.SetValByKey(BookAttr.FK_XJ,value);
			}
		}

		/// <summary>
		/// �ʹ���
		/// </summary>
		public string Sender
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Sender);
			}
			set
			{
				this.SetValByKey(BookAttr.Sender,value);
			}
		}
		public string Accepter
		{
			get
			{
				return this.GetValStringByKey(BookAttr.Accepter);
			}
			set
			{
				this.SetValByKey(BookAttr.Accepter,value);
			}
		}
		public string AccepterAddr
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterAddr);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterAddr,value);
			}
		}

		public string AccepterDisNote
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterDisNote);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterDisNote,value);
			}
		}

		
		public string AccepterNote
		{
			get
			{
				return this.GetValStringByKey(BookAttr.AccepterNote);
			}
			set
			{
				this.SetValByKey(BookAttr.AccepterNote,value);
			}
		}

		 



		public string JZR
		{
			get
			{
				return this.GetValStringByKey(BookAttr.JZR);
			}
			set
			{
				this.SetValByKey(BookAttr.JZR,value);
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// HisUAC
		/// </summary>
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.IsDelete=false;
				uac.IsInsert=false;
				uac.IsUpdate=false;
				uac.IsView=true;
				return uac;
			}
		}
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
				Map map = new Map( "WF_Book" );
				map.DepositaryOfMap=Depositary.None;
				map.EnDesc="����";
				map.AddTBIntPKOID();
				map.AddTBInt(BookAttr.WorkID,0,"����ID",false,true);
				map.AddDDLEntities(BookAttr.FK_Node, 0 , DataType.AppInt,"�ڵ�����",new Nodes(),NodeAttr.NodeID,NodeAttr.Name, false );
				map.AddDDLEntities(BookAttr.FK_NodeRefFunc,"","��������",new NodeRefFuncExts(),false);
				map.AddTBString(BookAttr.BookNo,null,"���",true,true,0,100,5);
				map.AddDDLSysEnum(BookAttr.BookState,1,"����״̬",true,true);

				map.AddTBString(BookAttr.FK_Taxpayer,null,"˰�������",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"��˰������",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerAddr,null,"��ַ",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerTel,null,"�绰",true,true,0,100,5);

				map.AddTBDate(BookAttr.RDT,"�����ӡʱ��",true,true);
				map.AddTBDate(BookAttr.ShouldSendDT,"Ӧ�ʹ�ʱ��",true,true);

				map.AddDDLEntities(BookAttr.Recorder,Web.WebUser.No,"��ӡ��",new Emps(),false);
				//�ʹ���
				map.AddTBString(BookAttr.Sender,null,"�ʹ���",true,true,0,100,5);
				//�ʹ�ص�
				map.AddTBString(BookAttr.Accepter,null,"���ʹ���",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterAddr,null,"�ʹ�ص�",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterDateTime,null,"�ռ�����",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterNote,null,"�����˴�������",true,true,0,100,5);
				map.AddTBString(BookAttr.AccepterDisNote,null,"���ʹ��˾������ɺ�����",true,true,0,100,5);
				map.AddTBString(BookAttr.JZR,null,"��֤��ǩ�������",true,true,0,100,5);

				map.AddDDLEntities(BookAttr.FK_ZSJG,TaxUser.FK_ZSJG,"�������",new BP.Tax.ZSJGs(),false);
				map.AddDDLEntities(BookAttr.FK_NY,DataType.CurrentYearMonth,"��������",new BP.Pub.NYs(),false);

				//map.AddDDLEntities(BookAttr.FK_XJ,TaxUser.FK_ZSJGOfXJ,"�ؾ�",new BP.Tax.XJs(),false);

				map.AddSearchAttr(BookAttr.FK_NY);
				map.AddSearchAttr(BookAttr.FK_ZSJG);
				map.AddSearchAttr(BookAttr.Recorder);
				map.AddSearchAttr(BookAttr.BookState);
				//map.AttrsOfSearch.AddFromTo("����",BookAttr.RDT, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

//		public void DoSendWork( int bookstate )
//		{
//			try
//			{
//				BP.WF.Node nd =  new BP.WF.Node(this.FK_Node);
//				Work wk = nd.HisWork;
//				wk.SetValByKey("OID",this.WorkID);
//				wk.Retrieve(); // ��ѯ����������
//
//				if (wk.EnMap.Attrs.Contains(WorkAttr.SendDateTime) && wk.EnMap.Attrs.Contains(WorkAttr.BookState))
//				{
//					wk.Update(WorkAttr.BookState, bookstate ,WorkAttr.SendDateTime,DataType.CurrentDataTime);
//				}
//
//				/*
//				wk.IsSend=true;
//				wk.SendDateTime=DataType.CurrentDataTime;
//				wk.DirectUpdate();
//				*/
//			}
//			catch(Exception ex)
//			{
//				throw new Exception("�����ʹ�ʧ��:"+ex.Message);
//			}
//
//		}
//		public void DoSend( int bookstate )
//		{
//			this.DoSendWork(bookstate);
//			//this.IsSend=true;
//			//this.SendDate=DataType.CurrentData;
//			this.Update(BookAttr.BookState, bookstate ,BookAttr.SendDate,DataType.CurrentDataTime);
//		}

		#region 
		protected override bool beforeUpdateInsertAction()
		{
			//			return base.beforeUpdateInsertAction ();
			//  
			//			if (this.IsReturn)
			//			{
			//				/*��������Ѿ��黹�ˣ�˵�����Ѿ�������*/
			//				if (this.MyBookState==BookState.UnSend)
			//				{
			//					//this.DoSendWork( (int)BookState.Send );
			//					this.MyBookState=BookState.Send;
			//					this.SendDate=DataType.CurrentData;
			//				}
			//			}
			//			 
			return base.beforeUpdateInsertAction();
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
	public class Books :EntitiesOID
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
