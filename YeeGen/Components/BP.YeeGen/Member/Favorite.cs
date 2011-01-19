using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.YG
{
	/// <summary>
	/// �ղؼ�
	/// </summary>
	public class FavoriteAttr: EntityOIDAttr
	{
		#region ��������
		/// <summary>
		/// �ļ�����
		/// </summary>
		public const  string FK_Type="FK_Type";
		/// <summary>
		/// ��Ӧ��������
		/// </summary>
		public const  string RefObj="RefObj";
		/// <summary>
		/// ����
		/// </summary>
		public const  string Title="Title";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Member="FK_Member";
		#endregion
	}
	/// <summary>
	/// �ղؼ�
	/// </summary>
	public class Favorite :Entity
	{	
		#region ��������
		public const string TypeOfShareFile="SF";
		public const string TypeOfShareFileFDB="FDB";
		public const string TypeOfNews="News";
		public const string TypeOfPost="BBS";
		public const string TypeOfFAQ="FAQ";
        public const string TypeOfWord = "BK";
        public const string TypeOfLink = "Link";
        public const string TypeOfYPage = "YP";

		#endregion 


		#region ��������
		public string FK_Type
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.FK_Type);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.FK_Type,value);
			}
		}
		public string RefObj
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.RefObj);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.RefObj,value);
			}
		}
		public string Title
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.Title);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.Title,value);
			}
		}
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(FavoriteAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(FavoriteAttr.FK_Member,value);
			}
		}
		#endregion 

		#region ���캯��
		public override UAC HisUAC
		{
			get
			{
				UAC uac = new UAC();
				uac.OpenForSysAdmin();
				return uac;
			}
		}
		/// <summary>
		/// �ղؼ�
		/// </summary>		
        public Favorite()
        {
        }
		/// <summary>
		/// FavoriteMap
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map();

				#region �������� 
				map.EnDBUrl =new DBUrl(DBUrlType.AppCenterDSN) ; 
				map.PhysicsTable="YG_Favorite";  
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None; 
				map.IsAllowRepeatNo=false;
				map.IsCheckNoLength=false;
				map.EnDesc="�ղؼ�";
				map.EnType=EnType.App;
				#endregion

				#region ���� 

				map.AddTBStringPK(FavoriteAttr.FK_Type,Favorite.TypeOfShareFile,"����",true,false,0,50,200);
				map.AddTBStringPK(FavoriteAttr.FK_Member,null,"��Ա",true,false,0,50,200);
				map.AddTBStringPK(FavoriteAttr.RefObj,Favorite.TypeOfShareFile,"RefObj",true,false,0,50,200);

				map.AddTBString(FavoriteAttr.Title,Favorite.TypeOfShareFile,"����",true,false,0,500,200);
				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// �ղؼ�
	/// </summary>
	public class Favorites : Entities
	{
		#region �ղؼ�
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Favorite();
			}
		}
		#endregion 

		#region ���췽��
		public Favorites(){}
		public Favorites(string fk_custmor)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(FavoriteAttr.FK_Member, fk_custmor);
			qo.addOrderByDesc("RefObj");
			qo.DoQuery();
		}
		#endregion
	}
	
}
