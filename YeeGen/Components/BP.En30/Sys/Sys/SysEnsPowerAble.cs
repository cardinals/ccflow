using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// abc_afs
	/// </summary>
	public class SysEnPowerAbleAttr : EntityEnsNameAttr
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
	/// SysEnPowerAbles
	/// </summary>
	public class SysEnPowerAble: EntityEnsName 
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
				return this.GetValStringByKey(SysEnPowerAbleAttr.EnEnsName ) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.EnEnsName,value) ; 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetValStringByKey(SysEnPowerAbleAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.Name,value) ; 
			}
		}
		/// <summary>
		/// ʵ������ 0 , Ӧ��, 1, ����Աά��, 2, Ԥ��ʵ��.
		/// </summary>
		public int HisEnType
		{
			get
			{
				return this.GetValIntByKey(SysEnPowerAbleAttr.EnType) ; 
			}
			set
			{
				this.SetValByKey(SysEnPowerAbleAttr.EnType,value) ; 
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// ϵͳʵ��
		/// </summary>
		public SysEnPowerAble()
		{
		}		
		/// <summary>
		/// ϵͳʵ��
		/// </summary>
		/// <param name="EnsEnsName">������</param>
		public SysEnPowerAble(string EnsEnsName )
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
				Map map = new Map("Sys_EnsPowerAble");
				map.DepositaryOfEntity=Depositary.None;
				map.EnDesc="ʵ����Ϣ";
				map.EnType=EnType.Sys;
				map.AddTBStringPK(SysEnPowerAbleAttr.EnsEnsName ,"EnsName",null,"ʵ����",true,true,0,90,10);
				map.AddTBString(SysEnPowerAbleAttr.EnEnsName,"EnName",null,"ʵ������",true,false,0,50,20);
				map.AddTBString(SysEnPowerAbleAttr.Name,null,"ʵ������",true,false,0,50,50);
				map.AddDDLSysEnum(SysEnPowerAbleAttr.EnType,0,"ʵ������",true,false,"EnType");
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 

		#region ��ѯ����
		
		
		#endregion


	}
	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class SysEnPowerAbles : EntitiesEnsName
	{		 
		public SysEnPowerAbles(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SysEnPowerAble();
			}
		}
		
	}
}
