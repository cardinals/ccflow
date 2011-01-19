using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// ZJType
	/// </summary>
    public class ZJTypeAttr : EntityNoNameAttr
    {
    }
	/// <summary>
	/// ZJType ��ժҪ˵����
	/// </summary>
    public class ZJType : EntityNoName
    {
        #region ���캯��
        #endregion ���캯��

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
        }
        /// <summary>
        /// �ͻ�
        /// </summary>		
        public ZJType() { }
        public ZJType(string no)
            : base(no)
        {
        }
        /// <summary>
        /// ZJTypeMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "YG_ZJType";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "ר������";
                map.EnType = EnType.App;
                #endregion

                map.AddTBStringPK(ZJTypeAttr.No, null, "���", true, false, 1, 20, 4);
                map.AddTBString(ZJTypeAttr.Name, null, "����", true, false, 0, 50, 200);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ר������
	/// </summary>
	public class ZJTypes : EntitiesNoName
    {
        #region �õ����� Entity
        /// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new ZJType();
            }
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ͻ�s
		/// </summary>
		public ZJTypes(){}
		#endregion
	}
	
}
