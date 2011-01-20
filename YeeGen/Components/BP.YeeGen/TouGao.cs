using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// TouGao
	/// </summary>
    public class TouGaoAttr : EntityNoNameAttr
    {
        public const string Author = "Author";
        public const string RDT = "RDT";
        public const string TouGaoSta = "TouGaoSta";

    }
	/// <summary>
	/// TouGao ��ժҪ˵����
	/// </summary>
    public class TouGao : EntityOIDName
    {
        #region ���캯��
        public string Author
        {
            get
            {
                return this.GetValStringByKey(TouGaoAttr.Author);
            }
            set
            {
                this.SetValByKey(TouGaoAttr.Author, value);
            }
        }
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(TouGaoAttr.RDT);
            }
            set
            {
                this.SetValByKey(TouGaoAttr.RDT, value);
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
        public TouGao() { }
        public TouGao(int no)
            : base(no)
        {
        }
        /// <summary>
        /// TouGaoMap
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
                map.PhysicsTable = "YG_TouGao";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "Ͷ��";
                map.EnType = EnType.App;
                #endregion

                map.AddTBIntPKOID();

                map.AddDDLSysEnum(TouGaoAttr.TouGaoSta, 0, "״̬", true, true, TouGaoAttr.TouGaoSta,"@0=δ���@1=���ͨ��@2=��ͨ��");
                map.AddTBString(TouGaoAttr.Name, null, "�������", true, false, 0, 50, 200);
                map.AddTBString(TouGaoAttr.Author, null, "����", true, false, 0, 50, 200);
                map.AddTBDateTime(TouGaoAttr.RDT, null, "��¼����", true, false);

                map.AddSearchAttr(TouGaoAttr.TouGaoSta);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// Ͷ��
	/// </summary>
	public class TouGaos : EntitiesNoName
    {
        #region �õ����� Entity
        /// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new TouGao();
            }
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ͻ�s
		/// </summary>
		public TouGaos(){}
		#endregion
	}
	
}
