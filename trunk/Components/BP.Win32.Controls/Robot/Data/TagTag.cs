using System;
using System.Data;
using BP.DA;
using BP.En.Base;
using BP.En;
using BP.Port;
//using BP.ZHZS.DS;


namespace BP.RB
{
	/// <summary>
	/// ��ǩvs��ǩ
	/// </summary>
    public class TagTagAttr
    {
        #region ��������
        /// <summary>
        /// T1
        /// </summary>
        public const string T1 = "T1";
        /// <summary>
        /// T2
        /// </summary>
        public const string T2 = "T2";
        #endregion
    }
	/// <summary>
	/// ��ǩvs��ǩ
	/// </summary>
    public class TagTag : Entity
    {
        #region ��������
        public string T1
        {
            get
            {
                return this.GetValStringByKey(TagTagAttr.T1);
            }
            set
            {
                this.SetValByKey(TagTagAttr.T1, value);
            }
        }
        public string T2
        {
            get
            {
                return this.GetValStringByKey(TagTagAttr.T2);
            }
            set
            {
                this.SetValByKey(TagTagAttr.T2, value);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        /// <summary>
        /// TagTag
        /// </summary>
        public TagTag()
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
                Map map = new Map();

                #region ��������
                map.EnDBUrl = new DBUrl(DBUrlType.AppCenterDSN);
                map.PhysicsTable = "SE_TagTag";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.None;
                map.EnDesc = "��ǩvs��ǩ";
                map.EnType = EnType.App;


                map.AddTBIntPK(TagTagAttr.T1, 0, "T1", true, true);
                map.AddTBIntPK(TagTagAttr.T2, 0, "T2", true, true);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
    }
	/// <summary>
	/// ��ǩvs��ǩ
	/// </summary>
	public class TagTags : Entities
	{
		#region 
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TagTag();
			}
		}	
		#endregion 

		#region ���췽��
		/// <summary>
		/// ��ǩvs��ǩ
		/// </summary>
		public TagTags(){}
		#endregion
	}
	
}
