using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;


namespace BP.RB
{
	/// <summary>
	/// ��������
	/// </summary>
    public class EncodeAttr : EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// Docs
        /// </summary>
        public const string Docs = "Docs";
        #endregion
    }
	/// <summary>
	/// ��������
	/// </summary>
    public class Encode : EntityNoName
    {
        #region ��������
        #endregion

        #region ���캯��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>		
        public Encode()
        {

        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="no"></param>
        public Encode(string no)
            : base(no)
        {
        }
        /// <summary>
        /// EncodeMap
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
                map.PhysicsTable = "RB_Encode";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��վ��������";
                map.EnType = EnType.App;
                #endregion

                #region ��������

                map.AddTBStringPK(EncodeAttr.No, null, "���", true, true, 2, 2, 4);
                map.AddTBString(EncodeAttr.Name, null, "����", true, true, 0, 4000, 30);

                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        #region ���ط���
        #endregion
    }
	/// <summary>
	/// ��������
	/// </summary>
    public class Encodes : EntitiesNoName
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Encode();
            }
        }	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��������
		/// </summary>
		public Encodes()
        {
        }
		#endregion

	 
	}
	
}
