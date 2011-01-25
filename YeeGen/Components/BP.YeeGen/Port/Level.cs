using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.YG
{
	/// <summary>
    /// �ȼ�
	/// </summary>
    public class LevelAttr : EntityNoNameAttr
    {
    }
	/// <summary>
    /// �ȼ� ��ժҪ˵����
	/// </summary>
    public class Level : EntityNoName
    {
        #region �ȼ�
        #endregion �ȼ�

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
        /// �ȼ�
        /// </summary>		
        public Level() { }
        public Level(string no)
            : base(no)
        {
        }
        /// <summary>
        /// LevelMap
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
                map.PhysicsTable = "YG_Level";
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "�ȼ�";
                map.EnType = EnType.App;
                #endregion

                map.AddTBStringPK(LevelAttr.No, null, "���", true, false, 1, 20, 4);
                map.AddTBString(LevelAttr.Name, null, "����", true, false, 0, 50, 200);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// �ȼ�s
	/// </summary>
	public class Levels : EntitiesNoName
    {
        #region �õ����� Entity
        /// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
            get
            {
                return new Level();
            }
		}	
		#endregion 

		#region ���췽��
		/// <summary>
        /// �ȼ�s
		/// </summary>
		public Levels(){}
		#endregion
	}
	
}
