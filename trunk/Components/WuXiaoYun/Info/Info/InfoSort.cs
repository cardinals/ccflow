using System;
using System.Data;
using BP.DA;
using BP.En;

namespace BP.GE
{
    /// <summary>
    /// ����
    /// </summary>
    public class InfoSortAttr : EntityNoNameAttr
    {
    }
    /// <summary>
    /// ��Ϣ���
    /// </summary>
    public class InfoSort : EntityNoName
    {
        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSort()
        {
        }
        public InfoSort(string no)
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
                Map map = new Map("GE_InfoSort");
                map.EnDesc = "���";
                map.TitleExt = " - <a href='Batch.aspx?EnsName=BP.GE.Infos' >" + BP.Sys.EnsAppCfgs.GetValString("BP.GE.Infos", "AppName") + "</a>";

                map.IsAutoGenerNo = false;
                map.IsAllowRepeatName = false; 
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.CodeStruct = "2";
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(InfoSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(InfoSortAttr.Name, null, "����", true, false, 1, 50, 300);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��Ϣ��� 
    /// </summary>
    public class InfoSorts : EntitiesNoName
    {
        /// <summary>
        /// ��ȡ��Ϣ���
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new InfoSort();
            }
        }

        #region ���캯��
        /// <summary>
        /// ��Ϣ���
        /// </summary>
        public InfoSorts()
        {
        }
        #endregion


    }
}


