using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
	/// <summary>
	///  
	/// </summary>
	public class AppUpdateRecAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string VerKey="VerKey";
		/// <summary>
		/// ʵ������
		/// </summary>
		public const string UpdateData="UpdateData";
	}
	/// <summary>
	/// AppUpdateRecs
	/// </summary>
    public class AppUpdateRec : Entity
    {
        #region ��������
        /// <summary>
        /// ʵ������
        /// </summary>
        public string UpdateData
        {
            get
            {
                return this.GetValStringByKey(AppUpdateRecAttr.UpdateData);
            }
            set
            {
                this.SetValByKey(AppUpdateRecAttr.UpdateData, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string VerKey
        {
            get
            {
                return this.GetValStringByKey(AppUpdateRecAttr.VerKey);
            }
            set
            {
                this.SetValByKey(AppUpdateRecAttr.VerKey, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        public AppUpdateRec()
        {
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_AppUpdateRec");
                map.EnDesc = "�Զ�����";
                map.EnType = EnType.Sys;
                map.AddTBStringPK(AppUpdateRecAttr.VerKey, "VerKey", null, "VerKey", true, true, 0, 90, 10);
                map.AddTBString(AppUpdateRecAttr.UpdateData, "UpdateData", null, "UpdateData", true, false, 0, 50, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ʵ�弯��
	/// </summary>
    public class AppUpdateRecs : Entities
    {
        #region ����
        public AppUpdateRecs()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new AppUpdateRec();
            }

        }
        #endregion
    }
}
