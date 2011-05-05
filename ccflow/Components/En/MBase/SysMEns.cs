using System;
using System.Collections;
using BP.DA;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// sss
	/// </summary>
	public class SysIEnAttr : EntityEnsNameAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
		/// <summary>
		/// ʵ������
		/// </summary>
		public const string EnEnsName="EnEnsName";
		/// <summary>
		/// ʵ������
		/// </summary> 
		public const string EnType="EnType";	
	}
	 
	/// <summary>
	/// SysIEns
	/// </summary>
	public class SysIEn: EntityEnsName 
	{
		#region ��������
		public Entity En
		{
			get
			{
			  return ClassFactory.GetEn(this.EnEnsName);
			}
		}
		public Entities Ens
		{
			get
			{
				return ClassFactory.GetEns(this.EnsEnsName );
			}
		}
		/// <summary>
		/// ʵ������
		/// </summary>
		public string EnEnsName
		{
			get
			{
				return this.GetValStringByKey(SysIEnAttr.EnEnsName ) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.EnEnsName,value) ; 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(SysIEnAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.Name,value) ; 
			}
		}
		/// <summary>
		/// ʵ������ 0 , Ӧ��, 1, ����Աά��, 2, Ԥ��ʵ��.
		/// </summary>
		public int HisEnType
		{
			get
			{
				return this.GetValIntByKey(SysIEnAttr.EnType) ; 
			}
			set
			{
				this.SetValByKey(SysIEnAttr.EnType,value) ; 
			}
		}
		#endregion

		#region ���췽��
		public SysIEn(){}
		/// <summary>
		/// EnsEnsName
		/// </summary>
		/// <param name="EnsEnsName">EnsEnsName</param>
		public SysIEn(string EnsEnsName )
		{
			this.EnsEnsName= EnsEnsName;
			if (this.IsExits==false)
			{
				Entities ens =ClassFactory.GetEns(this.EnsEnsName) ;
				Entity en = ens.GetNewEntity;
				this.Name = en.EnDesc;
				this.EnEnsName = en.ToString();
				this.Insert();
			}
			else
			{
				this.Retrieve();
			}

		}
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				Map map = new Map("Sys_Ens");
				map.EnDesc="ʵ����Ϣ";
				map.EnType=EnType.Sys;

				map.DepositaryOfEntity=Depositary.Application;
				map.DepositaryOfMap=Depositary.Application;


				map.AddTBStringPK(SysIEnAttr.EnsEnsName ,"EnsName",null,"ʵ����",true,true,1,200,4);
				map.AddTBString(SysIEnAttr.EnEnsName,"EnName",null,"ʵ������",true,false,1,200,50);
				map.AddTBString(SysIEnAttr.Name,null,"ʵ������",true,false,0,200,50);
				map.AddDDLSysEnum(SysIEnAttr.EnType,0,"ʵ������",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ��ѯ����
		public void RetrieveByEnEnsName(string EnEnsName)
		{
			QueryObject qo  = new QueryObject(this);
			qo.AddWhere(SysIEnAttr.EnEnsName,EnEnsName) ; 
			if (qo.DoQuery()==0)
				throw new Exception("@��ˢ�¼�¼.");
		}
		#endregion


	}
	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class SysIEns : EntitiesEnsName
	{		 
		public SysIEns(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysIEn();
			}
		}
		
	}
}
