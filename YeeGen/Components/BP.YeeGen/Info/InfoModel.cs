using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// InfoModel
	/// </summary>
    public class InfoModelAttr : EntityNoNameAttr
    {
    }
	/// <summary>
	/// InfoModel ��ժҪ˵����
	/// </summary>
    public class InfoModel : EntityNoName
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
        public InfoModel() { }
        public InfoModel(string no)
            : base(no)
        {
        }
        /// <summary>
        /// InfoModelMap
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
                map.PhysicsTable = "YG_InfoModel";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "ģ������";
                map.EnType = EnType.App;
                #endregion

                map.AddTBStringPK(InfoModelAttr.No, null, "���", true, false, 1, 20, 4);
                map.AddTBString(InfoModelAttr.Name, null, "����", true, false, 0, 50, 200);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ģ������
	/// </summary>
	public class InfoModels : EntitiesNoName
    {
        #region �õ����� Entity
        /// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new InfoModel();
            }
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ͻ�s
		/// </summary>
		public InfoModels(){}
		#endregion
	}
	
}
