using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
 	/// <summary>
	/// �ظ��ڵ�״̬
	/// </summary>
	public enum ReWorkState
	{
		/// <summary>
		/// ��
		/// </summary>
		None,
		/// <summary>
		/// ִ����(�ȴ����ڵ�ǩ��)
		/// </summary>
		Sending,
		/// <summary>
		/// δ�Ͽ�(���ʱ��ִ���˿���ǩ������, �ڵ�״̬����Ϊ None,
		/// ���µȴ�ִ����ȥ������ʱ���״̬Ϊ  UnRatify -> None -> Sending )
		/// </summary>
		UnRatify,
		/// <summary>
		/// �Ͽɣ������Ѿ�������
		/// </summary>
		Ratify
	}
	/// <summary>
	/// �ظ��ڵ�����
	/// </summary>
	public class ReWorkAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// ReWorkState
		/// </summary>
		public const string ReWorkState="ReWorkState";
		/// <summary>
		/// �ظ���Ϣ
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// �ظ���Ϣ
		/// </summary>
		public const string Doc="Doc";
		/// <summary>
		/// �����˶���ʱ��(��ȡ&��̬)
		/// </summary>
		public const string SenderActionDateTime="SenderActionDateTime"; 
		/// <summary>
		/// ���������
		/// </summary>
		public const string SenderNote="SenderNote";
	}
	/// <summary>
	/// �ظ��ڵ�
	/// </summary> 
	public class ReWork : WorkDtlBase
	{
		#region ��������
		/// <summary>
		/// Title
		/// </summary>
		public string Title
		{
			get
			{
				return  this.GetValStringByKey(ReWorkAttr.Title);
			}
			set
			{
				SetValByKey(ReWorkAttr.Title,value);
			}
		}
		public string Doc
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.Doc);
			}
			set
			{
				SetValByKey(ReWorkAttr.Doc,value);
			}
		}
		public string DocHtml
		{
			get
			{
				return this.GetValHtmlStringByKey(ReWorkAttr.Doc);
			}
		}
		/// <summary>
		/// �ظ������ڵ�״̬
		/// </summary>
		public ReWorkState ReWorkState
		{
			get
			{
				return (ReWorkState)this.GetValIntByKey(ReWorkAttr.ReWorkState);
			}
			set
			{
				this.SetValByKey(ReWorkAttr.ReWorkState, (int)value);
			}
		}
		/// <summary>
		/// ������Note
		/// </summary>
		public string SenderNote 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.SenderNote);
			}
			set
			{
				SetValByKey(ReWorkAttr.SenderNote,value);
			}
		}
		/// <summary>
		/// �����˶���ʱ��
		/// </summary>
		public string SenderActionDateTime 
		{
			get
			{
				return this.GetValStringByKey(ReWorkAttr.SenderActionDateTime);
			}
			set
			{
				SetValByKey(ReWorkAttr.SenderActionDateTime,value);
			}
		}
		/// <summary>
		/// ʱ��
		/// </summary>
		public DateTime SenderActionDateTime_S
		{
			get
			{
				return this.GetValDateTime(ReWorkAttr.SenderActionDateTime);
			}
		}
		#endregion
 
		#region ���캯��
		/// <summary>
		/// �ظ��ڵ�
		/// </summary>
		public ReWork()
		{
		}
		/// <summary>
		/// �ظ��ڵ�
		/// </summary>
		/// <param name="_No">No</param>
		public ReWork(int oid):base(oid)
		{
		}
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;

				Map map = new Map("TA_ReWork");
				map.EnDesc="�ظ��ڵ�";
				map.AddTBIntPKOID();
				map.AddTBInt(ReWorkAttr.ParentID,0,"���ڵ�ID",true,true);
                map.AddDDLSysEnum(ReWorkAttr.ReWorkState, (int)ReWorkState.None, "״̬", true, false, "ReWorkState", "@0=��@1=ִ����@2=δ�Ͽ�@3=�Ͽ�");

				map.AddDDLEntities(ReWorkAttr.Executer,null,"�ظ���", new Emps(),false );				 
				map.AddTBDateTime(ReWorkAttr.DTOfActive,"�ʱ��(�ظ�&�����ظ�)",false,false );

				map.AddTBString(WorkAttr.Title,null,"�������",true,false,0,500,15);
				map.AddTBString(WorkAttr.Doc,null,"��������",true,false,0,4000,15);

				map.AddDDLEntities(ReWorkAttr.Sender,null,"������", new Emps(),false );
				map.AddTBString(ReWorkAttr.SenderNote,null,"�����˱�ע",true,false,0,500,15);
				map.AddTBDateTime(ReWorkAttr.SenderActionDateTime,"�ʱ��(��ȡ&�Ͽɶ���)",false,false );
				map.AddTBInt(ReWorkAttr.AdjunctNum,0,"��������",true,true);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

	}
	/// <summary>
	/// �ظ��ڵ�s
	/// </summary> 
	public class ReWorks: Entities
	{
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReWork();
			}
		}
		/// <summary>
		/// ReWorks
		/// </summary>
		public ReWorks()
		{
		}


		/// <summary>
		///  
		/// </summary>
		/// <param name="emp"></param>
		/// <param name="ny"></param>		
		public ReWorks(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(ReWorkAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(ReWorkAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(ReWorkAttr.DTOfActive, " LIKE ", ny+"%");
			qo.DoQuery();
		}

		/// <summary>
		/// ��Ҫ����˻ظ��Ĺ�����
		/// </summary>
		/// <returns></returns>
		public int Reing()
		{
			QueryObject qo = new QueryObject(this);

			qo.AddWhere(ReWorkAttr.Sender ,WebUser.No);
			qo.addAnd();
			qo.AddWhere(ReWorkAttr.ReWorkState,(int)ReWorkState.Sending);

			qo.addOrderBy( ReWorkAttr.DTOfActive );
			return qo.DoQuery();
		}
	}
}
 