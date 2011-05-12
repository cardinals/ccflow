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
	public class BookReAttr:BookAttr
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
		/// ����ʱ��
		/// </summary>
		public const string SendDateTime="SendDateTime";
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
		public const string RecordDateTime="RecordDateTime";
		/// <summary>
		/// ��¼�ˣ�
		/// </summary>
		public const string Recorder="Recorder";
		/// <summary>
		/// ���ջ���
		/// </summary>
		public const string FK_XJ="FK_XJ";
		/// <summary>
		/// ���ջ���
		/// </summary>
		public const string FK_ZSJG="FK_ZSJG";
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary> 
	public class BookRe :Book
	{
		#region ��������

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
				uac.IsUpdate=true;
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
				//map.AddTBInt(BookAttr.WorkID,0,"����ID",false,true);
				//map.AddDDLEntities(BookAttr.FK_Node, 0 , DataType.AppInt,"�ڵ�����",new Nodes(),NodeAttr.OID,NodeAttr.Name, false );
				map.AddDDLEntities(BookAttr.FK_NodeRefFunc,0, DataType.AppInt,"��������",new NodeRefFuncs(),NodeRefFuncAttr.OID,NodeRefFuncAttr.Name,false);
				map.AddTBString(BookAttr.BookNo,null,"���",true,true,0,100,5);
				map.AddDDLSysEnum(BookAttr.BookState,0,"����״̬",true,true);
				map.AddTBDateTime(BookAttr.SendDateTime,"�ʹ�ʱ��",true,true);
				//map.AddTBDateTime(BookAttr.ReturnDateTime,"�黹ʱ��",true,true);

				map.AddTBString(BookAttr.FK_Taxpayer,null,"��˰�˱��",true,true,0,100,5);
				map.AddTBString(BookAttr.TaxpayerName,null,"��˰������",true,true,0,100,5);

				map.AddTBDate(BookAttr.RecordDateTime,"�����ӡʱ��",true,true);
				//map.AddDDLEntitiesNoName(BookAttr.Recorder,Web.WebUser.No,"��ӡ��",new EmpExts(),false);

				map.AddDDLEntitiesNoName(BookAttr.FK_ZSJG,TaxUser.FK_ZSJG,"���ջ���",new BP.Tax.ZSJGs(),false);
				map.AddDDLEntitiesNoName(BookAttr.FK_XJ,TaxUser.FK_ZSJGOfXJ,"�ؾ�",new BP.Tax.XJs(),false);

				map.AddSearchAttr(BookAttr.FK_ZSJG);
				map.AddSearchAttr(BookAttr.Recorder);
				map.AddSearchAttr(BookAttr.BookState);
				map.AttrsOfSearch.AddFromTo("����",BookAttr.RecordDateTime, DateTime.Now.AddMonths(-1).ToString(DataType.SysDataTimeFormat), DA.DataType.CurrentDataTime,6);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		 
		 
	}
	/// <summary>
	/// ��Ŀ
	/// </summary>
	public class BookRes :EntitiesOID
	{
		#region ���췽������
		/// <summary>
		/// Books
		/// </summary>
		public BookRes(){}
		#endregion 

		#region ����
		/// <summary>
		/// GetNewEntity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new BookRe();
			}
		}
		#endregion
	}
}
