using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
//using BP.ZHZS.Base;
using BP;
namespace BP.Sys
{
	/// <summary>
	///  
	/// </summary>
    public class SysEnAttr : EntityEnsNameAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string EnEnsName = "EnEnsName";
        /// <summary>
        /// �����
        /// </summary>
        public const string PTable = "PTable";
        /// <summary>
        /// ʵ������
        /// </summary> 
        public const string EnType = "EnType";
    }
	/// <summary>
	/// SysEns
	/// </summary>
    public class SysEn : EntityEnsName
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
                return ClassFactory.GetEns(this.EnsEnsName);
            }
        }
        /// <summary>
        /// ʵ������
        /// </summary>
        public string EnEnsName
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.EnEnsName);
            }
            set
            {
                this.SetValByKey(SysEnAttr.EnEnsName, value);
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string PTable
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.PTable);
            }
            set
            {
                this.SetValByKey(SysEnAttr.PTable, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(SysEnAttr.Name);
            }
            set
            {
                this.SetValByKey(SysEnAttr.Name, value);
            }
        }
        /// <summary>
        /// ʵ������ 0 , Ӧ��, 1, ����Աά��, 2, Ԥ��ʵ��.
        /// </summary>
        public int EnTypeOFInt
        {
            get
            {
                return this.GetValIntByKey(SysEnAttr.EnType);
            }
            set
            {
                this.SetValByKey(SysEnAttr.EnType, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        public SysEn()
        {
        }
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        /// <param name="EnsEnsName">������</param>
        public SysEn(string EnsEnsName)
        {
            this.EnsEnsName = EnsEnsName;
            if (this.IsExits == false)
            {
                Entities ens = ClassFactory.GetEns(this.EnsEnsName);
                Entity en = ens.GetNewEntity;
                this.Name = en.EnDesc;
                this.EnEnsName = en.ToString();
                this.EnTypeOFInt = (int)en.EnMap.EnType;
                this.PTable = en.EnMap.PhysicsTable;
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
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_Ens");
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "ʵ����Ϣ";

                map.EnType = EnType.Sys;
                map.AddTBString(SysEnAttr.Name, null, "ʵ������", true, false, 0, 100, 60);
                map.AddTBStringPK(SysEnAttr.EnsEnsName, "EnsName", null, "ʵ����", true, true, 0, 90, 10);
                map.AddTBString(SysEnAttr.EnEnsName, "EnName", null, "ʵ������", true, false, 0, 50, 20);
                map.AddDDLSysEnum(SysEnAttr.EnType, 0, "ʵ������", true, false, "EnType");
                map.AddTBString(SysEnAttr.PTable, null, "����Դ", true, false, 0, 50, 20);
                this._enMap = map;
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
	public class SysEns : EntitiesEnsName
	{		
		#region ����
		public SysEns(){}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SysEn();
			}

		}
		#endregion

		#region ��ѯ����
		/// <summary>
		/// ����ʵ������Ͳ�ѯ��
		/// </summary>
		/// <param name="type">ʵ�������</param>
		/// <returns>���ز�ѯ�ĸ���</returns>
		public int Retrieve(EnType type)
		{
			
			QueryObject qo =new QueryObject(this);
			qo.AddWhere(SysEnAttr.EnType,(int)type);
			return qo.DoQuery();
		}
		#endregion
		
	}
}
