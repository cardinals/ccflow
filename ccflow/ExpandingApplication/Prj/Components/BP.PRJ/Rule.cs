using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
	/// <summary>
	/// �ϴ�����
	/// </summary>
    public class RuleAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �Ƿ���ļ�
        /// </summary>
        public const string IsMultityFile = "IsMultityFile";
        /// <summary>
        /// �����ص���Ա
        /// </summary>
        public const string CanDownSQL = "CanDownSQL";
        /// <summary>
        /// ��ʽҪ��
        /// </summary>
        public const string FileFormat = "FileFormat";
        /// <summary>
        /// �ļ�Ŀ¼
        /// </summary>
        public const string FK_Dir = "FK_Dir";
    }
	/// <summary>
	/// �ϴ�����
	/// </summary>
    public class Rule : EntityNoName
    {
        #region ��������
        /// <summary>
        /// �Ƿ���ļ�
        /// </summary>
        public bool IsMultityFile
        {
            get
            {
                return this.GetValBooleanByKey(RuleAttr.IsMultityFile);
            }
            set
            {
                this.SetValByKey(RuleAttr.IsMultityFile, value);
            }
        }
        public string FK_Dir
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.FK_Dir);
            }
            set
            {
                this.SetValByKey(RuleAttr.FK_Dir, value);
            }
        }
        /// <summary>
        /// �����ص���Ա
        /// </summary>
        public string CanDownSQL
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.CanDownSQL);
            }
            set
            {
                this.SetValByKey(RuleAttr.CanDownSQL, value);
            }
        }
        /// <summary>
        /// �ļ���ʽ
        /// </summary>
          public string FileFormat
        {
            get
            {
                return this.GetValStrByKey(RuleAttr.FileFormat);
            }
            set
            {
                this.SetValByKey(RuleAttr.FileFormat, value);
            }
        }

        #endregion

        #region ���캯��
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public Rule() { }
        /// <summary>
        /// �ϴ�����
        /// </summary>
        public Rule(string no)
        {
            this.No = no;
            this.Retrieve();
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("PRJ_FileDesc");
                map.EnDesc = "�ϴ�����";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "3";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(RuleAttr.No, null, "���", true, false, 3, 3, 3);
                map.AddTBString(RuleAttr.Name, null, "����", true, false, 2, 60, 500);
                map.AddBoolean(RuleAttr.IsMultityFile, false, "�Ƿ���ļ�", true, false);
                map.AddTBString(RuleAttr.CanDownSQL, null, "�����ص���Ա", true, false, 0, 60, 500);
                map.AddTBString(RuleAttr.FileFormat, null, "��ʽҪ��", true, false, 0, 60, 500);
                map.AddTBString(RuleAttr.FK_Dir, null, "�ļ�Ŀ¼", true, false, 0, 60, 500);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ϴ�����s
	/// </summary>
	public class Rules : EntitiesNoName
	{	
		#region ���췽��
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Rule();
			}
		}
		/// <summary>
		/// �ϴ�����s 
		/// </summary>
        public Rules() { }
		#endregion
	}
	
}
