using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.Sys
{ 
	/// <summary>
	/// attr
	/// </summary>
	public class SysUIEnsRefFuncAttr :DictAttr
	{ 	 
		/// <summary>
		/// �������·��
		/// </summary>
		public const string Url="Url"; 
		/// <summary>
		/// �򿪷�ʽ
		/// </summary>
		public const string Target="Target"; 
		/// <summary>
		/// ���
		/// </summary>
		public const string Width="Width"; 
		/// <summary>
		/// �߶�
		/// </summary>
		public const string Height="Height"; 

		/// <summary>
		/// �Ƿ�ΪDtl
		/// </summary>
		public const string IsForDtl="IsForDtl"; 

		/// <summary>
		/// icon
		/// </summary>
		public const string Icon="Icon";
		/// <summary>
		/// ��ʾ��Ϣ
		/// </summary>
		public const string ToolTip="ToolTip";

	}
	/// <summary>
	/// ��������
	/// </summary>
	public class SysUIEnsRefFunc : Dict
	{
		#region  ��������		
		/// <summary>
		/// �������·��
		/// </summary>
		public string Url
		{
			get
			{
				return  this.GetValStringByKey(SysUIEnsRefFuncAttr.Url);
				
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Url,value);
			}
		}
		public string Icon
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.Icon);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Icon,value);
			}
		}
		public string ToolTip
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.ToolTip);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.ToolTip,value);
			}
		}
		/// <summary>
		/// �򿪷�ʽ
		/// </summary>
		public string Target
		{
			get
			{
				return this.GetValStringByKey(SysUIEnsRefFuncAttr.Target);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Target,value);
			}
		}
		/// <summary>
		/// ���
		/// </summary>
		public int Width
		{
			get
			{
				return this.GetValIntByKey(SysUIEnsRefFuncAttr.Width);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Width,value);
			}
		}
		/// <summary>
		/// �߶�
		/// </summary>
		public int Height
		{
			get
			{
				return this.GetValIntByKey(SysUIEnsRefFuncAttr.Height);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.Height,value);
			}
		}
		/// <summary>
		/// IsForDtl
		/// </summary>
		public bool IsForDtl
		{
			get
			{
				return this.GetValBooleanByKey(SysUIEnsRefFuncAttr.IsForDtl);
			}
			set
			{
				SetValByKey(SysUIEnsRefFuncAttr.IsForDtl,value);
			}
		}
		#endregion 
		 
		#region ���캯��
		/// <summary>
		/// ��ع���
		/// </summary>
		public SysUIEnsRefFunc(){}	
		/// <summary>
		/// ��ع���
		/// </summary>
		/// <param name="_oid">oid</param>
		public SysUIEnsRefFunc(int _oid ): base(_oid) {}
		
		/// <summary>
		/// Map
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) return this._enMap;
				Map map = new Map("Sys_UIEnsRefFunc");
				map.DepositaryOfEntity=Depositary.Application;
				map.EnType=EnType.Sys;
				map.EnDesc="��ع���";
				map.AddTBIntPKOID();
				map.AddTBString(SysUIEnsRefFuncAttr.No,null,"������",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Name,null,"��ʾ����",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Url,null,"����",true,false,1,200,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Icon,"/images/Default.gif","ͼ��",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.ToolTip,null,"��ʾ��Ϣ",true,false,1,100,20);
				map.AddTBString(SysUIEnsRefFuncAttr.Target,"WinOpen","�򿪷�ʽ",true,false,1,100,20);
				map.AddTBInt(SysUIEnsRefFuncAttr.Height,0,"�߶�",true,false);
				map.AddTBInt(SysUIEnsRefFuncAttr.Width,0,"���",true,false);
				map.AddTBInt(SysUIEnsRefFuncAttr.IsForDtl,0,"IsForDtl",true,false);

				this._enMap=map;
				return this._enMap;
			}
		}
		

		#endregion 

	}
	/// <summary>
	/// ��ع��ܼ���
	/// </summary>
	public class SysUIEnsRefFuncs: Dicts
	{		 
		/// <summary>
		/// ��ع��ܼ���
		/// </summary>
		public SysUIEnsRefFuncs(){} 
		/// <summary>
		/// ��ع��ܼ��ϣ����ݱ�ţ����ҡ�
		/// </summary>
		/// <param name="No"></param>
		public SysUIEnsRefFuncs(string No)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere("No",No);			
			qo.DoQuery();
		 
		}
		/// <summary>
		/// ʵ��
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new SysUIEnsRefFunc();
			}
		}
	
	
	}	
}
 