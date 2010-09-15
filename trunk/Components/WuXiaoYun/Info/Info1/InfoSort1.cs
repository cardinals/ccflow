using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class InfoSort1Attr : EntityNoNameAttr
    {
        public const string IsShowDtl = "IsShowDtl";
    }
    /// <summary>
    /// ��Ϣ���
    /// </summary>
    public class InfoSort1 : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSort1()
        {
        }
        public InfoSort1(string no)
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
                Map map = new Map("GE_InfoSort1");
                map.EnDesc = "���";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Info1s' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Info1s", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(InfoSort1Attr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(InfoSort1Attr.Name, null, "����", true, false, 1, 50, 300);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��Ϣ��� 
    /// </summary>
    public class InfoSort1s : EntitiesNoName
    {
        /// <summary>
        /// ��ȡ��Ϣ���
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoSort1();
            }
        }

        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSort1s()
        {
        }
        #endregion


    }
}


