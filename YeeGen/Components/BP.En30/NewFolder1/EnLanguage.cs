using System;
using System.Collections;
using BP.DA;
using BP.En.Base;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// ʵ������
	/// </summary>
	public class EnLanguageAttr : EntityClassNameAttr
	{
		public const string Name="Name";
		public const string FK_Language="FK_Language";
		public const string AttrKey="AttrKey";

	}
	 
	/// <summary>
	/// EnLanguages
	/// </summary>
	public class EnLanguage: EntityClassName 
	{
		#region ��������
		public string Name
		{
			get
			{
				return this.GetValStringByKey(EnLanguageAttr.Name ) ; 
			}
			set
			{
				this.SetValByKey(EnLanguageAttr.Name,value) ; 
			}
		}
		public string FK_Language
		{
			get
			{
				return this.GetValStringByKey(EnLanguageAttr.FK_Language ) ; 
			}
			set
			{
				this.SetValByKey(EnLanguageAttr.FK_Language,value) ; 
			}
		}
		public string AttrKey
		{
			get
			{
				return this.GetValStringByKey(EnLanguageAttr.AttrKey ) ; 
			}
			set
			{
				this.SetValByKey(EnLanguageAttr.AttrKey,value) ; 
			}
		}
		#endregion

		#region ���췽��
		public EnLanguage(){}	
		/// <summary>
		/// EnsClassName
		/// </summary>
		/// <param name="EnsClassName">EnsClassName</param>
		public EnLanguage(string EnsClassName )
		{
			this.EnsClassName= EnsClassName;
			if (this.IsExits==false)
			{
				Entities ens =ClassFactory.GetEns(this.EnsClassName) ;
				this.Name = ens.GetNewEntity.EnDesc;
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
				Map map = new Map("Sys_EnAttrLanguage");
				map.DepositaryOfEntity=Depositary.Application;
				map.EnDesc="ʵ������";
				//map.AddDDLEntitiesPK(EnLanguageAttr.EnsClassName ,null,DataType.AppString,"ʵ����",new SysEns(),"ClassName","Name",true);
				map.AddDDLEntitiesNoNamePK(EnLanguageAttr.FK_Language,null,"����",new Languages(),true);
				map.AddTBString(EnLanguageAttr.Name,null,"����",true,false,0,50,50);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 


	}
	
	/// <summary>
	/// ʵ�弯��
	/// </summary>
	public class EnLanguages : EntitiesClassName
	{		 
		public EnLanguages(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EnLanguage();
			}
		}
		
	}
}
