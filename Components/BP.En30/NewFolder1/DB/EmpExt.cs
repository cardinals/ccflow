using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.En.Base;

namespace BP.En
{
	/// <summary>
	/// EmpExtAttr
	/// </summary>
	public class EmpExtAttr
	{ 
		/// <summary>
		/// ֵ
		/// </summary>
		public const string Val="Val";
		/// <summary>
		/// �ֶ�
		/// </summary>
		public const string Field="Field";
		/// <summary>
		/// �û�
		/// </summary>
		public const string FK_Emp="FK_Emp";
	}
	/// <summary>
	/// sdfsad
	/// </summary>
	public class EmpExt : Entity
	{
		#region  ����
		/// <summary>
		///  ֵ
		/// </summary>
		public string  Val
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.Val);
			}
			set
			{
				SetValByKey(EmpExtAttr.Val,value);
			}
		}
		public string  Field
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.Field);
			}
			set
			{
				SetValByKey(EmpExtAttr.Field,value);
			}
		}
		public string  FK_Emp
		{
			get
			{
				return this.GetValStringByKey(EmpExtAttr.FK_Emp);
			}
			set
			{
				SetValByKey(EmpExtAttr.FK_Emp,value);
			}
		}
		#endregion 
		 
		#region ���캯��
		/// <summary>
		/// �û���չ��Ϣ
		/// </summary>
		public EmpExt()
		{
		}
		/// <summary>
		/// �û���չ��Ϣ
		/// </summary>
		/// <param name="fk_emp">��Ա</param>
		/// <param name="field">�ֶ�</param>
        public EmpExt(string fk_emp, string field)
        {
            this.FK_Emp = fk_emp;
            this.Field = field;
            try
            {
                this.Retrieve();
            }
            catch
            {
                this.Insert();
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fk_emp"></param>
		/// <param name="field"></param>
		/// <param name="IsNullVal"></param>
		public EmpExt(string fk_emp,string field, string IsNullVal)
		{ 
			this.FK_Emp=fk_emp;
			this.Field=field;
			try
			{
				this.Retrieve();
			}
			catch
			{
				this.Val=IsNullVal;
				this.Insert();
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
				
				Map map = new Map("Pub_EmpExt");
				map.EnDesc="�û���չ��Ϣ";
				map.EnType = EnType.View;
				map.DepositaryOfMap=Depositary.Application;
				map.DepositaryOfEntity=Depositary.Application;
                map.AddMyPK();

				map.AddTBString(EmpExtAttr.FK_Emp,null,"�û�",true,false,4,20,100);
				map.AddTBString(EmpExtAttr.Field,null,"�ֶ�",true,false,1,20,100);
				map.AddTBStringDoc(EmpExtAttr.Val,null,"����",true,false);
				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion 
	}
	/// <summary>
	/// �û���չ��Ϣs
	/// </summary>
	public class EmpExts: Entities
	{
		/// <summary>
		/// �û���չ��Ϣ
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new EmpExt();
			}
		}
		/// <summary>
		/// �û���չ��Ϣ
		/// </summary>
		public EmpExts()
		{
		}
	}
}
 