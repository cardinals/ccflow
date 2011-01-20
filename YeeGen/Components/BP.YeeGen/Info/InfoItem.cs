using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// InfoItem
	/// </summary>
    public class InfoItemAttr : EntityNoNameAttr
    {
        public const string FK_Model = "FK_Model";
    }
	/// <summary>
	/// InfoItem ��ժҪ˵����
	/// </summary>
    public class InfoItem : EntityNoName
    {
        #region ���캯��
        public string FK_Model
        {
            get
            {
                return this.GetValStrByKey(InfoItemAttr.FK_Model);
            }
            set
            {
                this.SetValByKey(InfoItemAttr.FK_Model, value);
            }
        }
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
        public InfoItem() { }
        public InfoItem(string no)
            : base(no)
        {
        }
        /// <summary>
        /// InfoItemMap
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
                map.PhysicsTable = "YG_InfoItem";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "ģ������";
                map.EnType = EnType.App;
                map.CodeStruct = "4";
                #endregion

                map.AddTBStringPK(InfoItemAttr.No, null, "���", true, false, 4, 4, 4);
                map.AddTBString(InfoItemAttr.Name, null, "����", true, false, 0, 50, 200);
                map.AddDDLEntities(InfoItemAttr.FK_Model, null, "ģ��", new InfoModels(), true);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// ģ������
	/// </summary>
	public class InfoItems : EntitiesNoName
    {
        #region �õ����� Entity
        /// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new InfoItem();
            }
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ͻ�s
		/// </summary>
		public InfoItems(){}
		#endregion
	}
	
}
