using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.YG
{
	/// <summary>
	/// ����
	/// </summary>
	public class CFriendAttr: EntityOIDAttr
	{
		#region ��������
		/// <summary>
		/// �ļ�����
		/// </summary>
		public const  string FK_Friend="FK_Friend";
		/// <summary>
		/// ����
		/// </summary>
		public const string FK_Member="FK_Member";
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary>
	public class CFriend :Entity
	{	
		#region ��������
		/// <summary>
		/// FK_Friend
		/// </summary>
		public string FK_Friend
		{
			get
			{
				return this.GetValStringByKey(CFriendAttr.FK_Friend);
			}
			set
			{
				this.SetValByKey(CFriendAttr.FK_Friend,value);
			}
		}
		/// <summary>
		/// FK_Member
		/// </summary>
		public string FK_Member
		{
			get
			{
				return this.GetValStringByKey(CFriendAttr.FK_Member);
			}
			set
			{
				this.SetValByKey(CFriendAttr.FK_Member,value);
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
		/// ����
		/// </summary>		
		public CFriend()
		{
		}
		/// <summary>
		/// CFriendMap
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
				map.PhysicsTable="YG_CFriend";  
				map.AdjunctType = AdjunctType.AllType ;  
				map.DepositaryOfMap=Depositary.Application; 
				map.DepositaryOfEntity=Depositary.None; 
				map.IsAllowRepeatNo=false;
				map.IsCheckNoLength=false;
				map.EnDesc="����";
				map.EnType=EnType.App;
				#endregion

				#region ���� 
				map.AddTBStringPK(CFriendAttr.FK_Member,Glo.MemberNo,"��Ա",true,false,1,50,200);
				map.AddTBStringPK(CFriendAttr.FK_Friend,"sd","FK_Friend",true,false,1,50,200);
				#endregion

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion
	}
	/// <summary>
	/// ����
	/// </summary>
	public class CFriends : Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CFriend();
			}
		}
		#endregion 

		#region ���췽��
		public CFriends(){}
		public CFriends(string fk_custmor)
		{
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(CFriendAttr.FK_Member, fk_custmor);
			qo.DoQuery();
		}
		#endregion
	}
	
}
