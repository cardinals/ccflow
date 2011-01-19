using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;

namespace BP.Sys
{
	/// <summary>
	/// �û�����
	/// </summary>
	public class UserQuestionAttr  //EntityClassNameAttr
	{
		/// <summary>
		/// OID
		/// </summary>
		public const string OID="OID";
		/// <summary>
		/// ������
		/// </summary>
		public const string Sender="Sender";
		/// <summary>
		/// ������
		/// </summary>
		public const string Reporter="Reporter";
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public const string SendDateTime="SendDateTime";
		/// <summary>
		/// ����
		/// </summary>
		public const string Title="Title";
		/// <summary>
		/// ����
		/// </summary>
		public const string Docs="Docs";
		/// <summary>
		/// 
		/// </summary>
		public const string ReDocs="ReDocs";

	}
	/// <summary>
	/// �û�����
	/// </summary> 
	public class UserQuestion:EntityOID 
	{
		#region ��������		 
		/// <summary>
		/// ������
		/// </summary>
		public  string  Sender
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Sender);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Sender,value);
			}
		}
		/// <summary>
		/// ��������ʱ��
		/// </summary>
		public  string  SendDateTime
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.SendDateTime);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.SendDateTime,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Title
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Title);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Title,value);
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public  string  Docs
		{
			get
			{
				return this.GetValStringByKey(UserQuestionAttr.Docs);
			}
			set
			{
				this.SetValByKey(UserQuestionAttr.Docs,value);
			}
		}		 
		#endregion 

		#region ���췽��
		/// <summary>
		/// �û�����
		/// </summary>
		public UserQuestion(){}
		/// <summary>
		/// �û�����
		/// </summary>
		/// <param name="oid"></param>
		public UserQuestion(int oid ) : base(oid)
		{
		}
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				if (BP.Web.TaxUser.No=="admin" || BP.Web.TaxUser.No.ToString().IndexOf("8888") > -1 )
				{
					uac.IsView=true;
					uac.IsDelete=true;
					uac.IsInsert=true;
					uac.IsUpdate=true;
					uac.IsAdjunct=true;
				}
				return uac;
			}
		}
		public  Map EnMap_del
		{
			get
			{
				if (this._enMap!=null)
					return this._enMap;
				Map map = new Map("Sys_UserQuestion");
				map.EnDesc="�û�����";
				map.EnType=EnType.Admin;
				map.AddTBIntPKOID();

				map.AddDDLEntitiesPK(UserQuestionAttr.Reporter,null,"������ID",new Emps(),false);
				map.AddTBString(UserQuestionAttr.Sender,null,"������",true,false,0,500,10);
				map.AddTBString(UserQuestionAttr.Title,null,"����",true,false,0,300,20);
				map.AddTBDateTime(UserQuestionAttr.SendDateTime,"����ʱ��",true,false);

				map.AddTBStringDoc(UserQuestionAttr.Docs,null,"��������",true,false);
				map.AddTBStringDoc(UserQuestionAttr.ReDocs,null,"�ش�",true,false);

				map.AddSearchAttr(UserQuestionAttr.Reporter);
				this._enMap=map;
				return this._enMap;
			}
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
				Map map = new Map("Sys_UserQuestion");
				map.EnDesc="�û�����";
				map.EnType=EnType.Admin;
				map.AddTBIntPKOID();


				map.AddDDLEntitiesPK(UserQuestionAttr.Reporter,null,"������ID",new Emps(),false);
				map.AddTBString(UserQuestionAttr.Sender,null,"������",true,false,0,500,10);
				map.AddTBString(UserQuestionAttr.Title,null,"����",true,false,0,300,20);
				map.AddTBDateTime(UserQuestionAttr.SendDateTime,"����ʱ��",true,false);

				map.AddTBStringDoc(UserQuestionAttr.Docs,null,"��������",true,false);
				map.AddTBStringDoc(UserQuestionAttr.ReDocs,null,"�ش�",true,false);

				map.AddSearchAttr(UserQuestionAttr.Reporter);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �û�����
	/// </summary> 
	public class UserQuestions : EntitiesOID  //EntitiesNoName
	{
		#region ˢ��
		/// <summary>
		/// ˢ��
		/// </summary>
		public static void RefreshUAC()
		{			
		}
		#endregion		 
		
		#region ���캯��
		/// <summary>
		/// ����ʵ����ʵĹ���
		/// </summary>
		public UserQuestions(){}
		/// <summary>
		/// New entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new UserQuestion();
			}
		}
		#endregion
	
	}
}
