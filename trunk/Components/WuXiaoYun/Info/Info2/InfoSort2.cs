using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class InfoSort2Attr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
    /// <summary>
    /// ��Ϣ���
    /// </summary>
    public class InfoSort2 : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSort2()
        {
        }
        public InfoSort2(string no)
        {
            this.No = no;
            try
            {
                this.Retrieve();
            }
            catch
            {

            }
        }
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("GE_InfoSort2");
                map.EnDesc = "���";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Info2s' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Info2s", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(InfoSort2Attr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(InfoSort2Attr.Name, null, "����", true, false, 1, 50, 300);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��Ϣ��� 
    /// </summary>
    public class InfoSort2s : EntitiesNoName
    {
        /// <summary>
        /// ��ȡ��Ϣ���
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoSort2();
            }
        }

        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSort2s()
        {
        }
        #endregion


    }
}


