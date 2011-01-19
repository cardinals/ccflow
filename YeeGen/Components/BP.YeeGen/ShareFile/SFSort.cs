using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Port;
using BP.YG;

namespace BP.SF
{
	/// <summary>
	/// �ļ����
	/// </summary>
    public class SFSortAttr : EntityNoNameAttr
    {
        #region ��������
        public const string DFor = "DFor";
        public const string Grade = "Grade";
        public const string IsDtl = "IsDtl";
        #endregion
    }
	/// <summary>
	/// SFSort ��ժҪ˵����
	/// </summary>
    public class SFSort : EntityNoName
    {
        #region ��������
        #endregion

        #region ���캯��
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
        /// �ļ����
        /// </summary>		
        public SFSort() { }
        public SFSort(string no)
            : base(no)
        {
        }
        /// <summary>
        /// SFSortMap
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
                map.PhysicsTable = "SF_Sort";
                map.AdjunctType = AdjunctType.AllType;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.EnDesc = "�ļ����";
                map.EnType = EnType.App;
                #endregion

                #region �ֶ�
                map.AddTBStringPK(SFSortAttr.No, null, "���", true, false, 2, 10, 50);
                map.AddTBString(SFSortAttr.Name, null, "����", true, false, 0, 50, 200);
                map.AddTBInt(SFSortAttr.Grade, 1, "Grade", true, false);
                map.AddTBInt(SFSortAttr.IsDtl, 1, "IsDtl", true, false);
                map.AddTBString(SFSort1Attr.FK_Sort1, null, "FK_Sort1", true, false, 0, 50, 200);
                #endregion

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
	/// �ļ����
	/// </summary>
    public class SFSorts : EntitiesNoName
    {
        #region �õ�����Entity
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SFSort();
            }
        }
        #endregion

        #region ���췽��
        public SFSorts()
        {
        }
        #endregion
    }
	
}
