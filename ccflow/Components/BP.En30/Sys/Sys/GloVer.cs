using System;
using System.Collections;
using BP.DA;
using BP.En;
namespace BP.Sys
{
    /// <summary>
    /// ȫ�ֱ���
    /// </summary>
    public class GloVerAttr : EntityNoNameAttr
    {
        /// <summary>
        /// Val
        /// </summary>
        public const string Val = "Val";
        /// <summary>
        /// Note
        /// </summary>
        public const string Note = "Note";
    }
    /// <summary>
    /// ȫ�ֱ���
    /// </summary>
    public class GloVer : EntityNoName
    {
        #region ����
        /// <summary>
        /// FontStyle
        /// </summary>
        public string Val
        {
            get
            {
                return this.GetValStringByKey(GloVerAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVerAttr.Val, value);
            }
        }
        /// <summary>
        /// note
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GloVerAttr.Note);
            }
            set
            {
                this.SetValByKey(GloVerAttr.Note, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ȫ�ֱ���
        /// </summary>
        public GloVer()
        {
        }
        /// <summary>
        /// ȫ�ֱ���
        /// </summary>
        /// <param name="mypk"></param>
        public GloVer(string no)
        {
            this.No = no;
            this.Retrieve();
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
                Map map = new Map("Sys_GloVer");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ȫ�ֱ���";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(GloVerAttr.No, null, "No", true, false, 1, 30, 20);
                map.AddTBString(GloVerAttr.Name, null, "Name", true, false, 0, 120, 20);
                map.AddTBString(GloVerAttr.Note, null, "Note", true, false, 0, 4000, 20);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ȫ�ֱ���s
    /// </summary>
    public class GloVers : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ȫ�ֱ���s
        /// </summary>
        public GloVers()
        {
        }
        /// <summary>
        /// ȫ�ֱ���s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public GloVers(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GloVer();
            }
        }
        #endregion
    }
}
