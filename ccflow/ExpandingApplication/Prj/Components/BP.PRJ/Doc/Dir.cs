
using System;
using System.Data;
using BP.DA;
using BP.Port; 
using BP.En;

namespace BP.PRJ
{
	/// <summary>
	/// �ļ�Ŀ¼
	/// </summary>
    public class DirAttr : EntityNoNameAttr
    {
        public const string DirPath = "DirPath";
        public const string ID = "ID";
        public const string PID = "PID";
    }
	/// <summary>
	/// �ļ�����
	/// </summary>
    public class Dir : EntityNoName
    {
        #region ��������
        /// <summary>
        /// λֵ
        /// </summary>
        public string DirPath
        {
            get
            {
                return this.GetValStrByKey(DirAttr.DirPath);
            }
            set
            {
                this.SetValByKey(DirAttr.DirPath, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �ļ�����
        /// </summary>
        public Dir() { }
        /// <summary>
        /// �ļ�����
        /// </summary>
        public Dir(string no)
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

                Map map = new Map("PRJ_Dir");
                map.EnDesc = "�ļ�����";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(DirAttr.No, null, "���", true, false, 10, 10, 10);
                map.AddTBString(DirAttr.Name, null, "����", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.ID, null, "ID", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.PID, null, "PID", true, false, 0, 60, 500);
                map.AddTBString(DirAttr.DirPath, null, "�ļ�·��", true, false, 0, 60, 500);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ļ�����s
	/// </summary>
	public class Dirs : EntitiesNoName
	{	
		#region ���췽��
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{			 
				return new Dir();
			}
		}
		/// <summary>
		/// �ļ�����s 
		/// </summary>
		public Dirs(){}
		#endregion
	}
	
}
