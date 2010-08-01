using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Port;

namespace BP.TA
{
 	/// <summary>
	/// �˻ؽڵ�״̬
	/// </summary>
	public enum ReturnWorkState
	{
		/// <summary>
		/// ��
		/// </summary>
		None,
		/// <summary>
		/// ������
		/// </summary>
		Sending,
		/// <summary>
		/// δ�Ͽ�
		/// </summary>
		UnRatify,
		/// <summary>
		/// �Ͽ�
		/// </summary>
		Ratify
	}
	/// <summary>
	/// �˻ؽڵ�����
	/// </summary>
	public class ReturnWorkAttr:WorkDtlBaseAttr
	{
		/// <summary>
		/// �˻�ԭ��
		/// </summary>
		public const string ReturnReason="ReturnReason";
		/// <summary>
		/// ���������
		/// </summary>
		public const string SenderNote="SenderNote";
		 
		/// <summary>
		/// �˻ؽڵ�״̬
		/// </summary>
		public const string ReturnWorkState="ReturnWorkState";
	}
	/// <summary>
	/// �˻ؽڵ�
	/// </summary> 
	public class ReturnWork : WorkDtlBase
	{
		#region ��չ ����
	 
		#endregion

		#region ��������
		/// <summary>
		/// �˻�ԭ��
		/// </summary>
		public string ReturnReason 
		{
			get
			{
				string str= this.GetValHtmlStringByKey(ReturnWorkAttr.ReturnReason);
				if (str=="")
					str="���ã�\n\n   ��������ԭ������Ҫ���˹����˻ظ�����\n\n\n ��! \n\n    "+WebUser.Name+"\n    "+DataType.CurrentDataTimeCN;
				return DataType.Html2Text(str);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.ReturnReason,value);
			}
		}
		public string ReturnReasonHtml
		{
			get
			{
				string str= this.GetValHtmlStringByKey(ReturnWorkAttr.ReturnReason);
				if (str=="")
					str="���ã�\n\n   ��������ԭ������Ҫ���˹����˻ظ�����\n\n\n ��! \n\n    "+WebUser.Name+"\n    "+DataType.CurrentDataTimeCN;
				return str;
				//return DataType.Html2Text(str);
			}
		}
		 
		/// <summary>
		/// ������Note
		/// </summary>
		public string SenderNote 
		{
			get
			{
				return this.GetValStringByKey(ReturnWorkAttr.SenderNote);
			}
			set
			{
				SetValByKey(ReturnWorkAttr.SenderNote,value);
			}
		}
		public ReturnWorkState ReturnWorkState
		{
			get
			{
				return (ReturnWorkState)this.GetValIntByKey(ReturnWorkAttr.ReturnWorkState);
			}
			set
			{
				this.SetValByKey(ReturnWorkAttr.ReturnWorkState,(int)value);
			}
		}
		public string ReturnWorkStateText
		{
			get
			{
				return this.GetValRefTextByKey(ReturnWorkAttr.ReturnWorkState);
			}
		}
		#endregion
 
		#region ���캯��
		/// <summary>
		/// �˻ؽڵ�
		/// </summary>
		public ReturnWork()
		{
		  
		}
		/// <summary>
		/// �˻ؽڵ�
		/// </summary>
		/// <param name="_No">No</param>
		public ReturnWork(int oid):base(oid)
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

				Map map = new Map("TA_ReturnWork");
				map.EnDesc="�˻ؽڵ�";
				map.AddTBIntPKOID();
				map.AddTBInt(ReturnWorkAttr.ParentID,0,"���ڵ�ID",true,true);
                map.AddDDLSysEnum(ReturnWorkAttr.ReturnWorkState, (int)ReturnWorkState.None, "״̬", true, false, "ReturnWorkState", "@0=��@1=������@2=δ�Ͽ�@3=�Ͽ�");
				map.AddDDLEntities(ReturnWorkAttr.Executer,null,"ִ����", new Emps(),false );
				map.AddTBString(ReturnWorkAttr.ReturnReason,null,"�˻�ԭ��",true,false,0,500,15);	
				map.AddTBDateTime(ReturnWorkAttr.DTOfActive,"�ʱ��(�˻�&�����˻�)",false,false );
				map.AddDDLEntities(ReturnWorkAttr.Sender,null,"������", new Emps(),false );
				map.AddTBString(ReturnWorkAttr.SenderNote,null,"�����˱�ע",true,false,0,500,15);	
				map.AddTBInt(ReturnWorkAttr.AdjunctNum,0,"��������",true,true);

				
 
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �˻ؽڵ�s
	/// </summary> 
	public class ReturnWorks: Entities
	{
		public int Returning()
		{
			QueryObject qo = new QueryObject(this);

			qo.AddWhere(ReturnWorkAttr.Sender ,WebUser.No);
			qo.addAnd();
			qo.AddWhere(ReturnWorkAttr.ReturnWorkState,(int)ReturnWorkState.Sending);

			qo.addOrderByDesc( ReturnWorkAttr.DTOfActive );
			return qo.DoQuery();
		}
		/// <summary>
		/// ��ȡentity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReturnWork();
			}
		}
		/// <summary>
		/// ReturnWorks
		/// </summary>
		public ReturnWorks()
		{

		}
		public ReturnWorks(string userNo,string ny)
		{
			QueryObject qo = new QueryObject(this);
			qo.addLeftBracket();
			qo.AddWhere(ReturnWorkAttr.Executer,userNo);
			qo.addOr();
			qo.AddWhere(ReturnWorkAttr.Sender,userNo);
			qo.addRightBracket();
			qo.addAnd();
			qo.AddWhere(ReturnWorkAttr.DTOfActive, " LIKE ", ny+"%");
			qo.DoQuery();
		}
		
	}
}
 